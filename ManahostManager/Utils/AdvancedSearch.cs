using ManahostManager.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Utils
{
    public interface IAdvancedSearch
    {
        SearchInfos Infos { get; set; }

        void ToSQL(String research, String clientId, ModelStateDictionary validationDictionnary);
    }

    public class AdvancedSearch : IAdvancedSearch
    {
        public static readonly int MAX_DEPTH = 2;
        public static readonly int MAX_LIMIT_FETCH = 100;
        private static readonly String regexProp = @"^[a-zA-Z0-9_\.]*$";
        private static readonly Char[] delimitor = { '/' };
        private static readonly Char[] inDelimitor = { ',' };
        private static readonly String[] keywords = new String[] { "/limit", "/where", "/and", "/or", "/count", "/Orderby" };

        private static readonly String[] resources = { "AdditionalBooking", "Bed", "Bill", "BillItem", "BillItemCategory", "Booking", "BookingDocument",
                                                "BookingStep", "BookingStepConfig", "BookingStepBooking", "Deposit", "DinnerBooking", "Document", "DocumentCategory",
                                                "GroupBillItem", "FieldGroup", "HomeConfig", "KeyGenerator", "MailConfig", "Meal", "MealBooking", "MealCategory",
                                                "MealPrice", "PaymentMethod", "PaymentType", "People", "PeopleBooking", "PeopleCategory", "PeopleField", "Period",
                                                "PricePerPerson", "Product", "ProductBooking", "ProductCategory", "Room", "RoomBooking", "RoomCategory", "RoomSupplement",
                                                "SatisfactionClient", "SatisfactionClientAnswer", "SatisfactionConfig", "SatisfactionConfigQuestion", "MStatistics",
                                                "SupplementRoomBooking", "Supplier", "Tax", "MailLog"};

        private static Dictionary<String, String> operators = new Dictionary<string, string>()
        {
            {"lt", " < {0}"},
            {"eq", " == {0}"},
            {"gt", " > {0}"},
            {"ge", " >= {0}"},
            {"le", " <= {0}"},
            {"ne", " != {0}"},
            {"lk", ".Contains({0})"},
            {"in", "in"}
        };

        public SearchInfos Infos { get; set; }

        private Dictionary<String, Func<int, bool>> dict;
        private String[] splitted;
        private String clientId;
        private bool where;
        private bool limit;
        private bool orderby;
        private bool count;
        private bool appendHomeCalled;

        public AdvancedSearch()
        {
            Infos = new SearchInfos();
            Infos.skip = 0;
            Infos.take = 100;
            count = false;
            where = false;
            limit = false;
            orderby = false;
            appendHomeCalled = false;
            dict = new Dictionary<string, Func<int, bool>>()
            {
                {keywords[0], new Func<int, bool>(limitFunc)},
                {keywords[1], new Func<int, bool>(whereFunc)},
                {keywords[2], new Func<int, bool>(andFunc)},
                {keywords[3], new Func<int, bool>(orFunc)},
                {keywords[4], new Func<int, bool>(countFunc)},
                {keywords[5], new Func<int, bool>(orderbyFunc)},
            };
        }

        public void ToSQL(String research, String clientId, ModelStateDictionary validationDictionnary)
        {
            if (research == null)
            {
                validationDictionnary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, GenericNames.ADVANCED_SEARCH, "search"), GenericError.CANNOT_BE_NULL_OR_EMPTY);
                return;
            }
            this.clientId = clientId;
            splitted = Regex.Split(research, String.Format(@"({0})", String.Join("|", keywords)));
            if (addResource() == false)
            {
                validationDictionnary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, GenericNames.ADVANCED_SEARCH, "search"), GenericError.RESOURCE_DOES_NOT_EXIST_OR_CANT_BE_SEARCHED);
                return;
            }
            for (int i = 1; i < splitted.Length; i++)
            {
                String[] partSplit = splitted[i].Split(delimitor, StringSplitOptions.RemoveEmptyEntries);
                Func<int, bool> val = null;

                dict.TryGetValue("/" + partSplit[0], out val);
                if (val == null)
                {
                    validationDictionnary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, GenericNames.ADVANCED_SEARCH, "search"), GenericError.ADVANCED_SEARCH_SYNTAX_ERROR);
                    return;
                }
                if (val.Invoke(i) == false)
                {
                    validationDictionnary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, GenericNames.ADVANCED_SEARCH, "search"), GenericError.ADVANCED_SEARCH_SYNTAX_ERROR);
                    return;
                }
                i++;
            }
            if (appendHomeCalled == false)
                appendHome();
            if (limit == false)
                appendLimit();
        }

        private bool addResource()
        {
            String[] resource = splitted[0].Split(delimitor);

            if (!resources.Contains(resource[0]))
                return false;
            Infos.resource = resource[0];
            return true;
        }

        private bool limitFunc(int offset)
        {
            int n1 = 0;
            int n2 = 0;
            if (count)
                return false;
            if (offset + 1 == splitted.Length)
                return false;
            String[] limitClause = splitted[offset + 1].Split(delimitor, StringSplitOptions.RemoveEmptyEntries);
            if (limitClause.Length < 1)
                return false;
            if (orderby == false)
            {
                appendHome();
                orderby = true;
                Infos.orderByClause = "id ascending";
            }
            if (int.TryParse(limitClause[0], out n1) == false || n1 < 0 || (limitClause.Length == 1 && n1 <= 0))
                return false;
            if (limitClause.Length > 1 && (int.TryParse(limitClause[1], out n2) == false || n2 <= 0))
                return false;
            if (limitClause.Length == 1 && n1 > MAX_LIMIT_FETCH)
                n1 = MAX_LIMIT_FETCH;
            else if ((n2 - n1) > MAX_LIMIT_FETCH)
                n2 = n1 + MAX_LIMIT_FETCH;
            if (limitClause.Length == 1)
            {
                Infos.skip = 0;
                Infos.take = n1;
            }
            else
            {
                Infos.skip = n1;
                Infos.take = n2;
            }
            limit = true;
            return true;
        }

        private bool whereFunc(int offset)
        {
            int dots = 0;

            if (splitted.Length - 2 < offset)
                return false;
            String[] whereClause = splitted[offset + 1].Split(delimitor, StringSplitOptions.RemoveEmptyEntries);
            String op = null;
            if (whereClause.Length != 3)
                return false;
            whereClause[2] = WebUtility.UrlDecode(whereClause[2]);
            if (!Regex.Match(whereClause[0], regexProp).Success)
                return false;
            if ((dots = whereClause[0].Count(p => p == SearchRepository.navigationPropertyDelimitor[0])) >= 1)
            {
                if (dots > MAX_DEPTH)
                    return false;
                DealWithDot(whereClause[0]);
            }
            operators.TryGetValue(whereClause[1], out op);
            if (op == null)
                return false;
            if ("in".Equals(op))
            {
                if (!DealWithIn(whereClause, " == {0}"))
                    return false;
            }
            else
            {
                if (!DealWithNull(whereClause[0], whereClause[2], op))
                {
                    Infos.whereClause.Append(whereClause[0]);
                    Infos.whereClause.Append(String.Format(op, "@" + Infos.whereParameters.Count));
                    Infos.whereParametersNames.Add(whereClause[0]);
                    Infos.whereParameters.Add(whereClause[2]);
                }
            }
            where = true;
            return true;
        }

        private void DealWithDot(String prop)
        {
            string[] propSplitted = prop.Split(SearchRepository.navigationPropertyDelimitor);

            for (int i = 0; i < propSplitted.Length; i++)
            {
                if (propSplitted.Length == (i + 1))
                    break;
                Infos.includes.Add(propSplitted[i]);
            }
        }

        private bool DealWithNull(String prop, String part, String op)
        {
            if ("null".Equals(part))
            {
                Infos.whereClause.Append(prop);
                Infos.whereClause.Append(String.Format(op, "null"));
                return true;
            }
            return false;
        }

        private bool DealWithIn(String[] whereClause, String op)
        {
            String[] inSplitted = whereClause[2].Split(inDelimitor);

            if (inSplitted.Length < 1)
                return false;

            Infos.whereClause.Append("(");
            if (!DealWithNull(whereClause[0], inSplitted[0], op))
            {
                Infos.whereClause.Append(String.Format("{0} == @{1}", whereClause[0], Infos.whereParameters.Count));
                Infos.whereParametersNames.Add(whereClause[0]);
                Infos.whereParameters.Add(inSplitted[0]);
            }
            for (int i = 1; i < inSplitted.Length; i++)
            {
                if (!DealWithNull(" || " + whereClause[0], inSplitted[i], op))
                {
                    Infos.whereClause.Append(String.Format(" || {0} == @{1}", whereClause[0], Infos.whereParameters.Count));
                    Infos.whereParametersNames.Add(whereClause[0]);
                    Infos.whereParameters.Add(inSplitted[i]);
                }
            }
            Infos.whereClause.Append(")");
            return true;
        }

        private bool andFunc(int offset)
        {
            Infos.whereClause.Append(" && ");
            return whereFunc(offset);
        }

        private bool orFunc(int offset)
        {
            appendHome();
            appendHomeCalled = false;
            Infos.whereClause.Append(" || ");
            if (whereFunc(offset) == false)
                return false;
            return true;
        }

        private bool countFunc(int offset)
        {
            if (limit || orderby)
                return false;
            if (splitted.Length - 2 != offset)
                return false;
            Infos.count = true;
            appendHome();
            count = true;
            return true;
        }

        private bool orderbyFunc(int offset)
        {
            int dots = 0;

            if (count)
                return false;
            if (orderby == true)
                return false;
            if (limit == true)
                return false;
            if (offset + 1 == splitted.Length)
                return false;
            String[] orderbyClause = splitted[offset + 1].Split(delimitor, StringSplitOptions.RemoveEmptyEntries);
            if (orderbyClause.Length != 2)
                return false;
            if ((dots = orderbyClause[0].Count(p => p == SearchRepository.navigationPropertyDelimitor[0])) >= 1)
            {
                if (dots > MAX_DEPTH)
                    return false;
                DealWithDot(orderbyClause[0]);
            }
            if (!Regex.Match(orderbyClause[0], regexProp).Success || (orderbyClause[1] != "asc" && orderbyClause[1] != "desc"))
                return false;
            appendHome();
            Infos.orderByClause = orderbyClause[0] + (orderbyClause[1].Equals("asc") ? " ascending" : " descending");
            orderby = true;
            return true;
        }

        private void appendHome()
        {
            appendHomeCalled = true;
            if (where)
                Infos.whereClause.Append(" && ");
            Infos.includes.Add("Home");
            Infos.whereClause.Append("Home.ClientId == " + clientId);
        }

        private void appendLimit()
        {
            if (!limit && !count)
            {
                limit = true;
                if (orderby == false)
                {
                    orderby = true;
                    Infos.orderByClause = "id ascending";
                }
                Infos.skip = 0;
                Infos.take = MAX_LIMIT_FETCH;
            }
        }
    }
}