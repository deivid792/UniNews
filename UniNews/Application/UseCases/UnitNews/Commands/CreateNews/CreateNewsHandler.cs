using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;
using Uninews.Domain.ValueObjects;
using DomainNews = Uninews.Domain.Entities.UnitNews.News;

namespace Uninews.Application.UseCases.UnitNews.Commands.CreateNews;

public sealed class CreateNewsHandler : ICreateNewsHandler
{
    private readonly INewsRepository _newsRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITagRepository _tagRepository;

    public CreateNewsHandler(
        INewsRepository newsRepository,
        IUserRepository userRepository,
        ITagRepository tagRepository)
    {
        _newsRepository = newsRepository;
        _userRepository = userRepository;
        _tagRepository = tagRepository;
    }

    public async Task<Result<NewsResponseDto>> HandleAsync(CreateNewsCommand command)
{
    var user = await _userRepository.GetByIdAsync(command.UserId);
    if (user == null)
    {
        return Result<NewsResponseDto>.Fail("Usuário criador não encontrado.");
    }

    var title = Title.Create(command.Title);
    var description = Description.Create(command.Description);

    var news = DomainNews.Create(user, title, description, command.Link);
    if (news.HasErros)
    {
        return Result<NewsResponseDto>.Fail(news.Erros);
    }

    if (command.TagIds != null && command.TagIds.Any())
    {
    var tags = await _tagRepository.GetByIdsAsync(command.TagIds);
    if (tags != null && tags.Any())
    {
        // A chave aqui: certifique-se de que cada tag esteja "Attached" ao contexto
        foreach (var tag in tags)
        {
            // Isso diz ao EF: "Esta tag já existe, não tente inseri-la novamente!"
            // Substitua _newsRepository pelo seu contexto se necessário ou injete o DbContext
            // Se não tiver acesso ao contexto aqui, o repositório deve lidar com isso.
            news.AddTag(tag);
        }
    }
    }

    await _newsRepository.AddAsync(news);

    var response = new NewsResponseDto
    {
        Id = news.Id.ToString(),
        Title = news.Title.Value!,
        Description = news.Description.Value!,
        Link = news.Link,
        Tags = news.Tags.Select(t => t.Name.Value).ToList()!
    };

    return Result<NewsResponseDto>.Success(response);
}
}