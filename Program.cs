using FoodSecurityMonitoring.Services;

var builder = WebApplication.CreateBuilder(args);

// Add configuration from appsettings.json.
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Force Kestrel to use a single port (e.g., 16225)
builder.WebHost.UseUrls("http://localhost:16225", "https://localhost:16225");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IFoodSecurityService, FoodSecurityService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:16225")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyCorsPolicy");
app.UseAuthorization();

// Map API controllers.
app.MapControllers();

// Serve static files from the React build.
// Ensure you have copied the build output into the wwwroot folder.
app.UseDefaultFiles();
app.UseStaticFiles();

// Fallback routing: any request not matching an API route will serve index.html.
app.MapFallbackToFile("index.html");

app.Run();