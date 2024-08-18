using NetDevPack.Domain;

namespace Core.Models;

public abstract class AuditableEntity : Entity
{
    public DateTime? CreatedAt { get; private set; }
    public string? CreatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedBy { get; private set; }

    public void SetCreatedInfo(DateTime createdAt, string createdBy)
    {
        CreatedAt = createdAt;
        CreatedBy = createdBy;
    }

    public void SetUpdatedInfo(DateTime updatedAt, string updatedBy)
    {
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy;
    }
}