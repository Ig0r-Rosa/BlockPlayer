using System.Runtime.InteropServices;

namespace BlockPlayer
{
    public class GlobalHotkey : IMessageFilter
    {
        private const int WM_HOTKEY = 0x0312;
        private int hotkeyId;
        private Form _form;
        public event Action OnHotkeyPressed;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [Flags]
        public enum Modifiers : uint
        {
            None = 0x0000,
            Alt = 0x0001,
            Control = 0x0002,
            Shift = 0x0004,
            Win = 0x0008
        }

        public GlobalHotkey(Form form, int id, Keys key, Modifiers modifiers = Modifiers.Control | Modifiers.Shift)
        {
            _form = form;
            hotkeyId = id;
            RegisterHotKey(form.Handle, hotkeyId, (uint)modifiers, (uint)key);
            Application.AddMessageFilter(this);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && (int)m.WParam == hotkeyId)
            {
                OnHotkeyPressed?.Invoke();
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            UnregisterHotKey(_form.Handle, hotkeyId);
            Application.RemoveMessageFilter(this);
        }
    }
}
