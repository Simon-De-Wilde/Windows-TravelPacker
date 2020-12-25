using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using TravelPackerAPI.Data;
using TravelPackerAPI.Data.Repositories;
using TravelPackerAPI.Models.RepositoryInterfaces;

namespace TravelPackerAPI {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddDbContext<TravelPackerDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
			);

			services.AddScoped<DataInitializer>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IItemRepository, ItemRepository>();
			services.AddScoped<IItineraryItemRepository, ItineraryItemRepository>();
			services.AddScoped<ITaskRepository, TaskRepository>();
			services.AddScoped<ITravelRepository, TravelRepository>();

			services.AddControllers().AddNewtonsoftJson();

			services.AddIdentity<IdentityUser, IdentityRole>(cfg =>
				cfg.User.RequireUniqueEmail = true
			).AddEntityFrameworkStores<TravelPackerDbContext>();

			services.Configure<IdentityOptions>(options => {
				// Password settings
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
			});

			services.AddAuthentication(x => {
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x => {
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters {
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(
					  Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
					ValidateIssuer = false,
					ValidateAudience = false,
					RequireExpirationTime = true //Ensure token hasn't expired
				};
			});

			services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()));

			services.AddSwaggerDocument(c => {
				c.DocumentName = "apidocs";
				c.Title = "TravelPacker API";
				c.Version = "v1";
				c.Description = "The TravelPacker API documentation";
				c.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme {
					Type = OpenApiSecuritySchemeType.ApiKey,
					Name = "Authorization",
					In = OpenApiSecurityApiKeyLocation.Header,
					Description = "Type into the textbox: Bearer {your JWT token}."
				});

				c.OperationProcessors.Add(
					new AspNetCoreOperationSecurityScopeProcessor("JWT")); //adds the token when a request is send
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataInitializer dataInitializer) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();
			app.UseAuthentication();

			app.UseOpenApi();
			app.UseSwaggerUi3();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});

			dataInitializer.InitializeData().Wait();
		}
	}
}
