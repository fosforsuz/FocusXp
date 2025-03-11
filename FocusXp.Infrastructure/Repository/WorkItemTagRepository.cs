using FocusXp.Domain.Entities;
using FocusXp.Infrastructure.Data;
using FocusXp.Infrastructure.Interfaces.Repository;

namespace FocusXp.Infrastructure.Repository;

internal class WorkItemTagRepository(FocusXpContext context) : Repository<WorkItemTag>(context), IWorkItemTagRepository;