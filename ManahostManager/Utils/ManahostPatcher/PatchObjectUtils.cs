using ManahostManager.Domain.DAL;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ManahostManager.Utils.ManahostPatcher
{
    public class PatchObjectUtils<T> where T : class
    {
        private static string[] sforbiddenReplacement = { "ClientId", "Id", "DateCreation" };

        public static void PatchObject(T entity, ManahostPatcherModel[] operations)
        {
            ManahostEntityCaching instance = ManahostEntityCaching.Instance;
            Type entityType = typeof(T);

            foreach (ManahostPatcherModel operation in operations)
            {
                PatchField(entity, instance, entityType, operation.Field, operation.Value);
            }
        }

        private static void PatchField(T entity, ManahostEntityCaching instance, Type entityType, String field, Object value)
        {
            PropertyInfo pInfoFromField = instance.RetrievePropertyInfoFromFieldName(entityType, field);

            if (pInfoFromField == null || Attribute.IsDefined(pInfoFromField, typeof(NotMappedAttribute)) ||
                Attribute.IsDefined(pInfoFromField, typeof(IgnoreDataMemberAttribute)) ||
                sforbiddenReplacement.Contains(field))
                return;
            pInfoFromField.SetValue(entity, value);
        }

        public static void ReplacementOrigByGiven(T given, T orig, params string[] forbiddenReplacement)
        {
            foreach (var fromProp in typeof(T).GetProperties())
            {
                if (forbiddenReplacement.Contains<string>(fromProp.Name) || sforbiddenReplacement.Contains<string>(fromProp.Name))
                    continue;
                var toProp = typeof(T).GetProperty(fromProp.Name);
                if (Attribute.IsDefined(toProp, typeof(NotMappedAttribute)))
                    continue;
                if (sforbiddenReplacement.Contains(fromProp.Name) || forbiddenReplacement.Contains(fromProp.Name))
                    continue;
                var toValue = toProp.GetValue(given, null);
                if (!Attribute.IsDefined(toProp, typeof(IgnoreDataMemberAttribute)))
                {
                    fromProp.SetValue(orig, toValue, null);
                }
            }
        }
    }
}