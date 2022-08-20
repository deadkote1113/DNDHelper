using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common.Enums;
using Common.Search;
using Dal.DbModels;

namespace Dal
{
	public class QuestsDal : BaseDal<DefaultDbContext, Quest, Entities.Quest, int, QuestsSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public QuestsDal()
		{
		}

		protected internal QuestsDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Quest entity, Quest dbObject, bool exists)
		{
			dbObject.Title = entity.Title;
			dbObject.FlavorText = entity.FlavorText;
			dbObject.IsComplited = entity.IsComplited;
			dbObject.NextQuestId = entity.NextQuestId;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Quest>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Quest> dbObjects, QuestsSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Quest>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Quest> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Quest, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Quest, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Quest ConvertDbObjectToEntity(Quest dbObject)
		{
			return dbObject == null ? null : new Entities.Quest(dbObject.Id, dbObject.Title, dbObject.FlavorText,
				dbObject.IsComplited, dbObject.NextQuestId);
		}
	}
}
