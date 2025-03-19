namespace WebApplication2.Models;

public class Movie
{
    public int MovieId { get; set; }
    public string? Title { get; set; }
    public string? Director { get; set; }  
    
    public string[]? Players { get; set; }
    public int ReleaseYear { get; set; }
    public string? ImageUrl { get; set; }
    public double Rating { get; set; }
    
}