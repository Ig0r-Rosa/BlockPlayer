using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System.Numerics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace BlockPlayer
{
    public partial class BackgroundSemVideo : Form
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;

        List<Control> Interface;

        public BackgroundSemVideo()
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
            Interface = new List<Control>
            {
                BarraVideo,
                TempoAtualFinal,
                VolumeVideo,
                VolumeTexto
            };

            foreach (var item in Interface)
            {
                item.Parent = this;
                item.BringToFront();
                item.Invalidate(); // força o redesenho
            }
        }

        private void ExibirInterface(bool exibe)
        {
            foreach (var item in Interface)
            {
                item.Visible = exibe;
                if (exibe)
                {
                    item.BringToFront();
                    item.Invalidate();
                }
            }
        }

        private void Pause()
        {
            if (Video.MediaPlayer.IsPlaying)
            {
                AtualizarTempoVideo();
                AtualizarVolume();
                Video.MediaPlayer.SetPause(true);
                ExibirInterface(true);
                TimerVideo.Stop();
            }
            else
            {
                AtualizarVolume();
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

        private void AtualizarVolume()
        {
            _mediaPlayer.Volume = VolumeVideo.Value;
            VolumeTexto.Text = VolumeVideo.Value.ToString();
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

        private void BotaoStop_Click(object sender, EventArgs e)
        {
            _mediaPlayer.Stop();
            ExibirInterface(false);
        }

        private void VolumeVideo_Scroll(object sender, EventArgs e)
        {
            AtualizarVolume();
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
