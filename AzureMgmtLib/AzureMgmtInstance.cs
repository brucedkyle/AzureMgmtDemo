using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using AzureMgmtCommon;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AzureMgmtLib
{
    /// <summary>
    /// This class implements the Azure Management Fluent API without exposing them to the consumer.
    /// It acts as implementation and as mapper.
    /// AzureMgmtCommon includes common methods and models called by the consumers of this class
    /// that do not include an instance of Azure
    /// </summary>
    public class AzureManagementInstance
    {
        public AzureManagementInstance (string clientId, string clientSecret, string tenantId)
        {

            // client id == app id 9989b18b-16c7-40ac-9840-fbd752e12760
            // tenant id == common ?
            // client secret == PAbhoAY0fEQN654U1rnb0CT
            this.credentials = this.CreateCredentialsFromServicePrincipal(clientId, clientSecret, tenantId, AzureEnvironment.AzureGlobalCloud);
            CreateAzureFromDefaultSubscription(credentials);
        }

        public AzureManagementInstance(string username, string password, string clientId, string tenantId)
        {
            this.credentials = this.CreateCrediantalsFromUser(username, password, clientId, tenantId, AzureEnvironment.AzureGlobalCloud);
            CreateAzureFromDefaultSubscription(credentials);
        }

        public List<AzureMgmtCommon.Models.AzureMgmtSubscription> GetSubscriptions()
        {
            var result = new List<AzureMgmtCommon.Models.AzureMgmtSubscription>();
            if (credentials != null)
            {
                try
                {
                    ISubscriptions subscriptions = Azure
                        .Configure()
                        .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                        .Authenticate(credentials).Subscriptions;
                    foreach(var subscription in subscriptions.List())
                    {
                        result.Add(new AzureMgmtCommon.Models.AzureMgmtSubscription(subscription.SubscriptionId,
                            subscription.DisplayName, subscription.State));
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    Utilities.Log(ex);
                    return null;
                }
            }
            return null;
        }

        #region private
        // remove default constructor
        private AzureManagementInstance() { }
        private AzureCredentials credentials { get; set; }
        private IAzure azure { get; set; }
        private void CreateAzureFromDefaultSubscription(AzureCredentials credentials)
        {
            if (credentials != null)
            {
                try
                {
                    azure = Azure
                        .Configure()
                        .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                        .Authenticate(credentials)
                        .WithDefaultSubscription();
                }
                catch (Exception ex)
                {
                    Utilities.Log(ex);
                }
            }
        }

        private AzureCredentials CreateCredentialsFromServicePrincipal(string clientId, string clientSecret, string tenantId, AzureEnvironment environment)
        {
            try
            {
                var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(clientId, clientSecret, tenantId, environment);
                return credentials;
            }
            catch (Exception ex)
            {
                Utilities.Log(ex);
                return null;
            }

        }

        private AzureCredentials CreateCrediantalsFromUser(string username, string password, string clientId, string tenantId, AzureEnvironment environment)
        {
            try
            {
                var credentials = SdkContext.AzureCredentialsFactory.FromUser(username, password, clientId, tenantId, environment);
                return credentials;
            }
            catch (Exception ex)
            {
                Utilities.Log(ex);
                return null;
            }
        }

        private ISubscriptions GetSubscriptions(AzureCredentials credentials)
        {
            if (credentials != null)
            {
                try
                {
                    var subscriptions =  Azure
                        .Configure()
                        .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                        .Authenticate(credentials)
                        .Subscriptions;
                    return subscriptions;

                }
                catch (Exception ex)
                {
                    Utilities.Log(ex);
                    return null;
                }
            }
            return null;
        }
        #endregion
    }
}
