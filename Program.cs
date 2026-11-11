using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddOData(options =>
    {
        options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100)
            .AddRouteComponents("odata", GetEdmModel(), new DefaultODataBatchHandler());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseODataBatching();
app.UseRouting();
app.MapControllers();

app.Run();

static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<DailyForecast>("DailyForecasts");
    return builder.GetEdmModel();
}


public class DailyForecast
{
    [Key]
    public int Id { get; set; }
    public int Temp { get; set; }
}

public class DailyForecastsController : ODataController
{
    [EnableQuery]
    public IActionResult Get()
    {
        var forecasts = new List<DailyForecast> { new DailyForecast { Id = 1, Temp = 25 } };
        return Ok(forecasts.AsQueryable());
    }
}