using BlockPlayer.Classes;
using LibVLCSharp.Shared;

namespace BlockPlayer
{
    public partial class Janela : Form
    {
        private void ConfigVLC()
        {
            Core.Initialize();
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            Video.MediaPlayer = _mediaPlayer;

            _mediaPlayer.EndReached += (sender, args) =>
            {
                this.Invoke(() =>
                {
                    TimerVideo.Stop();
                    _mediaPlayer.SetPause(true); // pausa no último frame
                    AtualizarTempoVideo();

                    // Marca o vídeo como finalizado
                    _videoFinalizado = true;
                });
            };
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

        private void ConfigMiniplayer()
        {
            _miniplayer = new Miniplayer(_mediaPlayer);
            _miniplayer.FormClosed += (s, e) => _miniplayer = null;

            pastaMiniaturas = Path.Combine(Application.StartupPath, "Thumbs");
            if (!Directory.Exists(pastaMiniaturas))
                Directory.CreateDirectory(pastaMiniaturas);
        }
    }
}
