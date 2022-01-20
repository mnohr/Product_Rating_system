﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace systemrating.Data.EntityModels
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {  }

        public DbSet<Product> Products { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
