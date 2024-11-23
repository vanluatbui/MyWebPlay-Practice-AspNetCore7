using Microsoft.AspNetCore.Hosting;
using MyWebPlay.Extension;
using MyWebPlay.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
builder.Services.AddTransient<IMailService,MailService>();

builder.Services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
builder.Services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
    cfg.Cookie.Name = "MyWebPlay";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
    cfg.IdleTimeout = new TimeSpan(24, 0, 0);    // Thời gian tồn tại của Session
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see http://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

var sao = System.IO.File.ReadAllText(Path.Combine(app.Environment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SecureSettingAdmin.txt")).Replace("\r", "").Split("\n");
var port = sao[21].Split("-");

builder.WebHost.UseUrls(port);

var phan = sao[20].Split("--");

var con = string.Format("@leftcontroller={0}@right/@leftaction={1}@right/@leftid?@right", phan[0], phan[1]);

//Unlock login admin
//var pam = Path.Combine(app.Environment.WebRootPath.Replace("\\wwwroot", ""), "PrivateFileAdmin", "Admin", "SettingAdminLoginConnect.txt");
//System.IO.File.WriteAllText(pam, string.Empty);

app.MapControllerRoute(
    name: "default",
    pattern: con.Replace("@left","{").Replace("@right", "}"));

BuildProgram.BuildProgramPlay(app.Environment);

app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
