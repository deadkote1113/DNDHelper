using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using StructuresToItemsOrCreature = Entities.StructuresToItemsOrCreature;

namespace BL
{
	public class StructuresToItemsOrCreaturesBL
	{
		public async Task<int> AddOrUpdateAsync(StructuresToItemsOrCreature entity)
		{
			entity.Id = await new StructuresToItemsOrCreaturesDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new StructuresToItemsOrCreaturesDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(StructuresToItemsOrCreaturesSearchParams searchParams)
		{
			return new StructuresToItemsOrCreaturesDal().ExistsAsync(searchParams);
		}

		public Task<StructuresToItemsOrCreature> GetAsync(int id)
		{
			return new StructuresToItemsOrCreaturesDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new StructuresToItemsOrCreaturesDal().DeleteAsync(id);
		}

		public Task<SearchResult<StructuresToItemsOrCreature>> GetAsync(StructuresToItemsOrCreaturesSearchParams searchParams)
		{
			return new StructuresToItemsOrCreaturesDal().GetAsync(searchParams);
		}
	}
}

