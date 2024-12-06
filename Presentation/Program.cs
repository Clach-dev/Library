using Presentation.Common.Extensions;

BuilderExtensions
    .CreateBuilder(args)
    .ConfigureServices()
    .Build()
    .ConfigureMiddleware();