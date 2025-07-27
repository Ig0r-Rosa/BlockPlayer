using BlockPlayer.Classes;
using LibVLCSharp.Shared;

namespace BlockPlayer
{
    public partial class Janela : Form
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;

        private List<Control> Interface;
        private bool EstaMaximizado = false;
        private bool EstaFullscreen = false;

        private Miniplayer _miniplayer;

        private GlobalHotkey _hotkey;
        private GlobalHotkey _hotkeyVoltar;
        private GlobalHotkey _hotkeyAvancar;

        private AjustarMiniplayer ajustarMiniplayerForm = null;

        private string _arquivoInicial;

        private bool _videoFinalizado = false;

        private Panel painelSelecionado = null;

        string pastaMiniaturas = "./";

        private Rectangle _boundsAntesFullscreen;
        private FormBorderStyle _bordaAnterior;
        private bool _topMostAnterior;

        private int VolumeAtual = 50; // valor entre 0 e 100
        private const int VolumeMaximo = 100;
        private bool _arrastandoBarra = false;
        private bool _arrastandoBarraVolume = false;

    }
}