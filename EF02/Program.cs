using EF02.Data;
using Microsoft.EntityFrameworkCore;

namespace EF02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var context = new EventHubDbContext())
                {
                    Console.WriteLine("Connecting to Database...");

                    bool created = context.Database.EnsureCreated();

                    Console.WriteLine("Database Created: " + created);

                    Console.WriteLine("Can Connect: " +
                        context.Database.CanConnect());

                    Console.WriteLine("Database Name: " +
                        context.Database.GetDbConnection().Database);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:");
                Console.WriteLine(ex.ToString());
            }

            Console.ReadKey();
        }
    }
}
