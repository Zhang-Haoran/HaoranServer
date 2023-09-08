using HaoranServer.Context;
using Microsoft.EntityFrameworkCore;

// 创建一个 WebApplicationBuilder 的实例
var builder = WebApplication.CreateBuilder(args);

// 添加 DB context 和 DB 连接; ConnectionString 在appsettings.json
builder.Services.AddDbContext<CommentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ReviewContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<TourContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 启用 API Explorer 的功能，使得 API 的数据可以被访问和暴露，Swagger所需。
builder.Services.AddEndpointsApiExplorer();
// 在NuGet install SwaggerGen 
builder.Services.AddSwaggerGen();
// 把新建的controller加入，否则swagger里无api显示
builder.Services.AddControllers()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); // avoid circular reference
// 添加AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// 设置跨域policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); ;
        });
});

// 实例化应用程序
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// MapControllers() 方法负责正确地将请求路由到相应的控制器方法 ，没有这行的话，execute swagger api 会404
app.MapControllers();

app.UseCors();

app.Run();
