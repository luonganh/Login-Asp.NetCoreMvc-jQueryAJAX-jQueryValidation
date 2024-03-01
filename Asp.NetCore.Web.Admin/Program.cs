var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContexts(builder.Configuration);
builder.Services.AddIntegrationServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(AppSettings.CorsPolicy);

// Serilog
var path = Directory.GetCurrentDirectory();
var loggerFactory = app.Services.GetService<ILoggerFactory>();
loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

app.UseAuthorization();

// Exception handler middleware
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();
    var roleManager = scope.ServiceProvider.GetService(typeof(RoleManager<AppRole>)) as RoleManager<AppRole>;
    var userManager = (UserManager<AppUser>)scope.ServiceProvider.GetService(typeof(UserManager<AppUser>));
    await new DataSeeder(roleManager, userManager, context).SeedAsync();
}

app.Run();