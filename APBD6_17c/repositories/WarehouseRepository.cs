using Microsoft.Data.SqlClient;

namespace APBD7_17c.repositories;

public class WarehouseRepository: IWarehouseRepository
{
    private readonly IConfiguration _configuration;
    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> CheckIfIDExist(int id)
    {
        var query = "SELECT 1 FROM Warehouse WHERE ID = @ID";
        
        //open connectiom
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        connection.Open();
        
        //create command
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);
        
        await connection.OpenAsync();
        
        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public async Task<bool> AddProduct(int id)
    {
        
    }

    public async Task<bool> CheckOrder(int id)
    {
        
    }
    
    public async Task<bool> UpdateFullfilledAtColumn(int id)
    {
        
    }
    
    public async Task<bool> InsertToProduct_WarehouseTable(int id)
    {
        
    }

}