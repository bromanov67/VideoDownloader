using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using static System.Net.WebRequestMethods;



namespace VideoDownloader.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet("Download/{url}")]
        public async Task<IActionResult> GetById(string url)
        {
            url = "https://www.youtube.com/watch?v=GVQON-muEFc";

            await Downloader.ASD(url);

            return new JsonResult(new { url });
        }
    }
    public class Downloader
    {
        public static async Task ASD(string url)
        {

            var youtube = new YoutubeClient();

            // You can specify either the video URL or its ID
            var videoUrl = "https://www.youtube.com/watch?v=GVQON-muEFc";
            var video = await youtube.Videos.GetAsync(videoUrl);

            var title = video.Title;
            var author = video.Author.ChannelTitle;
            var duration = video.Duration; 

            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();


            await youtube.Videos.Streams.DownloadAsync(streamInfo, "cc_track.srt");
        }
           
    }

}
