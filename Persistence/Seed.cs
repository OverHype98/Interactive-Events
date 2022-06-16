using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any() && !context.Activities.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com",
                        Bio = ""
                    },
                    new AppUser
                    {
                        DisplayName = "Johnny",
                        UserName = "johnny",
                        Email = "johnny@test.com",
                        Bio = ""
                    },
                    new AppUser
                    {
                        DisplayName = "Amber",
                        UserName = "amber",
                        Email = "johnny@test.com",
                        Bio = ""
                    },
                    new AppUser
                    {
                        DisplayName = "Andrei",
                        UserName = "andrei",
                        Email = "andreygherghina@yahoo.com",
                        Bio = ""
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                var activities = new List<Activity>
                {
                    new Activity
                    {
                        Title = "Let's get a drink",
                        Date = DateTime.UtcNow.AddMonths(-2),
                        Description = "Try out a new pub Murphy's",
                        Category = "drinks",
                        City = "Amsterdam",
                        Venue = "Pub",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[0],
                                IsHost = true
                            }
                        }
                    },
                    new Activity
                    {
                        Title = "Board game night",
                        Date = DateTime.UtcNow.AddMonths(-1),
                        Description = "Invite some friends over for a game night of board games and laser tag",
                        Category = "social",
                        City = "Romania",
                        Venue = "Bunker Laser Tag Arena",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[0],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "Watch The Northman",
                        Date = DateTime.UtcNow.AddMonths(1),
                        Description = "See the movie \"The Northman\" at the cinema",
                        Category = "film",
                        City = "Craiova",
                        Venue = "Patria",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[2],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "Enjoy a meal",
                        Date = DateTime.UtcNow.AddMonths(2),
                        Description = "Order a delicious meal plan at Jamies Italian",
                        Category = "food",
                        City = "London",
                        Venue = "Jamies Italian",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[0],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[2],
                                IsHost = false
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "New Band",
                        Date = DateTime.UtcNow.AddMonths(3),
                        Description = "Start a band",
                        Category = "music",
                        City = "Paris",
                        Venue = "Pub",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[0],
                                IsHost = false
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "Art & art history",
                        Date = DateTime.UtcNow.AddMonths(4),
                        Description = "Read a formal research paper on an interesting subject about art and art history",
                        Category = "education",
                        City = "London",
                        Venue = "British Museum",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = true
                            }
                        }
                    },
                    new Activity
                    {
                        Title = "Gardening",
                        Date = DateTime.UtcNow.AddMonths(5),
                        Description = "Pot some plants and put them around your pub",
                        Category = "relaxation",
                        City = "San Francisco",
                        Venue = "Foursquare pub",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[0],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "City Break",
                        Date = DateTime.UtcNow.AddMonths(6),
                        Description = "Have a city break in Manchester",
                        Category = "travel",
                        City = "Manchester",
                        Venue = "O2 Arena",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[2],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "Birthday party",
                        Date = DateTime.UtcNow.AddMonths(7),
                        Description = "Throwing a birthday party for a friend, everyone is welcomed!",
                        Category = "social",
                        City = "Berlin",
                        Venue = "All",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[0],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[2],
                                IsHost = false
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "Jam session",
                        Date = DateTime.UtcNow.AddMonths(8),
                        Description = "Have a jam session with everyone who enjoys rap",
                        Category = "drinks",
                        City = "London",
                        Venue = "Pub",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[2],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            },
                        }
                    }
                };

                await context.Activities.AddRangeAsync(activities);
                await context.SaveChangesAsync();
            }
        }
    }
}
