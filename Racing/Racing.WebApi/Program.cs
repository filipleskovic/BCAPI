using Autofac;
using Autofac.Extensions.DependencyInjection;
using Racing.Service;
using Racing.Models;
using Racing.Repository;
using Microsoft.Extensions.Hosting;
using Service.Common;
using Repository.Common;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


// Add services to the container.
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.Register(context =>
    {
        var config = context.Resolve<IConfiguration>();
        var connectionString = config.GetConnectionString("DefaultConnection");
        return new FormulaRepository(connectionString);
    }).As<IFormulaRepository>().InstancePerLifetimeScope();
    containerBuilder.Register(context =>
    {
        var config = context.Resolve<IConfiguration>();
        var connectionString = config.GetConnectionString("DefaultConnection");
        return new DriverRepository(connectionString);
    }).As<IRepository<Driver>>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<FormulaService>().As<IFormulaService>();
    containerBuilder.RegisterType<DriverService>().As<IService<Driver>>();


});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
