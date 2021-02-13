using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBalance.WebApi.Controllers.ApiModel;
using StoreBalance.WebApi.Domain.Dtos;
using StoreBalance.WebApi.Domain.Services;
using System;
using System.Threading.Tasks;

namespace StoreBalance.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        /// <summary>
        /// Get Wallet ByUserId
        /// </summary>
        /// <param name="userid">Id from user.</param>
        /// <param name="walletByUserIdService"></param>
        /// <returns>Returns the user's current balance</returns>
        [HttpGet]
        [ProducesResponseType(typeof(WalletDto.Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(WalletDto.Response), StatusCodes.Status404NotFound)]
        [Route("{userid}")]
        public async Task<IActionResult> WalletById([FromRoute] Guid userid, [FromServices] IQueryBalanceService walletByUserIdService)
        {
            var wallet = await walletByUserIdService.BalanceByUserId(userid);

            if (wallet.Succeeded)
            {
                return Ok(wallet.Data);
            }
            return NotFound(wallet.Errors);
        }

        /// <summary>
        /// Get Wallet Future Balance
        /// </summary>
        /// <param name="userid">Id from user.</param>
        /// <param name="enddate">End Datetime. (yyyy-MM-ddTHH:mm:ss)</param>
        /// <param name="queryFutureBalanceService"></param>
        /// <returns>Returns the user's future balance</returns>
        [HttpGet]
        [ProducesResponseType(typeof(WalletDto.Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(WalletDto.Response), StatusCodes.Status404NotFound)]
        [Route("{userid}/futurebalance/{enddate}")]
        public async Task<IActionResult> FutureBalance(
            [FromRoute] Guid userid,
            [FromRoute] DateTime enddate,
            [FromServices] IQueryFutureBalanceService queryFutureBalanceService)
        {
            var wallet = await queryFutureBalanceService.QueryFutureBalance(userid, enddate);

            if (wallet.Succeeded)
            {
                return Ok(wallet.Data);
            }
            return NotFound(wallet.Errors);
        }

        /// <summary>
        /// Get Records by period
        /// </summary>
        /// <param name="userid">Id from user.</param>
        /// <param name="begindate">Begin Datetime. (yyyy-MM-ddTHH:mm:ss)</param>
        /// <param name="enddate">End Datetime. (yyyy-MM-ddTHH:mm:ss)</param>
        /// <param name="walletRecordsByUserAndPeriod"></param>
        /// <returns>Returns the user's wallet's records</returns>
        [HttpGet]
        [ProducesResponseType(typeof(WalletDto.Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(WalletDto.Response), StatusCodes.Status404NotFound)]
        [Route("{userid}/records/{begindate}/{enddate}")]
        public async Task<IActionResult> RecordsByUserAndDate(
            [FromRoute] Guid userid,
            [FromRoute] DateTime begindate,
            [FromRoute] DateTime enddate,
            [FromServices] IQueryWalletRecordsService walletRecordsByUserAndPeriod)
        {
            var wallet = await walletRecordsByUserAndPeriod.RecordsByPeriod(userid, begindate, enddate);

            if (wallet.Succeeded)
            {
                return Ok(wallet.Data);
            }
            return NotFound(wallet.Errors);
        }

        /// <summary>
        /// Create New Wallet Debit
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="request"></param>
        /// <param name="createNewWalletDebitService"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [Route("{userid}/debit")]
        public async Task<IActionResult> CreateNewWalletDebit(
            [FromRoute] Guid userid,
            [FromBody] WalletRecordsDto.ResquestDebit request,
            [FromServices] ICreateNewWalletDebitService createNewWalletDebitService)
        {
            if (ModelState.IsValid)
            {
                var result = await createNewWalletDebitService.NewDebit(userid, request);

                if (result.Succeeded)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.ErrorResponse());
            }

            return BadRequest(ModelState.ErrorResponse());
        }

        /// <summary>
        /// Create New Wallet Credit
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="request"></param>
        /// <param name="createNewWalletCreditService"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [Route("{userid}/credit")]
        public async Task<IActionResult> CreateNewWalletCredit(
            [FromRoute] Guid userid,
            [FromBody] WalletRecordsDto.ResquestCredit request,
            [FromServices] ICreateNewWalletCreditService createNewWalletCreditService)
        {
            if (ModelState.IsValid)
            {
                var result = await createNewWalletCreditService.NewCredit(userid, request);

                if (result.Succeeded)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.ErrorResponse());
            }

            return BadRequest(ModelState.ErrorResponse());
        }

        /// <summary>
        /// Create New Wallet Credit Future
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="request"></param>
        /// <param name="createNewWalletCreditFutureService"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [Route("{userid}/creditfuture")]
        public async Task<IActionResult> CreateNewWalletCreditFuture(
            [FromRoute] Guid userid,
            [FromBody] WalletRecordsDto.ResquestCreditFuture request,
            [FromServices] ICreateNewWalletCreditFutureService createNewWalletCreditFutureService)
        {
            if (ModelState.IsValid)
            {
                var result = await createNewWalletCreditFutureService.NewCredit(userid, request);

                if (result.Succeeded)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.ErrorResponse());
            }

            return BadRequest(ModelState.ErrorResponse());
        }
    }
}
