// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using digitalsign.common.Enumeration;
using digitalsign.auth.Models;
using digitalsign.auth.Data;

namespace digitalsign.auth
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
        //    var services = new ServiceCollection();
        //    services.AddLogging();
        //    services.AddDbContext<ApplicationUserDbContext>(options =>
        //       options.UseSqlite(connectionString));

        //    services.AddIdentity<ApplicationUser, IdentityRole>()
        //        .AddEntityFrameworkStores<ApplicationUserDbContext>()
        //        .AddDefaultTokenProviders();

        //    using (var serviceProvider = services.BuildServiceProvider())
        //    {
        //        using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        //        {
        //            var context = scope.ServiceProvider.GetService<ApplicationUserDbContext>();
        //            context.Database.Migrate();

        //            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        //            var julie = userMgr.FindByNameAsync("julie").Result;
        //            if (julie == null)
        //            {
        //                julie = new ApplicationUser
        //                {
        //                    UserName = "julie",
        //                    FirstName = "Julie",
        //                    LastName = "Pharasyn",
        //                    Role = UserRole.User
        //                };
        //                var result = userMgr.CreateAsync(julie, "Pass123$").Result;
        //                if (!result.Succeeded)
        //                {
        //                    throw new Exception(result.Errors.First().Description);
        //                }

        //                result = userMgr.AddClaimsAsync(julie, new Claim[]{
        //                new Claim(JwtClaimTypes.Name, "Julie Pharasyn"),
        //                new Claim(JwtClaimTypes.GivenName, "Julie"),
        //                new Claim(JwtClaimTypes.FamilyName, "Pharasyn"),
        //                new Claim(JwtClaimTypes.Email, "julie.pharasyn@hotmail.com"),
        //                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
        //                new Claim(JwtClaimTypes.Role, UserRole.User.ToString(), ClaimValueTypes.Integer)
        //            }).Result;
        //                if (!result.Succeeded)
        //                {
        //                    throw new Exception(result.Errors.First().Description);
        //                }
        //                Log.Debug("julie created");
        //            }
        //            else
        //            {
        //                var temp = userMgr.DeleteAsync(julie).Result;
        //                julie = new ApplicationUser
        //                {
        //                    UserName = "julie",
        //                    FirstName = "Julie",
        //                    LastName = "Pharasyn",
        //                    Role = UserRole.User
        //                };
        //                var result = userMgr.CreateAsync(julie, "Pass123$").Result;
        //                if (!result.Succeeded)
        //                {
        //                    throw new Exception(result.Errors.First().Description);
        //                }

        //                result = userMgr.AddClaimsAsync(julie, new Claim[]{
        //                new Claim(JwtClaimTypes.Name, "Julie Pharasyn"),
        //                new Claim(JwtClaimTypes.GivenName, "Julie"),
        //                new Claim(JwtClaimTypes.FamilyName, "Pharasyn"),
        //                new Claim(JwtClaimTypes.Email, "julie.pharasyn@hotmail.com"),
        //                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
        //                new Claim(JwtClaimTypes.Role, UserRole.User.ToString(), ClaimValueTypes.Integer)
        //            }).Result;
        //            }

        //            var jochem = userMgr.FindByNameAsync("jochem").Result;
        //            if (jochem == null)
        //            {
        //                jochem = new ApplicationUser
        //                {
        //                    UserName = "jochem",
        //                    FirstName = "Jochem",
        //                    LastName = "Van Hespen",
        //                    Role = UserRole.User
        //                };
        //                var result = userMgr.CreateAsync(jochem, "Pass123$").Result;
        //                if (!result.Succeeded)
        //                {
        //                    throw new Exception(result.Errors.First().Description);
        //                }

        //                result = userMgr.AddClaimsAsync(jochem, new Claim[]{
        //                    new Claim(JwtClaimTypes.Name, "Jochem Van Hespen"),
        //                    new Claim(JwtClaimTypes.GivenName, "Jochem"),
        //                    new Claim(JwtClaimTypes.FamilyName, "Van Hespen"),
        //                    new Claim(JwtClaimTypes.Email, "jochemvanhespen@gmail.com"),
        //                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
        //                    new Claim(JwtClaimTypes.Role, UserRole.User.ToString(), ClaimValueTypes.Integer)
        //                }).Result;
        //                if (!result.Succeeded)
        //                {
        //                    throw new Exception(result.Errors.First().Description);
        //                }
        //                Log.Debug("jochem created");
        //            }
        //            else
        //            {
        //                var temp = userMgr.DeleteAsync(jochem).Result;
        //                jochem = new ApplicationUser
        //                {
        //                    UserName = "jochem",
        //                    FirstName = "Jochem",
        //                    LastName = "Van Hespen",
        //                    Role = UserRole.User
        //                };
        //                var result = userMgr.CreateAsync(jochem, "Pass123$").Result;
        //                if (!result.Succeeded)
        //                {
        //                    throw new Exception(result.Errors.First().Description);
        //                }

        //                result = userMgr.AddClaimsAsync(jochem, new Claim[]{
        //                    new Claim(JwtClaimTypes.Name, "Jochem Van Hespen"),
        //                    new Claim(JwtClaimTypes.GivenName, "Jochem"),
        //                    new Claim(JwtClaimTypes.FamilyName, "Van Hespen"),
        //                    new Claim(JwtClaimTypes.Email, "jochemvanhespen@gmail.com"),
        //                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
        //                    new Claim(JwtClaimTypes.Role, UserRole.User.ToString(), ClaimValueTypes.Integer)
        //                }).Result;
        //            }
        //            var admin = userMgr.FindByNameAsync("admin").Result;
        //            if (admin == null)
        //            {
        //                admin = new ApplicationUser
        //                {
        //                    UserName = "admin",
        //                    FirstName = "admin",
        //                    LastName = "admin",
        //                    Role = UserRole.Admin
                            
        //                };
        //                var result = userMgr.CreateAsync(admin, "Pass123$").Result;
        //                if (!result.Succeeded)
        //                {
        //                    throw new Exception(result.Errors.First().Description);
        //                }

        //                result = userMgr.AddClaimsAsync(admin, new Claim[]{
        //                new Claim(JwtClaimTypes.Name, "admin"),
        //                new Claim(JwtClaimTypes.GivenName, "admin"),
        //                new Claim(JwtClaimTypes.FamilyName, "admin"),
        //                new Claim(JwtClaimTypes.Email, "admin@digitalsign.be"),
        //                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
        //                new Claim(JwtClaimTypes.Role, UserRole.Admin.ToString(), ClaimValueTypes.Integer)
        //            }).Result;
        //                if (!result.Succeeded)
        //                {
        //                    throw new Exception(result.Errors.First().Description);
        //                }
        //                Log.Debug("admin created");
        //            }
        //            else
        //            {
        //                var temp = userMgr.DeleteAsync(admin).Result;
        //                admin = new ApplicationUser
        //                {
        //                    UserName = "admin",
        //                    FirstName = "admin",
        //                    LastName = "admin",
        //                };
        //                var result = userMgr.CreateAsync(admin, "Pass123$").Result;
        //                if (!result.Succeeded)
        //                {
        //                    throw new Exception(result.Errors.First().Description);
        //                }

        //                result = userMgr.AddClaimsAsync(admin, new Claim[]{
        //                new Claim(JwtClaimTypes.Name, "admin"),
        //                new Claim(JwtClaimTypes.GivenName, "admin"),
        //                new Claim(JwtClaimTypes.FamilyName, "admin"),
        //                new Claim(JwtClaimTypes.Email, "admin@digitalsign.be"),
        //                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
        //                new Claim(JwtClaimTypes.Role, UserRole.Admin.ToString(), ClaimValueTypes.Integer)
        //                }).Result;
        //                Log.Debug("admin already exists");
        //            }
        //            var system = userMgr.FindByNameAsync("system").Result;
        //            if (system == null)
        //            {
        //                system = new ApplicationUser
        //                {
        //                    UserName = "system",
        //                    FirstName = "system",
        //                    LastName = "system",
        //                    Role = UserRole.System
        //                };
        //                var result = userMgr.CreateAsync(system, "Pass123$").Result;
        //                if (!result.Succeeded)
        //                {
        //                    throw new Exception(result.Errors.First().Description);
        //                }

        //                result = userMgr.AddClaimsAsync(system, new Claim[]{
        //                new Claim(JwtClaimTypes.Name, "system"),
        //                new Claim(JwtClaimTypes.GivenName, "system"),
        //                new Claim(JwtClaimTypes.FamilyName, "system"),
        //                new Claim(JwtClaimTypes.Email, "system@digitalsign.be"),
        //                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
        //                new Claim(JwtClaimTypes.Role, UserRole.System.ToString(), ClaimValueTypes.Integer)
        //            }).Result;
        //                if (!result.Succeeded)
        //                {
        //                    throw new Exception(result.Errors.First().Description);
        //                }
        //                Log.Debug("system created");
        //            }
        //            else
        //            {
        //                var temp = userMgr.DeleteAsync(system).Result;
        //                system = new ApplicationUser
        //                {
        //                    UserName = "system",
        //                    FirstName = "system",
        //                    LastName = "system",
        //                    Role = UserRole.System
        //                };
        //                var result = userMgr.CreateAsync(system, "Pass123$").Result;
        //                if (!result.Succeeded)
        //                {
        //                    throw new Exception(result.Errors.First().Description);
        //                }

        //                result = userMgr.AddClaimsAsync(system, new Claim[]{
        //                new Claim(JwtClaimTypes.Name, "system"),
        //                new Claim(JwtClaimTypes.GivenName, "system"),
        //                new Claim(JwtClaimTypes.FamilyName, "system"),
        //                new Claim(JwtClaimTypes.Email, "system@digitalsign.be"),
        //                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
        //                new Claim(JwtClaimTypes.Role, UserRole.System.ToString(), ClaimValueTypes.Integer)
        //            }).Result;
        //            }
        //        }
        //    }
        }
    }
}
