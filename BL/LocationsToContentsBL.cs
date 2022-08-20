using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using LocationsToContent = Entities.LocationsToContent;

namespace BL
{
	public class LocationsToContentsBL
	{
		public async Task<int> AddOrUpdateAsync(LocationsToContent entity)
		{
			entity.Id = await new LocationsToContentsDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new LocationsToContentsDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(LocationsToContentsSearchParams searchParams)
		{
			return new LocationsToContentsDal().ExistsAsync(searchParams);
		}

		public Task<LocationsToContent> GetAsync(int id)
		{
			return new LocationsToContentsDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new LocationsToContentsDal().DeleteAsync(id);
		}

		public Task<SearchResult<LocationsToContent>> GetAsync(LocationsToContentsSearchParams searchParams)
		{
			return new LocationsToContentsDal().GetAsync(searchParams);
		}
	}
}

