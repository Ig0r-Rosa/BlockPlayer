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

        string pastaMiniaturas = "/";


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

            pastaMiniaturas = Path.Combine(Application.StartupPath, "Thumbs");
            if (!Directory.Exists(pastaMiniaturas))
                Directory.CreateDirectory(pastaMiniaturas);
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
            List<string> paths = Properties.Settings.Default.VideoPaths.Cast<string>().ToList();
            List<string> tempos = Properties.Settings.Default.VideoTimes.Cast<string>().ToList();
            List<string> datas = Properties.Settings.Default.VideoDatas.Cast<string>().ToList();
            List<string> duracoes = Properties.Settings.Default.VideoDuracao.Cast<string>().ToList();

            if (Properties.Settings.Default.VideoThumbs == null)
            {
                Properties.Settings.Default.VideoThumbs = new System.Collections.Specialized.StringCollection();
            }

            List<string> thumbs = Properties.Settings.Default.VideoThumbs.Cast<string>().ToList();

            List<VideoInfo> lista = new List<VideoInfo>();

            // Limpa os vídeos inválidos (removidos ou renomeados)
            for (int i = 0; i < paths.Count; i++)
            {
                string path = paths[i];

                if (!File.Exists(path))
                {
                    paths.RemoveAt(i);
                    tempos.RemoveAt(i);
                    datas.RemoveAt(i);
                    duracoes.RemoveAt(i);
                    thumbs.RemoveAt(i);
                    i--; // Corrige o índice após remover
                }
            }

            // Salva a configuração limpa
            Properties.Settings.Default.VideoPaths = new System.Collections.Specialized.StringCollection();
            Properties.Settings.Default.VideoTimes = new System.Collections.Specialized.StringCollection();
            Properties.Settings.Default.VideoDatas = new System.Collections.Specialized.StringCollection();
            Properties.Settings.Default.VideoDuracao = new System.Collections.Specialized.StringCollection();
            Properties.Settings.Default.VideoThumbs = new System.Collections.Specialized.StringCollection();

            for (int i = 0; i < paths.Count; i++)
            {
                string path = paths[i];
                string tempo = tempos[i];
                string data = datas[i];
                string duracao = duracoes[i];
                string thumb = thumbs[i];

                // Adiciona de volta nas configurações já ordenadas/limpas
                Properties.Settings.Default.VideoPaths.Add(path);
                Properties.Settings.Default.VideoTimes.Add(tempo);
                Properties.Settings.Default.VideoDatas.Add(data);
                Properties.Settings.Default.VideoDuracao.Add(duracao);
                Properties.Settings.Default.VideoThumbs.Add(thumb);

                lista.Add(new VideoInfo
                {
                    Caminho = path,
                    Tempo = long.Parse(tempo),
                    Duracao = long.Parse(duracao),
                    DataAtualizacao = DateTime.Parse(data, null, System.Globalization.DateTimeStyles.RoundtripKind),
                    CaminhoMiniatura = thumb
                });
            }

            Properties.Settings.Default.Save();

            // Organiza por data de atualização
            lista = lista.OrderByDescending(v => v.DataAtualizacao).ToList();

            ContinuarAssistindo.Controls.Clear();

            foreach (var info in lista)
            {
                Panel painel = CriarMiniatura(info);
                ContinuarAssistindo.Controls.Add(painel);
            }
        }

        private Panel CriarMiniatura(VideoInfo info)
        {
            Panel painel = new Panel
            {
                Tag = info,
                Size = new Size(200, 130),
                BackColor = Color.Black,
                Margin = new Padding(5)
            };

            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(200, 100),
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Top
            };

            string miniaturaPath = info.CaminhoMiniatura;

            if (!File.Exists(miniaturaPath) && File.Exists(info.Caminho))
            {
                miniaturaPath = CapturarMiniatura(info.Caminho);
                info.CaminhoMiniatura = miniaturaPath;
            }

            if (File.Exists(miniaturaPath))
            {
                using (var fs = new FileStream(miniaturaPath, FileMode.Open, FileAccess.Read))
                {
                    pictureBox.Image = Image.FromStream(fs);
                }
            }
            else
            {
                pictureBox.Image = Image.FromFile(Path.Combine(Application.StartupPath, "Arquivos", "PlayIcon.png"));
            }

            Label label = new Label
            {
                Text = Path.GetFileNameWithoutExtension(info.Caminho),
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(30, 30, 30)
            };

            painel.Controls.Add(pictureBox);
            painel.Controls.Add(label);

            painel.Click += (s, e) => SelecionarVideo(painel);
            pictureBox.Click += (s, e) => SelecionarVideo(painel);
            label.Click += (s, e) => SelecionarVideo(painel);

            return painel;
        }

        private void SelecionarVideo(Panel painel)
        {
            // Remove destaque anterior
            if (painelSelecionado != null)
            {
                painelSelecionado.BorderStyle = BorderStyle.None;
                painelSelecionado.Size = new Size(200, 100);
            }

            painelSelecionado = painel;

            // Destaca painel selecionado
            painelSelecionado.BorderStyle = BorderStyle.FixedSingle;
            painelSelecionado.Size = new Size(210, 110); // Ligeiramente maior para destaque

            // Atualiza informações na interface
            ContinuarAssistindo_SelectedIndexChanged(null, null);
        }

        private string CapturarMiniatura(string caminhoVideo)
        {
            try
            {
                using (var media = new Media(_libVLC, new Uri(caminhoVideo)))
                using (var mp = new MediaPlayer(media))
                {
                    string thumbPath = Path.Combine(pastaMiniaturas, Path.GetFileNameWithoutExtension(caminhoVideo) + ".png");

                    // Cria um contexto para renderizar a miniatura
                    var bitmap = new Bitmap(320, 180);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        mp.Hwnd = g.GetHdc();
                        mp.Play();
                        Thread.Sleep(500); // Espera o frame carregar
                        mp.Pause();
                        g.ReleaseHdc();
                    }

                    bitmap.Save(thumbPath);
                    return thumbPath;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
