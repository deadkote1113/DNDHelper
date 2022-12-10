using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using NominationsSelectionOption = Entities.NominationsSelectionOption;

namespace BL
{
	public class NominationsSelectionOptionsBL
	{
		public async Task<int> AddOrUpdateAsync(NominationsSelectionOption entity)
		{
			entity.Id = await new NominationsSelectionOptionsDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new NominationsSelectionOptionsDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(NominationsSelectionOptionsSearchParams searchParams)
		{
			return new NominationsSelectionOptionsDal().ExistsAsync(searchParams);
		}

		public Task<NominationsSelectionOption> GetAsync(int id)
		{
			return new NominationsSelectionOptionsDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new NominationsSelectionOptionsDal().DeleteAsync(id);
		}

		public Task<SearchResult<NominationsSelectionOption>> GetAsync(NominationsSelectionOptionsSearchParams searchParams)
		{
			return new NominationsSelectionOptionsDal().GetAsync(searchParams);
		}
	}
}

