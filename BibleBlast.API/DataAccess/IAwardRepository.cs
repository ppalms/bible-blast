using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAwardRepository
{
    Task<IEnumerable<Award>> GetAwards();
    Task<Award> GetAward(int id);
}
