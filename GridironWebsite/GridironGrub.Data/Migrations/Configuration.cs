namespace GridironGrub.Data.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<GridironGrub.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GridironGrub.Data.ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //// Create Admin Role
            //string[] roles = new string[5] { "Customer", "Admin", "Manager", "Vendor", "Runner" };
            //IdentityResult roleResult;

            //// Check to see if Roles Exist, if not create them
            //foreach (string role in roles)
            //{
            //    if (!roleManager.RoleExists(role))
            //    {
            //        roleResult = roleManager.Create(new IdentityRole(role));
            //    }
            //}

            //context.SaveChanges();

            //create a Park
            Park park = new Park
            {
                Name = "Minute Maid Park",
                TaxRate = 0.0825m,
                LogoUrl = "http://teamservicesllc.com/wp-content/uploads/2015/02/mmpark.gif"
            };

            context.Parks.AddOrUpdate(p => p.Name, park);
            context.SaveChanges();

            //create Areas
            Area[] areas = new Area[]
            {
                new Area {ParkId = park.Id, Name = "Bullpen Boxes"},
                new Area {ParkId = park.Id, Name = "Mezzanine"},
                new Area {ParkId = park.Id, Name = "Crawford Boxes" }
            };

            context.Areas.AddOrUpdate(a => a.Name, areas);
            context.SaveChanges();

            //create Restaurants
            Restaurant[] restaurants = new Restaurant[]
            {
                new Restaurant {AreaId = areas[0].Id, Name = "Premium Wines", Description = "Select Varietals are available by the glass.", ImageUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcQMplSeNZUmbSobvY-k9roZ0VxMtB-GeBesoyYir6_h5PYMnMOP"},
                new Restaurant {AreaId = areas[0].Id, Name = "Value Meals", Description = "Ballpark favorites paired with a Coke at a great price!", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ3ZQ-y3UWF5Jy_ilX_oqyun82sM-dXBUJOge5JWzOawL1apL-Y"},
                new Restaurant {AreaId = areas[0].Id, Name = "Astros Sizzling Extreme Grill", Description = "Offering specialty hot dogs and sausages: Featuring the Ken Hoffman New York Style dog. Build your own unique creation choosing from a large selection of toppings.", ImageUrl = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcSCqbRDx78JC5Hrvs4Fq1MrupwYTyUakYrdDV4a18sX_kH8_JVY"},
                new Restaurant {AreaId = areas[1].Id, Name = "Union Station Favorites", Description = "Traditional ballpark fare including Foot-Long and Regular Hot Dogs, Sausages, Pretzels, Peanuts, and Popcorn", ImageUrl = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQZbeQrHh1aT6XdANuHdU2mwcN4v5IbjPg47__VXa_whgMXVIwV"},
                new Restaurant {AreaId = areas[2].Id, Name = "St. Arnold's Bar", Description = "Destination Bar with a beer garden feel featuring seasonal favorites from Houston's St. Arnolds Brewery.", ImageUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcTcH-_YUrCN5a90jb5cEfmIH6ZV1UXPY43D-Vdvum5qn-Gfj22k"},
                new Restaurant {AreaId = areas[2].Id, Name = "Hand Made Funnel Cakes", Description = "Funnel cakes made fresh as you watch.", ImageUrl = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcRZ6f3Ovi5a5c--aFaTvE0V56SPyseZ8L4JvOu4nWj2eb5F_0781Q" }
            };

            context.Restaurants.AddOrUpdate(r => r.Name, restaurants);
            context.SaveChanges();

            //create Categories
            Category[] categories = new Category[]
            {
                new Category { RestaurantId = restaurants[0].Id, Name = "Wines" },
                new Category { RestaurantId = restaurants[0].Id, Name = "Champagne" },
                new Category { RestaurantId = restaurants[1].Id, Name = "Food" },
                new Category { RestaurantId = restaurants[1].Id, Name = "Drinks" },
                new Category { RestaurantId = restaurants[2].Id, Name = "On A Bun" },
                new Category { RestaurantId = restaurants[2].Id, Name = "In the Nude" },
                new Category { RestaurantId = restaurants[3].Id, Name = "Snackage" },
                new Category { RestaurantId = restaurants[4].Id, Name = "On Tap" },
                new Category { RestaurantId = restaurants[5].Id, Name = "Funnel Cakes" }
            };

            context.Categories.AddOrUpdate(c => c.Name, categories);
            context.SaveChanges();

            //create Items
            Item[] items = new Item[]
            {
                new Item {CategoryId = categories[0].Id, Name="House Merlot", Price=8.99m, IsAlcohol=true, ImageUrl="http://www.redwoodcreek.com/resources/images/wines/red/detail/merlot.png"},
                new Item {CategoryId = categories[0].Id, Name="Sparkling Rose", Price=7.99m, IsAlcohol=true, ImageUrl="http://www.mybar.ro/media/product_imagini/d95eJP%20Chenet%20Sparkling%20Rose.png"},
                new Item {CategoryId = categories[1].Id, Name="Champagne", Price=10.99m, IsAlcohol=true, ImageUrl="http://www.wineselectors.com.au/images/sites/1/products/D663DE23-294D-4DEC-8F68-3DCE53453FB9/125273-bottle.png"},
                new Item {CategoryId = categories[2].Id, Name="Cheeseburger", Price=5.99m, ImageUrl="http://static.tumblr.com/cdb30ee9681014d8e4e8af742e671e7f/jz8yp8s/nncn5ycbu/tumblr_static_8n7ubdjlh8g0wowkwkksws484.png"},
                new Item {CategoryId = categories[2].Id, Name="Nachos", Price=3.99m, ImageUrl="http://25.media.tumblr.com/tumblr_mef8dxzSmy1rtkd3to1_500.png"},
                new Item {CategoryId = categories[3].Id, Name="Large Coke", Price=2.99m, ImageUrl="http://www.dairyqueen.com/PageFiles/6085/dq-drinks-soft-coke.png?width=&height=810"},
                new Item {CategoryId = categories[3].Id, Name="Large Diet Coke", Price=2.99m, ImageUrl="http://www.dairyqueen.com/PageFiles/4951/dq-drinks-soft-dietcoke.png?width=&height=810"},
                new Item {CategoryId = categories[4].Id, Name="Chicago Dog", Price=1.99m, ImageUrl="http://www.sethscofield.com/websites/chicagodoganddeli/images/jumbodog.png"},
                new Item {CategoryId = categories[4].Id, Name="Chili Dog", Price=1.99m, ImageUrl="http://sonicmenu.s3.amazonaws.com/1821348869137.17631.png"},
                new Item {CategoryId = categories[5].Id, Name="Bratwurst", Price=2.99m, ImageUrl="http://www.basic-bio-genuss-fuer-alle.de/basicnews/images/bratwurst.png"},
                new Item {CategoryId = categories[5].Id, Name="Boudin Balls", Price=2.99m, ImageUrl="http://www.marketbasketfoods.com/wp-content/uploads/2012/11/spicy_boudin_ball-tray.png"},
                new Item {CategoryId = categories[6].Id, Name="Pretzel", Price=1.99m, ImageUrl="http://static1.squarespace.com/static/50eb2c61e4b0e6a1b5e56397/526fb24be4b0fa7c5c9b006e/526fb285e4b0c735eed666cc/1383052745658/original.png"},
                new Item {CategoryId = categories[6].Id, Name="Peanuts", Price=2.99m, ImageUrl="http://www.semar-egy.com/uploads/12-sizeimage.png"},
                new Item {CategoryId = categories[7].Id, Name="Sierra Nevada Coffee Stout", IsAlcohol=true, Price=4.99m, ImageUrl="http://www.cdn.sierranevada.com/sites/default/files/content/beers/coffee-stout/snowpack-coffeestout-bottlepint.png"},
                new Item {CategoryId = categories[7].Id, Name="House IPA", Price=5.99m, IsAlcohol=true, ImageUrl="http://odellbrewing.com/wp-content/uploads/2012/04/ipa-bottle-glass.png"},
                new Item {CategoryId = categories[8].Id, Name="Funnel Cake", Price=3.99m, ImageUrl="http://www.gatlinburgspaceneedle.com/blog/wp-content/uploads/2012/03/funnel-cake.gif"}
            };

            context.Items.AddOrUpdate(i => i.Name, items);
            context.SaveChanges();

            //create Seats
            SeatInfo[] seatInfos = new SeatInfo[]
            {
                new SeatInfo {AreaId = areas[0].Id, Section = "152", Row = "J", Chair = "14"},
                new SeatInfo {AreaId = areas[1].Id, Section = "257", Row = "C", Chair = "9"},
                new SeatInfo {AreaId = areas[2].Id, Section = "102", Row = "G", Chair = "1"}
            };

            context.SeatInfos.AddOrUpdate(s => s.Section, seatInfos);
            context.SaveChanges();

            //Create 3 Seats with password=123456
            var userSeats = new ApplicationUser[3];

            for (int i = 0; i < 3; i++)
            {
                userSeats[i] = userManager.FindByName(i.ToString() + "@gmail.com");

                if (userSeats[i] == null)
                {
                    userSeats[i] = new ApplicationUser
                    {
                        UserName = i.ToString() + "@gmail.com",
                        Email = i.ToString() + "@gmail.com",
                        SeatInfo = seatInfos[i]
                    };

                    userManager.Create(userSeats[i], "123456");
                    userSeats[i] = userManager.FindByName(i.ToString() + "@gmail.com");
                }

                ////Add to Role Customer
                //if (userSeats[i] != null)
                //{
                //    IdentityResult result = userManager.AddToRole(userSeats[i].Id, "Customer");
                //}
            }

            context.SaveChanges();

            //create AdminInfo
            AdminInfo adminInfo = new AdminInfo { ParkId = park.Id };

            context.AdminInfos.AddOrUpdate(s => s.ParkId, adminInfo);
            context.SaveChanges();

            //Create User=Admin with password=123456
            var admin = userManager.FindByName("admin@gmail.com");

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    AdminInfo = adminInfo
                };

                userManager.Create(admin, "123456");
                admin = userManager.FindByName("admin@gmail.com");
            }

            ////Add User Admin to Role Admin
            //if (admin != null)
            //{
            //    IdentityResult result = userManager.AddToRole(admin.Id, "Admin");
            //}

            context.SaveChanges();
        }
    }
}
