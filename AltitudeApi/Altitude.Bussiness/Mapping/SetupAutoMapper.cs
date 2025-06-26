using Altitude.Bussiness.Models;
using Altitude.Bussiness.Models.Product;
using Altitude.Bussiness.Models.User;
using Altitude.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Mapping
{
    public class SetupAutoMapper : Profile
    {
        public SetupAutoMapper()
        {
            #region User Mapping

            CreateMap<UserRegisterModel, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UserProfileUpdateModel, User>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
                 .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                 .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<User, UserProfileResponseModel>();
            CreateMap<User, UserResponseModel>()
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.UserRole.ToString()))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.ProfileImageUrl ?? string.Empty));
            CreateMap<ProductCreateModel, Product>()
                .ForMember(dest => dest.ProductImageUrl, opt => opt.Ignore());
            CreateMap<Product, ProductResponseModel>();

            #endregion

        }
    }
}
