using EPiServer.Security;

namespace PriceEvents.Helpers
{
    internal static class PriceEventHelpers
    {
        /// <summary>
        /// Returns the user name
        /// </summary>
        /// <returns>string</returns>
        public static string GetCurrentUsername()
        {
            return PrincipalInfo.CurrentPrincipal != null
                ? PrincipalInfo.CurrentPrincipal.Identity.Name
                : PriceEventResources.UNKNOWN;
        }

        /// <summary>
        /// Validates if the method arguments if valid
        /// </summary>
        /// <param name="methodArguments">object[]</param>
        /// <param name="index">int</param>
        /// <returns>bool</returns>
        public static bool IsMethodArgumentsValid(object[] methodArguments, int index)
        {
            return methodArguments != null
                   && methodArguments.Length >= index + 1
                   && methodArguments[index] != null;
        }
    }
}
