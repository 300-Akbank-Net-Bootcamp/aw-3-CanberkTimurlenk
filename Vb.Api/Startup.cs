
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Business.Features.Reference;

namespace VbApi;

public class Startup
{
    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string connection = Configuration.GetConnectionString("MsSqlConnection");
        services.AddDbContext<VbDbContext>(options => options.UseSqlServer(connection));
        //services.AddDbContext<VbDbContext>(options => options.UseNpgsql(connection));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).GetTypeInfo().Assembly));

        //var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig()));
        services.AddAutoMapper(typeof(AssemblyReference).GetTypeInfo().Assembly);

        services.AddControllers().AddFluentValidation(x =>
            x.RegisterValidatorsFromAssemblyContaining<AssemblyReference>());

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(x => { x.MapControllers(); });
    }
}
