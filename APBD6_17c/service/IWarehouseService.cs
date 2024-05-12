using APBD7_17c.dto;

namespace APBD7_17c.service;

public interface IWarehouseService
{
    Task<Product_Warehouse?> GetProduct_Warehouse(int idOrder);
    Task<bool> GetProduct(int id);
    Task<int> UpdateOrderDTO(OrderDTO orderDto);
    Task<OrderDTO?> CheckOrder(int idProduct, int amount, DateTime createdAt);

    Task<int> CreatedRecord(Product_Warehouse warehouse);
}