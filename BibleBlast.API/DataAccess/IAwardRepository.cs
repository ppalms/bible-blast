using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAwardRepository
{
    Task<Award> GetAward(int id);
    Task<IEnumerable<Award>> GetAwards();
    Task<IEnumerable<Award>> GetAwardsEarned(int categoryId);
}
