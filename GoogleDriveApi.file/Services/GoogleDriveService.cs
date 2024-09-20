using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

public class GoogleDriveService
{
    private readonly string[] _scopes = { DriveService.Scope.DriveFile };
    private readonly string _applicationName = "YourAppName";
    private readonly IConfiguration _configuration;

    public GoogleDriveService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DriveService GetDriveService()
    {
        UserCredential credential;
        var clientId = _configuration["GoogleDriveAPI:ClientId"];
        var clientSecret = _configuration["GoogleDriveAPI:ClientSecret"];

        using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
        {
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                _scopes,
                "user",
                CancellationToken.None,
                new FileDataStore("Drive.Auth.Store")
            ).Result;
        }

        // Tạo Drive API service
        var service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = _applicationName,
        });

        return service;
    }
    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var service = GetDriveService();

        var fileMetadata = new Google.Apis.Drive.v3.Data.File
        {
            Name = file.FileName
        };

        using (var stream = file.OpenReadStream())
        {
            var request = service.Files.Create(fileMetadata, stream, file.ContentType);
            request.Fields = "id";
            var fileResult = await request.UploadAsync();

            if (fileResult.Status == Google.Apis.Upload.UploadStatus.Failed)
            {
                throw new Exception($"File upload failed: {fileResult.Exception.Message}");
            }

            return request.ResponseBody.Id; // Trả về file ID từ Google Drive
        }
    }
}
