using Application;
using Infrastructure;
using Microsoft.Extensions.FileProviders;
using Prodcut.API;
using Web.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var a = builder.Environment.EnvironmentName;
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Đăng ký FileSettings từ appsettings.json
builder.Services.Configure<FileSettings>(builder.Configuration.GetSection("FileSettings"));

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddWebAPI();


var app = builder.Build();


#region config Static Files
FileSettings fileSettings = builder.Configuration.GetSection("FileSettings").Get<FileSettings>();
var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), fileSettings.UploadPath);
if (!Directory.Exists(uploadFolder))
{
    Directory.CreateDirectory(uploadFolder);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadFolder),
    RequestPath = "/" + fileSettings.UploadPath
});
#endregion

#region enviroment
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
