namespace VideoDownloader.Logic
{
    public class DownloadItem
    {
        public Guid Id { get; set; }
        public DownloadItemState State { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public VideoInfo Info { get; set; }
    }
}
