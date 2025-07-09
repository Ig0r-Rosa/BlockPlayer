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

            Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);

            Video.MediaPlayer = _mediaPlayer;

            Painel.Dock = DockStyle.Fill;
            Painel.BringToFront();
            Painel.BackColor = Color.Transparent;
            Painel.Click += (_, __) => Pause();
        }

        private void Pause()
        {
            if (Video.MediaPlayer.IsPlaying)
            {
                Video.MediaPlayer.SetPause(true);
            }
            else
            {
                Video.MediaPlayer.SetPause(false);
            }
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
