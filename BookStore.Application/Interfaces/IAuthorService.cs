using BookStore.Application.DTOs;
using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces;

public interface IAuthorService
{
    Author CreateAuthor(AuthorRequest request);
    List<AuthorDto> GetAuthors();
}