using EFDataAccess.Repositories;
using SpanishWords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace EFDataAccess.Repositories.Infrastructure
{
    public interface IWordRepository
    {
        bool Add(Word word);

        int GetLastStaticticId();
        Word GetWordById(int id);

        public IEnumerable<Word> GetAllWords();

        bool Edit(Word word);
        bool Delete(Word word);


    }
}
