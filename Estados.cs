
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

        private void AlternarMiniPlayer()
        {
            if (_miniplayer == null || _miniplayer.IsDisposed)
            {
                _miniplayer = new Miniplayer(_mediaPlayer);
                _miniplayer.Show();
            }
            else
            {
                _miniplayer.Close();
            }
        }
    }
}
