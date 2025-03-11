using FocusXp.Domain.Entities;
using FocusXp.Infrastructure.Data;
using FocusXp.Infrastructure.Interfaces.Repository;

namespace FocusXp.Infrastructure.Repository;

internal class SubWorkItemRepository(FocusXpContext context) : Repository<SubWorkItem>(context), ISubWorkItemRepository;