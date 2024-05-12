using APBD7_17c.dto;

namespace APBD7_17c.repositories;

public interface IWarehouseRepository
{
    Task<Product_Warehouse?> GetProduct_Warehouse(int idOrder);
    Task<ProductDTO?> GetProduct(int id);
    Task<int> UpdateOrderDTO(int id);
    Task<WarehouseDTO?> GetWarehouse(int id);
    Task<OrderDTO?> CheckOrder(int idProduct, int amount, DateTime createdAt);
    Task<int> CreatedRecord(Product_Warehouse warehouse, ProductDTO productDto, OrderDTO orderDto);
    Task<int> WarehouseException(Product_Warehouse warehouse);
}