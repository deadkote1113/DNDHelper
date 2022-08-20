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
	public class CreaturesDal : BaseDal<DefaultDbContext, Creature, Entities.Creature, int, CreaturesSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public CreaturesDal()
		{
		}

		protected internal CreaturesDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Creature entity, Creature dbObject, bool exists)
		{
			dbObject.Title = entity.Title;
			dbObject.FlavorText = entity.FlavorText;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Creature>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Creature> dbObjects, CreaturesSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Creature>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Creature> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Creature, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Creature, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Creature ConvertDbObjectToEntity(Creature dbObject)
		{
			return dbObject == null ? null : new Entities.Creature(dbObject.Id, dbObject.Title, dbObject.FlavorText);
		}
	}
}
