using ContactEntrySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactEntrySystem.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IContactRepository
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ContactSystemViewModel> GetAllContacts();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string InsertContact(ContactSystemViewModel input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ContactSystemViewModel GetContactById(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        bool UpdateContactById(string id, ContactSystemViewModel input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteContact(string id);
    }
}
