using Microsoft.AspNetCore.Authentication.Cookies;
using ToDo;
using ToDo.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
  options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
  {
    NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
  };
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
      options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
      options.SlidingExpiration = true;
      options.EventsType = typeof(CustomCookieAuthenticationEventsHandler);
      options.LoginPath = new PathString("/a/login");

      options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

builder.Services.AddScoped<CustomCookieAuthenticationEventsHandler>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.MinimumSameSitePolicy = SameSiteMode.Strict;
  options.Secure = CookieSecurePolicy.Always;
});

ConfigurationSingleton.Instance.ApplicationName = builder.Configuration["ApplicationName"];

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();