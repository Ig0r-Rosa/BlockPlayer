namespace BlockPlayer.Classes
{
    public class VideoInfo
    {
        public string Caminho { get; set; }
        public long Tempo { get; set; }
        public long Duracao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string CaminhoMiniatura { get; set; }


        public string Nome => Path.GetFileName(Caminho);
    }
}