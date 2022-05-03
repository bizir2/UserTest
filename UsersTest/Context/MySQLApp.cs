using System;
using Microsoft.EntityFrameworkCore;
using UsersTest.Entites;
using UsersTest.Interfaces;

namespace UsersTest.Context
{
    public class MySqlContext : DbContext
    {
        public DbSet<UserDocument> Users { get; set; }

        private readonly IMySqlConnection _mySqlConnection;
        
        public MySqlContext(IMySqlConnection mySqlConnection)
        {
            _mySqlConnection = mySqlConnection;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_mySqlConnection.ConnectionString,
                new MySqlServerVersion(new Version(_mySqlConnection.Version)));
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDocument>().HasKey(u => u.ID);
            modelBuilder.Entity<UserDocument>()
                .Property(e => e.ID).ValueGeneratedNever();
            modelBuilder.Entity<UserDocument>()
                .Property(e => e.Name).HasColumnType("VARCHAR").HasMaxLength(100);
        }
    }
}
