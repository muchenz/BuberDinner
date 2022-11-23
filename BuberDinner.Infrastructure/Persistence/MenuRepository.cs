using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using BuberDinner.Domain.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure.Persistence;
public class MenuRepository : IMenuRepository
{
    static readonly List<Menu> _menu = new List<Menu>();

    public void Add(Menu menu)
    {
        _menu.Add(menu);
    }
}
