using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//This lines is for appsettings(first) type
//var logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .Enrich.FromLogContext()
//    .CreateLogger();

//This one is for Json(second) type.
var logger = new LoggerConfiguration() //this method creates a new LoggeerConfiguration object used to cofigure the Serilog logger.
    .ReadFrom.Configuration(new ConfigurationBuilder() //this method configures the logger based on the application's configuration.
                                                       //it uses the builder.Configuration object to read the configuration settings and apply them to the logger.                         
    .AddJsonFile("seri-log.config.json")   /* Configuration object that reads from the JSON file named "seri-log.config.json"
                                            * using AddJsonFile()method and then passes the object  to the ReadFrom.Configuration() method*/
    .Build()) //After Loading the Configuration file,it can be built into a Cofiguration object by calling Build().
    .Enrich.FromLogContext() //This allows log events to be enriched with additional informations such as current method
    .CreateLogger(); //This method is used to create a seriolog logger
builder.Logging.ClearProviders(); //It can be called on an instance of a ILoggerFactory in a .NET application to remove all the logging providers from the logging pipeline.
builder.Logging.AddSerilog(logger); //This method is used to add the serilog logger to the logging pipeline of the logging pipeline
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
