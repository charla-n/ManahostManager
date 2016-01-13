using System;
using System.Collections.Generic;

using System.Web.Http.ModelBinding;

namespace ManahostManager.Utils
{
    public class NullCheckValidation
    {
        public static void NullValidation(String prop, Dictionary<String, Object> properties, ModelStateDictionary validationDictionnary)
        {
            foreach (KeyValuePair<String, Object> cur in properties)
            {
                if (cur.Value == null || (cur.Value is String && ((String)cur.Value).Length == 0))
                {
                    validationDictionnary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, prop, cur.Key), GenericError.CANNOT_BE_NULL_OR_EMPTY);
                }
            }
        }
    }
}