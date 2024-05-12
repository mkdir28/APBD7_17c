using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using APBD7_17c.dto;
using APBD7_17c.service;
using Microsoft.Data.SqlClient;

namespace APBD7_17c.Controllers;
[ApiController]
[Route("api/[controller]")]
public class WarehouseController(IWarehouseService service) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateWarehouse(Product_Warehouse productWarehouse)
    {
        var id = await service.CreatedRecord(productWarehouse);

        return StatusCode(id == -1 ? StatusCodes.Status204NoContent : StatusCodes.Status201Created);
    }
    
}

