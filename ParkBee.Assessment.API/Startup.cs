using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MediatR;
using ParkBee.Assessment.Persistence;
using ParkBee.Assessment.API.Services;
using ParkBee.Assessment.Domain.Entities;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.Garages.Queries.GetGarageDetails;
using ParkBee.Assessment.Application.Services;

namespace ParkBee.Assessment.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecurityKey"]))
                    };
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ParkBee.Assessment"
                });
            });
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("parkbee"));
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddScoped<ILoggedInUserContext, LoggedInUserContext>();

            services.AddScoped<IPingService, PingService>();
            
            services.AddMediatR(typeof(GetGarageDetailsQuery).Assembly);
            services.AddHttpContextAccessor();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("MyPolicy");
            }

            Seed(context);

            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ParkBee.Assessment API V1"));
        }


        private void Seed(ApplicationDbContext context)
        {
            context.Garages.AddRange(new List<Garage>
            {
                new()
                {
                    Id = 1,
                    Name = "Tehran",
                    Address = "Airport"
                },
                new()
                {
                    Id = 2,
                    Name = "Maragheh",
                    Address = "Bus Station"
                },
            });
            context.Doors.AddRange(new List<Door>
            {
                new()
                {
                    Id = 1,
                    GarageId = 1,
                    Name = "Main Door",
                    IP = "127.0.0.1"
                },
                new()
                {
                    Id = 2,
                    GarageId = 2,
                    Name = "First Door",
                    IP = "4.2.2.4"
                },
                new()
                {
                    Id = 3,
                    GarageId = 2,
                    Name = "Second Door",
                    IP = "192.168.255.255"
                }
            });
            context.DoorStatusHistories.AddRange(
            new List<DoorStatusHistory>
            {
                new()
                {
                    Id = 1,
                    DoorId = 1,
                    IsOnline = true,
                    ChangeDate = DateTimeOffset.Now.AddHours(-2),
                },
                new()
                {
                    Id = 2,
                    DoorId = 2,
                    IsOnline = false,
                    ChangeDate = DateTimeOffset.Now.AddMinutes(-30),
                },
                new()
                {
                    Id = 3,
                    DoorId = 2,
                    IsOnline = true,
                    ChangeDate = DateTimeOffset.Now.AddHours(-6),
                },
                new()
                {
                    Id = 4,
                    DoorId = 3,
                    IsOnline = true,
                    ChangeDate = DateTimeOffset.Now.AddDays(-1),
                },
            });
            context.SaveChanges();
        }
    }
}