using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Models;

public class Joke
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Text { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required]
    public string ApiId { get; set; }

    public Rating Rating { get; set; }
    public Favourite Favourite { get; set; }
}

public class Rating
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Joke")]
    public int JokeId { get; set; }
    public virtual Joke Joke { get; set; }

    [Range(1, 5)]
    public int Value { get; set; }
}

public class Favourite
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Joke")]
    public int JokeId { get; set; }
    public virtual Joke Joke { get; set; }

    public DateTime FavouritedAt { get; set; } = DateTime.Now;
}