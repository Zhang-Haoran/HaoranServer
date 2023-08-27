using HaoranServer.Context;
using Microsoft.EntityFrameworkCore;

// 创建一个 WebApplicationBuilder 的实例
var builder = WebApplication.CreateBuilder(args);

// 添加 DB context 和 DB 连接; ConnectionString 在appsettings.json
builder.Services.AddDbContext<CommentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 启用 API Explorer 的功能，使得 API 的数据可以被访问和暴露，Swagger所需。
builder.Services.AddEndpointsApiExplorer();
// 在NuGet install SwaggerGen 
builder.Services.AddSwaggerGen();
// 把新建的controller加入，否则swagger里无api显示
builder.Services.AddControllers();

// 实例化应用程序
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
