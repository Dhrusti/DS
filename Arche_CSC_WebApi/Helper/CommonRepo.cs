using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace Helper
{
	public class CommonRepo
	{
		private readonly ArcheCountryStateCityDbContext _dbContext;
		public CommonRepo(ArcheCountryStateCityDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IQueryable<CountryMst> countryList(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.CountryMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<StateMst> stateList(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.StateMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<CityMst> cityList(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.CityMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}


	}
}
