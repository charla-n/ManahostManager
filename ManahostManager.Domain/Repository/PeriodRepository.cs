using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManahostManager.Domain.Repository
{
    public interface IPeriodRepository : IAbstractRepository<Period>
    {
        Period GetPeriodById(int id, int idClient);

        List<Period> GetListPeriodBetween(DateTime begin, DateTime end);

        IEnumerable<Period> GetPeriodByDates(DateTime begin, DateTime end, int clientId);
    }

    public class PeriodRepository : AbstractRepository<Period>, IPeriodRepository
    {
        public PeriodRepository(ManahostManagerDAL ctx)
            : base(ctx)
        { }

        public Period GetPeriodById(int id, int idClient)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == idClient);
        }

        public List<Period> GetListPeriodBetween(DateTime begin, DateTime end)
        {
            return GetList(p => DateTime.Compare(begin, (DateTime)p.Begin) >= 0 &&
                                DateTime.Compare(end, (DateTime)p.End) <= 0).ToList();
        }

        public IEnumerable<Period> GetPeriodByDates(DateTime begin, DateTime end, int clientId)
        {
            return GetList<Period>(p => p.Home.ClientId == clientId && (
                p.Begin <= begin && p.End >= end || /* | ---- | */
                p.Begin <= begin && p.End >= begin && p.End <= end || /* | --|-- */
                p.Begin >= begin && p.Begin <= end && p.End >= end || /* --|-- | */
                p.Begin >= begin && p.End <= end && p.Begin <= end && p.End >= begin) /* -|--|- */);
        }

        public override void Delete(Period obj)
        {
            includes.Add("Period");
            DeleteRange<PricePerPerson>(GetList<PricePerPerson>(p => p.Period.Id == obj.Id));
            base.Delete(obj);
        }
    }
}