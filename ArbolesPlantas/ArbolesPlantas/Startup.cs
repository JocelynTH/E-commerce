using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ArbolesPlantas.Models;

[assembly: OwinStartupAttribute(typeof(ArbolesPlantas.Startup))]
namespace ArbolesPlantas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
			CreaRoles();
        }

		private void CreaRoles()
		{
			ApplicationDbContext applicationDbContext = new ApplicationDbContext();
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(applicationDbContext));

			var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(applicationDbContext));

			if (!roleManager.RoleExists("Admin"))
			{
				//Rol para el superadmin
				var role = new IdentityRole();
				role.Name = "Admin";
				roleManager.Create(role);

			}
			if (!roleManager.RoleExists("Compras"))
			{
				var role = new IdentityRole();
				role.Name = "Compras";
				roleManager.Create(role);

			}
			if (!roleManager.RoleExists("Ventas"))
			{
				var role = new IdentityRole();
				role.Name = "Ventas";
				roleManager.Create(role);

			}
			if (!roleManager.RoleExists("PagoProveedores"))
			{
				var role = new IdentityRole();
				role.Name = "PagoProveedores";
				roleManager.Create(role);

			}
			if (!roleManager.RoleExists("Finanzas"))
			{
				var role = new IdentityRole();
				role.Name = "Finanzas";
				roleManager.Create(role);
			}
			if (!roleManager.RoleExists("AlmacenE"))
			{
				var role = new IdentityRole();
				role.Name = "AlmacenE";
				roleManager.Create(role);
			}
		}
	}


}
