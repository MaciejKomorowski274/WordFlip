using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HelloWorld.Models
{
    public class MenuItem
    {
        public string Title { get; set; }
        public Type PageType { get; set; }
        public MenuItemType Type { get; set; }

        protected MenuItem()
        {
        }

        public MenuItem(MenuItemType menuType, string title, Type pageType) : this()
        {
            Type = menuType;
            Title = title;
            PageType = pageType;
        }
    }

    public enum MenuItemType
    {
        Sport,
        SUV,
        Sedan
    }
}
