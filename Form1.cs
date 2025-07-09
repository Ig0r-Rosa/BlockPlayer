using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;

namespace BlockPlayer
{
    public partial class Form1 : Form
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;

        public Form1()
        {
            InitializeComponent();

            ConfigVLC();

            ConfigPainel();

            ConfigInterface();


        }

        private void ConfigVLC()
        {
            Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);

            Video.MediaPlayer = _mediaPlayer;
        }

        private void ConfigPainel()
        {
            Painel.Dock = DockStyle.Fill;
            Painel.BringToFront();
            Painel.BackColor = Color.Transparent;

        }

        private void ConfigInterface()
        {
            BarraVideo.Parent = this;
            BarraVideo.BringToFront();

            TempoAtualFinal.Parent = this;
            TempoAtualFinal.BringToFront();

            // Força o controle a se refrescar corretamente
            BarraVideo.Invalidate();
            TempoAtualFinal.Invalidate();
        }

        private void ExibirInterface(bool exibe)
        {
            BarraVideo.Visible = exibe;
            TempoAtualFinal.Visible = exibe;
            if (exibe)
            {
                TempoAtualFinal.BringToFront();
                BarraVideo.BringToFront();
                BarraVideo.Invalidate(); // força o redesenho
                TempoAtualFinal.Invalidate();
            }
        }

        private void Pause()
        {
            if (Video.MediaPlayer.IsPlaying)
            {
                AtualizarTempoVideo();
                Video.MediaPlayer.SetPause(true);
                ExibirInterface(true);
                TimerVideo.Stop();
            }
            else
            {
                Video.MediaPlayer.SetPause(false);
                ExibirInterface(false);
                TimerVideo.Start();
            }
        }

        private string FormatarTempo(long ms)
        {
            var totalSeconds = ms / 1000;
            var minutes = totalSeconds / 60;
            var seconds = totalSeconds % 60;
            return $"{minutes:D2}:{seconds:D2}";
        }

        private void AtualizarTempoVideo()
        {
            if (_mediaPlayer.Length > 0)
            {
                long pos = _mediaPlayer.Time; // posição atual (ms)
                long len = _mediaPlayer.Length; // duração total (ms)

                // Atualiza a barra de progresso
                int progress = (int)((double)pos / len * BarraVideo.Maximum);
                BarraVideo.Value = Math.Min(progress, BarraVideo.Maximum);

                // Atualiza o label com o tempo atual e total
                TempoAtualFinal.Text = $"{FormatarTempo(pos)} / {FormatarTempo(len)}";
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
        }

        private void Video_DragDrop(object sender, DragEventArgs e)
        {
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
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Dispose();
            _libVLC.Dispose();
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
                long newTime = (long)(_mediaPlayer.Length * pos);
                _mediaPlayer.Time = newTime;
            }
            AtualizarTempoVideo();
        }

        private void BarraVideo_MouseUp(object sender, MouseEventArgs e)
        {
            AtualizarTempoVideo();
        }
    }

    public class TransparentPanel : Panel
    {
        public TransparentPanel()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Não pintar o fundo
        }
    }
}
