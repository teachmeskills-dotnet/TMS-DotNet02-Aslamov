using Identity.API.Common.Constants;
using Identity.API.Common.Enums;

namespace Identity.API.Common.Extensions
{
    /// <summary>
    /// Define extensions for the identity methods.
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Conver application role from Int32 to String.
        /// </summary>
        /// <param name="role">Application role.</param>
        /// <returns>Role in string format.</returns>
        public static string ConvertRole(this int role)
        {
            string result = role switch
            {
                (int)IdentityRoles.User => IdentityConstants.ROLE_USER,
                (int)IdentityRoles.Admin => IdentityConstants.ROLE_ADMIN,
                _ => IdentityConstants.ROLE_NONE,
            };
            return result;
        }
    }
}