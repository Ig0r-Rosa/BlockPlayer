// BackgroundSemVideo.cs
using BlockPlayer.Classes;
using LibVLCSharp.Shared;

namespace BlockPlayer
{
    public partial class Janela : Form
    {
        public Janela(string[] args = null)
        {
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

            pastaMiniaturas = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "BlockPlayer", "Thumbs");
            if (!Directory.Exists(pastaMiniaturas))
            {
                Directory.CreateDirectory(pastaMiniaturas);
            }

            Thread.Sleep(100);

            BarraVideo.Paint += BarraVideo_Paint;
            VolumeVideo.Paint += VolumeVideo_Paint;

            this.TopMost = false;
        }

        public void AbrirVideo(string caminho)
        {
            if (string.IsNullOrWhiteSpace(caminho) || !File.Exists(caminho))
            {
                MessageBox.Show("Arquivo inválido ou não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            painelSelecionado = null;

            if (_videoFinalizado)
            {
                foreach (Control ctrl in ContinuarAssistindo.Controls)
                {
                    if (ctrl is Panel painel && painel.Tag is VideoInfo info && info.Caminho == _mediaPlayer?.Media?.Mrl)
                    {
                        painelSelecionado = painel;
                        break;
                    }
                }

                if(painelSelecionado != null)
                {
                    ApagarContinuarAssistindoSelecionado();
                }
            }
            else
            {
                SalvarContinuarAssistindo();
            }

            painelSelecionado = null;

            foreach (Control ctrl in ContinuarAssistindo.Controls)
            {
                if (ctrl is Panel painel && painel.Tag is VideoInfo info && info.Caminho == caminho)
                {
                    painelSelecionado = painel;
                    break;
                }
            }

            if (painelSelecionado != null)
            {
                PlayContinuarAssistindo();
                AtualizarVisibilidadeVideo(true);
                ExibirInterface(false);
                _videoFinalizado = false;

            }
            else
            {
                var media = new Media(_libVLC, caminho, FromType.FromPath);
                _mediaPlayer.Play(media);
                AtualizarVisibilidadeVideo(true);
                ExibirInterface(false);
                _videoFinalizado = false;
            }

            this.TopMost = true;

            Thread.Sleep(20);

            this.TopMost = false;
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
            AtualizarVolume();

            SalvarContinuarAssistindo();

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
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

            if (!_mediaPlayer.IsPlaying)
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
            BarraVideo.Invalidate();
            AtualizarVolume();
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

        private void BotaoContinuarAssistindo_Click(object sender, EventArgs e)
        {
            PlayContinuarAssistindo();
        }

        private void ApagarContinuarAssistindo_Click(object sender, EventArgs e)
        {
            ApagarContinuarAssistindoSelecionado();
        }

        private void ContinuarAssistindo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelecionarContinuarAssistindo();
        }

        private void Player_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_videoFinalizado)
            {
                // Seleciona o painel do video atual
                // Procura o painel do vídeo atual na lista "ContinuarAssistindo"
                foreach (Control ctrl in ContinuarAssistindo.Controls)
                {
                    if (ctrl is Panel painel && painel.Tag is VideoInfo info && info.Caminho == _mediaPlayer?.Media?.Mrl)
                    {
                        painelSelecionado = painel;
                        break;
                    }
                }

                ApagarContinuarAssistindoSelecionado();
            }
            else
            {
                SalvarContinuarAssistindo();
            }

            HotKeysUnregister();
            _mediaPlayer.Stop();
            _mediaPlayer.Dispose();
            _libVLC.Dispose();
        }

        private void Janela_Resize(object sender, EventArgs e)
        {
            AtualizarBarraTempoContinuarAssistindo();
        }
    }
}
