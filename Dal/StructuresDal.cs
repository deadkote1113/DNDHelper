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
	public class StructuresDal : BaseDal<DefaultDbContext, Structure, Entities.Structure, int, StructuresSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public StructuresDal()
		{
		}

		protected internal StructuresDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Structure entity, Structure dbObject, bool exists)
		{
			dbObject.Title = entity.Title;
			dbObject.FlavorText = entity.FlavorText;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Structure>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Structure> dbObjects, StructuresSearchParams searchParams)
		{
			dbObjects = dbObjects.OrderByDescending(item => item.Id);
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Structure>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Structure> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Structure, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Structure, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Structure ConvertDbObjectToEntity(Structure dbObject)
		{
			return dbObject == null ? null : new Entities.Structure(dbObject.Id, dbObject.Title, dbObject.FlavorText);
		}

		internal static Entities.Structure ConvertDbObjectToEntityWithInnerEntites(Structure dbObject)
		{
			return dbObject == null ? null : new Entities.Structure(dbObject.Id, dbObject.Title, dbObject.FlavorText)
			{
				StructuresToItemsOrCreatures = dbObject.StructuresToItemsOrCreatures
					.Select(StructuresToItemsOrCreaturesDal.ConvertDbObjectToEntityWithInnerEntites).ToList()
			};
		}
	}
}
