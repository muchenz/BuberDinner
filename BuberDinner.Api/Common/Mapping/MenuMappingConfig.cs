using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queriers.Login;
using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Contracts.Menus;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.MenuReview.ValueObjects;
using Mapster;
using static System.Collections.Specialized.BitVector32;

namespace BuberDinner.Api.Common.Mapping;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //config.NewConfig<RegisterRequest, RegisterCommand>();
        //config.NewConfig<LoginRequest, LoginQuery>();


        //config.NewConfig<AuthenticationResult, AuthenticationResponse>()
        //    .Map(dest => dest.Token, src => src.Token).Map(dest => dest, src => src.user).Map(dest => dest.Id, src => src.user.Id.Value);

        config.NewConfig<(CreateMenuRequest Request, string HostId), CreateMenuCommand>()
            .Map(dest => dest.HostId, src => src.HostId)
            .Map(dest => dest, src => src.Request);


        config.NewConfig<Menu, MenuResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.AverageRating, src =>  src.AverageRating.NumRatings > 0 ? (float?)src.AverageRating.Value : null)
            .Map(desc => desc.HostId, src => src.HostId.Value)
            .Map(desc => desc.DinnerIds, src => src.DinnerIds.Select(a => a.Value))
            .Map(desc => desc.MenuReviewIds, src => src.MenuReviewIds.Select(a => a.Value));

        config.NewConfig<MenuSection, MenuSectionResponse>()
           .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<MenuItem, MenuItemResponse>()
           .Map(dest => dest.Id, src => src.Id.Value);
    }
}



