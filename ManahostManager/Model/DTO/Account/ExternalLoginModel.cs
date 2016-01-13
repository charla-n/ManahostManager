using ManahostManager.Utils;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ManahostManager.Model.DTO.Account
{
    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        //TODO ADD SOMETHING LIKE ADDRESS OR MOT DE PASSE FOR LOCAL ACCOUNT
        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public string Provider { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public string ExternalAccessToken { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public string client_id { get; set; }

        public string client_secret { get; set; }
    }

    public class ParsedExternalAccessToken
    {
        public string user_id { get; set; }

        public string app_id { get; set; }
    }

    public class ParsedExternalAccessTokenUserInfo
    {
        public string email { get; set; }

        public string given_name { get; set; }

        public string family_name { get; set; }
    }

    public class ExternalLocalAccessTokenModel
    {
        public string userName { get; set; }

        public string access_token { get; set; }

        public string token_type { get; set; }

        public string expires_in { get; set; }

        [DataMember(Name = ".issued")]
        public string issued { get; set; }

        [DataMember(Name = ".expires")]
        public string expires { get; set; }

        public string refresh_token { get; set; }
    }
}