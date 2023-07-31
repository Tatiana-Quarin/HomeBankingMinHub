using HomeBankingMindHub.dtos;
using HomeBankingMindHub.Models;
using HomeBankingMinHub.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using HomeBankingMinHub.Models;

namespace HomeBankingMindHub.Controllers

{

    [Route("api/[controller]")]

    [ApiController]

    public class AccountsController : ControllerBase

    {
        private IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository)

        {
            _accountRepository = accountRepository;
        }

        // GET: api/Accounts
        [HttpGet]
        public IActionResult Get()
        {
            try { 
            var accounts = _accountRepository.GetAllAccounts();
            var accountsDTO = new List<AccountDTO>();

                // Mapear las entidades de dominio a objetos DTO para devolver la información
                foreach (Account account in accounts) {
                    var newAccountDTOs = new AccountDTO
                    {
                        Id = account.Id,
                        CreationDate = account.CreationDate,
                        Number = account.Number,
                        Balance = account.Balance,
                        Transactions = account.Transactions.Select(tr => new TransactionDTO
                        {
                            Id = tr.Id,
                            Type = tr.Type,
                            Amount = tr.Amount,
                            Description = tr.Description,
                            Date = tr.Date,
                        }).ToList()

                        // Otros atributos relevantes de AccountDTO
                    };
                accountsDTO.Add(newAccountDTOs);
                 }

                return Ok(accountsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            try
            {

                var account = _accountRepository.FindById(id);

            if (account == null)
            {
                return NotFound(); // Devolver respuesta 404 si no se encuentra la cuenta
            }

            // Mapear la entidad de dominio a un objeto DTO para devolver la información
            var accountDTO = new AccountDTO
            {
                Id = account.Id,
                CreationDate = account.CreationDate,
                Number = account.Number,
                Balance = account.Balance,
                Transactions = account.Transactions.Select(tr => new TransactionDTO
                {
                    Id = tr.Id,
                    Type = tr.Type,
                    Amount = tr.Amount,
                    Description = tr.Description,
                    Date = tr.Date,
                }).ToList()

                // Otros atributos relevantes de AccountDTO
            };

            return Ok(accountDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
       
        }
    }


}

