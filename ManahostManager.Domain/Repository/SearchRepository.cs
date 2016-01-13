using ManahostManager.Domain.DAL;
using ManahostManager.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;

namespace ManahostManager.Domain.Repository
{
    public interface ISearchRepository : IAbstractRepository<Object>
    {
        IEnumerable<dynamic> Search(SearchInfos infos);
    }

    public class SearchInfos
    {
        public SearchInfos()
        {
            whereClause = new StringBuilder();
            whereParameters = new List<object>();
            whereParametersNames = new List<string>();
            includes = new HashSet<string>();
            count = false;
        }

        public string resource;

        public StringBuilder whereClause;
        public List<Object> whereParameters;
        public List<string> whereParametersNames;

        public string orderByClause;

        public bool count;

        public int skip;
        public int take;

        public HashSet<string> includes;
    }

    public class SearchRepository : AbstractRepository<Object>, ISearchRepository
    {
        public static Char[] navigationPropertyDelimitor = { '.' };

        public SearchRepository(ManahostManagerDAL ctx) : base(ctx)
        {
        }

        public IEnumerable<dynamic> Search(SearchInfos infos)
        {
            Type type = null;

            for (int i = 0; i < infos.whereParameters.Count; i++)
            {
                var paramValue = infos.whereParameters[i];
                var paramName = infos.whereParametersNames[i];

                type = ManahostEntityCaching.Instance.GetTypeFromFullPath(GenericNames.ENTITY_PATH + infos.resource);
                if (paramName.Contains(navigationPropertyDelimitor[0]))
                {
                    string[] splittedParamName = paramName.Split(navigationPropertyDelimitor);
                    paramName = splittedParamName[splittedParamName.Length - 1];
                    if (splittedParamName.Length > 1)
                        type = ManahostEntityCaching.Instance.GetTypeFromFullPath(GenericNames.ENTITY_PATH + splittedParamName[splittedParamName.Length - 2]);
                }
                PropertyInfo pInfo = null;
                Type t = null;
                Type underlyingType = null;

                if ((pInfo = ManahostEntityCaching.Instance.RetrievePropertiesInfoFromType(type).FirstOrDefault(p => p.Name.Equals(paramName, StringComparison.InvariantCultureIgnoreCase))) == null)
                    return null;
                t = pInfo.PropertyType;
                if ((underlyingType = Nullable.GetUnderlyingType(t)) != null)
                    infos.whereParameters[i] = Convert.ChangeType(paramValue, underlyingType);
                else
                    infos.whereParameters[i] = Convert.ChangeType(paramValue, pInfo.PropertyType);
            }

            type = ManahostEntityCaching.Instance.GetTypeFromFullPath(GenericNames.ENTITY_PATH + infos.resource);
            if (!infos.count && infos.includes.Count == 0)
            {
                IEnumerable finalQuery = RetrieveContext().Set(type).Where(infos.whereClause.ToString(), infos.whereParameters.ToArray()).
                    OrderBy(infos.orderByClause).Skip(infos.skip).Take(infos.take);
                return finalQuery.Cast<dynamic>();
            }
            if (!infos.count)
            {
                IEnumerable finalQuery = Include(RetrieveContext().Set(type), infos.includes).Where(infos.whereClause.ToString(), infos.whereParameters.ToArray()).
                    OrderBy(infos.orderByClause).Skip(infos.skip).Take(infos.take);
                return finalQuery.Cast<dynamic>();
            }
            return new List<dynamic>() { RetrieveContext().Set(type).Where(infos.whereClause.ToString(), infos.whereParameters.ToArray()).Count() };
        }

        private DbQuery Include(DbQuery set, HashSet<string> includes)
        {
            foreach (string cur in includes)
                set = set.Include(cur);
            return set;
        }
    }
}