using System.Runtime.InteropServices;
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

        // --- Constantes e imports para manipulação de janelas Win32 ---
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int WS_EX_TRANSPARENT = 0x00000020;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_TOPMOST = 0x00000008;

        private const int GWL_STYLE = -16;
        private const int WS_POPUP = unchecked((int)0x80000000);
        private const int WS_VISIBLE = 0x10000000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        static extern int SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOMOVE = 0x0002;
        const uint SWP_NOACTIVATE = 0x0010;
        const uint SWP_SHOWWINDOW = 0x0040;
        const uint SWP_NOZORDER = 0x0004;
        const uint SWP_FRAMECHANGED = 0x0020;
        const uint SWP_NOSENDCHANGING = 0x0400;

        // --- Configura miniplayer para overlay sem foco ---
        private void TornarMiniplayerOverlay()
        {
            int exStyle = GetWindowLong(_miniplayer.Handle, GWL_EXSTYLE);
            exStyle |= WS_EX_NOACTIVATE | WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_TOOLWINDOW;
            SetWindowLong(_miniplayer.Handle, GWL_EXSTYLE, exStyle);
        }

        // --- Remove WS_EX_TRANSPARENT para permitir clique (se quiser ativar isso) ---
        private void TornarMiniplayerCompatível()
        {
            int exStyle = GetWindowLong(_miniplayer.Handle, GWL_EXSTYLE);
            exStyle &= ~WS_EX_TRANSPARENT;
            exStyle |= WS_EX_NOACTIVATE | WS_EX_TOPMOST | WS_EX_TOOLWINDOW;
            SetWindowLong(_miniplayer.Handle, GWL_EXSTYLE, exStyle);
        }

        // --- Força estilo popup e visível para overlay ---
        private void ForcarOverlayCompleto()
        {
            int style = (int)GetWindowLongPtr(_miniplayer.Handle, GWL_STYLE);
            style |= WS_POPUP | WS_VISIBLE;
            SetWindowLongPtr(_miniplayer.Handle, GWL_STYLE, (IntPtr)style);
        }

        // --- Alterna miniplayer ---
        private void AlternarMiniplayer()
        {
            long tempoAtual = _mediaPlayer.Time;

            Thread.Sleep(50);

            if (_miniplayer.Visible)
            {
                _mediaPlayer.Stop();
                _miniplayer.Video.MediaPlayer = null;

                // Esconde o miniplayer e força atualização para evitar tela preta
                _miniplayer.Hide();
                _miniplayer.Invalidate();

                // Força estilo e posição para overlay principal
                ForcarOverlayCompleto();
                SetWindowPos(_miniplayer.Handle, HWND_TOPMOST, 0, 0, 0, 0,
                    SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE | SWP_SHOWWINDOW);

                // Mostra o player principal e traz para frente
                this.Show();
                this.BringToFront();
                this.Activate();

                Video.MediaPlayer = _mediaPlayer; // reassocia player de vídeo
                _mediaPlayer.Play();
                _mediaPlayer.Time = tempoAtual;

                _miniplayer.Hide();
                _miniplayer.Invalidate();

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

                TornarMiniplayerOverlay();

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
