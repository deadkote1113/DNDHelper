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
	public class QuestsToItemsOrCreaturesDal : BaseDal<DefaultDbContext, QuestsToItemsOrCreature, Entities.QuestsToItemsOrCreature, int, QuestsToItemsOrCreaturesSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public QuestsToItemsOrCreaturesDal()
		{
		}

		protected internal QuestsToItemsOrCreaturesDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.QuestsToItemsOrCreature entity, QuestsToItemsOrCreature dbObject, bool exists)
		{
			dbObject.QuestId = entity.QuestId;
			dbObject.ItemId = entity.ItemId;
			dbObject.CreatureId = entity.CreatureId;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<QuestsToItemsOrCreature>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<QuestsToItemsOrCreature> dbObjects, QuestsToItemsOrCreaturesSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.QuestsToItemsOrCreature>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<QuestsToItemsOrCreature> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<QuestsToItemsOrCreature, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.QuestsToItemsOrCreature, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.QuestsToItemsOrCreature ConvertDbObjectToEntity(QuestsToItemsOrCreature dbObject)
		{
			return dbObject == null ? null : new Entities.QuestsToItemsOrCreature(dbObject.Id, dbObject.QuestId,
				dbObject.ItemId, dbObject.CreatureId);
		}
	}
}
