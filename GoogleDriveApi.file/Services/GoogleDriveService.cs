using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

public interface IGoogleDriveService
{
    public DriveService GetDriveService();
    public Task<string> UploadFileAsync(IFormFile file);
}
public class GoogleDriveService
{
    private readonly string[] Scopes = { DriveService.Scope.DriveFile };
    private readonly string _applicationName = "YourAppName";
    private readonly IConfiguration _configuration;

    public GoogleDriveService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    private UserCredential Login(string ggClinetId, string ggClinetSecret)
    {
        ClientSecrets clientSecrets = new ClientSecrets()
        {
            ClientId = ggClinetId,
            ClientSecret = ggClinetSecret
        };
        return GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets,
            scopes: Scopes,
            user: "user",
            CancellationToken.None).Result;
    }
    public DriveService GetDriveService()
    {
        UserCredential credential;

        using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read)) // đọc thông tin trong credentials.json
        {
            string credPath = "token.json"; // Nơi lưu trữ Access và Refresh Token

            var secretsInfor = GoogleClientSecrets.FromStream(stream).Secrets;


            credential = Login(secretsInfor.ClientId, secretsInfor.ClientSecret);
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
