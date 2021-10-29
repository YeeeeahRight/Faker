using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using SimpleTypeGenerator;
using System.Collections.Generic;

namespace FakerTest
{
    [TestClass]
    public class FakerTest
    {
        static public FakerMain Faker;
        private class TestAllType
        {
            public int Int;
            public float Float;
            public long Long;
            public double Double;
            public char Char;
            public bool Bool;
            public string String;
            public DateTime DateTime;
        }
        private class TestConfig
        {
            public string ConfigString;
            public int ConfigInt;
            public string String;
            public int Int;
            public int PropInt { get; }
            public int PropIntConfig { get; }

            public TestConfig(int PropInt, int PropIntConfig)
            {
                this.PropInt = PropInt;
                this.PropIntConfig = PropIntConfig;
            }
        }
        private class TestConstructor
        {
            public int Int { get; }
            public string String { get; }
            public bool Bool { get; }
            public TestConstructor(int i)
            {

            }
            public TestConstructor(int i,int j)
            {
                Int = -10;
                Bool = false;
                String = "done";
            }
        }
        [TestInitialize]
        public void TestInit()
        {
            FakerConfiguration configuration = new FakerConfiguration();
            configuration.Add<TestConfig, string, StringGenerator.StringGenerator>(TestConfig => TestConfig.ConfigString);
            configuration.Add<TestConfig, int, IntGenerator>(TestConfig => TestConfig.ConfigInt);
            configuration.Add<TestConfig, int, IntGenerator>(TestConfig => TestConfig.PropIntConfig);
            Faker = new FakerMain(configuration);
        }

        [TestMethod]
        public void TestAllTypesMatch()
        {
            TestAllType testAllType= Faker.Create<TestAllType>();
            Assert.IsNotNull(testAllType);
            Assert.AreNotEqual(testAllType.Char,"");
            Assert.AreNotEqual(testAllType.Int, 0);
            Assert.IsNotNull(testAllType.DateTime);
            Assert.IsNotNull(testAllType.String);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            TestConstructor testConstructor = Faker.Create<TestConstructor>();
            Assert.IsNotNull(testConstructor);
            Assert.AreEqual(testConstructor.Int, -10);
            Assert.AreEqual(testConstructor.String, "done");
            Assert.AreEqual(testConstructor.Bool, false);
        }

        [TestMethod]
        public void ListTest()
        {
            List<TestAllType> testConstructor = Faker.Create<List<TestAllType>>();
            Assert.IsNotNull(testConstructor);
            CollectionAssert.AllItemsAreNotNull(testConstructor);
        }

        [TestMethod]
        public void DoubleListTest()
        {
            List<List<TestAllType>> testConstructor = Faker.Create<List<List<TestAllType>>>();
            Assert.IsNotNull(testConstructor);
            CollectionAssert.AllItemsAreNotNull(testConstructor);
        }
    }
}
