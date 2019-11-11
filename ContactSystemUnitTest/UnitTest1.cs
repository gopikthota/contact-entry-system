#region namespaces
using ContactEntrySystem.Controllers;
using ContactEntrySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
#endregion
namespace ContactSystemUnitTest
{
    /// <summary>
    /// unit test class for contact system controller
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        Mock<ILogger<ContactSystemController>> mockLogger;
        Mock<IContactRepository> mockRepo;
        ContactSystemController ctrl;

        /// <summary>
        /// constructor, mocking for all methods
        /// </summary>
        public UnitTest1()
        {
            var myJsonString = System.IO.File.ReadAllText($"contactsystemlist.json");
            var contactList = JsonConvert.DeserializeObject<IEnumerable<ContactSystemViewModel>>(myJsonString);
            var contact = contactList.Select(x => x).FirstOrDefault();
            mockLogger = new Mock<ILogger<ContactSystemController>>();
            mockRepo = new Mock<IContactRepository>();
            mockRepo.Setup(mr => mr.GetAllContacts()).Returns(contactList);
            mockRepo.Setup(mr => mr.GetContactById(It.IsAny<string>())).Returns(contact);
            string id = "00000000-0000-0000-0000-000000000000";
            mockRepo.Setup(mr => mr.InsertContact(contact)).Returns(id);
            mockRepo.Setup(mr => mr.UpdateContactById(id, contact)).Returns(true);
            mockRepo.Setup(mr => mr.DeleteContact(id)).Returns(true);
            ctrl = new ContactSystemController(mockLogger.Object, mockRepo.Object);
        }
        
        /// <summary>
        /// unit test for get all contacts
        /// </summary>
        [TestMethod]
        public void GetAllContacts()
        {
            // Arrange
            var result = ctrl.GetAllContacts();

            // Act
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        /// <summary>
        /// unit test for insert contact
        /// </summary>
        [TestMethod]
        public void InsertContact()
        {
            // Arrange
            var myJsonString = System.IO.File.ReadAllText("insertcontact.json");
            var contactSystemViewModel = JsonConvert.DeserializeObject<ContactSystemViewModel>(myJsonString);

            // Act
            var result = ctrl.InsertContact(contactSystemViewModel);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        /// <summary>
        /// unit test for get contact by id
        /// </summary>
        [TestMethod]
        public void GetContactById()
        {
            // Arrange
            string id = "00000000-0000-0000-0000-000000000000";
            // Act
            var result = ctrl.GetContactById(id);
            
            // Assert
            Assert.IsNotNull(result);
        }


        /// <summary>
        /// unit test for update contact by id
        /// </summary>
        [TestMethod]
        public void UpdateContactById()
        {
            // Arrange
            var myJsonString = System.IO.File.ReadAllText("updatecontact.json");
            var contactSystemViewModel = JsonConvert.DeserializeObject<ContactSystemViewModel>(myJsonString);

            string id = "00000000-0000-0000-0000-000000000000";
            // Act
            var result = ctrl.UpdateContactById(id, contactSystemViewModel);
            var okresult = result as OkObjectResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, okresult.Value);
        }

        /// <summary>
        /// unit test for delete contact by id
        /// </summary>
        [TestMethod]
        public void DeleteContactById()
        {
            // Arrange
            string id = "00000000-0000-0000-0000-000000000000";
            // Act
            var result = ctrl.DeleteContact(id);
            var okresult = result as OkObjectResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, okresult.Value);
        }
    }
}
