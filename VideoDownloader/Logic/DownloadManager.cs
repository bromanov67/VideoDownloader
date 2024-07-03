
using AngleSharp.Dom;

namespace VideoDownloader.Logic
{
    public class DownloadManager
    {
        public DownloadManager() 
        {
            Items = new List<DownloadItem>();
        }
        public async Task <Guid> AddToQueue(string url)
        {
            var id = Guid.NewGuid();
            var item = new DownloadItem
            {
                Id = id,
                State = State.Wait,
                Url = url,
                Name = id.ToString() + ".mp4",
            };
            Items.Add(item);

            return id;
        }

        internal void DownloadFromQueue()
        {
            var downloadItems = Items.FirstOrDefault(x => x.State == State.Wait);
            if (downloadItems != null)
            {
                downloadItems.State = State.InProcess;
                Downloader.ASD(downloadItems.Url, downloadItems.Name)
                    .GetAwaiter()
                    .GetResult();
                downloadItems.State = State.Ready;
            }
        }

        public List<DownloadItem> Items { get; set; }
        public class DownloadItem
        {
            public Guid Id { get; set; }
            public State State { get; set; }
            public string Url { get; set; }
            public string Name { get; set; }
        }

        public enum State
        {
            Wait = 0,
            InProcess = 1,
            Ready = 2,
        }
    }
}
