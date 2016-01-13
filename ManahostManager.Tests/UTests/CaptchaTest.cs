using ManahostManager.Utils;
using ManahostManager.Utils.CaptchaUtils;

namespace ManahostManager.Tests
{
    public class CaptchaTestTrue : ICaptcha
    {
        public void Validate()
        {
        }

        public void setParams(params string[] values)
        {
        }
    }

    public class CaptchaTestFalse : ICaptcha
    {
        public void Validate()
        {
            throw new ManahostException("error");
        }

        public void setParams(params string[] values)
        {
        }
    }
}