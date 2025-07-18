// BackgroundSemVideo.cs
using LibVLCSharp.Shared;

namespace BlockPlayer
{
    public partial class Janela : Form
    {
        public Janela(string[] args = null)
        {
            // Melhorar codígo e iniciar frontend
            // Melhorar o continuar assistindo

            InitializeComponent();

            if (args != null && args.Length > 0)
            {
                _arquivoInicial = args[0];
            }

            ConfigVLC();
            ConfigInterface();
            ConfigSemVideo();
            ConfigMiniplayer();
            ConfigHotKeys();

            // Janela obter foco ao iniciar e exibir na frente
            this.Activate();
            this.BringToFront();

            CarregarContinuarAssistindo();

            if (!string.IsNullOrEmpty(_arquivoInicial) && File.Exists(_arquivoInicial))
            {
                var media = new Media(_libVLC, _arquivoInicial, FromType.FromPath);
                _mediaPlayer.Play(media);
                AtualizarVisibilidadeVideo(true);
            }
        }

        private void Painel_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void Video_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;

            // Quando arrastar um video adicionar camada "Mova seu video aqui"
        }

        private void Video_DragDrop(object sender, DragEventArgs e)
        {
            VolumeVideo.Value = 100;
            AtualizarVolume();

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                string file = files[0];
                string ext = Path.GetExtension(file).ToLower();
                if (ext == ".mp4")
                {
                    _mediaPlayer.Play(new Media(_libVLC, file, FromType.FromPath));
                    _videoFinalizado = false;
                }
                else
                {
                    MessageBox.Show("Apenas arquivos .mp4 são suportados.");
                }
            }

            if (!Video.MediaPlayer.IsPlaying)
            {
                Pause();
            }

            AtualizarVisibilidadeVideo(true);
        }

        private void BotaoEscolhaVideo_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Vídeos (*.mp4)|*.mp4|Todos os arquivos (*.*)|*.*",
                Title = "Escolha um vídeo"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _mediaPlayer.Play(new Media(_libVLC, openFileDialog.FileName, FromType.FromPath));
                _videoFinalizado = false;
                AtualizarVisibilidadeVideo(true);
            }
        }

        private void TimerVideo_Tick(object sender, EventArgs e)
        {
            AtualizarTempoVideo();
        }

        private void BarraVideo_Scroll(object sender, EventArgs e)
        {
            if (_mediaPlayer.Length > 0)
            {
                float pos = (float)BarraVideo.Value / BarraVideo.Maximum;
                _mediaPlayer.Time = (long)(_mediaPlayer.Length * pos);
            }
            AtualizarTempoVideo();
        }

        private void BarraVideo_MouseUp(object sender, MouseEventArgs e)
        {
            AtualizarTempoVideo();
        }

        private void VolumeVideo_Scroll(object sender, EventArgs e)
        {
            AtualizarVolume();
        }

        private void Painel_DoubleClick(object sender, EventArgs e)
        {
            AlternarFullscreen();
            Pause();
        }

        private void BotaoAjusteMinipayer_Click(object sender, EventArgs e)
        {
            var form = new AjustarMiniplayer();
            form.ShowDialog();
        }

        private void Player_FormClosed(object sender, FormClosedEventArgs e)
        {
            HotKeysUnregister();
            SalvarContinuarAssistindo();
            _mediaPlayer.Stop();
            _mediaPlayer.Dispose();
            _libVLC.Dispose();
        }

        private void BotaoContinuarAssistindo_Click(object sender, EventArgs e)
        {
            if (painelSelecionado == null)
            {
                MessageBox.Show("Selecione um vídeo para continuar assistindo.");
                CarregarContinuarAssistindo();
                return;
            }

            if (painelSelecionado.Tag is VideoInfo info && File.Exists(info.Caminho))
            {
                var media = new Media(_libVLC, info.Caminho, FromType.FromPath);
                _mediaPlayer.Play(media);
                _mediaPlayer.Time = info.Tempo;

                AtualizarVisibilidadeVideo(true);
            }
            else
            {
                MessageBox.Show("Arquivo não encontrado ou informação inválida.");
                CarregarContinuarAssistindo();
            }
        }

        private void ApagarContinuarAssistindo_Click(object sender, EventArgs e)
        {
            if (painelSelecionado == null)
            {
                MessageBox.Show("Selecione um item para apagar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (painelSelecionado.Tag is VideoInfo info)
            {
                var paths = Properties.Settings.Default.VideoPaths;
                var tempos = Properties.Settings.Default.VideoTimes;
                var datas = Properties.Settings.Default.VideoDatas;
                var duracoes = Properties.Settings.Default.VideoDuracao;

                if (paths != null && tempos != null && datas != null && duracoes != null)
                {
                    int index = paths.IndexOf(info.Caminho);

                    if (index >= 0)
                    {
                        paths.RemoveAt(index);
                        tempos.RemoveAt(index);
                        datas.RemoveAt(index);
                        duracoes.RemoveAt(index);
                        Properties.Settings.Default.Save();
                    }
                }

                ContinuarAssistindo.Controls.Remove(painelSelecionado);
            }
        }

        private void ContinuarAssistindo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (painelSelecionado == null)
            {
                NomeVideoContinuarAssistindo.Visible = false;
                TempoVideoContinuarAssistindo.Visible = false;
                ProgressoVideoContinuarAssistindo.Visible = false;
                return;
            }

            NomeVideoContinuarAssistindo.Visible = true;
            TempoVideoContinuarAssistindo.Visible = true;
            ProgressoVideoContinuarAssistindo.Visible = true;

            NomeVideoContinuarAssistindo.Text = painelSelecionado.Text;

            if (painelSelecionado.Tag is VideoInfo info)
            {
                // Formata tempo atual e duração
                string tempoAtual = FormatarTempo(info.Tempo);
                string duracao = FormatarTempo(info.Duracao); // Assumindo que você adicionou esse campo
                TempoVideoContinuarAssistindo.Text = $"{tempoAtual} / {duracao}";

                // Calcula progresso como porcentagem
                if (info.Duracao > 0)
                {
                    int progresso = (int)((info.Tempo * 100) / info.Duracao);
                    ProgressoVideoContinuarAssistindo.Value = Math.Min(100, Math.Max(0, progresso));
                }
                else
                {
                    ProgressoVideoContinuarAssistindo.Value = 0;
                }
            }
            else
            {
                TempoVideoContinuarAssistindo.Text = "-";
                ProgressoVideoContinuarAssistindo.Value = 0;
            }
        }
    }
}
