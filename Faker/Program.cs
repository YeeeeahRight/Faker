﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace Test
{
    using  Faker;
   
 }
namespace Faker
{
    using Test;
    class Program
    {
        class A
        {
            public B B;
        }
        class B
        {
            public C C;

        }
        class C
        {
            public A A;
        }

        class TestAllType
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

        class TestConfig
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
        class TestClass{
            public int Int;
            public DateTime DateTime;
            public bool Bool;
        }
        class TestClassInField
        {
            public string String;
            public double Double;
            public TestClass TestClass;
        }
        class TestConstr
        {
            public TestConstr()
            {

            }
            public TestConstr(int i)
            {
                Console.WriteLine("Error");
            }
            public TestConstr(int i,int j)
            {
                Console.WriteLine("This is Ok");
            }
        }
        static void Main(string[] args) 
        {
            FakerConfiguration configuration1 = null;
            FakerMain faker1 = new FakerMain(configuration1);
            PrintObjectValue(faker1.Create<TestAllType>()," ");

            Console.WriteLine("----------------------------");

            PrintObjectValue(faker1.Create<TestConstr>(), " ");

            Console.WriteLine("----------------------------");

            Console.WriteLine("Test Dependency");
            PrintObjectValue(faker1.Create<A>()," ");

            Console.WriteLine("----------------------------");

            PrintObjectValue(faker1.Create<TestClassInField>(), " ");

            Console.WriteLine("----------------------------");

            List < TestClass > OneLVLList= faker1.Create<List<TestClass>>();
            foreach (TestClass testClass in OneLVLList) {
                PrintObjectValue(testClass," ");
            }

            Console.WriteLine("----------------------------");

            List<List<TestClass>> TwoLVLList = faker1.Create<List<List<TestClass>>>();
            foreach (List<TestClass> ListTestClass in TwoLVLList)
            {

                foreach (TestClass testClass in ListTestClass)
                {
                    PrintObjectValue(testClass, " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("----------------------------");

         /*   FakerConfiguration configuration2 = new FakerConfiguration();
            configuration2.Add<TestConfig, string, NewStringGenerator>(TestConfig => TestConfig.ConfigString);
            configuration2.Add<TestConfig, int, NewIntGenerator>(TestConfig => TestConfig.ConfigInt);
            configuration2.Add<TestConfig, int, NewIntGenerator>(TestConfig => TestConfig.PropIntConfig);
            Faker faker2 = new Faker(configuration2);
            PrintObjectValue(faker2.Create<TestConfig>(), " ");
     */ 

            Console.ReadLine();
        }
        public static void PrintObjectValue(object obj, string offset)
        {
            if (obj != null)
            {
                Type ClassType = obj.GetType();
                Console.WriteLine(offset+ClassType.Name);
                FieldInfo[] fieldinfo = ClassType.GetFields();
                PropertyInfo[] propertyInfo = ClassType.GetProperties();
                foreach (var field in fieldinfo)
                {
                    Type type = Type.GetType(field.FieldType.ToString());
                    if ((type.IsClass) && (type.Name != "String"))
                    {
                        offset += " ";
                        PrintObjectValue(field.GetValue(obj), offset);
                        offset.Remove(offset.Length - 1, 1);
                    }
                    else
                    {
                        Console.WriteLine(offset + "Name: " + field.Name + " Fieild Type: " + field.FieldType + " Value: " + field.GetValue(obj).ToString());
                    }
                }
                foreach (var property in propertyInfo)
                {
                    Type type2 = Type.GetType(property.PropertyType.ToString());
                    if ((type2.IsClass) && (type2.Name != "String"))
                    {
                        offset += " ";
                        PrintObjectValue(property.GetValue(obj), offset);
                        offset.Remove(offset.Length - 1, 1);
                    }
                    else
                    {
                        Console.WriteLine(offset + "Name: " + property.Name + " Fieild Type: " + property.PropertyType + " Value: " + property.GetValue(obj).ToString());
                    }
                }
            }
        }
    }

}
