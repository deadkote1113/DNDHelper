using System.Threading.Tasks;
using Dal;
using Common.Search;
using Award = Entities.Award;
using Entities;
using System.Collections.Generic;
using System.Linq;

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

		public async Task<IAwardItem> GetCurentNominationItem(int awardId, int passedNominations)
		{
			var nominations = await new NominationsBL().GetAsync(new NominationsSearchParams()
			{
				AwardId = awardId
			});
			var events = await new AwardEventsBL().GetAsync(new AwardEventsSearchParams()
			{
				AwardId = awardId
			});

			List<IAwardItem> awardItems = nominations.Objects.Select(item => (IAwardItem)item)
				.Concat(events.Objects.Select(item => (IAwardItem)item)).OrderBy(item => item.OrderId).ToList();
			return awardItems.Skip(passedNominations).First();
		}
	}
}

