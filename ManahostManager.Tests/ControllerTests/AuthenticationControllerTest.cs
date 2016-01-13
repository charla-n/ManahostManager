using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ManahostManager.Tests.ControllerTests
{
    [TestClass]
    public class AuthenticationControllerTest
    {
        private ServToken st;

        [TestCleanup]
        public void CleanUp()
        {
            st.server.Dispose();
        }

        [TestMethod]
        public void AuthenticationWithGoodParameter()
        {
            st = ControllerUtils.CreateAndAuthenticate();
        }
    }
}