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
	public class LandscapesDal : BaseDal<DefaultDbContext, Landscape, Entities.Landscape, int, LandscapesSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public LandscapesDal()
		{
		}

		protected internal LandscapesDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Landscape entity, Landscape dbObject, bool exists)
		{
			dbObject.Title = entity.Title;
			dbObject.FlavorText = entity.FlavorText;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Landscape>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Landscape> dbObjects, LandscapesSearchParams searchParams)
		{
			dbObjects = dbObjects.OrderByDescending(item => item.Id);
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Landscape>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Landscape> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Landscape, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Landscape, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Landscape ConvertDbObjectToEntity(Landscape dbObject)
		{
			return dbObject == null ? null : new Entities.Landscape(dbObject.Id, dbObject.Title, dbObject.FlavorText);
		}
	}
}
