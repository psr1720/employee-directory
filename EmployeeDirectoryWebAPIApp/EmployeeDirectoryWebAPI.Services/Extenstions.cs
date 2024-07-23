using EmployeeDirectory.Models.Entity;

namespace EmployeeDirectory.Services
{
    public static class Extenstions
    {
        public static void SetCreatedFields(this Audit auditType, string creatorId)
        {
            auditType.CreatedBy = creatorId;
            auditType.CreatedOn = DateTime.UtcNow;   
        }
        public static void SetUpdatedFields(this Audit auditType, string updaterId) 
        {
            auditType.UpdatedBy = updaterId;
            auditType.UpdatedOn = DateTime.UtcNow;
        }
    }
}
