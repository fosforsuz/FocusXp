using FocusXp.Domain.Entities;
using FocusXp.Infrastructure.Data;
using FocusXp.Infrastructure.Interfaces.Repository;

namespace FocusXp.Infrastructure.Repository;

internal class AttachmentRepository(FocusXpContext context) : Repository<Attachment>(context), IAttachmentRepository;