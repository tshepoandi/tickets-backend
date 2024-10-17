using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tickets.Entities
{
    public class Bus
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string BusNumber { get; set; }

        public int Capacity { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }

    public class Route
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<RouteStop> RouteStops { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }

    public class Stop
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<RouteStop> RouteStops { get; set; }
    }

    public class RouteStop
    {
        public int Id { get; set; }

        public int RouteId { get; set; }

        public Route Route { get; set; }

        public int StopId { get; set; }

        public Stop Stop { get; set; }

        public int Order { get; set; }
    }

    public class Schedule
    {
        public int Id { get; set; }

        public int RouteId { get; set; }

        public Route Route { get; set; }

        public int BusId { get; set; }

        public Bus Bus { get; set; }

        public TimeSpan DepartureTime { get; set; }
    }

    public class Ticket
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
