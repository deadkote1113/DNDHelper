using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common.Search;
using Dal.DbModels;

namespace Dal
{
	public class NominationsDal : BaseDal<DefaultDbContext, Nomination, Entities.Nomination, int, NominationsSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public NominationsDal()
		{
		}

		protected internal NominationsDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Nomination entity, Nomination dbObject, bool exists)
		{
			dbObject.Title = entity.Title;
			dbObject.Description = entity.Description;
			dbObject.AwardsId = entity.AwardsId;
			dbObject.OrderId = entity.OrderId;
			dbObject.ReaderId = entity.ReaderId;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Nomination>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Nomination> dbObjects, NominationsSearchParams searchParams)
		{
			if (searchParams.AwardId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.AwardsId == searchParams.AwardId);
			}
			dbObjects = dbObjects.OrderBy(item => item.OrderId);
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Nomination>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Nomination> dbObjects, object convertParams, bool isFull)
		{
			dbObjects = dbObjects
				.Include(item => item.Reader);
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Nomination, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Nomination, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Nomination ConvertDbObjectToEntity(Nomination dbObject)
		{
			return dbObject == null ? null : new Entities.Nomination(dbObject.Id, dbObject.Title, dbObject.Description,
				dbObject.AwardsId, dbObject.OrderId, dbObject.ReaderId)
			{
				Reader = ReadersDal.ConvertDbObjectToEntity(dbObject.Reader),
			};
		}
	}
}
