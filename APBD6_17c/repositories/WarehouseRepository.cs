using APBD7_17c.dto;
using Microsoft.Data.SqlClient;

namespace APBD7_17c.repositories;

public class WarehouseRepository(IConfiguration configuration): IWarehouseRepository
{
    // public async Task<bool> CheckIfIDExist(int id)
    // {
    //     var query = "SELECT 1 FROM Warehouse WHERE ID = @ID";
    //     
    //     //open connectiom
    //     await using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Docker"));
    //     connection.Open();
    //     
    //     //create command
    //     await using SqlCommand command = new SqlCommand();
    //     command.Connection = connection;
    //     command.CommandText = query;
    //     command.Parameters.AddWithValue("@ID", id);
    //     
    //     await connection.OpenAsync();
    //     
    //     var res = await command.ExecuteScalarAsync();
    //
    //     return res is not null;
    // }



    public async Task<OrderDTO?> CheckOrder(int idProduct, int amount, DateTime createdAt)
    {
        var query = "SELECT IdOrder, ProductDTO.IdProduct, Amount, CreatedAt, FullfiedAt FROM OrderDTO WHERE IdProduct = @idProduct and" +
                    "Amount = @amount and CreatedAt < @createdAt";
        //open connection
        await using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Docker"));
        connection.Open();
        
        //create command
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@idProduct", "idProduct");
        command.Parameters.AddWithValue("@amount", "amount");
        command.Parameters.AddWithValue("@createdAt", "createdAt");
        
        await connection.OpenAsync();
        
        var reader = await command.ExecuteReaderAsync();
        
        if (await reader.ReadAsync())
        {
            return new OrderDTO()
            {
                IdOrder = reader.GetInt32(reader.GetOrdinal("IdOrder")),
                IdProduct = new ProductDTO()
                {
                    IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct")),
                },
                Amount = reader.GetInt32(reader.GetOrdinal("Amount")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                FullfiedAt = reader.IsDBNull(reader.GetOrdinal("FullfiedAt")) 
                             && DateTime.TryParse(reader["FulfilledAt"].ToString(), out DateTime fulfilledDate) ? fulfilledDate : null
            };
        }
        else
            return null;
    }

    public async Task<OrderDTO> UpdateOrderDTO(int id)
    {
        
    }

    public async Task<ProductDTO?> AddProduct(int id)
    {
        
    }

    public async Task<ProductDTO?> GetProduct(int id)
    {
        
    }

    public async Task<Product_Warehouse?> GetProduct_Warehouse(int id)
    {
        
    }

}