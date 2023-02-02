using System.Security.Claims;
using FirebaseAdmin;
using FirebaseAdminAuthentification.DependencyInjection.Extensions;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authorization;
using WPFAndFirebaseAuthentification.Core.Responses;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(
    FirebaseApp.Create(new AppOptions() { Credential = GoogleCredential.FromJson(builder.Configuration.GetValue<string>("FIREBASE_CONFIG")) })
);
builder.Services.AddFirebaseAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", [Authorize] (ClaimsPrincipal principal) => {
    return Results.Json(new MessageResponse() {
        Message = "Firebase is cool"
    });
});

app.Run();