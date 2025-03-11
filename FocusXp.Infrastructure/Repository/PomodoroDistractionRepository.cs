using FocusXp.Domain.Entities;
using FocusXp.Infrastructure.Data;
using FocusXp.Infrastructure.Interfaces.Repository;

namespace FocusXp.Infrastructure.Repository;

internal class PomodoroDistractionRepository(FocusXpContext context)
    : Repository<PomodoroDistraction>(context), IPomodoroDistractionRepository;