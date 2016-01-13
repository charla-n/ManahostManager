using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Tests
{
    [TestClass]
    public class AdvancedSearchTest
    {
        private ModelStateDictionary wrapper;
        private AdvancedSearch search;

        [TestInitialize]
        public void Init()
        {
            wrapper = new ModelStateDictionary();
            search = new AdvancedSearch();
        }

        private void TestInfo(SearchInfos infos, SearchInfos expected)
        {
            Assert.AreEqual(expected.count, infos.count);
            Assert.AreEqual(expected.orderByClause, infos.orderByClause);
            Assert.AreEqual(expected.resource, infos.resource);
            Assert.AreEqual(expected.skip, infos.skip);
            Assert.AreEqual(expected.take, infos.take);
            Assert.AreEqual(expected.whereClause.ToString(), infos.whereClause.ToString());
            CollectionAssert.AreEqual(expected.whereParameters, infos.whereParameters);
        }

        [TestMethod]
        public void TransformSQLWhereClauseLt()
        {
            search.ToSQL("Document/where/date/lt/2014-12-30", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id ascending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("date < @0 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "2014-12-30" }
            });
        }

        [TestMethod]
        public void TransformSQLWhereClauseLtWithNavigationProperty()
        {
            search.ToSQL("Document/where/Booking.DateArrival/lt/2014-12-30", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id ascending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("Booking.DateArrival < @0 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "2014-12-30" }
            });
        }

        [TestMethod]
        public void TransformSQLWhereClauseLtWithBadNavigationPropertyErrorWithTooDeepAccess()
        {
            search.ToSQL("Document/where/Booking.Home.Client.DateModification/lt/2014-12-30", "1", wrapper);
            Assert.AreEqual(1, wrapper.Count);
        }

        [TestMethod]
        public void TransformSQLWithLike()
        {
            search.ToSQL("Document/where/comment/lk/%25bombe%25", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id ascending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("comment.Contains(@0) && Home.ClientId == 1"),
                whereParameters = new List<object>() { "%bombe%" }
            });
        }

        [TestMethod]
        public void TransformSQLWithLikeWithAndOr()
        {
            search.ToSQL("Document/where/comment/lk/%25bombe%25/and/title/lk/doc%25/or/comment/lk/%25end", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id ascending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("comment.Contains(@0) && title.Contains(@1) && Home.ClientId == 1 || comment.Contains(@2) && Home.ClientId == 1"),
                whereParameters = new List<object>() { "%bombe%", "doc%", "%end" }
            });
        }

        [TestMethod]
        public void TransformSQLWithLike2()
        {
            search.ToSQL("Document/where/comment/lk/b%25", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id ascending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("comment.Contains(@0) && Home.ClientId == 1"),
                whereParameters = new List<object>() { "b%" }
            });
        }

        [TestMethod]
        public void TransformSQLWithDelimitorsInside()
        {
            search.ToSQL("Document/where/date/lt/2014%2F12%2F30", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id ascending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("date < @0 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "2014/12/30" }
            });
        }

        [TestMethod]
        public void TransformSQLAllSimple()
        {
            search.ToSQL("Document", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id ascending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("Home.ClientId == 1"),
                whereParameters = new List<object>() { }
            });
        }

        [TestMethod]
        public void TransformSQLTestWhereClauseLtAndGt()
        {
            search.ToSQL("Document/where/date/lt/2014-12-30/and/date/gt/2013-12-30", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id ascending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("date < @0 && date > @1 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "2014-12-30", "2013-12-30" }
            });
        }

        [TestMethod]
        public void TransformSQLTestWhereClauseLtAndGtOrEq()
        {
            search.ToSQL("Document/where/date/lt/2014-12-30/and/date/gt/2013-12-30/or/date/eq/0001-01-01", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id ascending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("date < @0 && date > @1 && Home.ClientId == 1 || date == @2 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "2014-12-30", "2013-12-30", "0001-01-01" }
            });
        }

        [TestMethod]
        public void TransformSQLTestWhereInAnd()
        {
            search.ToSQL("Document/where/category_id/in/1,2,3,4,5/and/label/eq/coucou", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id ascending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("(category_id == @0 || category_id == @1 || category_id == @2 || category_id == @3 || category_id == @4) && label == @5 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "1", "2", "3", "4", "5", "coucou" }
            });
        }

        [TestMethod]
        public void TransformSQLTestWhereAndIn()
        {
            search.ToSQL("Document/where/label/eq/coucou/and/category_id/in/1,2,3,4,5", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id ascending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("label == @0 && (category_id == @1 || category_id == @2 || category_id == @3 || category_id == @4 || category_id == @5) && Home.ClientId == 1"),
                whereParameters = new List<object>() { "coucou", "1", "2", "3", "4", "5" }
            });
        }

        [TestMethod]
        public void TransformSQLTestWhereInOr()
        {
            search.ToSQL("Document/where/category_id/in/1,2,3,4,5/or/label/eq/coucou", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id ascending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("(category_id == @0 || category_id == @1 || category_id == @2 || category_id == @3 || category_id == @4) && Home.ClientId == 1 || label == @5 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "1", "2", "3", "4", "5", "coucou" }
            });
        }

        [TestMethod]
        public void TransformSQLAndAndCount()
        {
            search.ToSQL("Document/where/label/eq/coucou/and/date/lt/2014-12-31/count", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = true,
                orderByClause = null,
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("label == @0 && date < @1 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "coucou", "2014-12-31" }
            });
        }

        [TestMethod]
        public void TransformSQLAndOrOrderby()
        {
            search.ToSQL("Document/where/label/eq/coucou/and/date/lt/2014-12-31/or/label/eq/blabla/Orderby/id/desc", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id descending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("label == @0 && date < @1 && Home.ClientId == 1 || label == @2 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "coucou", "2014-12-31", "blabla" }
            });
        }

        [TestMethod]
        public void TransformSQLAndOrOrderbyWithNavigationProperty()
        {
            search.ToSQL("Document/where/label/eq/coucou/and/date/lt/2014-12-31/or/label/eq/blabla/Orderby/Booking.DateArrival/desc", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "Booking.DateArrival descending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("label == @0 && date < @1 && Home.ClientId == 1 || label == @2 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "coucou", "2014-12-31", "blabla" }
            });
        }

        [TestMethod]
        public void TransformSQLAndOrOrderbyWithNavigationPropertyButTooDeeply()
        {
            search.ToSQL("Document/where/label/eq/coucou/and/date/lt/2014-12-31/or/label/eq/blabla/Orderby/Booking.Home.Client.DateModification/desc", "1", wrapper);
            Assert.AreEqual(1, wrapper.Count);
        }

        [TestMethod]
        public void TransformSQLCount()
        {
            search.ToSQL("Document/count", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = true,
                orderByClause = null,
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("Home.ClientId == 1"),
                whereParameters = new List<object>() { }
            });
        }

        [TestMethod]
        public void TransformSQLOrderBy()
        {
            search.ToSQL("Document/Orderby/name/asc", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "name ascending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("Home.ClientId == 1"),
                whereParameters = new List<object>() { }
            });
        }

        [TestMethod]
        public void TransformSQLAndInOrInAndOrderBy()
        {
            search.ToSQL("Document/where/category/ne/coucou/and/category_id/in/5,10,15,20/or/category_id/in/3,6,9/and/label/eq/123456/Orderby/category_id/asc", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "category_id ascending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("category != @0 && (category_id == @1 || category_id == @2 || category_id == @3 || category_id == @4) && Home.ClientId == 1 || (category_id == @5 || category_id == @6 || category_id == @7) && label == @8 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "coucou", "5", "10", "15", "20", "3", "6", "9", "123456" }
            });
        }

        [TestMethod]
        public void TransformSQLInOrOrderByLimitWithoutOffset()
        {
            search.ToSQL("Document/where/category_id/in/1,2,3,4,5/or/label/eq/coucou/Orderby/i/desc/limit/10", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "i descending",
                resource = "Document",
                skip = 0,
                take = 10,
                whereClause = new System.Text.StringBuilder("(category_id == @0 || category_id == @1 || category_id == @2 || category_id == @3 || category_id == @4) && Home.ClientId == 1 || label == @5 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "1", "2", "3", "4", "5", "coucou" }
            });
        }

        [TestMethod]
        public void TransformSQLInOrOrderByLimitWithoutOffsetExceededLimitMax()
        {
            search.ToSQL("Document/where/category_id/in/1,2,3,4,5/or/label/eq/coucou/Orderby/i/desc/limit/101", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "i descending",
                resource = "Document",
                skip = 0,
                take = 100,
                whereClause = new System.Text.StringBuilder("(category_id == @0 || category_id == @1 || category_id == @2 || category_id == @3 || category_id == @4) && Home.ClientId == 1 || label == @5 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "1", "2", "3", "4", "5", "coucou" }
            });
        }

        [TestMethod]
        public void TransformSQLInOrOrderByLimitWithOffset()
        {
            search.ToSQL("Document/where/category_id/in/1,2,3,4,5/or/label/eq/coucou/Orderby/i/desc/limit/5/10", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "i descending",
                resource = "Document",
                skip = 5,
                take = 10,
                whereClause = new System.Text.StringBuilder("(category_id == @0 || category_id == @1 || category_id == @2 || category_id == @3 || category_id == @4) && Home.ClientId == 1 || label == @5 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "1", "2", "3", "4", "5", "coucou" }
            });
        }

        [TestMethod]
        public void TransformSQLInOrOrderByLimitWithOffsetExceededLimitMax()
        {
            search.ToSQL("Document/where/category_id/in/1,2,3,4,5/or/label/eq/coucou/Orderby/i/desc/limit/5/125", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "i descending",
                resource = "Document",
                skip = 5,
                take = 105,
                whereClause = new System.Text.StringBuilder("(category_id == @0 || category_id == @1 || category_id == @2 || category_id == @3 || category_id == @4) && Home.ClientId == 1 || label == @5 && Home.ClientId == 1"),
                whereParameters = new List<object>() { "1", "2", "3", "4", "5", "coucou" }
            });
        }

        [TestMethod]
        public void TransformSQLOrderByLimitWithOffset()
        {
            search.ToSQL("Document/Orderby/id/desc/limit/5/10", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id descending",
                resource = "Document",
                skip = 5,
                take = 10,
                whereClause = new System.Text.StringBuilder("Home.ClientId == 1"),
                whereParameters = new List<object>() { }
            });
        }

        [TestMethod]
        public void TransformSQLLimitWithOffset()
        {
            ModelStateDictionary wrapper = new ModelStateDictionary();
            AdvancedSearch search = new AdvancedSearch();
            search.ToSQL("Document/limit/5/10", "1", wrapper);
            TestInfo(search.Infos, new SearchInfos()
            {
                count = false,
                orderByClause = "id ascending",
                resource = "Document",
                skip = 5,
                take = 10,
                whereClause = new System.Text.StringBuilder("Home.ClientId == 1"),
                whereParameters = new List<object>() { }
            });
        }

        [TestMethod]
        public void TransformSQLLimitNegativeNumber()
        {
            ModelStateDictionary wrapper = new ModelStateDictionary();
            AdvancedSearch search1 = new AdvancedSearch();
            search1.ToSQL("Document/limit/-1/10", "1", wrapper);
            Assert.AreEqual(1, wrapper.Count);
            ModelStateDictionary wrapper2 = new ModelStateDictionary();
            AdvancedSearch search2 = new AdvancedSearch();
            search2.ToSQL("Document/limit/-1/-1", "1", wrapper2);
            Assert.AreEqual(1, wrapper2.Count);
            ModelStateDictionary wrapper3 = new ModelStateDictionary();
            AdvancedSearch search3 = new AdvancedSearch();
            search3.ToSQL("Document/limit/1/-1", "1", wrapper3);
            Assert.AreEqual(1, wrapper3.Count);
            ModelStateDictionary wrapper4 = new ModelStateDictionary();
            AdvancedSearch search4 = new AdvancedSearch();
            search4.ToSQL("Document/limit/1/0", "1", wrapper4);
            Assert.AreEqual(1, wrapper4.Count);
            ModelStateDictionary wrapper5 = new ModelStateDictionary();
            AdvancedSearch search5 = new AdvancedSearch();
            search5.ToSQL("Document/limit/0", "1", wrapper5);
            Assert.AreEqual(1, wrapper5.Count);
            ModelStateDictionary wrapper6 = new ModelStateDictionary();
            AdvancedSearch search6 = new AdvancedSearch();
            search6.ToSQL("Document/limit/0/-1", "1", wrapper6);
            Assert.AreEqual(1, wrapper6.Count);
        }

        [TestMethod]
        public void TransformSQLEmpty()
        {
            ModelStateDictionary wrapper = new ModelStateDictionary();
            AdvancedSearch search1 = new AdvancedSearch();
            search1.ToSQL("", "1", wrapper);
            Assert.AreEqual(1, wrapper.Count);
            ModelStateDictionary wrapper2 = new ModelStateDictionary();
            AdvancedSearch search2 = new AdvancedSearch();
            search2.ToSQL(null, "1", wrapper2);
            Assert.AreEqual(1, wrapper2.Count);
        }

        [TestMethod]
        public void TransformSQLRandom()
        {
            ModelStateDictionary wrapper = new ModelStateDictionary();
            AdvancedSearch search = new AdvancedSearch();
            search.ToSQL("fpefpoeà)orfàfioefsdfvkpoejkfge", "1", wrapper);
            Assert.AreEqual(1, wrapper.Count);
        }

        [TestMethod]
        public void TransformSQLUnknownResource()
        {
            ModelStateDictionary wrapper = new ModelStateDictionary();
            AdvancedSearch search = new AdvancedSearch();
            search.ToSQL("unknownresource/where/id/eq/10", "1", wrapper);
            Assert.AreEqual(1, wrapper.Count);
        }

        [TestMethod]
        public void TransformSQLUnknownOperator()
        {
            ModelStateDictionary wrapper = new ModelStateDictionary();
            AdvancedSearch search = new AdvancedSearch();
            search.ToSQL("Document/where/id/eqa/10", "1", wrapper);
            Assert.AreEqual(1, wrapper.Count);
            ModelStateDictionary wrapper2 = new ModelStateDictionary();
            AdvancedSearch search2 = new AdvancedSearch();
            search2.ToSQL("Document/where/id/eq/10/or/id/eqa/15", "1", wrapper2);
            Assert.AreEqual(1, wrapper2.Count);
        }

        [TestMethod]
        public void TransformSQLUnknownKeyWord()
        {
            ModelStateDictionary wrapper = new ModelStateDictionary();
            AdvancedSearch search = new AdvancedSearch();
            search.ToSQL("Document/whereas/id/eq/10", "1", wrapper);
            Assert.AreEqual(1, wrapper.Count);
            ModelStateDictionary wrapper2 = new ModelStateDictionary();
            AdvancedSearch search2 = new AdvancedSearch();
            search2.ToSQL("Document/where/id/eq/10/ora/id/eq/15", "1", wrapper2);
            Assert.AreEqual(1, wrapper2.Count);
        }

        [TestMethod]
        public void TransformSQLSlashes()
        {
            ModelStateDictionary wrapper = new ModelStateDictionary();
            AdvancedSearch search1 = new AdvancedSearch();
            search1.ToSQL("///////", "1", wrapper);
            Assert.AreEqual(1, wrapper.Count);

            ModelStateDictionary wrapper2 = new ModelStateDictionary();
            AdvancedSearch search2 = new AdvancedSearch();
            search2.ToSQL("/", "1", wrapper2);
            Assert.AreEqual(1, wrapper2.Count);

            ModelStateDictionary wrapper3 = new ModelStateDictionary();
            AdvancedSearch search3 = new AdvancedSearch();
            search3.ToSQL("//", "1", wrapper3);
            Assert.AreEqual(1, wrapper3.Count);

            ModelStateDictionary wrapper4 = new ModelStateDictionary();
            AdvancedSearch search4 = new AdvancedSearch();
            search4.ToSQL("/Document/", "1", wrapper4);
            Assert.AreEqual(1, wrapper4.Count);

            ModelStateDictionary wrapper5 = new ModelStateDictionary();
            AdvancedSearch search5 = new AdvancedSearch();
            search5.ToSQL("/where/id/eq/10", "1", wrapper5);
            Assert.AreEqual(1, wrapper5.Count);

            ModelStateDictionary wrapper6 = new ModelStateDictionary();
            AdvancedSearch search6 = new AdvancedSearch();
            search6.ToSQL("/and/and/and/and", "1", wrapper6);
            Assert.AreEqual(1, wrapper6.Count);
        }

        [TestMethod]
        public void TransformSQLCountError()
        {
            ModelStateDictionary wrapper = new ModelStateDictionary();
            AdvancedSearch search = new AdvancedSearch();
            search.ToSQL("Document/whereas/id/eq/10/count/count", "1", wrapper);
            Assert.AreEqual(1, wrapper.Count);

            ModelStateDictionary wrapper2 = new ModelStateDictionary();
            AdvancedSearch search2 = new AdvancedSearch();
            search2.ToSQL("Document/whereas/id/eq/10/limit/5/count", "1", wrapper2);
            Assert.AreEqual(1, wrapper2.Count);

            ModelStateDictionary wrapper3 = new ModelStateDictionary();
            AdvancedSearch search3 = new AdvancedSearch();
            search3.ToSQL("Document/whereas/id/eq/10/Orderby/title/count", "1", wrapper3);
            Assert.AreEqual(1, wrapper3.Count);
        }

        [TestMethod]
        public void TransformSQLOrderByError()
        {
            ModelStateDictionary wrapper = new ModelStateDictionary();

            AdvancedSearch search1 = new AdvancedSearch();
            search1.ToSQL("Document/whereas/id/eq/10/Orderby/count", "1", wrapper);
            Assert.AreEqual(1, wrapper.Count);

            ModelStateDictionary wrapper2 = new ModelStateDictionary();
            AdvancedSearch search2 = new AdvancedSearch();
            search2.ToSQL("Document/whereas/id/eq/10/Orderby/id/aaa", "1", wrapper2);
            Assert.AreEqual(1, wrapper2.Count);

            ModelStateDictionary wrapper3 = new ModelStateDictionary();
            AdvancedSearch search3 = new AdvancedSearch();
            search3.ToSQL("Document/whereas/id/eq/10/Orderby/id", "1", wrapper3);
            Assert.AreEqual(1, wrapper3.Count);

            ModelStateDictionary wrapper4 = new ModelStateDictionary();
            AdvancedSearch search4 = new AdvancedSearch();
            search4.ToSQL("Document/whereas/id/eq/10/Orderby/OrderBy/id/asc", "1", wrapper4);
            Assert.AreEqual(1, wrapper4.Count);

            ModelStateDictionary wrapper5 = new ModelStateDictionary();
            AdvancedSearch search5 = new AdvancedSearch();
            search5.ToSQL("Document/OrderBy/id/asc/whereas/id/eq/10///", "1", wrapper5);
            Assert.AreEqual(1, wrapper5.Count);

            ModelStateDictionary wrapper6 = new ModelStateDictionary();
            AdvancedSearch search6 = new AdvancedSearch();
            search6.ToSQL("Document/OrderBy/id/asc/whereas/id/eq/10", "1", wrapper6);
            Assert.AreEqual(1, wrapper6.Count);
        }
    }
}