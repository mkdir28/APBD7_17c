
using System.ComponentModel.DataAnnotations;

namespace APBD7_17c.dto;

public class WarehouseDTO
{
    [Required]
    [MaxLength(200)]
    public int IdWarehouse { get; set; }
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;
}

public class ProductDTO
{
    [Required]
    public int IdProduct { get; set; }
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(200)]
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; }
}

public class OrderDTO
{
    [Required]
    public int IdOrder { get; set; }
    public ProductDTO IdProduct { get; set; } = null!;
    [Required]
    public int Amount { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime? FullfiedAt { get; set; }
}

public class Product_Warehouse
{
    public int IdProduct { get; set; }
    [Required]
    public int IdWarehouse { get; set; }
    [Required]
    public int Amount { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}


