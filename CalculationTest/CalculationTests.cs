using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WSUniversalLib;

namespace CalculationTest
{
    [TestClass]
    public class CalculationTests
    {
        [TestMethod]
        public void GetQuantityForProduct_NonExistentProductType()
        {
            Calculation calculation = new Calculation();
            int provPerem = calculation.GetQuantityForProduct(4, 1, 5, 40, 45);
            Assert.AreEqual(-1, provPerem, "не правильно");

        }

        [TestMethod]
        public void GetQuantityForProduct_NonExistentMaterilType()
        {
            Calculation calculation = new Calculation();
            int provPerem = calculation.GetQuantityForProduct(2, 3, 5, 40, 45);
            Assert.AreEqual(-1, provPerem, "не правильно");

        }

        [TestMethod]
        public void GetQuantityForProduct_NonExistentCount()
        {
            Calculation calculation = new Calculation();
            int provPerem = calculation.GetQuantityForProduct(2, 3, -150, 40, 45);
            Assert.AreEqual(-1, provPerem, "не правильно");

        }
        [TestMethod]
        public void GetQuantityForProduct_NonExistentWidth()
        {
            Calculation calculation = new Calculation();
            int provPerem = calculation.GetQuantityForProduct(2, 3, 5, 0, 45);
            Assert.AreEqual(-1, provPerem, "не правильно");

        }

        [TestMethod]
        public void GetQuantityForProduct_NonExistentLength()
        {
            Calculation calculation = new Calculation();
            int provPerem = calculation.GetQuantityForProduct(2, 3, 5, 40, 0);
            Assert.AreEqual(-1, provPerem, "не правильно");

        }


        [TestMethod]
        public void GetQuantityForProduct_NegativVseData()
        {
            Calculation calculation = new Calculation();
            int provPerem = calculation.GetQuantityForProduct(5, 5, -5, -5, -5);
            Assert.AreEqual(-1, provPerem, "не правильно");

        }

        [TestMethod]
        public void GetQuantityForProduct_PositiveVseData()
        {
            Calculation calculation = new Calculation();
            int provPerem = calculation.GetQuantityForProduct(3, 1, 15, 20, 45);
            Assert.AreEqual(114147, provPerem, "не правильно");
        }
    }
}
