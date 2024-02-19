using System.Threading.Tasks;
using Dal;
using Common.Search;
using Vote = Entities.Vote;

namespace BL
{
	public class VotesBL
	{
		public async Task<int> AddOrUpdateAsync(Vote entity)
		{
			entity.Id = await new VotesDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new VotesDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(VotesSearchParams searchParams)
		{
			return new VotesDal().ExistsAsync(searchParams);
		}

		public Task<Vote> GetAsync(int id)
		{
			return new VotesDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new VotesDal().DeleteAsync(id);
		}

		public Task<SearchResult<Vote>> GetAsync(VotesSearchParams searchParams)
		{
			return new VotesDal().GetAsync(searchParams);
		}
	}
}

