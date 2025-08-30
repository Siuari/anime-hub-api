using AnimeHub.Api.Configurations;
using AnimeHub.Api.Middlewares;
using AnimeHub.Infra.IoC;
using Asp.Versioning;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("starting server.");
    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddSerilog((services, lc) => lc
           .ReadFrom.Configuration(builder.Configuration)
           .ReadFrom.Services(services)
           .Enrich.FromLogContext()
           .WriteTo.Console());

    builder.Services.AddControllers();

    //builder.Services
    //    .AddApiVersioning(options =>
    //    {
    //        options.AssumeDefaultVersionWhenUnspecified = true;
    //        options.DefaultApiVersion = new ApiVersion(1, 0);
    //        options.ReportApiVersions = true;
    //    })
    //    .AddMvc();

    builder.Services
        .AddApiVersioning(                    options =>
        {
            // reporting api versions will return the headers
            // "api-supported-versions" and "api-deprecated-versions"
            options.ReportApiVersions = true;

            options.Policies.Sunset(0.9)
                            .Effective(DateTimeOffset.Now.AddDays(60))
                            .Link("policy.html")
                                .Title("Versioning Policy")
                                .Type("text/html");
        })
        .AddMvc()
        .AddApiExplorer(
            options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });

    //builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.RegisterServices();

    builder.Services.AddDbContextConfiguration(builder.Configuration);

    builder.Services
        .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("AnimeHub.Application")));

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseSerilogRequestLogging();

    app.UseMiddleware<GlobalExceptionHandler>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}