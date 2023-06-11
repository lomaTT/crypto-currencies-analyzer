using System.Security.Claims;
using CryptoAnalyzerCore.DataBase;
using CryptoAnalyzerCore.DataBase.Services;
using CryptoAnalyzerCore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoAnalyzerWebUI.Controllers;

[ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly DatabaseManager _manager;

        public DatabaseController()
        {
            _manager = new DatabaseManager();
        }

        [HttpGet("fill")]
        public IActionResult Fill()
        {
            _manager.ClearAllData();
            _manager.FillDatabase();

            return Ok();
        }

        // User CRUD Endpoints

        [HttpGet("users/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _manager.UserService.GetUser(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("users")]
        public IActionResult AddUser(User user)
        {
            try
            {
                _manager.UserService.AddUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("users/{id}")]
        public IActionResult RemoveUser(int id)
        {
            try
            {
                _manager.UserService.RemoveUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("users")]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            
            Console.WriteLine($"User id: {userId}, name: {userName}, email: {userEmail}");
            
            var users = _manager.UserService.GetAllUsers();
            return Ok(users);
        }

        // ExchangeCurrencyRate CRUD Endpoints

        [HttpGet("exchange-currency-rates/{currencyId}")]
        public IActionResult GetExchangeCurrencyRate(int currencyId)
        {
            var exchangeCurrencyRate = _manager.ExchangeCurrencyRateService.GetExchangeCurrencyRate(currencyId);
            if (exchangeCurrencyRate == null)
                return NotFound();

            return Ok(exchangeCurrencyRate);
        }

        [HttpPost("exchange-currency-rates")]
        public IActionResult AddExchangeCurrencyRate(ExchangeCurrencyRate exchangeCurrencyRate)
        {
            try
            {
                _manager.ExchangeCurrencyRateService.AddExchangeCurrencyRate(exchangeCurrencyRate);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("exchange-currency-rates")]
        public IActionResult RemoveExchangeCurrencyRate(ExchangeCurrencyRate exchangeCurrencyRate)
        {
            try
            {
                _manager.ExchangeCurrencyRateService.RemoveExchangeCurrencyRate(exchangeCurrencyRate);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("exchange-currency-rates/exchange/{exchangeId}")]
        public IActionResult GetExchangeCurrencyRatesByExchangeId(int exchangeId)
        {
            var exchangeCurrencyRates = _manager.ExchangeCurrencyRateService.GetExchangeCurrencyRatesByExchangeId(exchangeId);
            return Ok(exchangeCurrencyRates);
        }

        // CurrencyRate CRUD Endpoints

        [HttpGet("currency-rates/{id}")]
        public IActionResult GetCurrencyRate(int id)
        {
            var currencyRate = _manager.CurrencyRateService.GetCurrencyRate(id);
            if (currencyRate == null)
                return NotFound();

            return Ok(currencyRate);
        }

        [HttpPost("currency-rates")]
        public IActionResult AddCurrencyRate(CurrencyRate currencyRate)
        {
            try
            {
                _manager.CurrencyRateService.AddCurrencyRate(currencyRate);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("currency-rates/currency/{currencyId}")]
        public IActionResult GetCurrencyRatesByCurrencyId(int currencyId)
        {
            var currencyRates = _manager.CurrencyRateService.GetCurrencyRatesByCurrencyId(currencyId);
            return Ok(currencyRates);
        }

        // Currency CRUD Endpoints

        [HttpGet("currencies/{id}")]
        public IActionResult GetCurrency(int id)
        {
            var currency = _manager.CurrencyService.GetCurrency(id);
            if (currency == null)
                return NotFound();

            return Ok(currency);
        }

        [HttpPost("currencies")]
        public IActionResult AddCurrency(Currency currency)
        {
            try
            {
                _manager.CurrencyService.AddCurrency(currency);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("currencies/{id}")]
        public IActionResult RemoveCurrency(int id)
        {
            try
            {
                _manager.CurrencyService.RemoveCurrency(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("currencies")]
        public IActionResult GetAllCurrencies()
        {
            var currencies = _manager.CurrencyService.GetAllCurrencies();
            return Ok(currencies);
        }

        // CurrencyType CRUD Endpoints

        [HttpGet("currency-types/{id}")]
        public IActionResult GetCurrencyType(int id)
        {
            var currencyType = _manager.CurrencyTypeService.GetCurrencyType(id);
            if (currencyType == null)
                return NotFound();

            return Ok(currencyType);
        }

        [HttpPost("currency-types")]
        public IActionResult AddCurrencyType(CurrencyType currencyType)
        {
            try
            {
                _manager.CurrencyTypeService.AddCurrencyType(currencyType);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("currency-types/{id}")]
        public IActionResult RemoveCurrencyType(int id)
        {
            try
            {
                _manager.CurrencyTypeService.RemoveCurrencyType(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Exchange CRUD Endpoints

        [HttpGet("exchanges/{id}")]
        public IActionResult GetExchange(int id)
        {
            var exchange = _manager.ExchangeService.GetExchange(id);
            if (exchange == null)
                return NotFound();

            return Ok(exchange);
        }

        [HttpPost("exchanges")]
        public IActionResult AddExchange(Exchange exchange)
        {
            try
            {
                _manager.ExchangeService.AddExchange(exchange);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("exchanges/{id}")]
        public IActionResult RemoveExchange(int id)
        {
            try
            {
                _manager.ExchangeService.RemoveExchange(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("exchanges")]
        public IActionResult GetAllExchanges()
        {
            var exchanges = _manager.ExchangeService.GetAllExchanges();
            return Ok(exchanges);
        }
    }