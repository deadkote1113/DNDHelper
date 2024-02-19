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
	public class PicturesDal : BaseDal<DefaultDbContext, Picture, Entities.Picture, int, PicturesSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public PicturesDal()
		{
		}

		protected internal PicturesDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Picture entity, Picture dbObject, bool exists)
		{
			dbObject.Title = entity.Title;
			dbObject.PicturePath = entity.PicturePath;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Picture>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Picture> dbObjects, PicturesSearchParams searchParams)
		{
			if(string.IsNullOrEmpty(searchParams.SearchQuery) == false)
			{
				dbObjects = dbObjects.Where(item => item.Title.Contains(searchParams.SearchQuery));
			}
			dbObjects = dbObjects.OrderByDescending(item => item.Id);
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Picture>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Picture> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Picture, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Picture, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Picture ConvertDbObjectToEntity(Picture dbObject)
		{
			return dbObject == null ? null : new Entities.Picture(dbObject.Id, dbObject.Title, dbObject.PicturePath);
		}
	}
}
