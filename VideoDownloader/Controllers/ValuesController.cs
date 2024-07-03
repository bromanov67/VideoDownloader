using Microsoft.AspNetCore.Mvc;
using VideoDownloader.Logic;



namespace VideoDownloader.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpPost("AddToDownload")]
        public async Task<IActionResult> AddToDownload([FromBody] Request model)
        {
            /*var url = "https://www.youtube.com/watch?v=GVQON-muEFc";*/
            var url = model.Url;
            var id = Globals.DownloadManager.AddToQueue(url);

            return new JsonResult(new { downloadId = id });;
        }
    }
    public class Request
    { 
        public string Url { get; set; }
    }
}
