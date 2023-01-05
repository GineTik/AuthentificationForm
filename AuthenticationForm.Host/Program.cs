using Microsoft.OpenApi.Models;

namespace AuthenticationForm.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    // configure behavior ApiController attribute
                    options.SuppressInferBindingSourcesForParameters = false;
                    options.SuppressModelStateInvalidFilter = false;
                    // options.InvalidModelStateResponseFactory = context => {}
                });

            //builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Authentification", 
                    Version = "v1",
                    Description = "Api description"
                });
            });

            builder.Services.AddAuthentication()
                .AddCookie(options =>
                {
                    options.LoginPath = "/Authentification/Login";
                    //options.AccessDeniedPath = "/auth/forbidden";
                });

            // configure https redirection
            builder.Services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 7037;
            });
            
            var app = builder.Build();

            // app.UseHsts(); // don't working by localhost host
            // can be changed if go by path C:\Windows\System32\drivers\etc\hosts(open as .txt) and add mappings!
            // before use, configure with AddHsts(options => ...)(services) and set MaxAge preferably no more than a day

            app.UseHttpsRedirection();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();

                app.UseSwagger(options =>
                {
                    options.RouteTemplate = "swagger/{documentName}/swagger.{json|yaml}"; // default value
                });
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}