using NUnit.Framework;
using KevValDSXignite;
using System;
using System.Collections.Generic;

namespace KeyValueDataStructureTests
{
    public class Tests
    {
        private static DataStructure<int, string> dataStructure;
        private static DataStructure<DummyClass, int> dataStructureDummyClassInt;
        private class DummyClass
        {

        }

        [SetUp]
        public void Setup()
        {

            dataStructure = new HashMap<int, string>();

            //Just to test if the kv datastructure works with different types
            dataStructureDummyClassInt = new HashMap<DummyClass, int>();
        }

        //Some basic tests - IntString
        [Test]
        public void TestPutGet()
        {
            dataStructure.Put(1, "abc");
            Assert.AreEqual(dataStructure.Get(1), "abc");
        }

        [Test]
        public void TestUpdate()
        {
            dataStructure.Put(1, "abc");
            dataStructure.Put(1, "xyz");
            Assert.AreEqual(dataStructure.Get(1), "xyz");
        }

        [Test]
        public void TestResize()
        {
            //Check that we get same values after resize
            dataStructure.Put(1, "abc");
            dataStructure.Put(2, "xyz");
            dataStructure.Put(3, "123");
            dataStructure.Put(4, "456");
            dataStructure.Put(5, "asd");
            dataStructure.Put(6, "cvb");
            dataStructure.Put(7, "hgj");
            dataStructure.Put(8, "rty");
            dataStructure.Put(9, "qwe");
            dataStructure.Put(10, "tyu");
            Assert.AreEqual(dataStructure.Get(1), "abc");
            Assert.AreEqual(dataStructure.Get(2), "xyz");
            Assert.AreEqual(dataStructure.Get(3), "123");
            Assert.AreEqual(dataStructure.Get(4), "456");
            Assert.AreEqual(dataStructure.Get(5), "asd");
            Assert.AreEqual(dataStructure.Get(6), "cvb");
            Assert.AreEqual(dataStructure.Get(7), "hgj");
            Assert.AreEqual(dataStructure.Get(8), "rty");
            Assert.AreEqual(dataStructure.Get(9), "qwe");
            Assert.AreEqual(dataStructure.Get(10), "tyu");
        }

        [Test]
        public void TestGetNull()
        {
            Assert.IsNull(dataStructure.Get(1));
        }


        [Test]
        public void TestGetRandom()
        {
            dataStructure.Put(1, "abc");
            dataStructure.Put(2, "xyz");
            List<int> expected = new List<int>() { 1,2 };
            CollectionAssert.Contains(expected, dataStructure.GetRandomKey());
        }

        [Test]
        public void TestGetRandomThrowsIndexOutOfRange()
        {
            Assert.Throws<IndexOutOfRangeException>(() => dataStructure.GetRandomKey());
        }

        [Test]
        public void TestDeleteTrueAndNull()
        {
            dataStructure.Put(1, "abc");
            Assert.IsTrue(dataStructure.Delete(1));
            Assert.IsNull(dataStructure.Get(1));
        }

        [Test]
        public void TestDeleteFalse()
        {
            Assert.IsFalse(dataStructure.Delete(1));
        }

        [Test]
        public void TestPutDeletePut()
        {
            dataStructure.Put(1, "xyz");
            Assert.IsTrue(dataStructure.Delete(1));
            dataStructure.Put(1, "abc");
            Assert.AreEqual(dataStructure.Get(1), "abc");
        }

        [Test]
        public void TestPutDummyclassInt()
        {
            var c = new DummyClass();
            dataStructureDummyClassInt.Put(c, 1);
            Assert.AreEqual(dataStructureDummyClassInt.Get(c), 1);
        }

        [Test]
        public void TestPutDummyclassIntTwoInstances()
        {
            var c1 = new DummyClass();
            var c2 = new DummyClass();
            dataStructureDummyClassInt.Put(c1, 1);
            dataStructureDummyClassInt.Put(c2, 2);
            Assert.AreNotEqual(dataStructureDummyClassInt.Get(c2), 1);
            Assert.AreNotEqual(dataStructureDummyClassInt.Get(c1), 2);
        }

    }

}