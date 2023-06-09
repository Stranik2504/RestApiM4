using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestApiM4.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TodoList"));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Todo list api",
        Description = "Something",
        Contact = new OpenApiContact
        {
            Name = "Example url",
            Url = new Uri("https://example.com/contact")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();

// app.UseRouting();

app.MapGet("/index", async context =>
{
    var mainPage = await File.ReadAllTextAsync("wwwroot/index.html");
    await context.Response.WriteAsync(mainPage);
});

app.MapGet("/create", async context =>
{
    var mainPage = await File.ReadAllTextAsync("wwwroot/create.html");
    await context.Response.WriteAsync(mainPage);
});

app.MapRazorPages();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
