﻿using BookSale.Managerment.Application.DTOs;

namespace BookSale.Managerment.Application.Abstracts
{
    public interface IBookService
    {
        Task<ResponseModel<BookDetailDTO>> Create(BookDetailDTO bookDetail);
        Task<ResponseModel<BookDetailDTO>> Update(int id, BookDetailDTO bookDetail);
        Task<ResponseModel<BookDetailDTO>> GetBook(int id);
        Task<ResponseModel<IEnumerable<BookDTO>>> GetBookList(RequestFilterModel filter);
    }
}