using ManahostManager.Domain.DAL;
using ManahostManager.Utils.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;

namespace ManahostManager.Tests.ControllerTests.Utils
{
    public class ControllerTest<T> where T : class
    {
        private static bool InitDB = false;
        protected ServToken st;
        protected RequestCreator<T> reqCreator;
        public T entity;
        public IMapper mapper;

        public ControllerTest(ServToken st) : this()
        {
            this.st = st;
        }

        public ControllerTest()
        {
            this.mapper = new ManahostMapster();
            reqCreator = new RequestCreator<T>();
        }

        [TestInitialize]
        public void Init()
        {
            if (!InitDB)
            {
                Database.SetInitializer(new ManahostManagerInitializer());
                using (ManahostManagerDAL prectx = new ManahostManagerDAL())
                {
                    prectx.Database.Delete();
                }
                InitDB = true;
            }
            st = ControllerUtils.CreateAndAuthenticate();
        }

        [TestCleanup]
        public void Cleanup()
        {
            st.server.Dispose();
        }
    }
}