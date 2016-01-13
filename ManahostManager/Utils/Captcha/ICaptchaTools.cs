using System.Threading.Tasks;

namespace ManahostManager.Utils.Captcha
{
    public interface ICaptchaTools
    {
        Task<bool> VerifyCaptcha(string token, string ip);
    }
}