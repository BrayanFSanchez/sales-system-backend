using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SalesSystem.DTO;
using SalesSystem.Model;

namespace SalesSystem.Utility
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            #region Role
            CreateMap<Role, RoleDTO>().ReverseMap();
            #endregion Role

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu

            #region User
            CreateMap<User, UserDTO>()
                .ForMember(destination =>
                    destination.RoleDescription,
                    opt => opt.MapFrom(origin => origin.IdRoleNavigation.Name)
                )
                .ForMember(destination =>
                destination.IsActive,
                opt => opt.MapFrom(origin => origin.IsActive == true ? 1 : 0)
                );


            CreateMap<User, SessionDTO>()
                .ForMember(destination =>
                    destination.RoleDescription,
                    opt => opt.MapFrom(origin => origin.IdRoleNavigation.Name)
                );

            CreateMap<UserDTO, User>()
                .ForMember(destination =>
                    destination.IdRoleNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destination =>
                destination.IsActive,
                opt => opt.MapFrom(origin => origin.IsActive == 1 ? true : false)
                );

            #endregion User

            #region Category
            CreateMap<Category, Category>().ReverseMap();
            #endregion Category

            #region Product
            CreateMap<Product, ProductDTO>()
                .ForMember(destination =>
                    destination.CategoryDescription,
                    opt => opt.MapFrom(origin => origin.IdCategorieNavigation.Name)
                )
                .ForMember(destination =>
                    destination.Price,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-HN")))
                )
                .ForMember(destination =>
                destination.IsActive,
                opt => opt.MapFrom(origin => origin.IsActive == true ? 1 : 0)
                );

            CreateMap<ProductDTO, Product>()
                .ForMember(destination =>
                    destination.IdCategorieNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destination =>
                    destination.Price,
                    opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Price, new CultureInfo("es-HN")))
                )
                .ForMember(destination =>
                destination.IsActive,
                opt => opt.MapFrom(origin => origin.IsActive == 1 ? true : false)
                );

            #endregion Product

            #region Sale
            CreateMap<Sale, SaleDTO>()
                .ForMember(destination =>
                    destination.TotalText,
                    opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Total.Value, new CultureInfo("es-HN")))
                )
                .ForMember(destination =>
                    destination.TotalText,
                    opt => opt.MapFrom(origin => origin.RegistrationDate.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<SaleDTO, Sale>()
                .ForMember(destination =>
                    destination.Total,
                    opt => opt.MapFrom(origin => Convert.ToDecimal(origin.TotalText, new CultureInfo("es-HN")))
                );

            #endregion Sale

            #region SaleDetail
            CreateMap<SaleDetail, SaleDetailDTO>()
                .ForMember(destination =>
                    destination.ProductDescription,
                    opt => opt.MapFrom(origin => origin.IdProductNavigation.Name)
                )
                .ForMember(destination =>
                    destination.PriceText,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-HN")))
                )
                .ForMember(destination =>
                    destination.TotalText,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-HN")))
                );

            CreateMap<SaleDetailDTO, SaleDetail>()
                .ForMember(destination =>
                    destination.Price,
                    opt => opt.MapFrom(origin => Convert.ToDecimal(origin.PriceText, new CultureInfo("es-HN")))
                )
                .ForMember(destination =>
                    destination.Total,
                    opt => opt.MapFrom(origin => Convert.ToDecimal(origin.TotalText, new CultureInfo("es-HN")))
                );


            #endregion SaleDetail

            #region Report
            CreateMap<SaleDetail, ReportDTO>()
                .ForMember(destination =>
                    destination.RegistrationDate,
                    opt => opt.MapFrom(origin => origin.IdProductNavigation.RegistrationDate.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destination =>
                    destination.DocumentNumber,
                    opt => opt.MapFrom(origin => origin.IdSaleNavigation.DocumentNumber)
                )
                .ForMember(destination =>
                    destination.PaymentType,
                    opt => opt.MapFrom(origin => origin.IdSaleNavigation.PaymentType)
                )
                .ForMember(destination =>
                    destination.TotalSales,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.IdSaleNavigation.Total.Value, new CultureInfo("es-HN")))
                )
                .ForMember(destination =>
                    destination.Product,
                    opt => opt.MapFrom(origin => origin.IdProductNavigation.Name)
                )
                .ForMember(destination =>
                    destination.Price,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-HN")))
                )
                .ForMember(destination =>
                    destination.Total,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-HN")))
                );

            #endregion Report
        }
    }
}
