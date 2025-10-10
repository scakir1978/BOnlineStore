using System.ComponentModel.DataAnnotations;

namespace BOnlineStore.IdentityServer.Attributes
{
    /// <summary>
    /// Validation attribute to ensure a Guid is not empty (Guid.Empty)
    /// </summary>
    public class NotEmptyGuidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is Guid guid)
            {
                return guid != Guid.Empty;
            }
            
            // If it's not a Guid, let other validators handle it
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage ?? "The {0} field must not be empty.", name);
        }
    }
}