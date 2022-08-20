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
	public class StructuresToItemsOrCreaturesDal : BaseDal<DefaultDbContext, StructuresToItemsOrCreature, Entities.StructuresToItemsOrCreature, int, StructuresToItemsOrCreaturesSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public StructuresToItemsOrCreaturesDal()
		{
		}

		protected internal StructuresToItemsOrCreaturesDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.StructuresToItemsOrCreature entity, StructuresToItemsOrCreature dbObject, bool exists)
		{
			dbObject.StructureId = entity.StructureId;
			dbObject.ItemId = entity.ItemId;
			dbObject.CreatureId = entity.CreatureId;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<StructuresToItemsOrCreature>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<StructuresToItemsOrCreature> dbObjects, StructuresToItemsOrCreaturesSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.StructuresToItemsOrCreature>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<StructuresToItemsOrCreature> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<StructuresToItemsOrCreature, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.StructuresToItemsOrCreature, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.StructuresToItemsOrCreature ConvertDbObjectToEntity(StructuresToItemsOrCreature dbObject)
		{
			return dbObject == null ? null : new Entities.StructuresToItemsOrCreature(dbObject.Id,
				dbObject.StructureId, dbObject.ItemId, dbObject.CreatureId);
		}
	}
}
