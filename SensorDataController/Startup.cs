using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SensorData.DBLayer;
using SensorDataService.Authentication;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System;

namespace SensorDataController {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            //todo: we should pull this from secure pages or the config file.
            DBLayer pDBAccess = new DBLayer("SensorData", 5433, "SensorDBAdmin", "DBAdmin321!");
            List<DBUser> pUsers = pDBAccess.GetAllUsers() ?? new List<DBUser>(0);

            //I know this is ugly but I gotta finish the thing now. 
            //todo: Refactor this query to make it more efficient.
            List<DBFullUser> pFullUsers = pUsers.Select(p => new DBFullUser(p, pDBAccess.GetRolesForUser(p.UserID))).ToList();
            List<ServiceUser> pServiceUsers = new List<ServiceUser>(pFullUsers.Count);
            foreach (DBFullUser pUser in pFullUsers) {
                try {
                    List<UserRoles> pCastRoles = pUser.Roles.Select(q => Enum.Parse<UserRoles>(q.RoleName, true)).ToList();
                    pServiceUsers.Add(new ServiceUser(pUser.User.UserID, pUser.User.Login, pUser.User.Pass, pCastRoles));
                }
                catch(Exception e) {
                    //todo: Logging
                }                
            }

            services.AddSingleton<IUserService>(p => new UserAuthenticationService(pServiceUsers));            
            services.AddSingleton<IDBLayer>(p => pDBAccess);                       
            
            //todo: we should pull this from secure pages or the config file.
            //services.AddSingleton<IDBLayer>(p => new DBLayer("SensorData",5433,"SensorDBAdmin","DBAdmin321!"));
            
            
            services.AddControllers();

            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
