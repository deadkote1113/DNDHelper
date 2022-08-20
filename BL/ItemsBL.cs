using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using Item = Entities.Item;

namespace BL
{
	public class ItemsBL
	{
		public async Task<int> AddOrUpdateAsync(Item entity)
		{
			entity.Id = await new ItemsDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new ItemsDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(ItemsSearchParams searchParams)
		{
			return new ItemsDal().ExistsAsync(searchParams);
		}

		public Task<Item> GetAsync(int id)
		{
			return new ItemsDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new ItemsDal().DeleteAsync(id);
		}

		public Task<SearchResult<Item>> GetAsync(ItemsSearchParams searchParams)
		{
			return new ItemsDal().GetAsync(searchParams);
		}
	}
}

