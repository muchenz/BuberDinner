using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Menu;
public sealed class Menu : AggregateRoot<MenuId>
{
    private readonly List<MenuSection> _sections = new();
    private readonly List<DinnerId> _dinnerIds = new();
    private readonly List<MenuReviewId> _menuReviewIds = new();
    public string Name { get; }
    public string Description { get; }
    public float AverageRating { get; }

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
    }

    public static Menu Create(string name, string description, HostId hostId, List<MenuSection> menuSections)
    {
        return new(MenuId.CreateUnique(), name, description, hostId, menuSections, DateTime.UtcNow, DateTime.UtcNow);
    }
}
