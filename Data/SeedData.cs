using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using tickets.Entities;

using Route = tickets.Entities.Route;

namespace tickets.Data
{
    public static class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (
                var context =
                    new TicketsDbContext(serviceProvider
                            .GetRequiredService
                            <DbContextOptions<TicketsDbContext>>())
            )
            {
                if (
                    context.Stops.Any() ||
                    context.Routes.Any() ||
                    context.Buses.Any()
                )
                {
                    return; // Data was already seeded
                }

                var random = new Random();

                // Add stops
                var stops =
                    new Stop[] {
                        new Stop { Name = "Johannesburg CBD" },
                        new Stop { Name = "Randburg" },
                        new Stop { Name = "Midrand Second" },
                        new Stop { Name = "Midrand First" },
                        new Stop { Name = "Mecer" },
                        new Stop { Name = "Pretoria" },
                        new Stop { Name = "Centurion" },
                        new Stop { Name = "Sandton" }
                    };
                context.Stops.AddRange (stops);
                context.SaveChanges();

                // Add routes
                var routes =
                    new Route[] {
                        new Route { Name = "Mecer to Johannesburg CBD" },
                        new Route { Name = "Pretoria to Sandton" },
                        new Route { Name = "Centurion to Randburg" }
                    };
                context.Routes.AddRange (routes);
                context.SaveChanges();

                // Add route stops
                foreach (var route in routes)
                {
                    var shuffledStops =
                        stops.OrderBy(x => random.Next()).ToList();
                    var selectedStops =
                        shuffledStops.Take(random.Next(3, 6)).ToList();
                    for (int i = 0; i < selectedStops.Count; i++)
                    {
                        context
                            .RouteStops
                            .Add(new RouteStop {
                                RouteId = route.Id,
                                StopId = selectedStops[i].Id,
                                Order = i + 1
                            });
                    }
                }
                context.SaveChanges();

                // Add buses
                var buses =
                    new Bus[] {
                        new Bus { BusNumber = "Bus001", Capacity = 50 },
                        new Bus { BusNumber = "Bus002", Capacity = 50 },
                        new Bus { BusNumber = "Bus003", Capacity = 50 },
                        new Bus { BusNumber = "Bus004", Capacity = 50 },
                        new Bus { BusNumber = "Bus005", Capacity = 50 }
                    };
                context.Buses.AddRange (buses);
                context.SaveChanges();

                // Add schedules
                var schedules = new List<Schedule>();
                foreach (var route in routes)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var departureTime =
                            new TimeSpan(random.Next(5, 20),
                                random.Next(0, 60),
                                0);
                        schedules
                            .Add(new Schedule {
                                RouteId = route.Id,
                                BusId = buses[random.Next(buses.Length)].Id,
                                DepartureTime = departureTime
                            });
                    }
                }
                context.Schedules.AddRange (schedules);
                context.SaveChanges();
            }
        }
    }
}
