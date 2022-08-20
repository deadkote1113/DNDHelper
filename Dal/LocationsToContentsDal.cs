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
	public class LocationsToContentsDal : BaseDal<DefaultDbContext, LocationsToContent, Entities.LocationsToContent, int, LocationsToContentsSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public LocationsToContentsDal()
		{
		}

		protected internal LocationsToContentsDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.LocationsToContent entity, LocationsToContent dbObject, bool exists)
		{
			dbObject.Title = entity.Title;
			dbObject.LocationId = entity.LocationId;
			dbObject.StructureId = entity.StructureId;
			dbObject.LandscapeId = entity.LandscapeId;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<LocationsToContent>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<LocationsToContent> dbObjects, LocationsToContentsSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.LocationsToContent>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<LocationsToContent> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<LocationsToContent, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.LocationsToContent, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.LocationsToContent ConvertDbObjectToEntity(LocationsToContent dbObject)
		{
			return dbObject == null ? null : new Entities.LocationsToContent(dbObject.Id, dbObject.Title,
				dbObject.LocationId, dbObject.StructureId, dbObject.LandscapeId);
		}
	}
}
