#region namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LiteDB;
using Microsoft.Extensions.Logging;
using ContactEntrySystem.Models;
using Newtonsoft.Json;
#endregion

namespace ContactEntrySystem.Controllers
{
    /// <summary>
    /// Contact Entry System Controller which has CRUD operations using  LiteDB
    /// contactsystem.db is created inside the application
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContactSystemController : ControllerBase
    {
        /// <summary>
        /// logger 
        /// </summary>
        private readonly ILogger<ContactSystemController> _logger;

        /// <summary>
        /// interface for contact repository
        /// </summary>
        private readonly IContactRepository contactRepository;

        /// <summary>
        /// constructor for Contact Entry System 
        /// </summary>
        /// <param name="logger"></param>
        public ContactSystemController(ILogger<ContactSystemController> logger,
            IContactRepository _repo)
        {
            this._logger = logger;
            this.contactRepository = _repo;
        }

        /// <summary>
        /// Get all the contacts in the db.
        /// feed all the contacts with input as JSON file
        /// </summary>
        /// <returns>
        /// list of all contacts in the db
        /// </returns>
        [HttpGet]
        [Route("contacts")]
        public IActionResult GetAllContacts()
        {
            return Ok(this.contactRepository.GetAllContacts());
        }

        /// <summary>
        /// insert a contact by taking input as view model
        /// </summary>
        /// <param name="input"></param>
        /// <returns>200-status code on successful insertion</returns>
        [Route("contacts")]
        [HttpPost]
        public IActionResult InsertContact(ContactSystemViewModel input)
        {
            return Ok(this.contactRepository.InsertContact(input));
        }

        /// <summary>
        /// Get the contact by id
        /// </summary>
        /// <param name="id">id to get the contact view model</param>
        /// <returns>contact view model</returns>
        [HttpGet]
        [Route("contacts/{id}")]
        public ContactSystemViewModel GetContactById(string id)
        {
            return this.contactRepository.GetContactById(id);
        }

        /// <summary>
        /// update the contact 
        /// </summary>
        /// <param name="id">id for which contact will be updated</param>
        /// <param name="input">contact view model</param>
        /// <returns>boolean- true/false</returns>
        [HttpPut]
        [Route("contacts/{id}")]
        public IActionResult UpdateContactById(string id, ContactSystemViewModel input)
        {
            return Ok(this.contactRepository.UpdateContactById(id, input));
        }

        /// <summary>
        /// delete the contact 
        /// </summary>
        /// <param name="id">identifier to delete the contact</param>
        /// <returns>200-status code</returns>
        [HttpDelete]
        [Route("contacts/{id}")]
        public IActionResult DeleteContact(string id)
        {
            return Ok(this.contactRepository.DeleteContact(id));
        }
    }
}