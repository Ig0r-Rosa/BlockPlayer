using LibVLCSharp.Shared;
using System.Runtime.InteropServices;

namespace BlockPlayer
{
    public partial class Janela : Form
    {
        private void ConfigVLC()
        {
            // Inicializa o LibVLC e o MediaPlayer
            Core.Initialize();
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            Video.MediaPlayer = _mediaPlayer;

            // Ao acabar o vídeo, pausa no último frame
            _mediaPlayer.EndReached += (sender, args) =>
            {
                this.BeginInvoke(() =>
                {
                    if (_miniplayer.Visible)
                    {
                        _mediaPlayer.SetPause(true);
                        // Evita conflito com threads do VLC
                        Task.Delay(1).ContinueWith(_ =>
                        {
                            this.BeginInvoke(() =>
                            {
                                AlternarMiniplayer();
                                Thread.Sleep(10);
                                TimerVideo.Stop();
                                _mediaPlayer.SetPause(true);
                                ExibirInterface(true);
                                TempoAtualFinal.Text = "Vídeo finalizado!";
                                _videoFinalizado = true;
                            });
                        });
                    }
                    else
                    {
                        TimerVideo.Stop();
                        _mediaPlayer.SetPause(true);
                        AtualizarTempoVideo();
                        ExibirInterface(true);
                        TempoAtualFinal.Text = "Vídeo finalizado!";
                        _videoFinalizado = true;
                    }
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
                PainelInterface,
                PainelVolume,
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

            BarraVideo.Parent = PainelInterface;

            TempoAtualFinal.Parent = PainelInterface;
            PainelVolume.Parent = PainelInterface;
            PainelVolume.Dock = DockStyle.Right;
            VolumeVideo.Parent = PainelVolume;
            VolumeTexto.Parent = PainelVolume;
            VolumeVideo.Dock = DockStyle.Right;
            VolumeTexto.Dock = DockStyle.Right;
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
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
        int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
        int nWidthEllipse, int nHeightEllipse);
    }
}
