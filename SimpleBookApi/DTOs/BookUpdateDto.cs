﻿using System.ComponentModel.DataAnnotations;

public class BookUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = "";

    [Required]
    public string Author { get; set; } = "";

    [Range(0, 2100)]
    public int Year { get; set; }
}
