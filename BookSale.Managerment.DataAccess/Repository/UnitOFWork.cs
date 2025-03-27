using BookSale.Managerment.DataAccess.DataAccess;
using BookSale.Managerment.Domain.Abstract;
using System.Threading.Tasks;

namespace BookSale.Managerment.DataAccess.Repository
{
    public class UnitOFWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IGenreRepository? _genreRepository;
        private IBookImageRepository? _bookImageRepository;
        private IBookRepository? _bookRepository;
        private IBookTagRepository? _bookTagRepository;
        private ITagsRepository? _tagsRepository;
        public UnitOFWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_context);
        public IBookImageRepository BookImageRepository => _bookImageRepository ??= new BookImageRepository(_context);
        public IBookRepository BookRepository => _bookRepository ??= new BookRepository(_context);
        public IBookTagRepository BookTagRepository => _bookTagRepository ??= new BookTagRepository(_context);
        public ITagsRepository TagsRepository => _tagsRepository ??= new TagsRepository(_context);
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
