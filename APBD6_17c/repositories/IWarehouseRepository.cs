using APBD7_17c.dto;

namespace APBD7_17c.repositories;

public interface IWarehouseRepository
{
    Task<Product_Warehouse?> GetProduct_Warehouse(int id);
    Task<ProductDTO?> GetProduct(int id);
    Task<ProductDTO?> AddProduct(int id);
    Task<OrderDTO> UpdateOrderDTO(int id);
    Task<OrderDTO?> CheckOrder(int idProduct, int amount, DateTime createdAt);
}