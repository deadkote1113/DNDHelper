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
	public class ReadersDal : BaseDal<DefaultDbContext, Reader, Entities.Reader, int, ReadersSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public ReadersDal()
		{
		}

		protected internal ReadersDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Reader entity, Reader dbObject, bool exists)
		{
			dbObject.Name = entity.Name;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Reader>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Reader> dbObjects, ReadersSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Reader>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Reader> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Reader, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Reader, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Reader ConvertDbObjectToEntity(Reader dbObject)
		{
			return dbObject == null ? null : new Entities.Reader(dbObject.Id, dbObject.Name);
		}
	}
}
