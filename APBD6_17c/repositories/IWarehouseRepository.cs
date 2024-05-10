using APBD7_17c.dto;

namespace APBD7_17c.repositories;

public interface IWarehouseRepository
{
    Task<Product_Warehouse?> GetProduct_Warehouse(int idOrder);
    Task<bool> GetProduct(int id);
    Task<ProductDTO?> AddProduct(int id);
    Task<int> UpdateOrderDTO(OrderDTO orderDto);
    Task<OrderDTO?> CheckOrder(int idProduct, int amount, DateTime createdAt);
}