using System.Threading.Tasks;
using Dal;
using Common.Search;
using Reader = Entities.Reader;

namespace BL
{
	public class ReadersBL
	{
		public async Task<int> AddOrUpdateAsync(Reader entity)
		{
			entity.Id = await new ReadersDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new ReadersDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(ReadersSearchParams searchParams)
		{
			return new ReadersDal().ExistsAsync(searchParams);
		}

		public Task<Reader> GetAsync(int id)
		{
			return new ReadersDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new ReadersDal().DeleteAsync(id);
		}

		public Task<SearchResult<Reader>> GetAsync(ReadersSearchParams searchParams)
		{
			return new ReadersDal().GetAsync(searchParams);
		}
	}
}

