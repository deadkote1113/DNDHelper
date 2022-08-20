using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using Quest = Entities.Quest;

namespace BL
{
	public class QuestsBL
	{
		public async Task<int> AddOrUpdateAsync(Quest entity)
		{
			entity.Id = await new QuestsDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new QuestsDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(QuestsSearchParams searchParams)
		{
			return new QuestsDal().ExistsAsync(searchParams);
		}

		public Task<Quest> GetAsync(int id)
		{
			return new QuestsDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new QuestsDal().DeleteAsync(id);
		}

		public Task<SearchResult<Quest>> GetAsync(QuestsSearchParams searchParams)
		{
			return new QuestsDal().GetAsync(searchParams);
		}
	}
}

