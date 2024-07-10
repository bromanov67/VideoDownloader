﻿using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace VideoDownloader.Logic
{
    public partial class Downloader
    {
        public static async Task<VideoInfo> Download(string url, string path)
        {
            var youtube = new YoutubeClient();

            var video = await youtube.Videos.GetAsync(url);
            var title = video.Title;
            var duration = video.Duration;
           
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(url);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
            await youtube.Videos.Streams.DownloadAsync(streamInfo, path);
            var biteSize = new FileInfo(path).Length;

            return new VideoInfo
            {
                Title = title,
                Duration = (int?)duration?.TotalSeconds,
                BiteSize = biteSize,
            };
        }

    }
}
