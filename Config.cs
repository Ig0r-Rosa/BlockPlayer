using LibVLCSharp.Shared;

namespace BlockPlayer
{
    public partial class Janela : Form
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;

        private List<Control> Interface;
        private bool EstaMaximizado = false;
        private bool EstaFullscreen = false;

        private Miniplayer _miniplayer;
        private GlobalHotkey _hotkey;

        private void ConfigVLC()
        {
            Core.Initialize();
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            Video.MediaPlayer = _mediaPlayer;
        }

        private void ConfigInterface()
        {
            Painel.Dock = DockStyle.Fill;
            Painel.BringToFront();
            Painel.BackColor = Color.Transparent;

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
                item.Invalidate();
            }
        }

        private void ConfigSemVideo()
        {
            AtualizarVisibilidadeVideo(false);
            AtualizarVolume();
        }
    }
}
