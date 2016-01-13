using ManahostManager.Services;
using ManahostManager.Utils;
using ManahostManager.Utils.Attributs;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;

namespace ManahostManager.Controllers
{
    public class AdvSearch
    {
        public AdvSearch()
        {
            Include = new List<string>();
        }

        /// <summary>
        /// The search
        /// </summary>
        /// <remarks>
        /// Document/where/date/lt/2014-12-30
        /// Document/where/title/lk/Doc
        /// </remarks>
        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public String Search { get; set; }

        /// <summary>
        /// The list of nested entities you want to include (RoomBeds in Room, People in Product, ...)
        /// </summary>
        /// <remarks>
        /// The list of includes.<br/><para/>
        /// By default the API won't include nested entities. If you want nested entites you should specify them.<br/><para/>
        /// For the room entity if you want the list of beds and the roomcategory : ["RoomBeds", "RoomCategory"]
        /// </remarks>
        public List<String> Include { get; set; }
    }

    public class SearchController : AbstractAPIController
    {
        [Dependency]
        public ISearchService searchService { get; set; }

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="search"></param>
        /// <remarks>
        /// Permission REGISTERED and VIP
        /// <para/><para/><br/><br/>How can I use it ?
        /// <para/>
        /// <para><br/><br/>6 keywords to do researches</para>
        /// <para><br/>where, limit, and, or, count, Orderby</para>
        /// <para/>
        /// <para><br/><br/>8 operators</para>
        /// <para><br/>lt, eq, gt, ge, le, ne, in, lk</para>
        /// <para/>
        /// <para><br/><br/>All resources are available except Client, Home, Payment and Subscription</para>
        /// <para/>
        /// <para><br/><br/>Important notes on dates</para>
        /// <para><br/>The format is YYYYMMDD (YYYY = year / MM = month / DD = day) or YYYY-MM-DD (prefer this one).
        /// If you need to select on hours/minutes seconds the format is : YYYY-MM-DDTHH:mm:ss(where T is the delimitor, HH = hours, mm = minutes, ss = seconds)</para>
        /// <para/>
        /// <para><br/><br/>Important note on non alphanumeric characters</para>
        /// <para><br/>If you need to select something with non alphanumeric characters please url encode it.
        /// For exemple if you have a request like this : People/where/remark/eq/le 21/10/2013 il a mis le feu à la chambre it won't work you should do this People/where/remark/eq/le%2021%2F10%2F2013%20il%20a%20mis%20le%20feu%20%C3%A0%20la%20chambre</para>
        /// <para/>
        /// <para><br/><br/>Protections</para>
        /// <para><br/>Limit keyword is protected by limiting results at 100 rows max. It means you can't fetch more than 100 rows at a time, you need to use pagination !
        /// For example you can't do that /limit/5/200, the API will return you only 100 results max.
        /// <br/>Where keyword is protected by limiting results at 100 rows max. It means you can't fetch more than 100 rows at a time, you need to use pagination !
        /// On a where request which returns more than 100 results, you'll get only 100 results max.</para><para/><br/>
        /// <para/><br/>Document/where/date/lt/2014-12-30
        /// <para/><br/>Document/where/title/lk/Doc
        /// <para/><br/>Document/where/DocumentCategory.Title/eq/coucou
        /// <para/><br/>Document/Orderby/DocumentCategory.Id/asc
        /// <para/><br/>Document/where/date/lt/2014-12-30/and/date/gt/2013-12-30
        /// <para/><br/>Document/where/category_id/in/1,2,3,4,5/and/label/eq/coucou
        /// <para/><br/>Document/where/date/lt/2014-12-30/and/date/gt/2013-12-30/or/date/eq/0001-01-01
        /// <para/><br/>Document/where/category_id/in/1,2,3,4,5/and/label/eq/coucou
        /// <para/><br/>Document/where/category_id/in/1,2,3,4,5/or/label/eq/coucou
        /// <para/><br/>Document/where/date/gt/2013-12-30/and/label/eq/coucou/count
        /// <para/><br/>Document/where/label/eq/coucou/Orderby/date/desc
        /// <para/><br/>Document/where/label/eq/coucou/Orderby/date/desc/limit/10 (means take 10 results)
        /// <para/><br/>Document/where/label/eq/coucou/Orderby/date/desc/limit/5/10 (means from the offset 5 take 10 results)
        /// <para/><br/>Document/limit/5/10
        /// <para/><br/>Document/Orderby/id/asc/limit/5/10
        /// </remarks>
        /// <permission cref="GenericNames.REGISTERED_VIP">REGISTERED and VIP</permission>
        /// <response code="200">List of dynamic object</response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        /// <response code="400">KEY=AdvancedSearch.search VALUE=CANNOT_BE_NULL_OR_EMPTY (If search sent is null)
        /// <para/><br/><br/>KEY=AdvancedSearch.search VALUE = RESOURCE_DOES_NOT_EXIST_OR_CANT_BE_SEARCHED (You asked for a forbidden or a nonexistent resource (Dokument, ClIeNt, ...))
        /// <para/><br/><br/>KEY=AdvancedSearch.search VALUE = ADVANCED_SEARCH_SYNTAX_ERROR (There is a syntax error)
        /// <para/><br/><br/>KEY=AdvancedSearch.search VALUE = ADVANCED_SEARCH_SQL_EXCEPTION (A sql exception was raised it means maybe the property you asked (Date instead of DateUpload for example) doesn't exist or there is a syntax error (for dates for exemple if you input 30/12/2014 instead of 2014-12-30))
        /// </response>
        [ManahostAuthorize(Roles = GenericNames.REGISTERED_VIP)]
        public object Post([FromBody] AdvSearch search)
        {
            return searchService.GetService(ModelState, currentClient, search);
        }
    }
}