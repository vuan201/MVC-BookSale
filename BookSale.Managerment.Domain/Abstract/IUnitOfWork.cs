﻿using System.Threading.Tasks;

namespace BookSale.Managerment.Domain.Abstract
{
    public interface IUnitOfWork
    {
        IGenreRepository GenreRepository { get; }
        IBookImageRepository BookImageRepository { get; }
        IBookRepository BookRepository { get; }
        IBookTagRepository BookTagRepository { get; }
        ITagsRepository TagsRepository { get; }
        Task SaveChangesAsync();
    }
}