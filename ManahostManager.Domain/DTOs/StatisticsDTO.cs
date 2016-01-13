using ExpressiveAnnotations.Attributes;
using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using System;

namespace ManahostManager.Domain.DTOs
{
    public class MStatisticsDTO : IDTO
    {
        public int? Id { get; set; }

        // This field will be automatically computed by the API
        // {"Q":["Q1","Q2","Q3"],"A":[[5],[8],[0,2,0,3,4,1]],"T":10}
        // or
        // {"NumberCancelled": "3", "Online": "0", "TotalNight": "10", "TotalRes": "5", "TotalPeople": "9", "PeopleCategories": [{"Title": "Enfants", "Number": 1}, {"Title" : "Adultes", "Number": 8}],
        // "Products": [{"Title" : "Massage", "Number": 5}, {"Title" : "Choucroute", "Number": 9}], "TotalProducts": 14,
        // "Rooms": [{"Title": "Annabelle", "Days": [0, 0, 0, 0, 0, 2, 2]}, {"Title": "Corinne", "Days": [0, 0, 0, 0, 0, 3, 3]}, {"Title": "Cédric", "Days": [0, 0, 0, 0, 0, 0, 0]}], "TotalRooms": 10,
        // "Dinners": [[0, 0, 0, 0, 0, 3, 4], [0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0]], "TotalDinners": 7}
        // or
        // {Income:5500, Outcome: 1080, "Rooms" : [{"Title": "Annabelle", "Income": 2800}, {"Title": "Corinne", "Income": 200}],
        // "Products" : [{"Title": "Massage", "Income": 250}],
        // "Dinners" : [750, 250, 500], Other: 250}
        // Please take a look on http://confluence.manahost.fr/pages/viewpage.action?pageId=4948288
        public String Data { get; set; }

        // Set by the API at utcnow
        public DateTime Date { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // See above
        public StatisticsTypes Type { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}