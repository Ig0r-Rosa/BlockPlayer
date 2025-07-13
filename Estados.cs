
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

        private void AlternarMiniplayer()
        {
            if (_miniplayer.Visible)
            {
                _miniplayer.Hide();
                this.Show();
                _mediaPlayer.Play();
                if (Video.MediaPlayer.IsPlaying == true)
                {
                    Pause();
                }
            }
            else
            {
                _miniplayer.Show();
                this.Hide();
                _miniplayer._mediaPlayer.Play();
                if (Video.MediaPlayer.IsPlaying == false)
                {
                    Pause();
                }
            }
        }
    }
}
