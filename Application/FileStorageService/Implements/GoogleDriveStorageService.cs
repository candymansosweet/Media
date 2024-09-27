using Application.FileStorageService.Interfaces;
using Application.FileStorageService.Requests;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace Application.FileStorageService.FileStorageServices
{
    public class GoogleDriveStorageService : IFileStorageService
    {
        private readonly string[] Scopes = { DriveService.Scope.DriveFile };
        private readonly string _applicationName = "Media";
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
        public async Task DeleteFileAsync(string fileId)
        {
            // Tạo một instance của Google Drive API service
            var service = GetDriveService();
            try
            {
                // Tạo request để xóa file theo fileId
                var deleteRequest = service.Files.Delete(fileId);

                // Thực hiện request để xóa file
                await deleteRequest.ExecuteAsync();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu quá trình xóa gặp vấn đề
                throw new Exception($"Failed to delete file with ID {fileId}: {ex.Message}");
            }
        }
        public async Task<Stream> DownloadFileAsync(string fileId)
        {
            // Tạo một instance của Google Drive API service
            var service = GetDriveService();
            try
            {
                // Tạo request để lấy thông tin file
                var getRequest = service.Files.Get(fileId);
                // Tạo một stream để chứa dữ liệu của file
                var memoryStream = new MemoryStream();
                // Thực hiện request để tải file về và ghi vào memoryStream
                await getRequest.DownloadAsync(memoryStream);
                // Đặt vị trí của stream về đầu để có thể đọc dữ liệu từ đầu
                memoryStream.Position = 0;
                return memoryStream; // Trả về stream chứa nội dung file
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu quá trình tải file gặp vấn đề
                throw new Exception($"Failed to download file with ID {fileId}: {ex.Message}");
            }
        }
        public async Task<string> UploadFileAsync(FileUploadDto fileUploadDto)
        {
            // Tạo một instance của Google Drive API service
            var service = GetDriveService();

            // Tạo metadata cho file sẽ upload lên Google Drive
            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = fileUploadDto.fileName
            };
            // Sử dụng stream trực tiếp thay vì gọi OpenReadStream()
            var request = service.Files.Create(fileMetadata, fileUploadDto.fileStream, "application/octet-stream");


            // Chỉ định rằng chỉ lấy trường "id" từ kết quả phản hồi
            // Google Drive sẽ trả về thông tin về file sau khi upload, nhưng ở đây chỉ cần lấy ID của file.
            request.Fields = "id";

            // Thực hiện upload và chờ kết quả
            var fileResult = await request.UploadAsync();

            // Kiểm tra nếu quá trình upload thất bại
            if (fileResult.Status == Google.Apis.Upload.UploadStatus.Failed)
            {
                // Nếu thất bại, ném ra một exception với thông báo lỗi từ Google API
                throw new Exception($"File upload failed: {fileResult.Exception.Message}");
            }

            // Nếu thành công, trả về file ID từ Google Drive
            return request.ResponseBody.Id;
        }
    }

}
