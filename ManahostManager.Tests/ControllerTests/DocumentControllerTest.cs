//using ManahostManager.Domain.Entity;
//using ManahostManager.Tests.ControllerTests.Utils;
//using ManahostManager.Tests.Repository;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Globalization;
//using System.IO;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;

//namespace ManahostManager.Tests.ControllerTests
//{
//    [TestClass]
//    public class DocumentControllerTest : ControllerTest<Document>
//    {
//        private const string path = "/api/Document";

//#if !DEBUG
//        [DeploymentItem(@"..\1.jpg")]
//#endif

//        [TestMethod]
//        public void PostPutDelete()
//        {
//            Post();
//            Put();
//            UploadPublic();
//            DownloadPublic();
//            DownloadThubmnailPublic();
//            Delete();
//            Post();
//            UploadPrivate();
//            DownloadPrivate();
//            DownloadThubmnailPrivate();
//            Delete();
//            GetLog();
//        }

//        private void Delete()
//        {
//            reqCreator.CreateRequest(st, path + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Delete, null, HttpStatusCode.OK, false, true);
//        }

//        private void Put()
//        {
//            reqCreator.CreateRequest(st, path, HttpMethod.Put, reqCreator.deserializedEntity, HttpStatusCode.OK, false, false);
//        }

//        private void Post()
//        {
//            reqCreator.CreateRequest(st, path, HttpMethod.Post, new DocumentRepositoryTest().GetDocumentById(1, 0), HttpStatusCode.OK, true);
//        }

//        private void UploadPublic()
//        {
//            HttpResponseMessage response = CreateRequestWithFile(st, path + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Post, new DocumentRepositoryTest().GetDocumentById(1, 0));
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content.ReadAsStringAsync().Result);
//        }

//        private void UploadPrivate()
//        {
//            HttpResponseMessage response = CreateRequestWithFile(st, path + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Post, new DocumentRepositoryTest().GetDocumentById(2, 0));
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content.ReadAsStringAsync().Result);
//        }

//        private void DownloadPublic()
//        {
//            reqCreator.CreateRequest(st, path + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Get, new DocumentRepositoryTest().GetDocumentById(1, 0), HttpStatusCode.OK, false);
//        }

//        private void DownloadThubmnailPublic()
//        {
//            reqCreator.CreateRequest(st, path + "/image/thumbnail/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Get, new DocumentRepositoryTest().GetDocumentById(1, 0), HttpStatusCode.OK, false);
//        }

//        private void DownloadPrivate()
//        {
//            reqCreator.CreateRequest(st, path + "/" + reqCreator.deserializedEntity.HomeId.ToString() + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Get, new DocumentRepositoryTest().GetDocumentById(2, 0), HttpStatusCode.OK, false);
//        }

//        private void DownloadThubmnailPrivate()
//        {
//            reqCreator.CreateRequest(st, path + "/image/thumbnail/" + reqCreator.deserializedEntity.HomeId.ToString() + "/" + reqCreator.deserializedEntity.Id.ToString(), HttpMethod.Get, new DocumentRepositoryTest().GetDocumentById(2, 0), HttpStatusCode.OK, false);
//        }

//        private void RequestActionUpload(HttpRequestMessage msg)
//        {
//            Stream ok = File.OpenRead(@"1.jpg");
//            var okok = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture));

//            okok.Add(new StreamContent(ok), "image", "test.jpg");
//            msg.Content = okok;
//            msg.Method = HttpMethod.Post;
//        }

//        private HttpResponseMessage CreateRequestWithFile(ServToken st, string path, HttpMethod method, Document entity)
//        {
//            return st.server.CreateRequest(path).
//                And(RequestActionUpload).
//                AddHeader("Content-type", "multipart/form-data").
//                AddHeader("Authorization", new AuthenticationHeaderValue("Bearer", st.token).ToString()).
//                SendAsync(method.Method).Result;
//        }

//        private void GetLog()
//        {
//            reqCreator.CreateRequest(st, path + "/Log", HttpMethod.Get, null, HttpStatusCode.OK);
//        }
//    }
//}