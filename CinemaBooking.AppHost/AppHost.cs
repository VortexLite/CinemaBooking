var builder = DistributedApplication.CreateBuilder(args);

var server = builder.AddProject<Projects.ApiGateway>("server")
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints();

var webfrontend = builder.AddViteApp("webfrontend", "../frontend")
    .WithReference(server)
    .WaitFor(server);

server.PublishWithContainerFiles(webfrontend, "wwwroot");

builder.AddProject<Projects.Movie_Api>("movie-api");

builder.AddProject<Projects.Booking_Api>("booking-api");

builder.AddProject<Projects.Identity_Api>("identity-api");

builder.AddProject<Projects.Notification_Api>("notification-api");

builder.Build().Run();
