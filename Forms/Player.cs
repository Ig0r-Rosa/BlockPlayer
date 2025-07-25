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

            Thread.Sleep(100);

            BarraVideo.Paint += BarraVideo_Paint;

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
            HotKeysUnregister();
            SalvarContinuarAssistindo();
            _mediaPlayer.Stop();
            _mediaPlayer.Dispose();
            _libVLC.Dispose();
        }

        private void BarraVideo_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int barraAltura = BarraVideo.Height;

            // Fundo
            using (var fundo = new SolidBrush(Color.FromArgb(40, 40, 40)))
                g.FillRectangle(fundo, 0, 0, BarraVideo.Width, barraAltura);

            if (_mediaPlayer.Length <= 0) return;

            float progresso = _mediaPlayer.Time / (float)_mediaPlayer.Length;
            int larguraProgresso = (int)(BarraVideo.Width * progresso);

            // Progresso azul
            using (var progressoBrush = new SolidBrush(Color.DeepSkyBlue))
                g.FillRectangle(progressoBrush, 0, 0, larguraProgresso, barraAltura);

            // Círculo branco indicador (tamanho levemente maior que a barra)
            int tamanhoCirculo = (int)(barraAltura * 0.95f);
            int offsetY = (barraAltura - tamanhoCirculo) / 2;

            using (var circuloBrush = new SolidBrush(Color.White))
                g.FillEllipse(circuloBrush, larguraProgresso - (tamanhoCirculo / 2), offsetY, tamanhoCirculo, tamanhoCirculo);
        }

        private void BarraVideo_MouseDown(object sender, MouseEventArgs e)
        {
            if (_mediaPlayer.Length <= 0) return;

            float pos = (float)e.X / BarraVideo.Width;
            _mediaPlayer.Time = (long)(_mediaPlayer.Length * pos);
            BarraVideo.Invalidate();
        }
    }
}
