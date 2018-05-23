using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMarioWorldRemake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake.Tests
{
    [TestClass()]
    public class FilerReaderTests
    {
        FileReader reader = new FileReader();
        string file = @"Content\UnitTest.txt";
        string[,] test;
        string[,] test2;
       



        [TestInitialize]
        public void TestInit()
        {
             test = new string[6, 2];
            test[0, 0] = "0";
            test[1, 0] = "0";
            test[2, 0] = "1";
            test[3, 0] = "1";
            test[4, 0] = "2";
            test[5, 0] = "2";
            test[0, 1] = "1";
            test[1, 1] = "1";
            test[2, 1] = "2";
            test[3, 1] = "2";
            test[4, 1] = "3";
            test[5, 1] = "3";
            test2=reader.Read(file);

        }
        [TestMethod()]
        public void ReadTest()
        {

            try
            {
                for (int i = 0; i < test2.GetLength(0)+1; i++)
                {
                    for (int j = 0; j < test2.GetLength(1); j++)
                    {
                        if (!test[i, j].Equals(test2[i, j]))
                        {
                            Assert.Fail();
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("de file is niet juist geformat");
            }


        }
    }
}