using System.Collections.Generic;
using System.Net.Http;

namespace ManahostManager.Utils
{
    public class GenericError
    {
        public const string INVALID_HEADER = "1";
        public const string INVALID_GIVEN_PARAMETER = "2";
        public const string ACCOUNT_DISABLED = "3";
        public const string NEED_CAPTCHA = "4";
        public const string CANNOT_BE_NULL_OR_EMPTY = "5";
        public const string DOES_NOT_MEET_REQUIREMENTS = "6";
        public const string ALREADY_EXISTS = "7";
        public const string FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST = "8";
        public const string RESOURCE_UNREACHABLE = "9";
        public const string INVALID_SEARCH = "10";
        public const string TOKEN_NOT_EXPIRED = "11";
        public const string TOKEN_EXPIRED = "12";
        public const string TOKEN_NOT_GENERATED = "13";
        /* Old reCaptcha */
        public const string CAPTCHA_INVALID_PRIV_KEY = "14";
        public const string CAPTCHA_INVALID_CHALLENGE = "15";
        public const string CAPTCHA_INVALID_SOLUTION = "16";
        public const string CAPTCHA_TIMEOUT = "17";
        public const string CAPTCHA_NOT_REACHABLE = "18";
        /* ! reCapcha */
        public const string TOKEN_OVER_USED = "19";
        public const string RESOURCE_DOES_NOT_EXIST_OR_CANT_BE_SEARCHED = "20";
        public const string ADVANCED_SEARCH_SYNTAX_ERROR = "21";
        public const string ADVANCED_SEARCH_SQL_EXCEPTION = "22";
        public const string DEFAULT_HOME_ID_NOT_SET = "23";
        public const string WRONG_DATA = "24";
        public const string NOT_AUTHORIZED = "25";
        public const string CAPTCHA_MISSING_SECRET = "26";
        public const string CAPTCHA_INVALID_SECRET = "27";
        public const string CAPTCHA_MISSING_RESPONSE = "28";
        public const string CAPTCHA_INVALID_RESPONSE = "29";
        public const string CAPTCHA_UNKNOWN_ERROR = "30";
        public const string THROTTLING_UNKNOWN_CLIENT = "31";
        public const string THROTTLING_EXCCEEDED_REQUEST_COUNT = "32";

        // The file uploaded isn't found
        public const string FILE_NOT_FOUND = "33";

        // The client is above his upload limit
        public const string UPLOAD_LIMIT = "34";

        public const string ALREADY_INIT = "35";

        public const string CLIENT_DISABLED = "36";

        public const string EMAIL_NOT_CONFIRMED = "37";

        public const string SQLEXCEPTION = "38";
    }

    public class GenericNames
    {
        public static readonly HttpMethod PATCH_VERB = new HttpMethod("PATCH");

        public const string DISABLED = "DISABLED";
        public const string MANAGER = "MANAGER";
        public const string REGISTERED_VIP = "REGISTERED";
        public const string VIP = "VIP";
        public const string ADMINISTRATOR = "ADMINISTRATOR";

        public const string MANAGER_REGISTERED = "MANAGER,REGISTERED";
        public const string MANAGER_REGISTERED_VIP = "MANAGER,REGISTERED,VIP";

        public const string ADVANCED_SEARCH = "AdvancedSearch";
        public const string THROTTLING = "Throttling";
        public const string HEADER_AUTHORIZATION = "Authorization";
        public const string AUTHENTICATION = "Authentication";
        public const string CAPTCHA = "Captcha";
        public const string HEADER_PASSWORD = "X-Password";
        public const string TOKEN_HEADER = "Token";
        public const string TOKEN_EXP_HEADER = "Token-Expiration";
        public const string APP_JSON = "application/json";
        public const string APP_MSGPACK = "application/x-msgpack";
        public const string MANAHOST_THROTTLING_HEADER = "Manahost-GUID-Throttling";
        public const string MANAHOST_SERV_CREDENTIALS = "Manahost-Serv-Credentials";
        public static readonly string ENTITY_PATH = "ManahostManager.Domain.Entity.";
        public static readonly string DTO_PATH = "ManahostManager.Domain.DTOs.";

        // ENTITY.PROPERTY
        public const string MODEL_STATE_FORMAT = "{0}.{1}";

        public const string OWIN_CONTEXT_CORS = "as:AllowedOrigin";
        public const string OWIN_CONTEXT_REFRESH_TOKEN_LIFETIME = "as:RefreshTokenLifeTime";
        public const string OWIN_CONTEXT_CORS_HEADER = "Access-Control-Allow-Origin";
        public const string AUTHENTICATION_CLIENT_ID_KEY = "client_id";
        public const string AUTHENTICATION_EXTERNAL_LOGIN = "ExternalAccessToken";
        public const string AUTHENTICATION_REFRESH_TOKEN_LIFETIME = "refresh_expires_in";

        public const string GOOGLE_CLIENT_ID = "GOOGLE_CLIENT_ID";
        public const string GOOGLE_CLIENT_SECRET = "GOOGLE_CLIENT_SECRET";
        public const string GOOGLE_RECAPTCHA_FORM = "g-recaptcha-response";
        public const string GOOGLE_RECAPTCHA_SECRET = "GOOGLE_RECAPTCHA_SECRET";
        public const string CAPTCHA_FAILED_COUNT = "CAPTCHA_FAILED_COUNT";

        public const string ONLY_NUMBER_REGEX = @"^[0-9]*$";

        public static readonly HashSet<string> ALLOWED_DTO_PATH = new HashSet<string>()
        {
            "AdditionalBookingDTO.BookingDTO",
            "AdditionalBookingDTO.BillItemCategoryDTO",
            "AdditionalBookingDTO.TaxDTO",
            "BedDTO.RoomDTO",
            "BedDTO.RoomDTO.BillItemCategoryDTO",
            "BedDTO.RoomDTO.RoomCategoryDTO",
            "BedDTO.RoomDTO.BedDTO",
            "BedDTO.RoomDTO.DocumentDTO",
            "ProductDTO.BillItemCategoryDTO",
            "ProductDTO.ProductCategoryDTO",
            "ProductDTO.PeopleDTO",
            "ProductDTO.SupplierDTO",
            "ProductDTO.TaxDTO",
            "ProductDTO.DocumentDTO",
            "ProductDTO.DocumentDTO.DocumentCategoryDTO",
            "RoomDTO.BillItemCategoryDTO",
            "RoomDTO.RoomCategoryDTO",
            "RoomDTO.BedDTO",
            "RoomDTO.BedDTO.RoomDTO",
            "RoomDTO.DocumentDTO",
            "RoomDTO.DocumentDTO.DocumentCategoryDTO",
            "BookingDTO.PeopleDTO",
            "BookingDTO.AdditionalBookingDTO",
            "BookingDTO.AdditionalBookingDTO.BillItemCategoryDTO",
            "BookingDTO.AdditionalBookingDTO.TaxDTO",
            "BookingDTO.BookingStepBookingDTO",
            "BookingDTO.BookingStepBookingDTO.BookingStepConfigDTO",
            "BookingDTO.BookingStepBookingDTO.BookingStepDTO",
            "BookingDTO.DepositDTO",
            "BookingDTO.DinnerBookingDTO",
            "BookingDTO.DinnerBookingDTO.MealBookingDTO",
            "BookingDTO.DinnerBookingDTO.MealBookingDTO.MealDTO",
            "BookingDTO.DinnerBookingDTO.MealBookingDTO.PeopleCategoryDTO",
            "BookingDTO.DinnerBookingDTO.MealBookingDTO.PeopleCategoryDTO.TaxDTO",
            "BookingDTO.ProductBookingDTO",
            "BookingDTO.ProductBookingDTO.ProductDTO",
            "BookingDTO.ProductBookingDTO.ProductDTO.BillItemCategoryDTO",
            "BookingDTO.ProductBookingDTO.ProductDTO.ProductCategoryDTO",
            "BookingDTO.ProductBookingDTO.ProductDTO.PeopleDTO",
            "BookingDTO.ProductBookingDTO.ProductDTO.SupplierDTO",
            "BookingDTO.ProductBookingDTO.ProductDTO.TaxDTO",
            "BookingDTO.ProductBookingDTO.ProductDTO.DocumentDTO",
            "BookingDTO.RoomBookingDTO",
            "BookingDTO.RoomBookingDTO.RoomDTO",
            "BookingDTO.RoomBookingDTO.RoomDTO.BillItemCategoryDTO",
            "BookingDTO.RoomBookingDTO.RoomDTO.RoomCategoryDTO",
            "BookingDTO.RoomBookingDTO.RoomDTO.BedDTO",
            "BookingDTO.RoomBookingDTO.RoomDTO.BedDTO.RoomDTO",
            "BookingDTO.RoomBookingDTO.RoomDTO.DocumentDTO",
            "BookingDTO.RoomBookingDTO.PeopleBookingDTO",
            "BookingDTO.RoomBookingDTO.PeopleBookingDTO.PeopleCategoryDTO",
            "BookingDTO.RoomBookingDTO.PeopleBookingDTO.PeopleCategoryDTO.TaxDTO",
            "BookingDTO.RoomBookingDTO.SupplementRoomBookingDTO",
            "BookingDTO.RoomBookingDTO.SupplementRoomBookingDTO.RoomSupplementDTO",
            "BookingDTO.SupplementRoomBookingDTO",
            "BookingDTO.SupplementRoomBookingDTO.RoomSupplementDTO",
            "BookingDTO.SupplementRoomBookingDTO.RoomSupplementDTO.TaxDTO",
            "BookingStepBookingDTO.BookingDTO",
            "BookingStepBookingDTO.BookingStepConfigDTO",
            "BookingStepBookingDTO.BookingStepDTO",
            "BookingStepBookingDTO.BookingStepConfigDTO.BookingStepDTO",
            "BookingStepDTO.BookingStepConfigDTO",
            "BookingStepConfigDTO.BookingStepDTO",
            "BookingStepConfigDTO.BookingStepDTO.BookingStepConfigDTO",
            "BookingStepConfigDTO.BookingStepDTO.DocumentDTO",
            "BookingStepDTO.DocumentDTO",
            "DepositDTO.BookingDTO",
            "DinnerBookingDTO.BookingDTO",
            "DinnerBookingDTO.MealBookingDTO",
            "DinnerBookingDTO.MealBookingDTO.PeopleCategoryDTO",
            "DinnerBookingDTO.MealBookingDTO.MealDTO",
            "DocumentDTO.DocumentCategoryDTO",
            "FieldGroupDTO.PeopleFieldDTO.PeopleDTO",
            "FieldGroupDTO.PeopleFieldDTO",
            "HomeConfigDTO.DocumentDTO",
            "HomeConfigDTO.DocumentDTO.DocumentCategoryDTO",
            "HomeConfigDTO.MailConfigDTO",
            "HomeConfigDTO.BillItemCategoryDTO",
            "MealBookingDTO.MealDTO",
            "MealBookingDTO.PeopleCategoryDTO",
            "MealBookingDTO.DinnerBookingDTO",
            "MealDTO.MealCategoryDTO",
            "MealDTO.BillItemCategoryDTO",
            "MealDTO.DocumentDTO",
            "MealDTO.DocumentDTO.DocumentCategoryDTO",
            "MealDTO.MealPriceDTO",
            "MealDTO.MealPriceDTO.TaxDTO",
            "MealDTO.MealPriceDTO.PeopleCategoryDTO",
            "MealPriceDTO.MealDTO",
            "MealPriceDTO.TaxDTO",
            "MealPriceDTO.MealDTO",
            "MealPriceDTO.PeopleCategoryDTO",
            "PaymentMethodDTO.PaymentTypeDTO",
            "PaymentMethodDTO.BillDTO",
            "PeopleBookingDTO.RoomBookingDTO",
            "PeopleBookingDTO.PeopleCategoryDTO",
            "PeopleCategoryDTO.TaxDTO",
            "PeopleFieldDTO.FieldGroupDTO",
            "PeopleFieldDTO.PeopleDTO",
            "PricePerPersonDTO.PeopleCategoryDTO",
            "PricePerPersonDTO.PeriodDTO",
            "PricePerPersonDTO.RoomDTO",
            "PricePerPersonDTO.TaxDTO",
            "ProductBookingDTO.BookingDTO",
            "ProductBookingDTO.ProductDTO",
            "ProductBookingDTO.ProductDTO.SupplierDTO",
            "ProductBookingDTO.ProductDTO.TaxDTO",
            "ProductBookingDTO.ProductDTO.ProductCategoryDTO",
            "ProductBookingDTO.ProductDTO.BillItemCategoryDTO",
            "ProductBookingDTO.ProductDTO.DocumentDTO",
            "RoomBookingDTO.BookingDTO",
            "RoomBookingDTO.RoomDTO",
            "RoomBookingDTO.RoomDTO.RoomCategoryDTO",
            "RoomBookingDTO.RoomDTO.BedDTO",
            "RoomBookingDTO.PeopleBookingDTO.PeopleCategoryDTO",
            "RoomBookingDTO.PeopleBookingDTO",
            "RoomBookingDTO.SupplementRoomBookingDTO",
            "RoomBookingDTO.SupplementRoomBookingDTO.RoomSupplementDTO",
            "RoomSupplementDTO.TaxDTO",
            "SatisfactionClientAnswerDTO.SatisfactionClientDTO",
            "SatisfactionClientDTO.PeopleDTO",
            "SatisfactionClientDTO.ClientDTO",
            "SatisfactionClientDTO.BookingDTO",
            "SatisfactionClientDTO.SatisfactionClientAnwserDTO",
            "SatisfactionConfigDTO.SatisfactionConfigQuestionDTO",
            "SatisfactionConfigQuestionDTO.SatisfactionConfigDTO",
            "SupplementRoomBookingDTO.RoomBookingDTO",
            "SupplementRoomBookingDTO.RoomSupplementDTO",
            "SupplementRoomBookingDTO.RoomSupplementDTO.TaxDTO",
            "BillItemDTO.BillDTO",
            "BillItemDTO.GroupBillItemDTO",
            "BillItemDTO.BillItemCategoryDTO",
            "BillDTO.SupplierDTO",
            "BillDTO.BillItemDTO",
            "BillDTO.BillItemDTO.BillDTO",
            "BillDTO.BillItemDTO.GroupBillItemDTO",
            "BillDTO.BillItemDTO.BillItemCategoryDTO",
            "BillDTO.DocumentDTO",
            "BillDTO.SupplierDTO",
            "BillDTO.BookingDTO",
            "BillDTO.PaymentMethodDTO",
            "BillDTO.PaymentMethodDTO.PaymentTypeDTO",
        };
    }
}