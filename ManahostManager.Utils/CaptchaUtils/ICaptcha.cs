namespace ManahostManager.Utils.CaptchaUtils
{
    public interface ICaptcha
    {
        void Validate();

        void setParams(params string[] values);
    }
}