using Microsoft.AspNetCore.Identity;
using RunGroupWebApp.Data;
using RunGroupWebApp.Models;

namespace RunGroopWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Clubs.Any())
                {
                    context.Clubs.AddRange(new List<Club>()
                    {
                        new Club()
                        {
                            Title = "Running Club 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            clubcategory = ClubCategory.City,
                            adress = new Adress()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                state = "NC"
                            }
                         },
                        new Club()
                        {
                            Title = "Running Club 2",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            clubcategory = ClubCategory.Endurance,
                            adress = new Adress()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                state = "NC"
                            }
                        },
                        new Club()
                        {
                            Title = "Running Club 3",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first club",
                            clubcategory = ClubCategory.Trails,
                            adress = new Adress()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                state = "NC"
                            }
                        },
                        new Club()
                        {
                            Title = "Running Club 3",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first club",
                            clubcategory = ClubCategory.City,
                            adress = new Adress()
                            {
                                Street = "123 Main St",
                                City = "Michigan",
                                state = "NC"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Races
                if (!context.Races.Any())
                {
                    context.Races.AddRange(new List<Race>()
                    {
                        new Race()
                        {
                            Title = "Running Race 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first race",
                            clubcategory = RaceCategory.Marathon,
                            adress = new Adress()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                state = "NC"
                            }
                        },
                        new Race()
                        {
                            Title = "Running Race 2",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first race",
                            clubcategory = RaceCategory.Ultra,
                            AdressId = 5,
                            adress = new Adress()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                state = "NC"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "teddysmithdeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "teddysmithdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Adress = new Adress()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            state = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Adress = new Adress()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            state = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}