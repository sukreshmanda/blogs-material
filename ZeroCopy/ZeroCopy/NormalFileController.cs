using Microsoft.AspNetCore.Mvc;

namespace ZeroCopy;

[ApiController]
[Route("api/download/file")]
public class NormalFileController : ControllerBase
{
    
    [HttpGet("normal")]
    public IActionResult DownloadFile()
    {
        var filePath = Path.Combine("Files", "fileToTransfer.txt");
        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        var fileStream = System.IO.File.OpenRead(filePath);
        return File(fileStream, "application/octet-stream", "fileToTransfer.txt");
    }
    
    [HttpGet("improved")]
    public async Task<IActionResult> DownloadWithSendFile()
    {
        var filePath = Path.Combine("Files", "fileToTransfer.txt");
        
        var fileInfo = new FileInfo(filePath);

        Response.ContentType = "application/octet-stream";
        Response.ContentLength = fileInfo.Length;
        Response.Headers.ContentDisposition = $"attachment; filename=\"fileToTransfer.txt\"";
        Response.Headers.AcceptRanges = "bytes";

        // This uses sendfile syscall (Linux) or TransmitFile (Windows)
        await Response.SendFileAsync(filePath, 0, fileInfo.Length);
        
        return new EmptyResult();
    }
}