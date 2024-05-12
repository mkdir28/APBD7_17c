using APBD7_17c.dto;
using Microsoft.Data.SqlClient;

namespace APBD7_17c.repositories;

public class WarehouseRepository(IConfiguration configuration): IWarehouseRepository
{
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

    public async Task<int> UpdateOrderDTO(int id)
    {
        var query = "UPDATE [OrderDTO] SET FullfilledAt = @FullfilledAt where IdOrder=@id";
        //open connection
        await using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Docker"));
        await connection.OpenAsync();
        
        //create command
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@FullfilledAt", DateTime.Now);

        return await command.ExecuteNonQueryAsync();
    }
    
    public async Task<ProductDTO?> GetProduct(int id)
    {
        var queryproduct = "Select 1 From [ProductDTO] where IdProduct=@id";

        //open connection
        await using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Docker"));
        await connection.OpenAsync();
        
        //create command
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = queryproduct;
        
        var querywarehouse = "Select 1 From [WarehouseDTO] where IdWarehouse=@id";
        command.CommandText = querywarehouse;

        
        var result = await command.ExecuteReaderAsync();

        if (!await result.ReadAsync())
            return null;

        return new ProductDTO
        {
            IdProduct = (int)result["IdProduct"],
            Name = result["Name"].ToString(),
            Description = result["Description"].ToString(),
            Price = Convert.ToDecimal(result["Price"].ToString())
        };
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

    public async Task<int> CreatedRecord(Product_Warehouse warehouse, ProductDTO productDto, OrderDTO orderDto)
    {
        var query =
            "Insert into  Product_Warehouse(IdProduct, IdWarehouse, Amount, Price, CreatedAt) " +
            "VALUES (@IdProduct, @IdWarehouse, @Amount, @Price, @CreatedAt)";
        //open connection
        await using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Docker"));
        await connection.OpenAsync();
        
        //create command
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;

        command.Parameters.AddWithValue("@IdProduct", productDto.IdProduct);
        command.Parameters.AddWithValue("@IdWarehouse", warehouse.IdWarehouse);
        command.Parameters.AddWithValue("@Amount", warehouse.Amount);
        command.Parameters.AddWithValue("@Price", warehouse.Amount * productDto.Price);
        command.Parameters.AddWithValue("@CreatedAt", warehouse.CreatedAt);
        
        var id = await command.ExecuteScalarAsync();

        if (id is null) throw new Exception();
	    
        return Convert.ToInt32(id);
    }

    public async Task<WarehouseDTO?> GetWarehouse(int id)
    {
        var query = "SELECT IdWarehouse, Name, Address FROM Warehouse WHERE IdWarehouse = @id";
        //open connection
        await using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Docker"));
        await connection.OpenAsync();
        
        //create command
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        
        command.Parameters.AddWithValue("@id", id);
        var result = await command.ExecuteReaderAsync();

        if (!await result.ReadAsync())
            return null;

        return new WarehouseDTO
        {
            IdWarehouse = (int)result["IdWarehouse"],
            Name = result["Name"].ToString(),
            Address = result["Address"].ToString()
        };
    }
    
    public async Task<int> WarehouseException(Product_Warehouse warehouse){
        throw new NotImplementedException();
    }
}