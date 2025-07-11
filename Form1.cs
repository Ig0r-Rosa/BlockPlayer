using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System.Diagnostics.Eventing.Reader;
using System.Numerics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace BlockPlayer
{
    public partial class BackgroundSemVideo : Form
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;

        List<Control> Interface;

        // Estados de visualização
        private bool EstaMaximizado = false;
        private bool EstaFullscreen = false;


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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Delete:
                    _mediaPlayer.Stop();
                    AtualizarVisibilidadeVideo(false);
                    return true;

                case Keys.Space:
                    Pause();
                    return true;

                case Keys.Right:
                    _mediaPlayer.Time += 5000;
                    AtualizarTempoVideo();
                    return true;

                case Keys.Left:
                    _mediaPlayer.Time = Math.Max(0, _mediaPlayer.Time - 5000);
                    AtualizarTempoVideo();
                    return true;

                case Keys.Up:
                    VolumeVideo.Value = Math.Min(VolumeVideo.Value + 5, VolumeVideo.Maximum);
                    AtualizarVolume();
                    return true;

                case Keys.Down:
                    VolumeVideo.Value = Math.Max(VolumeVideo.Value - 5, VolumeVideo.Minimum);
                    AtualizarVolume();
                    return true;

                case Keys.Enter:
                    AlternarMaximizado();
                    return true;

                case Keys.F11:
                    AlternarFullscreen();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AlternarMaximizado()
        {
            if (EstaFullscreen)
            {
                AlternarFullscreen();
            }

            if (EstaMaximizado)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
            }

            EstaMaximizado = !EstaMaximizado;
        }

        private void AlternarFullscreen()
        {
            if (EstaMaximizado)
            {
                AlternarMaximizado();
            }

            if (!EstaFullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
            }
            EstaFullscreen = !EstaFullscreen;
        }

        private void AtualizarVisibilidadeVideo(bool visivel)
        {
            Video.Visible = visivel;
            PainelSemVideo.Visible = !visivel;
            if (visivel)
            {
                Video.BringToFront();
                Video.Invalidate();

                Painel.BringToFront();
            }
            else
            {
                ExibirInterface(false);
                PainelSemVideo.BringToFront();
                PainelSemVideo.Invalidate();

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
                TimerVideo.Stop();
                AtualizarTempoVideo();
                AtualizarVolume();
                Video.MediaPlayer.SetPause(true);
                ExibirInterface(true);
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

            if (!Video.MediaPlayer.IsPlaying)
            {
                Pause();
            }

            AtualizarVisibilidadeVideo(true);
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

        private void VolumeVideo_Scroll(object sender, EventArgs e)
        {
            AtualizarVolume();
        }

        private void Painel_DoubleClick(object sender, EventArgs e)
        {
            AlternarFullscreen();
            Pause();
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
