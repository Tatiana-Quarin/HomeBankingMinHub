using HomebankingMindHub.dtos;
using HomebankingMindHub.Models;
using HomebankingMindHub.Repositories;
using HomeBankingMindHub.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace HomeBankingMindHub.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IAccountRepository _accountRepository;

        private IClientRepository _clientRepository;
        public AccountsController(IAccountRepository accountRepository, IClientRepository clientRepository)
        {
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
        }

        [HttpGet("accounts")]
        public IActionResult Get()
        {
            try
            {
                var accounts = _accountRepository.GetAllAccounts();
                var accountsDTO = new List<AccountDTO>();

                foreach (Account account in accounts)
                {
                    var newAccountDTO = new AccountDTO
                    {
                        Id = account.Id,
                        Number = account.Number,
                        CreationDate = account.CreationDate,
                        Balance = account.Balance,
                        Transactions = account.Transactions.Select(tr => new TransactionDTO
                        {
                            Id = tr.Id,
                            Type = tr.Type,
                            Amount = tr.Amount,
                            Description = tr.Description,
                            Date = tr.Date,
                        }).ToList()
                    };
                    accountsDTO.Add(newAccountDTO);
                }
                return Ok(accountsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("accounts/{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                var account = _accountRepository.FindById(id);
                if (account == null)
                {
                    return Forbid();
                }
                var accountDTO = new AccountDTO
                {
                    Id = account.Id,
                    Number = account.Number,
                    CreationDate = account.CreationDate,
                    Balance = account.Balance,
                    Transactions = account.Transactions.Select(tr => new TransactionDTO
                    {
                        Id = tr.Id,
                        Type = tr.Type,
                        Amount = tr.Amount,
                        Description = tr.Description,
                        Date = tr.Date,
                    }).ToList()
                };
                return Ok(accountDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("clients/current/accounts")]
        public IActionResult GetAccounts()
        {
            string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
            if (email == string.Empty)
            {
                return Forbid();
            }

            Client client = _clientRepository.FindByEmail(email);

            if (client == null)
            {
                return Forbid();
            }

            var currentAccounts = _accountRepository.GetAccountsByClient(client.Id);
            return Ok(currentAccounts);
        }

        [HttpPost("clients/current/accounts")]

        public IActionResult PostCurrentAccounts()
        {
            string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
            if (email == string.Empty)
            {
                return Forbid();
            }

            Client client = _clientRepository.FindByEmail(email);

            if (client == null)
            {
                return Forbid();
            }

            if (client.Accounts.Count >= 3)
            {
                return StatusCode(403, "El maximo de cuentas por cliente es de 3");
            }

            var random = new Random();
            var account = new Account
            {
                ClientId = client.Id,
                Number = "VIN-" + random.Next(100000, 1000000).ToString(),
                CreationDate = DateTime.Now,
                Balance = 0,
            };

            _accountRepository.Save(account);

            return Created("", account);
        }
    }
}
