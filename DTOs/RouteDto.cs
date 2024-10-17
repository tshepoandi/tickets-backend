public class RouteDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<RouteStopDto> RouteStops { get; set; }
}

public class RouteStopDto
{
    public int Id { get; set; }

    public int StopId { get; set; }

    public StopDto Stop { get; set; }

    public int Order { get; set; }
}

public class StopDto
{
    public int Id { get; set; }

    public string Name { get; set; }
}
