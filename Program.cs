using Microsoft.AspNetCore.Authentication.Cookies;
using ReferralManagementSystem.Models;
using ReferralManagementSystem.Repository;
using ReferralManagementSystem.Repository.IRepository;

namespace ReferralManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
//add session midleware here and set bellow
            builder.Services.AddSession();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5); // 5 minutes
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IRolesRepository, RolesRepository>();
            builder.Services.AddScoped<IReferralHospitalDetailRepository, ReferralHospitalDetailRepository>();
            builder.Services.AddScoped<IPatientReferralFormRepository, PatientReferralFormRepository>();

            var app = builder.Build();

            app.UseSession(); // Enable Session Middleware

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Doctor}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
