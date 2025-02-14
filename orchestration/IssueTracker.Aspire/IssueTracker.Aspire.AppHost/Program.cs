var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.IssueTracker_MVC>("Website");
builder.Build().Run();