using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactEntrySystem.Models
{
    public class ContactRepository : IContactRepository
    {
        public ContactRepository()
        {
        }

        bool IContactRepository.DeleteContact(string id)
        {
            bool isDeleted = false;
            using (var db = new LiteDatabase(@"ContactSystem.db"))
            {
                var collection = db.GetCollection<ContactSystemViewModel>("ContactEntrySystem");
                var result = collection.Find(x => x.Id == Guid.Parse(id)).FirstOrDefault();
                if (result != null)
                {
                    isDeleted = collection.Delete(id);
                }
            }
            return isDeleted;
        }

        IEnumerable<ContactSystemViewModel> IContactRepository.GetAllContacts()
        {
            LiteCollection<ContactSystemViewModel> list = null;
            var myJsonString = System.IO.File.ReadAllText($"contactsystemlist.json");
            var contactSystemViewModel = JsonConvert.DeserializeObject<IEnumerable<ContactSystemViewModel>>(myJsonString);
            using (var db = new LiteDatabase(@"ContactSystem.db"))
            {
                list = db.GetCollection<ContactSystemViewModel>("ContactEntrySystem");
            }
            return list.FindAll();
        }

        ContactSystemViewModel IContactRepository.GetContactById(string id)
        {
            using (var db = new LiteDatabase(@"ContactSystem.db"))
            {
                var collection = db.GetCollection<ContactSystemViewModel>("ContactEntrySystem");
                var result = collection.Find(x => x.Id == Guid.Parse(id)).FirstOrDefault();
                return result;
            }
        }

        string IContactRepository.InsertContact(ContactSystemViewModel input)
        {
            BsonValue bsonValue;
            using (var db = new LiteDatabase(@"ContactSystem.db"))
            {
                var collection = db.GetCollection<ContactSystemViewModel>("ContactEntrySystem");
                bsonValue = collection.Insert(input);
                return bsonValue.AsString;
                //collection.EnsureIndex(x => x.Id);
            }
        }

        bool IContactRepository.UpdateContactById(string id, ContactSystemViewModel input)
        {
            bool isUpdated = false;
            using (var db = new LiteDatabase(@"ContactSystem.db"))
            {
                var collection = db.GetCollection<ContactSystemViewModel>("ContactEntrySystem");
                var result = collection.Find(x => x.Id == Guid.Parse(id)).FirstOrDefault();
                result = input;
                isUpdated = collection.Update(input);
            }
            return isUpdated;
        }
    }
}
