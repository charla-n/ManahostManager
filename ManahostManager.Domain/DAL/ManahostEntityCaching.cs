using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ManahostManager.Domain.DAL
{
    public sealed class ManahostEntityCaching
    {
        private static object LOCKING_OBJ = new Object();
        private static readonly ManahostEntityCaching INSTANCE = new ManahostEntityCaching();

        private Dictionary<Type, PropertyInfo[]> manahostEntities;

        static ManahostEntityCaching()
        { }

        private ManahostEntityCaching()
        {
            manahostEntities = new Dictionary<Type, PropertyInfo[]>();
        }

        public static ManahostEntityCaching Instance
        {
            get
            {
                return INSTANCE;
            }
        }

        private void EntityExists(Type t)
        {
            if (manahostEntities.ContainsKey(t) == false)
                manahostEntities.Add(t, t.GetProperties());
        }

        public Type GetTypeFromFullPath(String path)
        {
            lock (LOCKING_OBJ)
            {
                Type ret = Type.GetType(path);

                if (ret == null)
                    return null;
                EntityExists(ret);
                return ret;
            }
        }

        public PropertyInfo[] RetrievePropertiesInfoFromType(Type t)
        {
            lock (LOCKING_OBJ)
            {
                PropertyInfo[] ret = null;

                EntityExists(t);
                manahostEntities.TryGetValue(t, out ret);
                return ret;
            }
        }

        public PropertyInfo RetrievePropertyInfoFromFieldName(Type t, String field)
        {
            lock (LOCKING_OBJ)
            {
                PropertyInfo[] ret = null;

                EntityExists(t);
                manahostEntities.TryGetValue(t, out ret);
                return ret.FirstOrDefault(p => p.Name.Equals(field));
            }
        }
    }
}