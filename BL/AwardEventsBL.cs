using System.Threading.Tasks;
using Dal;
using Common.Search;
using AwardEvent = Entities.AwardEvent;

namespace BL
{
	public class AwardEventsBL
	{
		public async Task<int> AddOrUpdateAsync(AwardEvent entity)
		{
			entity.Id = await new AwardEventsDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new AwardEventsDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(AwardEventsSearchParams searchParams)
		{
			return new AwardEventsDal().ExistsAsync(searchParams);
		}

		public Task<AwardEvent> GetAsync(int id)
		{
			return new AwardEventsDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new AwardEventsDal().DeleteAsync(id);
		}

		public Task<SearchResult<AwardEvent>> GetAsync(AwardEventsSearchParams searchParams)
		{
			return new AwardEventsDal().GetAsync(searchParams);
		}
	}
}

