﻿namespace Shared;

public class ArtorDTO
{
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public List<WorkDTO>? Works { get; set; }
}
