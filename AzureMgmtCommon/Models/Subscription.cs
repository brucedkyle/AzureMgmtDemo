using System;
using System.Collections.Generic;
using System.Text;

namespace AzureMgmtCommon.Models
{
    public class AzureMgmtSubscription 
    {
        private AzureMgmtSubscription() { }
        public AzureMgmtSubscription(string subscriptionId, string displayName, string state)
        {
            this.SubscriptionId = subscriptionId;
            this.DisplayName = displayName;
            this.State = state;
        }
        // Returns:
        //     the UUID of the subscription
        public string SubscriptionId { get; private set; }
        //
        // Returns:
        //     the name of the subscription for humans to read
        public string DisplayName { get; private set; }
        //
        // Returns:
        //     the state of the subscription.
        public string State { get; private set; }

        //TODO include locations and policies
    }
}
