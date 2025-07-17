using System.Threading;

namespace BlockPlayer
{
    public partial class Janela : Form
    {
        public void AlternarMaximizado()
        {
            if (EstaFullscreen) AlternarFullscreen();
            WindowState = EstaMaximizado ? FormWindowState.Normal : FormWindowState.Maximized;
            EstaMaximizado = !EstaMaximizado;
        }

        public void AlternarFullscreen()
        {
            if (EstaMaximizado) AlternarMaximizado();
            FormBorderStyle = EstaFullscreen ? FormBorderStyle.Sizable : FormBorderStyle.None;
            WindowState = EstaFullscreen ? FormWindowState.Normal : FormWindowState.Maximized;
            EstaFullscreen = !EstaFullscreen;
        }

        public void AtualizarVisibilidadeVideo(bool visivel)
        {
            Video.Visible = visivel;
            PainelSemVideo.Visible = !visivel;
            if (visivel)
            {
                Video.Invalidate();
            }
            else
            {
                CarregarContinuarAssistindo();
                ExibirInterface(false);
                PainelSemVideo.BringToFront();
                PainelSemVideo.Invalidate();
            }
        }

        public void ExibirInterface(bool exibe)
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

        public void Pause()
        {
            if (_mediaPlayer.IsPlaying)
            {
                TimerVideo.Stop();
                ExibirInterface(true);
                AtualizarTempoVideo();
                AtualizarVolume();
                _mediaPlayer.SetPause(true);
            }
            else
            {
                ExibirInterface(false);
                AtualizarVolume();
                _mediaPlayer.SetPause(false);
                TimerVideo.Start();
            }
        }

        private void AlternarMiniplayer()
        {
            long tempoAtual = _mediaPlayer.Time;

            Thread.Sleep(50);

            if (_miniplayer.Visible)
            {
                _mediaPlayer.Stop();
                _miniplayer.Video.MediaPlayer = null;
                Video.MediaPlayer = _mediaPlayer;
                _miniplayer.Hide();
                this.Show();
                this.Activate();
                _mediaPlayer.Play();

                Thread.Sleep(50);

                _mediaPlayer.Time = tempoAtual;

                Thread.Sleep(50);

                Pause();

            }

            else if (_mediaPlayer.Media != null)
            {
                _mediaPlayer.Stop();
                Video.MediaPlayer = null;
                _miniplayer.Video.MediaPlayer = _mediaPlayer;
                _miniplayer.AtualizarTamanho();
                _miniplayer.Show();
                this.Hide();
                _miniplayer.Activate();
                _mediaPlayer.Play();

                Thread.Sleep(50);

                _mediaPlayer.Time = tempoAtual;
            }
        }
    }
}
