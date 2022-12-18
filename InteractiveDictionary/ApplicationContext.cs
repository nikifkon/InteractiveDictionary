using System;
using System.IO;
using InteractiveDictionary.domain;
using Microsoft.EntityFrameworkCore;

namespace InteractiveDictionary
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Word> Words {get;set; } = null!;
        public DbSet<Tag> Tags {get;set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "InteractiveDictionary");
            if (!Directory.Exists(appDataFolder))
            {
                Directory.CreateDirectory(appDataFolder);
            }
            var absolutePath = Path.Combine(appDataFolder, "InteractiveDictionary.db");
            if (!File.Exists(absolutePath))
            {
                using var file = File.Create(absolutePath);
            }
            optionsBuilder.UseSqlite($"Data Source={absolutePath}");
        }
    }
}