using System;

namespace AspCoreSandbox.Data.Entities
{
    public class Product
  {
    public int Id { get; set; }
    public string Category { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateOfProduceStart { get; set; }
    public DateTime DateOfProduceEnd{ get; set; }
    public string CountryOfOrigin { get; set; }
  }
}
