using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Services;

public class AuthorService(IBookStoreDbContext dbContext) : IAuthorService
{
    public Author CreateAuthor(AuthorRequest request)
    {
        var newAuthor = new Author()
        {
            Name = request.Name,
        };
        dbContext.Authors.Add(newAuthor);

        dbContext.SaveChanges();
        return newAuthor;
    }

    public List<AuthorDto> GetAuthors()
    {
        return dbContext.Authors
            .Select(author => new AuthorDto()
            {
                Name = author.Name,
                Id = author.Id
            })
            .ToList();
    }
}