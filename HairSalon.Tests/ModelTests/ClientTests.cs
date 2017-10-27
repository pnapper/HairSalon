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

    [TestMethod]
    public void Equals_OverrideTrueIfNamesAreSame_Client()
    {
      //Arrange, Act
      Client firstClient = new Client("Adam Connover", 1);
      Client secondClient = new Client("Adam Connover", 1);

      //Assert
      Assert.AreEqual(firstClient, secondClient);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ClientList()
    {
      //Arrange
      Client testClient = new Client("Adam Connover", 1, 1);

      //Act
      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
  }

}
