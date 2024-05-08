namespace WebApp.Data;

public class Movie
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Genre { get; set; }
    public float Rating { get; set; }
}
