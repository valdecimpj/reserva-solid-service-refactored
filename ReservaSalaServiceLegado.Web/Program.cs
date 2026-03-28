using Microsoft.AspNetCore.Mvc;
using ReservaSalaServiceLegado.Core.Repository;
using ReservaSalaServiceLegado.Core.Service;
using ReservaSalaServiceLegado.Core.UseCases.RentRoom;
using ReservaSalaServiceLegado.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<RentRoomHandler>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPaymentValidatorService, PaymentValidatorService>();
builder.Services.AddScoped<IReciepeService, ReciepeService>();
builder.Services.AddScoped<IRoomRentalInformationValidator, RoomRentalInformationValidator>();
builder.Services.AddScoped<IRoomRentalValueCalculator, RoomRentalValueCalculator>();
builder.Services.AddSingleton<IRoomRentalRepository, RoomRentalRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/rent-room", async ([FromServices] RentRoomHandler handler, [FromBody] RentRoomRequest request) =>
{
    var message = await handler.Handle(request);
    return Results.Json(new {Message = message});
})
.WithName("RentRoom");

app.Run();
