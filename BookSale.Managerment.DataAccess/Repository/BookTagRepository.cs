﻿using BookSale.Managerment.DataAccess.DataAccess;
using BookSale.Managerment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookSale.Managerment.Domain.Abstract;
namespace BookSale.Managerment.DataAccess.Repository
{
    public class BookTagRepository : BaseRepository<BookTags, ApplicationDbContext>, IBookTagRepository
    {
        public BookTagRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
