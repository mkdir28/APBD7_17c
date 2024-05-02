
using System.ComponentModel.DataAnnotations;

namespace APBD7_17c.dto;

public class WarehouseDTO
{
    public int IdProduct { get; set; }
    [Required]
    [MaxLength(200)]
    
    public int IdWarehouse { get; set; }
    [Required]
    [MaxLength(200)]
    
    public int Amount { get; set; }
    [Required]
    
    public string CreatedAt { get; set; }
}