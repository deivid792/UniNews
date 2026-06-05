namespace Uninews.Application.UseCases.Users.Commands.UpdatePreferences;

public sealed class UpdatePreferencesCommand
    {
        public Guid UserId { get; private set; }
        public List<Guid> TagIds { get; private set; }

        public UpdatePreferencesCommand(Guid userId, List<Guid> tagIds)
        {
            UserId = userId;
            TagIds = tagIds ?? new List<Guid>();
        }
    }