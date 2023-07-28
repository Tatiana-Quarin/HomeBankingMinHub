﻿using System;
using System.Linq;

namespace HomeBankingMindHub.Models
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
                    new Client
                    {
                        FirstName = "Lucia",
                        LastName = "Ramirez",
                        Email = "lu.ra@gmail.com",
                        Password = "3562",
                    },
                    new Client
                    {
                        FirstName = "Juan",
                        LastName = "Perez",
                        Email = "perezjuan@gmail.com",
                        Password = "hora2315",
                    },
                };

                foreach (var client in clients)
                {
                    context.Clients.Add(client);
                }
            }
             context.SaveChanges();

            if (!context.Account.Any())
            {
                var accountVictor = context.Clients.FirstOrDefault(c => c.Email == "vcoronado@gmail.com");
                if (accountVictor != null)
                {
                    var accounts = new Account[]
                    {
                        new Account {ClientId = accountVictor.Id, CreationDate = DateTime.Now, Number = string.Empty, Balance = 0 }
                    };
                    foreach (Account account in accounts)
                    {
                        context.Account.Add(account);
                    }
                    context.SaveChanges();

                }
            }

        }

 
    }
}



