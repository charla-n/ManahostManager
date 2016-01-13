using ManahostManager.Utils.ManahostPatcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Serialization;

namespace ManahostManager.Tests.UTests
{
    public class EntityTest
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int HomeId { get; set; }

        public DateTime DateCreation { get; set; }

        [IgnoreDataMember]
        public int ShouldNotReplace { get; set; }

        public int ShouldReplace { get; set; }
    }

    [TestClass]
    public class EntityReplacementTest
    {
        private EntityTest orig;

        [TestInitialize]
        public void Init()
        {
            orig = new EntityTest()
            {
                ClientId = 1,
                DateCreation = DateTime.MinValue,
                HomeId = 1,
                Id = 1,
                ShouldNotReplace = 1,
                ShouldReplace = 1
            };
        }

        [TestMethod]
        public void ForbiddenReplacement()
        {
            PatchObjectUtils<EntityTest>.ReplacementOrigByGiven(new EntityTest()
            {
                ClientId = 2,
                DateCreation = DateTime.Now,
                HomeId = 2,
                Id = 2,
                ShouldNotReplace = 1000,
            }, orig, new string[] { });
            Assert.AreEqual(1, orig.ClientId);
            Assert.AreEqual(DateTime.MinValue, orig.DateCreation);
            Assert.AreEqual(2, orig.HomeId);
            Assert.AreEqual(1, orig.ShouldNotReplace);
            Assert.AreEqual(1, orig.Id);
        }

        [TestMethod]
        public void ShouldReplace()
        {
            PatchObjectUtils<EntityTest>.ReplacementOrigByGiven(new EntityTest()
            {
                ClientId = 2,
                DateCreation = DateTime.Now,
                HomeId = 2,
                Id = 2,
                ShouldNotReplace = 10,
                ShouldReplace = 10
            }, orig, new string[] { "ShouldNotReplace", "ShouldReplace" });
            Assert.AreEqual(1, orig.ClientId);
            Assert.AreEqual(DateTime.MinValue, orig.DateCreation);
            Assert.AreEqual(2, orig.HomeId);
            Assert.AreEqual(1, orig.Id);
            Assert.AreEqual(1, orig.ShouldNotReplace);
            Assert.AreEqual(1, orig.ShouldReplace);
        }

        [TestMethod]
        public void ShouldNotReplaceBecauseOfForbiddenReplacement()
        {
            PatchObjectUtils<EntityTest>.ReplacementOrigByGiven(new EntityTest()
            {
                ClientId = 2,
                DateCreation = DateTime.Now,
                HomeId = 2,
                Id = 2,
                ShouldNotReplace = 10,
                ShouldReplace = 10
            }, orig);
            Assert.AreEqual(1, orig.ClientId);
            Assert.AreEqual(DateTime.MinValue, orig.DateCreation);
            Assert.AreEqual(2, orig.HomeId);
            Assert.AreEqual(1, orig.Id);
            Assert.AreEqual(1, orig.ShouldNotReplace);
            Assert.AreEqual(10, orig.ShouldReplace);
        }

        [TestMethod]
        public void PatchObjetShouldNotReplace()
        {
            PatchObjectUtils<EntityTest>.PatchObject(orig, new ManahostPatcherModel[]
                {
                    new ManahostPatcherModel()
                    {
                        Field = "ShouldNotReplace",
                        Value = -1
                    },
                    new ManahostPatcherModel()
                    {
                        Field = "ClientId",
                        Value = -1
                    },
                    new ManahostPatcherModel()
                    {
                        Field = "DateCreation",
                        Value = DateTime.Now
                    },
                    new ManahostPatcherModel()
                    {
                        Field = "Id",
                        Value = -1
                    },
                });
            Assert.AreEqual(1, orig.ClientId);
            Assert.AreEqual(DateTime.MinValue, orig.DateCreation);
            Assert.AreEqual(1, orig.HomeId);
            Assert.AreEqual(1, orig.Id);
            Assert.AreEqual(1, orig.ShouldNotReplace);
            Assert.AreEqual(1, orig.ShouldReplace);
        }

        [TestMethod]
        public void PatchObjetPropertyDoesNotExist()
        {
            PatchObjectUtils<EntityTest>.PatchObject(orig, new ManahostPatcherModel[]
                {
                    new ManahostPatcherModel()
                    {
                        Field = "DoesNotExist",
                        Value = "blabla"
                    },
                });
            Assert.AreEqual(1, orig.ClientId);
            Assert.AreEqual(DateTime.MinValue, orig.DateCreation);
            Assert.AreEqual(1, orig.HomeId);
            Assert.AreEqual(1, orig.Id);
            Assert.AreEqual(1, orig.ShouldNotReplace);
            Assert.AreEqual(1, orig.ShouldReplace);
        }

        [TestMethod]
        public void PatchObjetShouldReplace()
        {
            PatchObjectUtils<EntityTest>.PatchObject(orig, new ManahostPatcherModel[]
                {
                    new ManahostPatcherModel()
                    {
                        Field = "ShouldNotReplace",
                        Value = -1
                    },
                    new ManahostPatcherModel()
                    {
                        Field = "ShouldReplace",
                        Value = 10
                    }
                });
            Assert.AreEqual(1, orig.ClientId);
            Assert.AreEqual(DateTime.MinValue, orig.DateCreation);
            Assert.AreEqual(1, orig.HomeId);
            Assert.AreEqual(1, orig.Id);
            Assert.AreEqual(1, orig.ShouldNotReplace);
            Assert.AreEqual(10, orig.ShouldReplace);
        }
    }
}