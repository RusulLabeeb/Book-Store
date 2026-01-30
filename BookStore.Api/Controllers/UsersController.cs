using BookStore.Api.Common;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

public class UsersController : BaseController
{
    private readonly ILoanService _loanService;

    public UsersController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpGet("{userId:int}/loans")]
    public async Task<ActionResult<List<LoanDto>>> GetUserLoans(int userId)
    {
        var result = await _loanService.GetUserLoansAsync(userId);
        return Ok(result);
    }
}
