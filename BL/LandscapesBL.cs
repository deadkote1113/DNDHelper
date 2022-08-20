using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using Landscape = Entities.Landscape;

namespace BL
{
	public class LandscapesBL
	{
		public async Task<int> AddOrUpdateAsync(Landscape entity)
		{
			entity.Id = await new LandscapesDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new LandscapesDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(LandscapesSearchParams searchParams)
		{
			return new LandscapesDal().ExistsAsync(searchParams);
		}

		public Task<Landscape> GetAsync(int id)
		{
			return new LandscapesDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new LandscapesDal().DeleteAsync(id);
		}

		public Task<SearchResult<Landscape>> GetAsync(LandscapesSearchParams searchParams)
		{
			return new LandscapesDal().GetAsync(searchParams);
		}
	}
}

