//using Microsoft.EntityFrameworkCore;
//using SalesSystem.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesSystem.DAL.DBContext;
using SalesSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SalesSystem.DAL.Repositories.Contract;
using SalesSystem.DAL.Repositories;

using SalesSystem.Utility;
using SalesSystem.BLL.Services.Contract;
using SalesSystem.BLL.Services;

namespace SalesSystem.IOC
{
    public static class Dependency
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<DbsaleContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("stringSQL"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ISaleRepository, SaleRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();
        }
    }
}
