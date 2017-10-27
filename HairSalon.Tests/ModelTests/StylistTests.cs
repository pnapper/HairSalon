using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using HairSalonApp;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{

  [TestClass]

  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=patrick_napper_test;";
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyFirst_0()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_OverrideTrueIfNamesAreSame_Stylist()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("Paul Mitchell", "Men's cuts", 1);
      Stylist secondStylist = new Stylist("Paul Mitchell", "Men's cuts", 1);

      //Assert
      Assert.AreEqual(firstStylist, secondStylist);
    }

    [TestMethod]
    public void Save_SavesToDatabase_StylistList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Paul Mitchell", "Men's cuts", 1);

      //Act
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Assert
      Stylist testStylist = new Stylist("Paul Mitchell", "Men's cuts", 1);

      //Act
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindStylistInDatabase_Stylist()
    {
      // Arrange
      Stylist testStylist = new Stylist("Paul Mitchell", "Men's cuts", 1);
      testStylist.Save();

      // Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      // Assert
      Assert.AreEqual(testStylist, foundStylist);
    }
  }
}
