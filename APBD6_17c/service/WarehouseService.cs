using APBD7_17c.dto;
using APBD7_17c.repositories;

namespace APBD7_17c.service;

public class WarehouseService(IWarehouseRepository repository) : IWarehouseService
{
    public Task<OrderDTO?> CheckOrder(int idProduct, int amount, DateTime createdAt)
    {
        //Business logic
        return repository.CheckOrder(idProduct, amount, createdAt);
    }

    public Task<int> UpdateOrderDTO(OrderDTO orderDto)
    {
        return repository.UpdateOrderDTO(orderDto);
    }

    public Task<bool> GetProduct(int id)
    {
        return repository.GetProduct(id);
    }

    public Task<Product_Warehouse?> GetProduct_Warehouse(int idOrder)
    {
        return repository.GetProduct_Warehouse(idOrder);
    }
}