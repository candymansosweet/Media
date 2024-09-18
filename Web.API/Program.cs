using Application;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Prodcut.API;
using Web.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
// Đăng ký FileSettings từ appsettings.json
builder.Services.Configure<FileSettings>(builder.Configuration.GetSection("FileSettings"));

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddWebAPI();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();
// Cho phép phục vụ các tệp tĩnh từ thư mục "Uploads"
var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
if (!Directory.Exists(uploadFolder))
{
    Directory.CreateDirectory(uploadFolder);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadFolder),
    RequestPath = "/Uploads"
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
