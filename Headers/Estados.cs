using LibVLCSharp.Shared;
using System.Runtime.InteropServices;
using System.Threading;

namespace BlockPlayer
{
    public partial class Janela : Form
    {
        // Alterna entre janela normal e maximizada (sem ser fullscreen)
        public void AlternarMaximizado()
        {
            if (EstaFullscreen) return;

            if (WindowState == FormWindowState.Maximized)
                WindowState = FormWindowState.Normal;
            else
                WindowState = FormWindowState.Maximized;

            EstaMaximizado = (WindowState == FormWindowState.Maximized);
        }

        // Alterna entre janela normal/maximizada e modo fullscreen real
        public void AlternarFullscreen()
        {
            if (!EstaFullscreen)
            {
                // Salva posição e tamanho atuais para restaurar depois
                _boundsAntesFullscreen = (WindowState == FormWindowState.Normal) ? Bounds : RestoreBounds;
                _bordaAnterior = FormBorderStyle;
                _topMostAnterior = TopMost;

                // Ativa o fullscreen real
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Normal;
                Bounds = Screen.FromControl(this).Bounds;
                TopMost = true;

                EstaFullscreen = true;
            }
            else
            {
                // Restaura o estado anterior ao fullscreen
                FormBorderStyle = _bordaAnterior;
                TopMost = _topMostAnterior;

                WindowState = FormWindowState.Normal;
                Bounds = _boundsAntesFullscreen;

                EstaFullscreen = false;
            }
        }

        // Mostra ou oculta o vídeo principal
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

        // Exibe ou oculta os controles visuais da interface (barra, botões etc.)
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

        // Alterna entre play/pause, com verificação de finalização e delay inicial
        public void Pause()
        {
            if (_videoFinalizado)
            {
                _mediaPlayer.Stop(); // Reinicia o vídeo se tiver sido finalizado
                _mediaPlayer.Play();
                _videoFinalizado = false;
                TimerVideo.Start();
                ExibirInterface(false);
                return;
            }

            if (_mediaPlayer.IsPlaying)
            {
                // Aguarda tempo inicial atualizar (evita congelamento no 00:00)
                int tentativas = 0;
                while (_mediaPlayer.Time == 0 && tentativas < 15)
                {
                    Thread.Sleep(20);
                    tentativas++;
                }

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

        // --- Win32 API: Manipulação de janelas no Windows ---

        // Constantes de estilo de janelas
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int WS_EX_TRANSPARENT = 0x00000020;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_TOPMOST = 0x00000008;

        private const int GWL_STYLE = -16;
        private const int WS_POPUP = unchecked((int)0x80000000);
        private const int WS_VISIBLE = 0x10000000;

        // Funções nativas do Windows
        [DllImport("user32.dll", SetLastError = true)] private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", SetLastError = true)] private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")] private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")] static extern int SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll")] static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")] private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")] private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);
        [DllImport("kernel32.dll")] private static extern uint GetCurrentThreadId();
        [DllImport("user32.dll")] private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);
        [DllImport("user32.dll")] private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // Constantes para ShowWindow / SetWindowPos
        private const int SW_RESTORE = 9;
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOMOVE = 0x0002;
        const uint SWP_NOACTIVATE = 0x0010;
        const uint SWP_SHOWWINDOW = 0x0040;
        const uint SWP_NOZORDER = 0x0004;
        const uint SWP_FRAMECHANGED = 0x0020;
        const uint SWP_NOSENDCHANGING = 0x0400;

        // Força o foco para uma janela específica
        private static void ForcarFocoJanela(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero || hWnd == GetForegroundWindow())
                return;

            GetWindowThreadProcessId(hWnd, out uint processoAtivo);
            uint idThreadAtual = GetCurrentThreadId();
            uint idThreadJanela = GetWindowThreadProcessId(hWnd, out processoAtivo);

            AttachThreadInput(idThreadAtual, idThreadJanela, true);
            ShowWindow(hWnd, SW_RESTORE);
            SetForegroundWindow(hWnd);
            AttachThreadInput(idThreadAtual, idThreadJanela, false);
        }

        // Aplica estilo de janela para overlay sem foco e sem bordas
        private void TornarMiniplayerOverlay()
        {
            int exStyle = GetWindowLong(_miniplayer.Handle, GWL_EXSTYLE);
            exStyle |= WS_EX_NOACTIVATE | WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_TOOLWINDOW;
            SetWindowLong(_miniplayer.Handle, GWL_EXSTYLE, exStyle);
        }

        // Remove transparência para permitir cliques no miniplayer
        private void TornarMiniplayerCompatível()
        {
            int exStyle = GetWindowLong(_miniplayer.Handle, GWL_EXSTYLE);
            exStyle &= ~WS_EX_TRANSPARENT;
            exStyle |= WS_EX_NOACTIVATE | WS_EX_TOPMOST | WS_EX_TOOLWINDOW;
            SetWindowLong(_miniplayer.Handle, GWL_EXSTYLE, exStyle);
        }

        // Aplica estilo WS_POPUP | WS_VISIBLE necessário para sobreposição
        private void ForcarOverlayCompleto()
        {
            int style = (int)GetWindowLongPtr(_miniplayer.Handle, GWL_STYLE);
            style |= WS_POPUP | WS_VISIBLE;
            SetWindowLongPtr(_miniplayer.Handle, GWL_STYLE, (IntPtr)style);
        }

        // Alterna entre o player principal e o miniplayer
        private void AlternarMiniplayer()
        {
            long tempoAtual = _mediaPlayer.Time;

            if (_miniplayer.Visible)
            {
                _mediaPlayer.Stop();
                _miniplayer.Video.MediaPlayer = null;

                // Oculta o miniplayer e atualiza para evitar tela preta
                _miniplayer.Hide();
                _miniplayer.Invalidate();

                // Restaura player principal
                ForcarOverlayCompleto();
                SetWindowPos(_miniplayer.Handle, HWND_TOPMOST, 0, 0, 0, 0,
                    SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE | SWP_SHOWWINDOW);

                this.Show();
                this.BringToFront();

                Video.MediaPlayer = _mediaPlayer;
                _mediaPlayer.Play();
                _mediaPlayer.Time = tempoAtual;

                _miniplayer.Visible = false;
                _miniplayer.Video.MediaPlayer = null;

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

                TornarMiniplayerCompatível();
                TornarMiniplayerOverlay();
                ForcarOverlayCompleto();

                SetWindowPos(_miniplayer.Handle, HWND_TOPMOST, 0, 0, 0, 0,
                    SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE | SWP_SHOWWINDOW);

                IntPtr janelaAnterior = GetForegroundWindow(); // Salva foco atual (jogo, por exemplo)

                this.Hide(); // Oculta player principal antes de mostrar o mini

                _miniplayer.Show();
                _miniplayer.BringToFront();
                _miniplayer.Video.Invalidate();
                _miniplayer.Video.Update();    
                
                Thread.Sleep(50);

                // Após exibir o miniplayer, retorna o foco à janela anterior (jogo)
                ForcarFocoJanela(janelaAnterior);

                _mediaPlayer.Play();
                Thread.Sleep(50);
                _mediaPlayer.Time = tempoAtual;
            }
        }
    }
}
