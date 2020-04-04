using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickleAndHope.Models;

namespace PickleAndHope.DataAccess
{
    public class PickleRepository
    {
        static List<Pickle> _pickles = new List<Pickle> {new Pickle {Type = "Bread and Butter", NumberInStock = 5}};

        public void Add(Pickle pickle)
        {
            _pickles.Add(pickle);
        }

        public void Remove(string type)
        {
            throw new NotImplementedException();
        }

        public Pickle Update(Pickle pickle)
        {
            var pickleToupdate = GetByType(pickle.Type);

            pickleToupdate.NumberInStock += pickle.NumberInStock;

            return pickleToupdate;
        }

        public Pickle GetByType(string type)
        {
            return _pickles.FirstOrDefault(p => p.Type == type);
        }

        public List<Pickle> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
