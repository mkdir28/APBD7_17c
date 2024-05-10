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

    public async Task<int> UpdateOrderDTO(OrderDTO orderDto)
    {
        var query = "UPDATE [OrderDTO] SET FullfilledAt = @FullfilledAt where IdOrder=@id";
        //open connection
        await using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Docker"));
        await connection.OpenAsync();
        
        //create command
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@id", orderDto.IdOrder);
        command.Parameters.AddWithValue("@FullfilledAt", DateTime.Now);

        var id = await command.ExecuteScalarAsync();

        if (id is null) throw new Exception();
	    
        return Convert.ToInt32(id);
    }

    public async Task<ProductDTO?> AddProduct(int id)
    {
        
    }

    public async Task<bool> GetProduct(int id)
    {
        var queryproduct = "Select 1 From [ProductDTO] where id=@id";
        //open connection
        await using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Docker"));
        await connection.OpenAsync();
        
        //create command
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = queryproduct;
        
        var querywarehouse = "Select 1 From [WarehouseDTO] where id=@id";
        command.CommandText = querywarehouse;

        
        var result = await command.ExecuteScalarAsync();

        return result is not null;
    }


    public async Task<Product_Warehouse?> GetProduct_Warehouse(int idOrder)
    {
        var query = "Select IdOrder from Product_Warehouse where IdProduct=@IdOrder";
        //open connection
        await using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Docker"));
        await connection.OpenAsync();
        
        //create command
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;

        command.Parameters.AddWithValue("@idOrder", idOrder);
        
        var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new Product_Warehouse()
            {
                IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct"))
            };
        }
        else
            return null;
    }

}