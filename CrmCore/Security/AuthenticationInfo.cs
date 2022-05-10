
namespace CrmCore.Security
{
    /// <summary>
    /// The informations about the authentication.
    /// </summary>
    public class AuthenticationInfo
    {
        #region Constructors
#pragma warning disable CA1054 // Uri parameters should not be strings
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationInfo"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="instanceUrl">The instance URL.</param>
        public AuthenticationInfo(string accessToken, string instanceUrl)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            AccessToken = accessToken;
            InstanceUrl = instanceUrl;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the access token that acts as a session ID that the application
        /// uses for making requests. This token should be protected as
        /// though it were user credentials.
        /// </summary>
        public string AccessToken { get; private set; }

#pragma warning disable CA1056 // Uri properties should not be strings
        /// <summary>
        /// Gets the identifies (URL) the Salesforce instance to which API calls should be sent.
        /// </summary>
        public string InstanceUrl { get; private set; }
#pragma warning restore CA1056 // Uri properties should not be strings
        #endregion
    }
}
