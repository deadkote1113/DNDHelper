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
	public class AwardSessionsDal : BaseDal<DefaultDbContext, AwardSession, Entities.AwardSession, int, AwardSessionsSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public AwardSessionsDal()
		{
		}

		protected internal AwardSessionsDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.AwardSession entity, AwardSession dbObject, bool exists)
		{
			dbObject.UserId = entity.UserId;
			dbObject.ConnectionCode = entity.ConnectionCode;
			dbObject.State = (int)entity.State;
			dbObject.NominationPassed = entity.NominationPassed;
			dbObject.AwardId = entity.AwardId;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<AwardSession>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<AwardSession> dbObjects, AwardSessionsSearchParams searchParams)
		{
			if (searchParams.State.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.State == (int)searchParams.State);
			}
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.AwardSession>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<AwardSession> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<AwardSession, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.AwardSession, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.AwardSession ConvertDbObjectToEntity(AwardSession dbObject)
		{
			return dbObject == null ? null : new Entities.AwardSession(dbObject.Id, dbObject.UserId,
				dbObject.ConnectionCode, (AwardSessionsState)dbObject.State, dbObject.NominationPassed,
				dbObject.AwardId);
		}
	}
}
