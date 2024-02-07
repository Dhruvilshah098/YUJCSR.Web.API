using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using YUJCSR.API.Extensions;
using YUJCSR.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region "Connection string "
builder.Services.AddDbContext<YUJCSRContext>(a => a.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
#endregion
#region "Extension"
builder.Services.RegisterBusinessManager();
builder.Services.RegisterInfrastructure();
builder.Services.RegisterShared();
//builder.Services.AddScoped<TenantFilter>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
                          Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
    RequestPath = "/StaticFiles",
    EnableDefaultFiles = false
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
