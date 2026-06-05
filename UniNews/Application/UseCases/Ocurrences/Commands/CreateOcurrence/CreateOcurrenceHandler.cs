using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Entities.Ocurrences;
using Uninews.Domain.Interfaces;
using Uninews.Domain.ValueObjects;

namespace Uninews.Application.UseCases.Ocurrences.Commands.CreateOcurrences;

public class CreateOcurrenceHandler : ICreateOcurrenceHandler
{
    private readonly IOcurrenceRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly ITagRepository _tagRepository;

    public CreateOcurrenceHandler(IOcurrenceRepository repository, IUserRepository userRepo, ITagRepository tagRepo)
    {
        _repository = repository;
        _userRepository = userRepo;
        _tagRepository = tagRepo;
    }

    public async Task<Result<OcurrenceResponseDto>> HandleAsync(CreateOcurrenceCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user == null) return Result<OcurrenceResponseDto>.Fail("Usuário não encontrado.");

        var title = Title.Create(command.Title);
        var category = Category.Create(command.Category);
        var description = Description.Create(command.Description);
        var minister = Name.Create(command.Minister);
        var location = Location.Create(command.Location);

        var ocurrence = Ocurrence.Create(user, title, category, description, minister, command.Date, command.Time, location, command.Link);

        if (ocurrence.HasErros)
            return Result<OcurrenceResponseDto>.Fail(ocurrence.Erros);

        // Associa Tags se houver
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
            ocurrence.AddTag(tag);
        }
    }
    }

        await _repository.AddAsync(ocurrence);

        var response = new OcurrenceResponseDto
        {
            Id = ocurrence.Id.ToString(),
            Title = ocurrence.Title.Value!,
            Category = ocurrence.Category.Value!,
            Description = ocurrence.Description.Value!,
            Minister = ocurrence.Minister.Value!,
            Date = ocurrence.Date,
            Time = ocurrence.Time,
            Location = ocurrence.Location.Value!,
            Link = ocurrence.Link,
            Tags = ocurrence.Tags.Select(t => t.Name.Value).ToList()!
        };

        return Result<OcurrenceResponseDto>.Success(response);
    }
}