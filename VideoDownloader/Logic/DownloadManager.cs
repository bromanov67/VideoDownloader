
namespace VideoDownloader.Logic
{
    public partial class DownloadManager
    {
        public DownloadManager() 
        {
            Items = new List<DownloadItem>();
        }
        public DownloadItem AddToQueue(string url)
        {
            var id = Guid.NewGuid();
            var name = id.ToString() + ".mp4";
            var fullPath = Path.Combine(Globals.Settings.VideoFolderPath, name);

            var item = new DownloadItem
            {
                Id = id,
                State = DownloadItemState.Wait,
                Url = url,
                Name = name,
                FullPath = fullPath,
            };
            Items.Add(item);

            return item;
        }

        internal void DownloadFromQueue()
        {
            var downloadItems = Items.FirstOrDefault(x => x.State == DownloadItemState.Wait);
            if (downloadItems != null)
            {

                downloadItems.State = DownloadItemState.InProcess;
                var info = Downloader.Download(downloadItems.Url, downloadItems.Name)
                    .GetAwaiter()
                    .GetResult();
                downloadItems.State = DownloadItemState.Ready;
                downloadItems.Info = info;
            }
        }

        public List<DownloadItem> Items { get; set; }
    }
}
