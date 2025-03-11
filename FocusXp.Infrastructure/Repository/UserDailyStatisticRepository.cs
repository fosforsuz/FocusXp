using FocusXp.Domain.Entities;
using FocusXp.Infrastructure.Data;
using FocusXp.Infrastructure.Interfaces.Repository;

namespace FocusXp.Infrastructure.Repository;

internal class UserDailyStatisticRepository(FocusXpContext context)
    : Repository<UserDailyStatistic>(context), IUserDailyStatisticRepository;