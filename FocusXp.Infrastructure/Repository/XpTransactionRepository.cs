using FocusXp.Domain.Entities;
using FocusXp.Infrastructure.Data;
using FocusXp.Infrastructure.Interfaces.Repository;

namespace FocusXp.Infrastructure.Repository;

internal class XpTransactionRepository(FocusXpContext context)
    : Repository<XpTransaction>(context), IXpTransactionRepository;