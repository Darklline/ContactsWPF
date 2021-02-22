namespace Contacts.DataAccess.Migrations
{
    using Contacts.Model;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Contacts.DataAccess.ContactDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Contacts.DataAccess.ContactDbContext context)
        {
            context.Contacts.AddOrUpdate(c => c.FirstName,
                new Contact { FirstName = "John", LastName = "Cena" },
                new Contact { FirstName = "Rendy", LastName = "Orton" });
        }
    }
}
