using APBD7_17c.dto;
using APBD7_17c.repositories;

namespace APBD7_17c.service;

public class WarehouseService(IWarehouseRepository repository) : IWarehouseService
{
    public async Task<OrderDTO?> CheckOrder(int idProduct, int amount, DateTime createdAt)
    {
        return await repository.CheckOrder(idProduct, amount, createdAt);
    }

    public async Task<int> UpdateOrderDTO(int id)
    {
        return await repository.UpdateOrderDTO(id);
    }

    public async Task<ProductDTO?> GetProduct(int id)
    {
        return await repository.GetProduct(id);
    }

    public async Task<Product_Warehouse?> GetProduct_Warehouse(int idOrder)
    {
        return await repository.GetProduct_Warehouse(idOrder);
    }
    
    public async Task<int> CreatedRecord(Product_Warehouse warehouse_product)
    {
        var productDTO = repository.GetProduct(warehouse_product.IdProduct);
        var orderDTO = repository.CheckOrder(warehouse_product.IdProduct, warehouse_product.Amount, warehouse_product.CreatedAt);
        var warehouseDTO = repository.GetWarehouse(warehouse_product.IdProduct);

        var product = await productDTO;
        var order = await orderDTO;
        var warehouse = await warehouseDTO;

        if (product == null || order == null || warehouse == null || warehouse_product.Amount <= 0)
            return -1;

        repository.UpdateOrderDTO(order.IdOrder);
        return await repository.CreatedRecord(warehouse_product, product, order);
    }
}