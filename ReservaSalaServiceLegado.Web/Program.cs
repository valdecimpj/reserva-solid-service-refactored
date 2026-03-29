using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ReservaSalaServiceLegado.Core.Repository;
using ReservaSalaServiceLegado.Core.Service;
using ReservaSalaServiceLegado.Core.Service.RoomRentalInformationValidator;
using ReservaSalaServiceLegado.Core.Service.RoomRentalValueCalculator;
using ReservaSalaServiceLegado.Core.UseCases.RentRoom;
using ReservaSalaServiceLegado.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddTransient<RentRoomHandler>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IPaymentValidatorService, PaymentValidatorService>();
builder.Services.AddTransient<IReciepeService, ReciepeService>();
builder.Services.AddTransient<IRoomRentalInformationValidatorService, RoomRentalInformationValidatorService>();
builder.Services.AddTransient<IRoomRentalValueCalculatorService, RoomRentalValueCalculatorService>();
builder.Services.AddSingleton<IRoomRentalRepository, RoomRentalRepository>();
builder.Services.AddScoped<EventLoggingService>();
builder.Services.AddScoped<IEventLoggingService>(serviceProvider => serviceProvider.GetRequiredService<EventLoggingService>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = string.Empty;
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Rent Room API V1");
    });
}

app.MapPost(
        "/v1/rent-room",
        async (
            [FromServices] RentRoomHandler handler,
            [FromServices] EventLoggingService eventLoggingService,
            [FromBody] RentRoomRequest request
        ) =>
        {
            var response = await handler.Handle(request);

            if (response.Message.ToLower().Contains("invalid"))
                return Results.BadRequest(
                    new { Response = response, Logs = eventLoggingService.GetLoggedEvents() }
                );

            return Results.Json(
                new { Response = response, Logs = eventLoggingService.GetLoggedEvents() }
            );
        }
    )
    .WithName("RentRoom");

app.Run();
