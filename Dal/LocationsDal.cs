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
	public class LocationsDal : BaseDal<DefaultDbContext, Location, Entities.Location, int, LocationsSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public LocationsDal()
		{
		}

		protected internal LocationsDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Location entity, Location dbObject, bool exists)
		{
			dbObject.Title = entity.Title;
			dbObject.FlavorText = entity.FlavorText;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Location>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Location> dbObjects, LocationsSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Location>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Location> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Location, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Location, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Location ConvertDbObjectToEntity(Location dbObject)
		{
			return dbObject == null ? null : new Entities.Location(dbObject.Id, dbObject.Title, dbObject.FlavorText);
		}
	}
}
