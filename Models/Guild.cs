using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace TitanBot.Models
{
    public class Guild
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual List<Rule> Rules { get; set; }

        public virtual List<Rank> Ranks { get; set; }

    }

    public class Rule
    {
        public int ID { get; set; }

        public string Text { get; set; }

        public int Number { get; set; }
    }

    public class Member
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public int Points { get; set; }

        public string DiscordId { get; set; }
    }

    public class Rank
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public virtual List<Permission> Permissions { get; set; }

        public virtual List<Member> Members { get; set; }
    }

    public class Permission
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }

    public class TitanDBContext : DbContext
    {
        public TitanDBContext() : base("TitanDBContext")
        {

        }

        public DbSet<Guild> Guilds { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}
