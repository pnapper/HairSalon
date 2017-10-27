using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using HairSalonApp;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{

  [TestClass]

  public class HairSalonTest : IDisposable
  {
    public HairSalonTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=patrick_napper_test;";
    }

    public void Dispose()
    {
      Client.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyFirst_0()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
  }
}
