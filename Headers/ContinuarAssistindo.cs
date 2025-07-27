
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

            pastaMiniaturas = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "BlockPlayer", "Thumbs");
            if (!Directory.Exists(pastaMiniaturas))
            {
                Directory.CreateDirectory(pastaMiniaturas);
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
                    string thumbPath = thumbs[i];
                    if (!string.IsNullOrEmpty(thumbPath) && File.Exists(thumbPath))
                    {
                        try
                        {
                            File.Delete(thumbPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro ao excluir miniatura: {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
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
                Size = new Size(200, 140),
                BackColor = Color.Black,
                Margin = new Padding(5)
            };

            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(200, 100),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(0, 10),
            };

            Label label = new Label
            {
                Text = Path.GetFileNameWithoutExtension(info.Caminho),
                Size = new Size(200, 30),
                Location = new Point(0, 110),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
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

            painel.Controls.Add(pictureBox);
            painel.Controls.Add(label);

            // Clique em qualquer parte do painel
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
                painelSelecionado.BackColor = Color.Black;
            }

            painelSelecionado = painel;

            // Destaca o painel com borda e leve padding
            painelSelecionado.BorderStyle = BorderStyle.FixedSingle;
            painelSelecionado.BackColor = Color.FromArgb(125, 125, 125);

            ContinuarAssistindo_SelectedIndexChanged(null, null);
        }

        private string CapturarMiniatura(string caminhoImagem)
        {
            if (_mediaPlayer == null || !_mediaPlayer.IsPlaying)
                return "";

            try
            {
                uint largura = 320;
                uint altura = 180;

                bool sucesso = _mediaPlayer.TakeSnapshot(0, caminhoImagem, largura, altura);

                if (!sucesso)
                {
                    MessageBox.Show("Falha ao capturar miniatura.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                Task.Delay(50);

                return caminhoImagem;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao capturar miniatura:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
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

                // Nome do snapshot
                string nomeArquivoMiniatura = Path.GetFileNameWithoutExtension(path) + ".jpg";
                string caminhoMiniatura = Path.Combine(pastaMiniaturas, nomeArquivoMiniatura);

                // ➕ Verifica se está pausado e faz play temporário
                bool estavaPausado = !_mediaPlayer.IsPlaying;
                if (estavaPausado)
                {
                    _mediaPlayer.Play();
                    Thread.Sleep(50);
                }

                bool snapshotSucesso = _mediaPlayer.TakeSnapshot(0, caminhoMiniatura, 320, 180);

                if (estavaPausado)
                {
                    _mediaPlayer.Pause();
                }

                // Só salva se snapshot funcionou
                if (snapshotSucesso)
                {
                    paths.Add(path);
                    tempos.Add(tempo.ToString());
                    datas.Add(agora.ToString("o"));
                    duracoes.Add(duracao.ToString());
                    thumbs.Add(caminhoMiniatura);
                }

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
                MessageBox.Show("Selecione um vídeo para continuar assistindo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Arquivo não encontrado ou informação inválida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            AtualizarBarraTempoContinuarAssistindo();
        }

        private void AtualizarBarraTempoContinuarAssistindo()
        {
            if (painelSelecionado == null || !(painelSelecionado.Tag is VideoInfo info))
                return;

            float progresso = (info.Duracao > 0) ? (info.Tempo / (float)info.Duracao) * 100f : 0f;

            ProgressoVideoContinuarAssistindo.Paint -= ProgressoVideoContinuarAssistindo_Paint;
            ProgressoVideoContinuarAssistindo.Paint += ProgressoVideoContinuarAssistindo_Paint;

            // Força o redraw
            ProgressoVideoContinuarAssistindo.Invalidate();
        }

        private void ProgressoVideoContinuarAssistindo_Paint(object sender, PaintEventArgs e)
        {
            if (painelSelecionado == null || !(painelSelecionado.Tag is VideoInfo info))
                return;

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int barraAltura = ProgressoVideoContinuarAssistindo.Height;
            int barraLargura = ProgressoVideoContinuarAssistindo.Width;

            float progresso = (info.Duracao > 0) ? (info.Tempo / (float)info.Duracao) : 0f;

            // Fundo
            using (var fundo = new SolidBrush(Color.FromArgb(40, 40, 40)))
                g.FillRectangle(fundo, 0, 0, barraLargura, barraAltura);

            // Barra de progresso (DeepSkyBlue)
            int larguraProgresso = (int)(barraLargura * progresso);
            using (var progressoBrush = new SolidBrush(Color.DeepSkyBlue))
                g.FillRectangle(progressoBrush, 0, 0, larguraProgresso, barraAltura);

            // Círculo branco indicador
            int tamanhoCirculo = (int)(barraAltura * 0.95f);
            int offsetY = (barraAltura - tamanhoCirculo) / 2;
            int centroX = larguraProgresso;

            // Garante que o círculo não ultrapasse os limites
            centroX = Math.Max(centroX, tamanhoCirculo / 2);
            centroX = Math.Min(centroX, barraLargura - tamanhoCirculo / 2);

            using (var circuloBrush = new SolidBrush(Color.White))
                g.FillEllipse(circuloBrush, centroX - (tamanhoCirculo / 2), offsetY, tamanhoCirculo, tamanhoCirculo);
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
                var thumbs = Propriedades.Settings.Default.VideoThumbs;

                if (paths != null && tempos != null && datas != null && duracoes != null && thumbs != null)
                {
                    int index = paths.IndexOf(info.Caminho);

                    if (index >= 0)
                    {
                        // Exclui a miniatura fisicamente, se existir
                        string thumbPath = thumbs[index];
                        if (!string.IsNullOrEmpty(thumbPath) && File.Exists(thumbPath))
                        {
                            try
                            {
                                File.Delete(thumbPath);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Erro ao excluir miniatura: {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        // Remove dados salvos
                        paths.RemoveAt(index);
                        tempos.RemoveAt(index);
                        datas.RemoveAt(index);
                        duracoes.RemoveAt(index);
                        thumbs.RemoveAt(index);

                        Propriedades.Settings.Default.Save();
                    }
                }

                ContinuarAssistindo.Controls.Remove(painelSelecionado);
            }

            painelSelecionado = null;
        }
    }
}