using FocusXp.Domain.Entities;
using FocusXp.Infrastructure.Data;
using FocusXp.Infrastructure.Interfaces.Repository;

namespace FocusXp.Infrastructure.Repository;

internal class PomodoroSessionRepository(FocusXpContext context)
    : Repository<PomodoroSession>(context), IPomodoroSessionRepository;