public class RouteDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<RouteStopDto> RouteStops
    { get; set;
    } = new List<RouteStopDto>();
}

public class RouteStopDto
{
    public int Id { get; set; }

    public int StopId { get; set; }

    public StopDto Stop { get; set; } = new StopDto();

    public int Order { get; set; }
}

public class StopDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
