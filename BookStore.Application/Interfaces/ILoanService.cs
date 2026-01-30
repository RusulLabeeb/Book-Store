using BookStore.Application.DTOs;

namespace BookStore.Application.Interfaces;

public interface ILoanService
{
    Task<LoanDto> CreateLoanAsync(CreateLoanRequest request);
    Task<List<LoanDto>> GetUserLoansAsync(int userId);
}
