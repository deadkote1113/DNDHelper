using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using Structure = Entities.Structure;

namespace BL
{
	public class StructuresBL
	{
		public async Task<int> AddOrUpdateAsync(Structure entity)
		{
			entity.Id = await new StructuresDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new StructuresDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(StructuresSearchParams searchParams)
		{
			return new StructuresDal().ExistsAsync(searchParams);
		}

		public Task<Structure> GetAsync(int id)
		{
			return new StructuresDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new StructuresDal().DeleteAsync(id);
		}

		public Task<SearchResult<Structure>> GetAsync(StructuresSearchParams searchParams)
		{
			return new StructuresDal().GetAsync(searchParams);
		}
	}
}

