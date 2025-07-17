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


        private void ConfigVLC()
        {
            Core.Initialize();
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            Video.MediaPlayer = _mediaPlayer;

            _mediaPlayer.EndReached += (sender, args) =>
            {
                this.Invoke(() =>
                {
                    TimerVideo.Stop();
                    _mediaPlayer.SetPause(true); // pausa no último frame
                    AtualizarTempoVideo();

                    // Marca o vídeo como finalizado
                    _videoFinalizado = true;
                });
            };
        }

        private void ConfigInterface()
        {
            Painel.Dock = DockStyle.Fill;
            Painel.BringToFront();               
            Painel.BackColor = Color.Transparent;

            Interface = new List<Control>
            {
                BarraVideo,
                TempoAtualFinal,
                VolumeVideo,
                VolumeTexto
            };

            foreach (var item in Interface)
            {
                item.Parent = this;
                item.BringToFront();
                item.Invalidate();
            }
        }

        private void ConfigSemVideo()
        {
            AtualizarVisibilidadeVideo(false);
            AtualizarVolume();
        }

        private void ConfigMiniplayer()
        {
            _miniplayer = new Miniplayer(_mediaPlayer);
            _miniplayer.FormClosed += (s, e) => _miniplayer = null;
        }

        private void ConfigHotKeys()
        {
            _hotkey = new GlobalHotkey(this, 1, Keys.M);
            _hotkey.OnHotkeyPressed += AlternarMiniplayer;

            _hotkeyVoltar = new GlobalHotkey(this, 2, Keys.Oemcomma);
            _hotkeyVoltar.OnHotkeyPressed += () =>
            {
                if(_miniplayer != null && _miniplayer.Visible)
                {
                    AjustarTempo(-10000);
                }
            };

            _hotkeyAvancar = new GlobalHotkey(this, 3, Keys.OemPeriod);
            _hotkeyAvancar.OnHotkeyPressed += () =>
            {
                if (_miniplayer != null && _miniplayer.Visible)
                {
                    AjustarTempo(10000);
                }
            };
        }

        private void CarregarContinuarAssistindo()
        {
            var paths = Properties.Settings.Default.VideoPaths;
            var tempos = Properties.Settings.Default.VideoTimes;
            var datas = Properties.Settings.Default.VideoDatas;
            var duracoes = Properties.Settings.Default.VideoDuracao;

            if (paths == null || tempos == null || datas == null || duracoes == null)
                return;

            var lista = new List<VideoInfo>();

            for (int i = 0; i < paths.Count; i++)
            {
                string path = paths[i];
                string tempo = tempos[i];
                string data = datas[i];
                string duracao = duracoes[i]; 

                if (File.Exists(path))
                {
                    lista.Add(new VideoInfo
                    {
                        Caminho = path,
                        Tempo = long.Parse(tempo),
                        Duracao = long.Parse(duracao),
                        DataAtualizacao = DateTime.Parse(data, null, System.Globalization.DateTimeStyles.RoundtripKind)
                    });
                }
            }

            // Ordena do mais recente para o mais antigo
            var ordenados = lista.OrderByDescending(v => v.DataAtualizacao).ToList();

            ContinuarAssistindo.Items.Clear();

            foreach (var video in ordenados)
            {
                var item = new ListViewItem(System.IO.Path.GetFileName(video.Caminho));
                item.SubItems.Add(FormatarTempo(video.Tempo));
                item.SubItems.Add(video.DataAtualizacao.ToString("dd/MM/yyyy HH:mm"));
                item.Tag = video;
                ContinuarAssistindo.Items.Add(item);
            }

            // Atualiza apenas com os que ainda existem
            Properties.Settings.Default.VideoPaths = new System.Collections.Specialized.StringCollection();
            Properties.Settings.Default.VideoTimes = new System.Collections.Specialized.StringCollection();
            Properties.Settings.Default.VideoDatas = new System.Collections.Specialized.StringCollection();
            Properties.Settings.Default.VideoDuracao = new System.Collections.Specialized.StringCollection();

            foreach (var video in ordenados)
            {
                Properties.Settings.Default.VideoPaths.Add(video.Caminho);
                Properties.Settings.Default.VideoTimes.Add(video.Tempo.ToString());
                Properties.Settings.Default.VideoDatas.Add(video.DataAtualizacao.ToString("o"));
                Properties.Settings.Default.VideoDuracao.Add(video.Duracao.ToString());
            }

            Properties.Settings.Default.Save();
        }
    }
}
