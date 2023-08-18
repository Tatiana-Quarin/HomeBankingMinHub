using HomebankingMindHub.Models;
using HomeBankingMindHub.Models;
using Microsoft.VisualBasic;
using System;
using System.Linq;


namespace HomebankingMindHub.Models
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
                    
                    new Client { FirstName = "Victor", LastName = "Coronado", Email = "vcoronado@gmail.com", Password = "123456"},
                    new Client { FirstName = "Tatiana", LastName = "Quarin", Email = "tatiana.quarin@gmail.com",Password = "12345"},
                    new Client { FirstName = "Lucia", LastName = "Ramirez", Email = "lu.ra@gmail.com", Password = "3562" },
                    new Client { FirstName = "Juan", LastName = "Perez", Email = "perezjuan@gmail.com",Password = "hora2315" },
                };
                foreach (var client in clients)
                {
                    context.Clients.Add(client);
                }
                context.SaveChanges();
            }
     
            if (!context.Accounts.Any())
            {
                var client1 = context.Clients.FirstOrDefault(c => c.Email == "vcoronado@gmail.com");
                if (client1 != null)
                {
                    var accounts = new Account[]
                    {
                        new Account {ClientId = client1.Id, CreationDate = DateTime.Now, Number = "VIN001", Balance = 26000 },
                        new Account {ClientId = client1.Id, CreationDate = DateTime.Now, Number = "VIN002", Balance = 2500 },
                    };
                    foreach (Account account in accounts)
                    {
                        context.Accounts.Add(account);
                    }
                }
                var client2 = context.Clients.FirstOrDefault(c => c.Email == "tatiana.quarin@gmail.com");
                if (client2 != null)
                {
                    var accounts = new Account[]
                    {
                        new Account {ClientId = client2.Id, CreationDate = DateTime.Now, Number = "VIN003", Balance = 3000 },
                        new Account {ClientId = client2.Id, CreationDate = DateTime.Now, Number = "VIN004", Balance = 1700 },
                    };
                    foreach (Account account in accounts)
                    {
                         context.Accounts.Add(account);
                    }
                }
                    context.SaveChanges();
            }

            if (!context.Transactions.Any())
            {
                var account1 = context.Accounts.FirstOrDefault(c => c.Number == "VIN001");

                if (account1 != null)

                {
                    var transactions = new Transaction[]

                    {
                        new Transaction { AccountId= account1.Id, Amount = 10000, Date= DateTime.Now.AddHours(-5), Description = "Transferencia recibida", Type = TransactionType.CREDIT.ToString() },

                        new Transaction { AccountId= account1.Id, Amount = -2000, Date= DateTime.Now.AddHours(-6), Description = "Compra en tienda mercado libre", Type = TransactionType.DEBIT.ToString() },

                        new Transaction { AccountId= account1.Id, Amount = -3000, Date= DateTime.Now.AddHours(-7), Description = "Compra en tienda xxxx", Type = TransactionType.DEBIT.ToString() },
                    };

                    foreach (Transaction transaction in transactions)

                    {

                        context.Transactions.Add(transaction);

                    }
                        
                        context.SaveChanges();

                }

            }

            if (!context.ClientLoans.Any())
            {
                
                if (!context.Loans.Any())
                {
                    var loans = new Loan[]
                    {
                        new Loan { Name = "Hipotecario", MaxAmount = 500000, Payments = "12,24,36,48,60" },
                        new Loan { Name = "Personal", MaxAmount = 100000, Payments = "6,12,24" },
                        new Loan { Name = "Automotriz", MaxAmount = 300000, Payments = "6,12,24,36" },
                    };
                    foreach (Loan loan in loans)
                    {
                        context.Loans.Add(loan);
                    };
                    context.SaveChanges();
                }
                var loanHipoteca = context.Loans.FirstOrDefault(l => l.Name == "Hipotecario");
                var loanPersonal = context.Loans.FirstOrDefault(l => l.Name == "Personal");
                var loanAuto = context.Loans.FirstOrDefault(l => l.Name == "Automotriz");


                
                var client1 = context.Clients.FirstOrDefault(c => c.Email == "vcoronado@gmail.com");
                if (client1 != null)
                {
                    var clientLoans = new ClientLoan[]
                    {
                        new ClientLoan { Amount = 400000, ClientId = client1.Id, LoanId = loanHipoteca.Id, Payments = "60" },
                        new ClientLoan { Amount = 100000, ClientId = client1.Id, LoanId = loanPersonal.Id, Payments = "24" },
                        new ClientLoan { Amount = 300000, ClientId = client1.Id, LoanId = loanAuto.Id, Payments = "36" },
                    };
                    foreach (ClientLoan clientLoan in clientLoans)
                    {
                        context.ClientLoans.Add(clientLoan);
                    }
                }

                var client2 = context.Clients.FirstOrDefault(c => c.Email == "tatiana.quarin@gmail.com");
                if (client2 != null && client2?.ClientLoans == null)
                {
                    var clientLoans = new ClientLoan[]
                    {
                        new ClientLoan { Amount = 100000, ClientId = client2.Id, LoanId = loanPersonal.Id, Payments = "24" },
                        new ClientLoan { Amount = 300000, ClientId = client2.Id, LoanId = loanAuto.Id, Payments = "36" },
                    };
                    foreach (ClientLoan clientLoan in clientLoans)
                    {
                        context.ClientLoans.Add(clientLoan);
                    }
                }
                context.SaveChanges();

            }

            if (!context.Cards.Any())
            {
                var client1 = context.Clients.FirstOrDefault(c => c.Email == "vcoronado@gmail.com");
                if (client1 != null)
                {
                    var cards = new Card[]
                    {
                        new Card {
                            ClientId= client1.Id, CardHolder = client1.FirstName + " " + client1.LastName,
                            Type = CardType.DEBIT.ToString(), Color = CardColor.GOLD.ToString(),
                            Number = "3325-6745-7876-4445", Cvv = 990,
                            FromDate= DateTime.Now, ThruDate= DateTime.Now.AddYears(4),
                        },
                        new Card {
                            ClientId= client1.Id, CardHolder = client1.FirstName + " " + client1.LastName,
                            Type = CardType.CREDIT.ToString(), Color = CardColor.TITANIUM.ToString(),
                            Number = "2234-6745-552-7888", Cvv = 750,
                            FromDate= DateTime.Now, ThruDate= DateTime.Now.AddYears(5),
                        },
                    };
                    foreach (Card card in cards)
                    {
                        context.Cards.Add(card);
                    }
                }

                var client2 = context.Clients.FirstOrDefault(c => c.Email == "tatiana.quarin@gmail.com");
                if (client2 != null)
                {
                    var cards = new Card[]
                    {
                        new Card {
                            ClientId= client2.Id, CardHolder = client2.FirstName + " " + client2.LastName,
                            Type = CardType.DEBIT.ToString(), Color = CardColor.GOLD.ToString(),
                            Number = "3325-6745-7777-4444", Cvv = 330,
                            FromDate= DateTime.Now, ThruDate= DateTime.Now.AddYears(6),
                        },
                    };
                    foreach (Card card in cards)
                    {
                        context.Cards.Add(card);
                    }

                    context.SaveChanges();
                }


            }
        }
    }
}








