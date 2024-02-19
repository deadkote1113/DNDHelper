using System.Threading.Tasks;
using Dal;
using Common.Search;
using Award = Entities.Award;

namespace BL
{
	public class AwardsBL
	{
		public async Task<int> AddOrUpdateAsync(Award entity)
		{
			entity.Id = await new AwardsDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new AwardsDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(AwardsSearchParams searchParams)
		{
			return new AwardsDal().ExistsAsync(searchParams);
		}

		public Task<Award> GetAsync(int id)
		{
			return new AwardsDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new AwardsDal().DeleteAsync(id);
		}

		public Task<SearchResult<Award>> GetAsync(AwardsSearchParams searchParams)
		{
			return new AwardsDal().GetAsync(searchParams);
		}
	}
}

