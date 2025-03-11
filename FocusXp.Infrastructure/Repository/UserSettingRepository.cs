using FocusXp.Domain.Entities;
using FocusXp.Infrastructure.Data;
using FocusXp.Infrastructure.Interfaces.Repository;

namespace FocusXp.Infrastructure.Repository;

internal class UserSettingRepository(FocusXpContext context) : Repository<UserSetting>(context), IUserSettingRepository;