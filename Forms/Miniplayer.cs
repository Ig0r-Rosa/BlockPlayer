using LibVLCSharp.Shared;
using System.Runtime.InteropServices;
using LibVLCSharp.WinForms;
using BlockPlayer.Propriedades;

namespace BlockPlayer
{
    public partial class Miniplayer : Form
    {
        //private readonly MediaPlayer _mediaPlayer;

        public VideoView Video => videoView1;

        public Miniplayer(MediaPlayer mediaPlayer)
        {
            // Carregar configuração salva
            this.Left = Propriedades.Settings.Default.MiniplayerX;
            this.Top = Propriedades.Settings.Default.MiniplayerY;
            this.Opacity = Propriedades.Settings.Default.MiniplayerOpacity;
            this.Width = Propriedades.Settings.Default.MiniplayerSizeX;
            this.Height = Propriedades.Settings.Default.MiniplayerSizeY;
            InitializeComponent();

            // Quando inicializo os componentes antes de definir os paramentros de tamanho, etc

            // Faz ele não receber foco
            int initialStyle = (int)WinAPI.GetWindowLong(this.Handle, WinAPI.GWL_EXSTYLE);
            WinAPI.SetWindowLong(this.Handle, WinAPI.GWL_EXSTYLE, initialStyle | WinAPI.WS_EX_NOACTIVATE);

            SetClickThrough();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_NOACTIVATE = 0x08000000;
                var cp = base.CreateParams;
                cp.ExStyle |= WS_EX_NOACTIVATE;
                return cp;
            }
        }

        // Torna o formulário não clicável
        public void SetClickThrough()
        {
            int exStyle = (int)GetWindowLong(this.Handle, GWL_EXSTYLE);
            exStyle |= WS_EX_LAYERED | WS_EX_TRANSPARENT;
            SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)exStyle);
        }

        public void AtualizarTamanho()
        {
            this.Left = Propriedades.Settings.Default.MiniplayerX;
            this.Top = Propriedades.Settings.Default.MiniplayerY;
            this.Opacity = Propriedades.Settings.Default.MiniplayerOpacity;
            this.Width = Propriedades.Settings.Default.MiniplayerSizeX;
            this.Height = Propriedades.Settings.Default.MiniplayerSizeY;
            SetClickThrough();
        }

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
    }

    public static class WinAPI
    {
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_NOACTIVATE = 0x08000000;

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    }
}