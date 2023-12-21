using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;

namespace barboek.Interface.IServices;

public interface IDbPriceService
{
    DbPrice GetDbById(Guid id);
    Price? MapDbToApi(DbPrice? dbPrice);
    List<Price> MapDbToApi(List<DbPrice> dbPrices);
}