using Microsoft.AspNetCore.Mvc;
using System;
using VideoDownloader.Logic;



namespace VideoDownloader.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {

        }

        [HttpPost("AddToDownload")]
        public async Task<IActionResult> AddToDownload([FromBody] Request model)
        {
            var url = model.Url;
            var item = Globals.DownloadManager.AddToQueue(url);

            return new JsonResult(new { downloadId = item.Id });;
        }


        [HttpGet("state/{id}")]
        public IActionResult State(Guid id)
        {
            var item = Globals.DownloadManager.Items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return new JsonResult(new { state = "NotFound" });
            }
            if (item.State != DownloadItemState.Ready)
            {
                return new JsonResult(new { state = item.State.ToString() });
            }
            return new JsonResult(new { state = item.State.ToString(), title = item.Info.Title, size = item.Info.BiteSize });
        }

        [HttpGet("download/{id}")]
        public IActionResult Download(Guid id)
        {
            var item = Globals.DownloadManager.Items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return new JsonResult(new { error = true, message = "Файл не найден" });
            }
            if (item.State != DownloadItemState.Ready)
            {
                return new JsonResult(new { error = true, message = "Состояние не готово. Сейчас находится в " + item.State });
            }
            return File(System.IO.File.ReadAllBytes(item.FullPath), "video/mp4", item.Info.Title + ".mp4");
        }
    }
    public class Request
    { 
        public string Url { get; set; }
    }
}
