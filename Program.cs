using ReceiptProcessorAPI.Services;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
    WebRootPath = "wwwroot"
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ReceiptService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Ensure it listens on all interfaces (inside Docker)
app.Urls.Add("http://+:80");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

