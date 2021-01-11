using Hotel.Client.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Client.Pages
{
    public partial class Restaurant
    {
        [Inject] AppState AppState { get; set; }

        protected override void OnInitialized()
        {
            AppState.Flush();
        }

        List<BreakFast> breakFasts = new List<BreakFast>
        {
            new BreakFast 
            {
                Name="Cinnamon Roll", 
                Description="Served warm and loaded with cinnamon layered between buttery, flaky pastry dough that is drizzled with a delicious cream cheese icing",
                Image="/Images/Breakfast/img1.jpg"
            },
            new BreakFast 
            {
                Name="Baked Apple Pie", 
                Description="American-grown apples, with a lattice crust baked to perfection and topped with sprinkled sugar",
                Image="/Images/Breakfast/img2.jpg"
            },   
            new BreakFast 
            {
                Name="Egg Muffin", 
                Description="Bacon,Egg & Cheese Biscuit breakfast sandwich features a warm, buttermilk biscuit brushed with real butter, thick cut Applewood smoked bacon, a fluffy folded egg, and a slice of melty American cheese.",
                Image="/Images/Breakfast/img3.jpg"
            },
            new BreakFast 
            {
                Name="Sausage Muffin with Egg", 
                Description="Muffin with Egg features a savory hot sausage, a slice of melty American cheese, and a delicious freshly cracked egg all on a freshly toasted English muffin",
                Image="/Images/Breakfast/img4.jpg"
            },

        };

        List<Menu> Menus = new List<Menu>
        { 
            new Menu
            {
                Name="Quarter Pounder Bacon",
                Description=" Layered with two slices of melty American cheese, slivered onions and tangy pickles on a soft, fluffy sesame seed hamburger bun",
                Image="/Images/Menu/img1.jpg",
                Price= 89
            }, 
            new Menu
            {
                Name="Quarter Pounder Deluxe",
                Description="Cheese Deluxe is a fresh take on a Quarter Pounder® classic burger. Crisp leaf lettuce and three Roma tomato slices top",
                Image="/Images/Menu/img2.jpg",
                Price= 95
            }, 
            new Menu
            {
                Name="Double Cheeseburger",
                Description="Double Cheeseburger features two 100% pure beef burger patties seasoned with just a pinch of salt and pepper",
                Image="/Images/Menu/img3.jpg",
                Price= 45
            }, 
            new Menu
            {
                Name="Cheeseburger Combo Meal",
                Description="Cheeseburger Combo Meal is a simple, served with our World Famous Fries and your choice of a Medium soda or soft drink",
                Image="/Images/Menu/img3.jpg",
                Price= 75
            },

        };

        List<Beverages> Drinks = new List<Beverages>
        {
            new Beverages
            {
                Name="Coca-Cola",
                Image="Images/Beverage/img1.jpg",
                Price= 15
            }, 
            new Beverages
            {
                Name="Sprite",
                Image="Images/Beverage/img2.jpg",
                Price= 15
            },
            new Beverages
            {
                Name="Water",
                Image="Images/Beverage/img3.jpg",
                Price= 15
            },
            new Beverages
            {
                Name="Juice",
                Image="Images/Beverage/img4.jpg",
                Price= 15
            },
        };

        public class BreakFast
        {
            public string Name { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }
        }
        public class Menu
        {
            public string Name { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }
            public double Price { get; set; }
        }  
        public class Beverages
        {
            public string Name { get; set; }
            public string Image { get; set; }
            public double Price { get; set; }
        }   

    }
}
