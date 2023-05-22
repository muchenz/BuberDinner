using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.Events;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Menu;
public sealed class Menu : AggregateRoot2<MenuId, Guid>
{
    private readonly List<MenuSection> _sections = new();
    private readonly List<DinnerId> _dinnerIds = new();
    private readonly List<MenuReviewId> _menuReviewIds = new();
    public string Name { get; }
    public string Description { get; }
    public AverageRating AverageRating { get; } 

    public HostId HostId { get; }

    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();

    public DateTime CreatedDatetime { get; }
    public DateTime UpdatedDatetime { get; }

    private Menu(MenuId menuId, string name, string description, HostId hostId, List<MenuSection> menuSections,DateTime createdDatetime, DateTime updatedDatetime):base(menuId)
    {
        Name = name;
        Description = description;
        HostId = hostId;
        CreatedDatetime = createdDatetime;
        UpdatedDatetime = updatedDatetime;

        _sections.AddRange(menuSections);
        AverageRating = AverageRating.CreateNew();
    }

    public static Menu Create(string name, string description, HostId hostId, List<MenuSection> menuSections)
    {
        Menu menu = new(MenuId.CreateUnique(), name, description, hostId, menuSections ?? new(), DateTime.UtcNow, DateTime.UtcNow);

        menu.AddDomainEvent(new MenuCreated(menu));

        return menu;
    }

#pragma warning disable CS8618
    private Menu()
    {

    }
#pragma warning restore CS8618

}
