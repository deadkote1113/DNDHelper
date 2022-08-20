using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using QuestsToItemsOrCreature = Entities.QuestsToItemsOrCreature;

namespace BL
{
	public class QuestsToItemsOrCreaturesBL
	{
		public async Task<int> AddOrUpdateAsync(QuestsToItemsOrCreature entity)
		{
			entity.Id = await new QuestsToItemsOrCreaturesDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new QuestsToItemsOrCreaturesDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(QuestsToItemsOrCreaturesSearchParams searchParams)
		{
			return new QuestsToItemsOrCreaturesDal().ExistsAsync(searchParams);
		}

		public Task<QuestsToItemsOrCreature> GetAsync(int id)
		{
			return new QuestsToItemsOrCreaturesDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new QuestsToItemsOrCreaturesDal().DeleteAsync(id);
		}

		public Task<SearchResult<QuestsToItemsOrCreature>> GetAsync(QuestsToItemsOrCreaturesSearchParams searchParams)
		{
			return new QuestsToItemsOrCreaturesDal().GetAsync(searchParams);
		}
	}
}

