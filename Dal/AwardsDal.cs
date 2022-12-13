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
	public class AwardsDal : BaseDal<DefaultDbContext, Award, Entities.Award, int, AwardsSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public AwardsDal()
		{
		}

		protected internal AwardsDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Award entity, Award dbObject, bool exists)
		{
			dbObject.UserId = entity.UserId;
			dbObject.Title = entity.Title;
			dbObject.Description = entity.Description;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Award>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Award> dbObjects, AwardsSearchParams searchParams)
		{
			if(string.IsNullOrEmpty(searchParams.SearchQuery) == false)
			{
				dbObjects = dbObjects.Where(item => item.Title.ToLower().Contains(searchParams.SearchQuery.ToLower()));
			}
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Award>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Award> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Award, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Award, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Award ConvertDbObjectToEntity(Award dbObject)
		{
			return dbObject == null ? null : new Entities.Award(dbObject.Id, dbObject.UserId, dbObject.Title,
				dbObject.Description);
		}
	}
}
