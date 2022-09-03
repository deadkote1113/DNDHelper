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
	public class ItemsDal : BaseDal<DefaultDbContext, Item, Entities.Item, int, ItemsSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public ItemsDal()
		{
		}

		protected internal ItemsDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Item entity, Item dbObject, bool exists)
		{
			dbObject.Title = entity.Title;
			dbObject.FlavorText = entity.FlavorText;
			dbObject.CreaturesId = entity.CreaturesId;
			dbObject.StructuresId = entity.StructuresId;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Item>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Item> dbObjects, ItemsSearchParams searchParams)
		{
			dbObjects = dbObjects.OrderByDescending(item => item.Id);
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Item>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Item> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Item, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Item, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Item ConvertDbObjectToEntity(Item dbObject)
		{
			return dbObject == null ? null : new Entities.Item(dbObject.Id, dbObject.Title, dbObject.FlavorText,
				dbObject.CreaturesId, dbObject.StructuresId);
		}
	}
}
