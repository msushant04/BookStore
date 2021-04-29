﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {

        }

        public DbSet<Books> Books { get; set; }
        public DbSet<Language> Language { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=TECH-LP-872\\MSSQLSERVER2019;Database=BookStore;user=sa;Password=Pass@123;");
        //    base.OnConfiguring(optionsBuilder); 
        //}
    }
}
