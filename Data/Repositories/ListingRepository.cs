﻿using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Repositories
{
    public class ListingRepository: EntityBaseRepository<Listing>, IListingRepository
    {
        private readonly ApplicationDbContext _context;

        public ListingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
