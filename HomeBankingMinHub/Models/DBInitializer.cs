using System.Linq;

namespace HomeBankingMinHub.Models
{
    public class DBInitializer
    {
        private static Client client;

        public static void Initialize(HomeBankingContext context)
        {
            if (!context.Clients.Any())
            {
                var clients = new Client[]
                {
                    new Client
                    {
                        FirstName = "Victor",
                        LastName = "Coronado",
                        Email = "vcoronado@gmail.com",
                        Password = "123456",
                    },
                    new Client
                    {
                        FirstName = "Tatiana",
                        LastName = "Quarin",
                        Email = "tatiana.quarin@gmail.com",
                        Password = "12345",
                    },
                };

                foreach (var client in clients)
                {
                    context.Clients.Add(client);
                }
            }
             context.SaveChanges();
        }
    }
}



