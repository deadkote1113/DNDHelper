using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using Nomination = Entities.Nomination;

namespace BL
{
	public class NominationsBL
	{
		public async Task<int> AddOrUpdateAsync(Nomination entity)
		{
			entity.Id = await new NominationsDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new NominationsDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(NominationsSearchParams searchParams)
		{
			return new NominationsDal().ExistsAsync(searchParams);
		}

		public Task<Nomination> GetAsync(int id)
		{
			return new NominationsDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new NominationsDal().DeleteAsync(id);
		}

		public Task<SearchResult<Nomination>> GetAsync(NominationsSearchParams searchParams)
		{
			return new NominationsDal().GetAsync(searchParams);
		}
	}
}

