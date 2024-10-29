using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OpenHRCore.API.ServicesConfiguration
{
    public static class SwaggerGenService
    {
        public static IServiceCollection AddSwaggerGenService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(
                config =>
                {
                    config.SwaggerDoc(
                        "v1",
                        new OpenApiInfo { Title = "OpenHRCore API", Version = "v1" }
                    );
                    config.OperationFilter<AcceptLanguageHeaderFilter>();
                    config.AddSecurityDefinition(
                        "Bearer",
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description = "Please enter a valid token",
                            Name = "Authorization",
                            Type = SecuritySchemeType.Http,
                            BearerFormat = "JWT",
                            Scheme = "Bearer"
                        }
                    );
                    config.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] {  }
                            }
                        }
                    );
                }
            );

            return services;
        }

        private class AcceptLanguageHeaderFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    In = ParameterLocation.Header,
                    Name = "accept-language",
                    Description = "en-US, my-MM",
                    Schema = new OpenApiSchema
                    {
                        Type = "String"
                    }
                });
            }
        }
    }
}
