using LibVLCSharp.Shared;
using System.Runtime.InteropServices;

namespace BlockPlayer
{
    public partial class Miniplayer : Form
    {
        public Miniplayer()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            ShowInTaskbar = false;
            BackColor = Color.Black;
            Opacity = 0.8; // Leve transparência
            Width = 300;
            Height = 80;
            Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Width - 10, 10);

            SetClickThrough(); // ⬅ essencial
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

        // Torna o formulário não clicável (ignora mouse)
        private void SetClickThrough()
        {
            int exStyle = (int)GetWindowLong(this.Handle, GWL_EXSTYLE);
            exStyle |= WS_EX_LAYERED | WS_EX_TRANSPARENT;
            SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)exStyle);
        }

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
    }
}