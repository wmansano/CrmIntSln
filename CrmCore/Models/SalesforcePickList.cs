﻿namespace CrmCore.Models
{
    /// <summary>
	/// Represents a picklist on Salesforce.
    /// </summary>
    public class SalesforcePickList
    {
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CrmSfApi.Models.SalesforcePickList"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active { get; set; }

		/// <summary>
		/// Gets or sets the label.
		/// </summary>
		/// <value>The label.</value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CrmSfApi.Models.SalesforcePickList"/> default value.
        /// </summary>
        /// <value><c>true</c> if default value; otherwise, <c>false</c>.</value>
        public bool DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the value of the Base64 encoded Index of the <see cref="CrmSfApi.Models.SalesforcePickList"/> controlling field.
        /// </summary>
        /// <value>A Base64 encoded string</value>
        public string ValidFor { get; set; }
    }
}