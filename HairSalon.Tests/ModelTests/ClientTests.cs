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

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Assert
      Client testClient = new Client("Adam Connover", 1, 8);

      //Act
      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindClientInDatabase_Client()
    {
      // Arrange
      Client testClient = new Client("Adam Connover", 1, 8);
      testClient.Save();

      // Act
      Client foundClient = Client.Find(testClient.GetId());

      // Assert
      Assert.AreEqual(testClient, foundClient);
    }

    [TestMethod]
    public void Update_UpdatesClientInDatabase_String()
    {
      // Arrange
      string name = "Adam Connover";
      Client testClient = new Client(name, 1, 8);
      testClient.Save();
      string newName = "Don King";

      // Act
      testClient.UpdateName(newName);

      string result = Client.Find(testClient.GetId()).GetName();

      // Assert
      Assert.AreEqual(newName, result);
    }
  }

}
