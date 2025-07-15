// BackgroundSemVideo.cs
using LibVLCSharp.Shared;

namespace BlockPlayer
{
    public partial class Janela : Form
    {
        public Janela()
        {
            InitializeComponent();
            ConfigVLC();
            ConfigInterface();
            ConfigSemVideo();
            ConfigMiniplayer();
            ConfigHotKeys();

            // Janela obter foco ao iniciar e exibir na frente
            this.Activate();
            this.BringToFront();


            // Adição manual para teste, precisa fazer o salvamento e resgatar
            var item = new ListViewItem("Nome do vídeo");
            item.Tag = new VideoInfo
            {
                Caminho = @"C:\Users\igord\Videos\Animes\[AnimeFire.plus] Grand Blue Season 2 - Episódio 1 (HD).mp4",
                Tempo = 10000 // tempo em milissegundos onde parou
            };
            ContinuarAssistindo.Items.Add(item);
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
                if (ext == ".mp4" || ext == ".mp3")
                {
                    _mediaPlayer.Play(new Media(_libVLC, file, FromType.FromPath));
                }
                else
                {
                    MessageBox.Show("Apenas arquivos .mp4 ou .mp3 são suportados.");
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
            _mediaPlayer.Stop();
            _mediaPlayer.Dispose();
            _libVLC.Dispose();
        }

        private void BotaoContinuarAssistindo_Click(object sender, EventArgs e)
        {
            if (ContinuarAssistindo.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um vídeo para continuar assistindo.");
                return;
            }

            var itemSelecionado = ContinuarAssistindo.SelectedItems[0];

            if (itemSelecionado.Tag is VideoInfo info && File.Exists(info.Caminho))
            {
                var media = new Media(_libVLC, info.Caminho, FromType.FromPath);
                _mediaPlayer.Play(media);

                // Espera um pouco para garantir que o vídeo carregue
                System.Windows.Forms.Timer delay = new System.Windows.Forms.Timer { Interval = 200 };
                delay.Tick += (s, ev) =>
                {
                    delay.Stop();
                    delay.Dispose();
                    _mediaPlayer.Time = info.Tempo;
                };
                delay.Start();

                AtualizarVisibilidadeVideo(true);
            }
            else
            {
                MessageBox.Show("Arquivo não encontrado ou informação inválida.");
            }
        }
    }
}
