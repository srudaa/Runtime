// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Applications;
using Dolittle.Tenancy;

namespace Dolittle.Runtime.EventHorizon.Producer
{
    /// <summary>
    /// Represents the configuration for the consent of an event horizon.
    /// </summary>
    public class EventHorizonConsentConfiguration
    {
        /// <summary>
        /// Gets or sets the <see cref="Microservice" /> to give consent to.
        /// </summary>
        public Microservice Microservice { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="TenantId" /> tenant to give consent to.
        /// </summary>
        public TenantId Tenant { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EventHorizonConsent" /> for the tenant in microservice.
        /// </summary>
        public EventHorizonConsent Consent { get; set; }
    }
}