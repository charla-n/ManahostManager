using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ManahostManager.Tests.UTests
{
    [TestClass]
    public class PeriodUtilsTest
    {
        [TestMethod]
        public void IsDayCrossTestReturnFalse()
        {
            List<Period> periods = new List<Period>()
            {
                new Period()
                {
                    Begin = new DateTime(2015, 3, 5, 0, 0, 0),
                    End = new DateTime(2015, 3, 9, 23, 59, 59),
                    Days = 1,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 10, 0, 0, 0),
                    End = new DateTime(2015, 3, 13, 23, 59, 59),
                    Days = 2 | 64,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 10, 0, 0, 0),
                    End = new DateTime(2015, 3, 13, 23, 59, 59),
                    Days = 4 | 32,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 16, 0, 0, 0),
                    End = new DateTime(2015, 3, 29, 23, 59, 59),
                    Days = 8,
                    IsClosed = false
                },
            };
            Assert.IsFalse(PeriodUtils.IsDaysCross(periods, new Period()
            {
                Begin = new DateTime(2015, 3, 5, 0, 0, 0),
                End = new DateTime(2015, 3, 29, 23, 59, 59),
                Days = 16,
                IsClosed = false
            }));
        }

        [TestMethod]
        public void IsDayCrossTestReturnTrue()
        {
            List<Period> periods = new List<Period>()
            {
                new Period()
                {
                    Begin = new DateTime(2015, 3, 5, 0, 0, 0),
                    End = new DateTime(2015, 3, 9, 23, 59, 59),
                    Days = 1,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 10, 0, 0, 0),
                    End = new DateTime(2015, 3, 13, 23, 59, 59),
                    Days = 2 | 64 | 16,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 10, 0, 0, 0),
                    End = new DateTime(2015, 3, 13, 23, 59, 59),
                    Days = 4 | 32,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 16, 0, 0, 0),
                    End = new DateTime(2015, 3, 29, 23, 59, 59),
                    Days = 8,
                    IsClosed = false
                },
            };
            Assert.IsTrue(PeriodUtils.IsDaysCross(periods, new Period()
            {
                Begin = new DateTime(2015, 3, 5, 0, 0, 0),
                End = new DateTime(2015, 3, 29, 23, 59, 59),
                Days = 16,
                IsClosed = false
            }));
        }

        [TestMethod]
        public void NotAllCoveredShouldReturnFalse()
        {
            List<Period> periods = new List<Period>()
            {
                new Period()
                {
                    Begin = new DateTime(2015, 3, 5, 0, 0, 0),
                    End = new DateTime(2015, 3, 9, 23, 59, 59),
                    Days = 1 | 8 | 16 | 32 | 64,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 10, 0, 0, 0),
                    End = new DateTime(2015, 3, 13, 23, 59, 59),
                    Days = 2 | 8 | 16,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 10, 0, 0, 0),
                    End = new DateTime(2015, 3, 13, 23, 59, 59),
                    Days = 4,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 16, 0, 0, 0),
                    End = new DateTime(2015, 3, 29, 23, 59, 59),
                    Days = 1 | 2 | 4 | 8 | 16 | 32 | 64,
                    IsClosed = false
                },
            };
            Assert.IsFalse(PeriodUtils.AllCovered(periods, new DateTime(2015, 3, 9, 14, 0, 0), new DateTime(2015, 3, 29, 10, 0, 0)));
        }

        [TestMethod]
        public void AllCoveredShouldReturnTrue()
        {
            List<Period> periods = new List<Period>()
            {
                new Period()
                {
                    Begin = new DateTime(2015, 3, 5, 0, 0, 0),
                    End = new DateTime(2015, 3, 9, 23, 59, 59),
                    Days = 1 | 8 | 16 | 32 | 64,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 10, 0, 0, 0),
                    End = new DateTime(2015, 3, 13, 23, 59, 59),
                    Days = 2 | 8 | 16,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 10, 0, 0, 0),
                    End = new DateTime(2015, 3, 13, 23, 59, 59),
                    Days = 4,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 14, 0, 0, 0),
                    End = new DateTime(2015, 3, 15, 23, 59, 59),
                    Days = 32 | 64,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 16, 0, 0, 0),
                    End = new DateTime(2015, 3, 29, 23, 59, 59),
                    Days = 1 | 2 | 4 | 8 | 16 | 32 | 64,
                    IsClosed = false
                },
            };
            Assert.IsTrue(PeriodUtils.AllCovered(periods, new DateTime(2015, 3, 9, 14, 0, 0), new DateTime(2015, 3, 29, 10, 0, 0)));
        }

        [TestMethod]
        public void NotAllCoveredShouldReturnTrueBecauseOfDatesNotInTheRangeOfNoneCovered()
        {
            List<Period> periods = new List<Period>()
            {
                new Period()
                {
                    Begin = new DateTime(2015, 3, 5, 0, 0, 0),
                    End = new DateTime(2015, 3, 9, 23, 59, 59),
                    Days = 1 | 8 | 16 | 32 | 64,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 10, 0, 0, 0),
                    End = new DateTime(2015, 3, 13, 23, 59, 59),
                    Days = 2 | 8 | 16,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 10, 0, 0, 0),
                    End = new DateTime(2015, 3, 13, 23, 59, 59),
                    Days = 4,
                    IsClosed = false
                },
                new Period()
                {
                    Begin = new DateTime(2015, 3, 16, 0, 0, 0),
                    End = new DateTime(2015, 3, 29, 23, 59, 59),
                    Days = 1 | 2 | 4 | 8 | 16 | 32 | 64,
                    IsClosed = false
                },
            };
            Assert.IsTrue(PeriodUtils.AllCovered(periods, new DateTime(2015, 3, 16, 14, 0, 0), new DateTime(2015, 3, 29, 10, 0, 0)));
        }
    }
}