namespace APBD7_17c.repositories;

public interface IWarehouseRepository
{
    Task<bool> CheckIfIDExist(int id);
    Task<bool> AddProduct(int id);
    Task<bool> UpdateFullfilledAtColumn(int id);
    Task<bool> InsertToProduct_WarehouseTable(int id);
    Task<bool> CheckOrder(int id);
}