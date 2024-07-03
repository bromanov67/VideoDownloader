using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace VideoDownloader.Logic
{
    public class Downloader
    {
        public static async Task ASD(string url, string name)
        {

            var youtube = new YoutubeClient();

            // You can specify either the video URL or its ID
            var videoUrl = "https://www.youtube.com/watch?v=Hqm9z6Riov0";
            var video = await youtube.Videos.GetAsync(videoUrl);

            var title = video.Title;
            var author = video.Author.ChannelTitle;
            var duration = video.Duration;

            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
            await youtube.Videos.Streams.DownloadAsync(streamInfo, name);

        }

    }
}
