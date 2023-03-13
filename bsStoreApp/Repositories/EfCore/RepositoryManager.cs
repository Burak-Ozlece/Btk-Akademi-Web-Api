using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _contex;
        private readonly Lazy<IBookRepository> _bookRepository;

        public RepositoryManager(RepositoryContext contex, Lazy<IBookRepository> bookRepository)
        {
            _contex = contex;
            _bookRepository = new Lazy<IBookRepository>(()=> new BookRepository(_contex));
        }

        public IBookRepository Book()
        {
            return _bookRepository.Value;
        }

        public void Save() => _contex.SaveChanges();
    }
}
