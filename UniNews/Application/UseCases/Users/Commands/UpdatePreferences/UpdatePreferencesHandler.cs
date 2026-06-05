using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;

namespace Uninews.Application.UseCases.Users.Commands.UpdatePreferences;

public sealed class UpdatePreferencesHandler : IUpdatePreferencesHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ITagRepository _tagRepository;

        public UpdatePreferencesHandler(IUserRepository userRepository, ITagRepository tagRepository)
        {
            _userRepository = userRepository;
            _tagRepository = tagRepository;
        }

        public async Task<Result> HandleAsync(UpdatePreferencesCommand command)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);
            if (user == null)
            {
                return Result.Fail("Usuário não encontrado.");
            }

            var tags = await _tagRepository.GetByIdsAsync(command.TagIds);
            if (tags == null || !tags.Any())
            {
                return Result.Fail("Nenhuma tag válida foi encontrada");
            }

            user.AddTagsList(tags);

            await _userRepository.UpdateAsync(user);

            return Result.Success();
        }
    }