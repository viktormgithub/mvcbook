using Microsoft.AspNetCore.Mvc;

namespace BookMVC.Models;

public class Book
{
    [HiddenInput(DisplayValue=false)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public short Year { get; set; }
    public string Publisher { get; set; }
    public string Annotation { get; set; }
    public string ISBN10 { get; set; }
    public string ISBN13 { get; set; }
    public string Content { get; set; }
}