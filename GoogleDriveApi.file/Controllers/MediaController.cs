using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GoogleDriveApi.file.Controllers
{
    public class MediaController : ControllerBase
    {

        private readonly GoogleDriveService _googleDriveService;

        public MediaController(GoogleDriveService googleDriveService)
        {
            _googleDriveService = googleDriveService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file selected for upload.");
            }

            try
            {
                var fileId = await _googleDriveService.UploadFileAsync(file);
                return Ok(new { FileId = fileId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
