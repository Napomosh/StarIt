var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<StarIt.Dal.AuthDal.IAuthDal, StarIt.Dal.AuthDal.AuthDal>();
builder.Services.AddSingleton<StarIt.Bl.IEncrypt, StarIt.Bl.Encrypt>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<StarIt.Bl.Auth.IAuth, StarIt.Bl.Auth.Auth>();
builder.Services.AddScoped<StarIt.Bl.ICurrentUser, StarIt.Bl.CurrentUser>();


builder.Services.AddMvc();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();