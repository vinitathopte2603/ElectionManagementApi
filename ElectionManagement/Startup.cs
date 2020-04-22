using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using RepositoryLayer.ModelContext;
using RepositoryLayer.Services;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = this.Configuration["Jwt:Issuer"],
                    ValidAudience = this.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Version = "v1", Title = "Fundoo Application", Description = "Swagger core API" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.DocumentFilter<SecurityRequirementDocumentFilter>();

            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<DBContext>(opts => opts.UseSqlServer(this.Configuration["ConnectionStrings:ElectionData"]));
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IAdminBusiness, AdminBusiness>();
            services.AddTransient<IConstituencyRepository , ConstituencyRepository>();
            services.AddTransient<IConstituencyBusiness, ConstituencyBusiness>();
            services.AddTransient<IPartyRepository, PartyRepository>();
            services.AddTransient<IPartyBusiness, PartyBusiness>();
            services.AddTransient<ICandidateRepository, CandidateRepository>();
            services.AddTransient<ICandidateBusiness, CandidateBusiness>();
            services.AddTransient<IVoterRepository, VoterRepository>();
            services.AddTransient<IVoterBusiness, VoterBusiness>();
            services.AddTransient<IElectionResultStatusRepository, ElectionResultStatusRepository>();
            services.AddTransient<IElectionResultStatusBusiness, ElectionResultStatusBusiness>();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:3000")
                    );
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(
             c =>
             {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");
             }
                );
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
    public class SecurityRequirementDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument document, DocumentFilterContext context)
        {
            document.Security = new List<IDictionary<string, IEnumerable<string>>>()
            {
                new Dictionary<string, IEnumerable<string>>()
                {
                    { "Bearer", new string[] { } },
                    { "Basic", new string[] { } },
                }
            };
        }
    }
}
