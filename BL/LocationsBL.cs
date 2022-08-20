using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using Location = Entities.Location;

namespace BL
{
	public class LocationsBL
	{
		public async Task<int> AddOrUpdateAsync(Location entity)
		{
			entity.Id = await new LocationsDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new LocationsDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(LocationsSearchParams searchParams)
		{
			return new LocationsDal().ExistsAsync(searchParams);
		}

		public Task<Location> GetAsync(int id)
		{
			return new LocationsDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new LocationsDal().DeleteAsync(id);
		}

		public Task<SearchResult<Location>> GetAsync(LocationsSearchParams searchParams)
		{
			return new LocationsDal().GetAsync(searchParams);
		}
	}
}

