using System.Threading;

namespace BlockPlayer
{
    public partial class Janela : Form
    {
        public void AlternarMaximizado()
        {
            if (EstaFullscreen) return;

            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
            }

            EstaMaximizado = (WindowState == FormWindowState.Maximized);
        }


        public void AlternarFullscreen()
        {
            if (!EstaFullscreen)
            {
                // Salva estado da janela antes do fullscreen (tamanho e posição apenas)
                if (WindowState == FormWindowState.Normal)
                    _boundsAntesFullscreen = Bounds;
                else
                    _boundsAntesFullscreen = RestoreBounds;

                _bordaAnterior = this.FormBorderStyle;
                _topMostAnterior = this.TopMost;

                // Entra em fullscreen
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Normal;
                Bounds = Screen.FromControl(this).Bounds;
                TopMost = true;

                EstaFullscreen = true;
            }
            else
            {
                // Sai do fullscreen e volta ao estado "normal", sempre
                FormBorderStyle = _bordaAnterior;
                TopMost = _topMostAnterior;

                WindowState = FormWindowState.Normal;
                Bounds = _boundsAntesFullscreen;

                EstaFullscreen = false;
            }
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
            if (_videoFinalizado)
            {
                _mediaPlayer.Stop(); // Reinicia
                _mediaPlayer.Play();
                _videoFinalizado = false;
                TimerVideo.Start();
                return;
            }

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
                _mediaPlayer.Time = tempoAtual;

                Thread.Sleep(50);

                _mediaPlayer.Time = tempoAtual;

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
