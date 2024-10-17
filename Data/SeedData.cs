using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using tickets.Data;
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

                // Add stops
                var stops =
                    new Stop[] {
                        new Stop { Name = "Johannesburg CBD" },
                        new Stop { Name = "Randburg" },
                        new Stop { Name = "Midrand Second" },
                        new Stop { Name = "Midrand First" },
                        new Stop { Name = "Mecer" }
                    };
                context.Stops.AddRange (stops);
                context.SaveChanges();

                // Add route
                var route = new Route { Name = "Mecer to Johannesburg CBD" };
                context.Routes.Add (route);
                context.SaveChanges();

                // Add route stops
                var routeStops =
                    new RouteStop[] {
                        new RouteStop {
                            RouteId = route.Id,
                            StopId = stops[4].Id,
                            Order = 1
                        }, // Mecer
                        new RouteStop {
                            RouteId = route.Id,
                            StopId = stops[3].Id,
                            Order = 2
                        }, // Midrand First
                        new RouteStop {
                            RouteId = route.Id,
                            StopId = stops[2].Id,
                            Order = 3
                        }, // Midrand Second
                        new RouteStop {
                            RouteId = route.Id,
                            StopId = stops[1].Id,
                            Order = 4
                        }, // Randburg
                        new RouteStop {
                            RouteId = route.Id,
                            StopId = stops[0].Id,
                            Order = 5
                        } // Johannesburg CBD
                    };
                context.RouteStops.AddRange (routeStops);
                context.SaveChanges();

                // Add buses
                var buses =
                    new Bus[] {
                        new Bus { BusNumber = "Bus001", Capacity = 50 },
                        new Bus { BusNumber = "Bus002", Capacity = 50 },
                        new Bus { BusNumber = "Bus003", Capacity = 50 }
                    };
                context.Buses.AddRange (buses);
                context.SaveChanges();

                // Add schedules
                var schedules =
                    new Schedule[] {
                        new Schedule {
                            RouteId = route.Id,
                            BusId = buses[0].Id,
                            DepartureTime = new TimeSpan(6, 0, 0)
                        },
                        new Schedule {
                            RouteId = route.Id,
                            BusId = buses[1].Id,
                            DepartureTime = new TimeSpan(13, 0, 0)
                        },
                        new Schedule {
                            RouteId = route.Id,
                            BusId = buses[2].Id,
                            DepartureTime = new TimeSpan(17, 0, 0)
                        }
                    };
                context.Schedules.AddRange (schedules);
                context.SaveChanges();
            }
        }
    }
}
