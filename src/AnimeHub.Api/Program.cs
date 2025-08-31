using AnimeHub.Api.Configurations;
using AnimeHub.Api.Middlewares;
using AnimeHub.Infra.Data.Context;
using AnimeHub.Infra.IoC;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Templates;
using Serilog.Templates.Themes;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Iniciando Aplicação");
    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddSerilog((services, lc) => lc
           .ReadFrom.Configuration(builder.Configuration)
           .ReadFrom.Services(services)
           .Enrich.FromLogContext()
           .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Hour)
           .WriteTo.Console(new ExpressionTemplate(
                "[{@t:HH:mm:ss} {@l:u3}{#if @tr is not null} ({substring(@tr,0,4)}:{substring(@sp,0,4)}){#end}] {@m}\n{@x}",
                theme: TemplateTheme.Code)));

    builder.Services.AddControllers();

    builder.Services
        .AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.Policies
                .Sunset(0.9)
                .Effective(DateTimeOffset.Now.AddDays(60))
                .Link("policy.html")
                    .Title("Versioning Policy")
                    .Type("text/html");
        })
        .AddMvc()
        .AddApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

    builder.Services.AddSwaggerGen();

    builder.Services.RegisterServices();

    builder.Services.AddDbContextConfiguration(builder.Configuration);

    builder.Services
        .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("AnimeHub.Application")));

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseAuthorization();

    app.UseSerilogRequestLogging();

    app.UseMiddleware<GlobalExceptionHandler>();

    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AnimeHubContext>();
        db.Database.Migrate();
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Inicialização da aplicação falhou");
}
finally
{
    Log.CloseAndFlush();
}