using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using AwardSession = Entities.AwardSession;

namespace BL
{
	public class AwardSessionsBL
	{
		public async Task<int> AddOrUpdateAsync(AwardSession entity)
		{
			entity.Id = await new AwardSessionsDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new AwardSessionsDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(AwardSessionsSearchParams searchParams)
		{
			return new AwardSessionsDal().ExistsAsync(searchParams);
		}

		public Task<AwardSession> GetAsync(int id)
		{
			return new AwardSessionsDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new AwardSessionsDal().DeleteAsync(id);
		}

		public Task<SearchResult<AwardSession>> GetAsync(AwardSessionsSearchParams searchParams)
		{
			return new AwardSessionsDal().GetAsync(searchParams);
		}
	}
}

