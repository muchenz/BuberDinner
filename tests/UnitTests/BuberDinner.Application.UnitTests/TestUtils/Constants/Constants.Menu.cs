using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.UnitTests.TestUtils.Constants;
public static partial class Constants
{
    public static class Menu
    {
        public const string Name = "Menu Name";
        public const string Description = "Menu Decription";
        public const string SectionName = "Menu Section Name";
        public const string SectionDescription = "Menu Section Description";
        public const string ItemName = "Menu Item Name";
        public const string ItemDescription = "Menu Item Description";
        public static string SectionNameFromIndex(int index) => $"{SectionName} {index}";
        public static string SectionDescriptionFromIndex(int index) => $"{SectionDescription} {index}";
        public static string ItemNameFromIndex(int index) => $"{ItemName} {index}";
        public static string ItemDescriptionFromIndex(int index) => $"{ItemDescription} {index}";
    }
}
