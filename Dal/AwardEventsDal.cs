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
	public class AwardEventsDal : BaseDal<DefaultDbContext, AwardEvent, Entities.AwardEvent, int, AwardEventsSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public AwardEventsDal()
		{
		}

		protected internal AwardEventsDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.AwardEvent entity, AwardEvent dbObject, bool exists)
		{
			dbObject.Title = entity.Title;
			dbObject.Description = entity.Description;
			dbObject.OrderId = entity.OrderId;
			dbObject.AwardsId = entity.AwardsId;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<AwardEvent>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<AwardEvent> dbObjects, AwardEventsSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.AwardEvent>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<AwardEvent> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<AwardEvent, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.AwardEvent, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.AwardEvent ConvertDbObjectToEntity(AwardEvent dbObject)
		{
			return dbObject == null ? null : new Entities.AwardEvent(dbObject.Id, dbObject.Title, dbObject.Description,
				dbObject.OrderId, dbObject.AwardsId);
		}
	}
}
