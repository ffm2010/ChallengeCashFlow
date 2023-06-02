using FinancialChallenge.Web.Models;
using FinancialChallenge.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinancialChallenge.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<IActionResult> TransactionIndex()
        {
            List<TransactionDto> list = new();
            decimal totalCashSlow = 0;

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _transactionService.GetAllTransactionsAsync<ResponseDto>(accessToken);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<TransactionDto>>(Convert.ToString(response.Result));
            }

            foreach (var item in list)
            {
                if (item.TypePayment == Models.Enums.TypePayment.Credit)
                    totalCashSlow += (item.Total / item.AmountParcels);
                else
                    totalCashSlow += item.Total;
            }

            ViewBag.TotalCashFlow = totalCashSlow;

            return View(list);
        }

        public async Task<IActionResult> TransactionCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransactionCreate(TransactionDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                model.UserCreated = User.Identity.Name;
                var response = await _transactionService.CreateTransactionAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(TransactionIndex));
                }
            }
            return View(model);
        }
        public async Task<IActionResult> TransactionEdit(Guid transactionId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _transactionService.GetTransactionByIdAsync<ResponseDto>(transactionId, accessToken);
            if (response != null && response.IsSuccess)
            {
                TransactionDto model = JsonConvert.DeserializeObject<TransactionDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransactionEdit(TransactionDto model)
        {
            if (ModelState.IsValid)
            {

                var accessToken = await HttpContext.GetTokenAsync("access_token");
                model.UserUpdate = User.Identity.Name;
                var response = await _transactionService.UpdateTransactionAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(TransactionIndex));
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TransactionDelete(Guid transactionId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _transactionService.GetTransactionByIdAsync<ResponseDto>(transactionId, accessToken);
            if (response != null && response.IsSuccess)
            {
                TransactionDto model = JsonConvert.DeserializeObject<TransactionDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransactionDelete(TransactionDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                model.UserUpdate = User.Identity.Name;
                var response = await _transactionService.DeleteTransactionAsync<ResponseDto>(model.TransactionId, accessToken);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(TransactionIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> TransactionDashboard()
        {
            List<TransactionDto> list = new();
            decimal totalCashSlow = 0;

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _transactionService.GetAllTransactionsAsync<ResponseDto>(accessToken);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<TransactionDto>>(Convert.ToString(response.Result));
            }

            foreach (var item in list)
            {
                if (item.TypePayment == Models.Enums.TypePayment.Credit)
                    totalCashSlow += (item.Total / item.AmountParcels);
                else
                    totalCashSlow += item.Total;
            }

            ViewBag.TotalCashFlow = totalCashSlow;

            return View(list);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransactionDashboard(DateTimeOffset date)
        {
            List<TransactionDto> list = new();
            decimal totalCashSlow = 0;

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _transactionService.GetAllTransactionsByDateAsync<ResponseDto>(date, accessToken);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<TransactionDto>>(Convert.ToString(response.Result));
            }

            foreach (var item in list)
            {
                if (item.TypePayment == Models.Enums.TypePayment.Credit)
                    totalCashSlow += (item.Total / item.AmountParcels);
                else
                    totalCashSlow += item.Total;
            }

            ViewBag.TotalCashFlow = totalCashSlow;

            return View(list);
        }
    }
}
