using API.Controllers.v1;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1
{
    [CustomAuthorize("PASSENGER")]
    public class ChargingBalanceController : BaseApiController
    {
        private readonly ITokenHandlerService _tokenHandlerService;
        private readonly IChargingTransactionRepository _chargingTransactionRepository;
        private readonly ICreditCardsRepository _creditCardsRepository;

        public ChargingBalanceController(
            ITokenHandlerService tokenHandlerService,
            IChargingTransactionRepository chargingTransactionRepository,
            ICreditCardsRepository creditCardsRepository
            )
        {
            _tokenHandlerService = tokenHandlerService;
            _chargingTransactionRepository = chargingTransactionRepository;
            _creditCardsRepository = creditCardsRepository;
        }

        [HttpPost("ChargeWallet")]
        public ActionResult ChargeWallet(ChargingTransactionCreateDto chargingTransactionCreateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            var creditCard = _creditCardsRepository.GetCreditCardByCardNumber(chargingTransactionCreateDto.CardNumber);
            if (creditCard == null)
                return NotFound("No Matching Card Found, Best Luck Next Time");
            if(creditCard.CardType != chargingTransactionCreateDto.paymentMethod)
                return BadRequest("Wrong Card Type");
            if (creditCard.CVC != chargingTransactionCreateDto.CVC)
                return BadRequest("CVC is not Correct, Stealing Credit Cards? eh?");
            if (creditCard.ExpirationDate < DateOnly.FromDateTime(DateTime.UtcNow))
                return BadRequest("Card Expired, go outside once and renew it");
            if (creditCard.ExpirationDate != chargingTransactionCreateDto.ExpirationDate)
                return BadRequest("Bad Thief Catch him ... or her i don't know");
            if (creditCard.Balance < chargingTransactionCreateDto.Amount)
                return BadRequest("Insuffucient Balance, so POOOOOOOOOR!");
            if (!_chargingTransactionRepository.CreateChargingTransaction(chargingTransactionCreateDto, Id))
                return StatusCode(500, "Server Error1");
            if (!_chargingTransactionRepository.SaveChanges())
                return StatusCode(500, "Server Error2");

            return StatusCode(201, new { Message = "Charged Successfully, Lucky Booooy" });
        }
    }
}