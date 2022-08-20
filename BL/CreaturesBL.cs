using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using Creature = Entities.Creature;

namespace BL
{
	public class CreaturesBL
	{
		public async Task<int> AddOrUpdateAsync(Creature entity)
		{
			entity.Id = await new CreaturesDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new CreaturesDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(CreaturesSearchParams searchParams)
		{
			return new CreaturesDal().ExistsAsync(searchParams);
		}

		public Task<Creature> GetAsync(int id)
		{
			return new CreaturesDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new CreaturesDal().DeleteAsync(id);
		}

		public Task<SearchResult<Creature>> GetAsync(CreaturesSearchParams searchParams)
		{
			return new CreaturesDal().GetAsync(searchParams);
		}
	}
}

