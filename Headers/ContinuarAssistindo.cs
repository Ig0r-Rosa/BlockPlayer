
using BlockPlayer.Classes;
using LibVLCSharp.Shared;

namespace BlockPlayer
{
    public partial class Janela : Form
    {
        private void CarregarContinuarAssistindo()
        {
            if (Propriedades.Settings.Default.VideoPaths == null)
            {
                Propriedades.Settings.Default.VideoPaths = new System.Collections.Specialized.StringCollection();
            }
            if (Propriedades.Settings.Default.VideoTimes == null)
            {
                Propriedades.Settings.Default.VideoTimes = new System.Collections.Specialized.StringCollection();
            }
            if (Propriedades.Settings.Default.VideoDatas == null)
            {
                Propriedades.Settings.Default.VideoDatas = new System.Collections.Specialized.StringCollection();
            }
            if (Propriedades.Settings.Default.VideoDuracao == null)
            {
                Propriedades.Settings.Default.VideoDuracao = new System.Collections.Specialized.StringCollection();
            }
            if (Propriedades.Settings.Default.VideoThumbs == null)
            {
                Propriedades.Settings.Default.VideoThumbs = new System.Collections.Specialized.StringCollection();
            }

            List<string> paths = Propriedades.Settings.Default.VideoPaths.Cast<string>().ToList();
            List<string> tempos = Propriedades.Settings.Default.VideoTimes.Cast<string>().ToList();
            List<string> datas = Propriedades.Settings.Default.VideoDatas.Cast<string>().ToList();
            List<string> duracoes = Propriedades.Settings.Default.VideoDuracao.Cast<string>().ToList();
            List<string> thumbs = Propriedades.Settings.Default.VideoThumbs.Cast<string>().ToList();

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
            Propriedades.Settings.Default.VideoPaths = new System.Collections.Specialized.StringCollection();
            Propriedades.Settings.Default.VideoTimes = new System.Collections.Specialized.StringCollection();
            Propriedades.Settings.Default.VideoDatas = new System.Collections.Specialized.StringCollection();
            Propriedades.Settings.Default.VideoDuracao = new System.Collections.Specialized.StringCollection();
            Propriedades.Settings.Default.VideoThumbs = new System.Collections.Specialized.StringCollection();

            for (int i = 0; i < paths.Count; i++)
            {
                string path = paths[i];
                string tempo = tempos[i];
                string data = datas[i];
                string duracao = duracoes[i];
                string thumb = thumbs[i];

                // Adiciona de volta nas configurações já ordenadas/limpas
                Propriedades.Settings.Default.VideoPaths.Add(path);
                Propriedades.Settings.Default.VideoTimes.Add(tempo);
                Propriedades.Settings.Default.VideoDatas.Add(data);
                Propriedades.Settings.Default.VideoDuracao.Add(duracao);
                Propriedades.Settings.Default.VideoThumbs.Add(thumb);

                lista.Add(new VideoInfo
                {
                    Caminho = path,
                    Tempo = long.Parse(tempo),
                    Duracao = long.Parse(duracao),
                    DataAtualizacao = DateTime.Parse(data, null, System.Globalization.DateTimeStyles.RoundtripKind),
                    CaminhoMiniatura = thumb
                });
            }

            Propriedades.Settings.Default.Save();

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

        private void SalvarContinuarAssistindo()
        {
            if (_mediaPlayer?.Media?.Mrl != null && _mediaPlayer.Length > 0)
            {
                string path = Uri.UnescapeDataString(_mediaPlayer.Media.Mrl.Replace("file:///", "").Replace("/", "\\"));
                long tempo = _mediaPlayer.Time;
                long duracao = _mediaPlayer.Length;
                DateTime agora = DateTime.UtcNow;

                var paths = Propriedades.Settings.Default.VideoPaths ?? new System.Collections.Specialized.StringCollection();
                var tempos = Propriedades.Settings.Default.VideoTimes ?? new System.Collections.Specialized.StringCollection();
                var datas = Propriedades.Settings.Default.VideoDatas ?? new System.Collections.Specialized.StringCollection();
                var duracoes = Propriedades.Settings.Default.VideoDuracao ?? new System.Collections.Specialized.StringCollection();
                var thumbs = Propriedades.Settings.Default.VideoThumbs ?? new System.Collections.Specialized.StringCollection();

                // Remove se já existe para regravar
                int existingIndex = paths.IndexOf(path);
                if (existingIndex >= 0)
                {
                    paths.RemoveAt(existingIndex);
                    tempos.RemoveAt(existingIndex);
                    datas.RemoveAt(existingIndex);
                    duracoes.RemoveAt(existingIndex);
                    thumbs.RemoveAt(existingIndex);
                }

                // Salvar miniatura como .jpg
                string nomeArquivoMiniatura = Path.GetFileNameWithoutExtension(path) + ".jpg";
                string caminhoMiniatura = Path.Combine(pastaMiniaturas, nomeArquivoMiniatura);

                // Tira snapshot atual
                bool snapshotSucesso = _mediaPlayer.TakeSnapshot(0, caminhoMiniatura, 320, 180);

                // Só salva se o snapshot funcionou
                if (snapshotSucesso)
                {
                    paths.Add(path);
                    tempos.Add(tempo.ToString());
                    datas.Add(agora.ToString("o"));
                    duracoes.Add(duracao.ToString());
                    thumbs.Add(caminhoMiniatura);
                }

                // Salva atualizações
                Propriedades.Settings.Default.VideoPaths = paths;
                Propriedades.Settings.Default.VideoTimes = tempos;
                Propriedades.Settings.Default.VideoDatas = datas;
                Propriedades.Settings.Default.VideoDuracao = duracoes;
                Propriedades.Settings.Default.VideoThumbs = thumbs;

                Propriedades.Settings.Default.Save();
            }
        }

        private void PlayContinuarAssistindo()
        {
            if (painelSelecionado == null)
            {
                MessageBox.Show("Selecione um vídeo para continuar assistindo.");
                CarregarContinuarAssistindo();
                return;
            }

            if (painelSelecionado.Tag is VideoInfo info && File.Exists(info.Caminho))
            {
                var media = new Media(_libVLC, info.Caminho, FromType.FromPath);
                _mediaPlayer.Play(media);
                _mediaPlayer.Time = info.Tempo;

                AtualizarVisibilidadeVideo(true);
            }
            else
            {
                MessageBox.Show("Arquivo não encontrado ou informação inválida.");
                CarregarContinuarAssistindo();
            }
        }

        private void SelecionarContinuarAssistindo()
        {
            if (painelSelecionado == null || !(painelSelecionado.Tag is VideoInfo info))
            {
                NomeVideoContinuarAssistindo.Visible = false;
                TempoVideoContinuarAssistindo.Visible = false;
                ProgressoVideoContinuarAssistindo.Visible = false;
                return;
            }

            NomeVideoContinuarAssistindo.Visible = true;
            TempoVideoContinuarAssistindo.Visible = true;
            ProgressoVideoContinuarAssistindo.Visible = true;

            // Usa o nome do vídeo vindo do VideoInfo
            NomeVideoContinuarAssistindo.Text = info.Nome;

            // Formata tempo atual e duração
            string tempoAtual = FormatarTempo(info.Tempo);
            string duracao = FormatarTempo(info.Duracao);
            TempoVideoContinuarAssistindo.Text = $"{tempoAtual} / {duracao}";

            // Calcula progresso como porcentagem
            if (info.Duracao > 0)
            {
                int progresso = (int)((info.Tempo * 100) / info.Duracao);
                ProgressoVideoContinuarAssistindo.Value = Math.Min(100, Math.Max(0, progresso));
            }
            else
            {
                ProgressoVideoContinuarAssistindo.Value = 0;
            }
        }

        private void ApagarContinuarAssistindoSelecionado()
        {
            if (painelSelecionado == null)
            {
                MessageBox.Show("Selecione um item para apagar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NomeVideoContinuarAssistindo.Visible = false;
            TempoVideoContinuarAssistindo.Visible = false;
            ProgressoVideoContinuarAssistindo.Visible = false;

            if (painelSelecionado.Tag is VideoInfo info)
            {
                var paths = Propriedades.Settings.Default.VideoPaths;
                var tempos = Propriedades.Settings.Default.VideoTimes;
                var datas = Propriedades.Settings.Default.VideoDatas;
                var duracoes = Propriedades.Settings.Default.VideoDuracao;

                if (paths != null && tempos != null && datas != null && duracoes != null)
                {
                    int index = paths.IndexOf(info.Caminho);

                    if (index >= 0)
                    {
                        paths.RemoveAt(index);
                        tempos.RemoveAt(index);
                        datas.RemoveAt(index);
                        duracoes.RemoveAt(index);
                        Propriedades.Settings.Default.Save();
                    }
                }

                ContinuarAssistindo.Controls.Remove(painelSelecionado);
            }

            painelSelecionado = null;
        }
    }
}