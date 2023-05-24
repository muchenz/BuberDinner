using BuberDinner.Domain.Host.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.UnitTests.TestUtils.Constants;
public static partial class Constants
{
    public static class Host
    {
        public static readonly HostId Id = HostId.Create("HostId");
    }
}