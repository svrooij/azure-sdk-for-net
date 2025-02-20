// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using Azure.Core;

namespace Azure.Analytics.Synapse.Artifacts.Models
{
    /// <summary> Web activity. </summary>
    public partial class WebActivity : ExecutionActivity
    {
        /// <summary> Initializes a new instance of WebActivity. </summary>
        /// <param name="name"> Activity name. </param>
        /// <param name="method"> Rest API method for target endpoint. </param>
        /// <param name="url"> Web activity target endpoint and path. Type: string (or Expression with resultType string). </param>
        /// <exception cref="ArgumentNullException"> <paramref name="name"/> or <paramref name="url"/> is null. </exception>
        public WebActivity(string name, WebActivityMethod method, object url) : base(name)
        {
            Argument.AssertNotNull(name, nameof(name));
            Argument.AssertNotNull(url, nameof(url));

            Method = method;
            Url = url;
            Datasets = new ChangeTrackingList<DatasetReference>();
            LinkedServices = new ChangeTrackingList<LinkedServiceReference>();
            Type = "WebActivity";
        }

        /// <summary> Initializes a new instance of WebActivity. </summary>
        /// <param name="name"> Activity name. </param>
        /// <param name="type"> Type of activity. </param>
        /// <param name="description"> Activity description. </param>
        /// <param name="state"> Activity state. This is an optional property and if not provided, the state will be Active by default. </param>
        /// <param name="onInactiveMarkAs"> Status result of the activity when the state is set to Inactive. This is an optional property and if not provided when the activity is inactive, the status will be Succeeded by default. </param>
        /// <param name="dependsOn"> Activity depends on condition. </param>
        /// <param name="userProperties"> Activity user properties. </param>
        /// <param name="additionalProperties"> Additional Properties. </param>
        /// <param name="linkedServiceName"> Linked service reference. </param>
        /// <param name="policy"> Activity policy. </param>
        /// <param name="method"> Rest API method for target endpoint. </param>
        /// <param name="url"> Web activity target endpoint and path. Type: string (or Expression with resultType string). </param>
        /// <param name="headers"> Represents the headers that will be sent to the request. For example, to set the language and type on a request: "headers" : { "Accept-Language": "en-us", "Content-Type": "application/json" }. Type: string (or Expression with resultType string). </param>
        /// <param name="body"> Represents the payload that will be sent to the endpoint. Required for POST/PUT method, not allowed for GET method Type: string (or Expression with resultType string). </param>
        /// <param name="authentication"> Authentication method used for calling the endpoint. </param>
        /// <param name="datasets"> List of datasets passed to web endpoint. </param>
        /// <param name="linkedServices"> List of linked services passed to web endpoint. </param>
        /// <param name="connectVia"> The integration runtime reference. </param>
        internal WebActivity(string name, string type, string description, ActivityState? state, ActivityOnInactiveMarkAs? onInactiveMarkAs, IList<ActivityDependency> dependsOn, IList<UserProperty> userProperties, IDictionary<string, object> additionalProperties, LinkedServiceReference linkedServiceName, ActivityPolicy policy, WebActivityMethod method, object url, object headers, object body, WebActivityAuthentication authentication, IList<DatasetReference> datasets, IList<LinkedServiceReference> linkedServices, IntegrationRuntimeReference connectVia) : base(name, type, description, state, onInactiveMarkAs, dependsOn, userProperties, additionalProperties, linkedServiceName, policy)
        {
            Method = method;
            Url = url;
            Headers = headers;
            Body = body;
            Authentication = authentication;
            Datasets = datasets;
            LinkedServices = linkedServices;
            ConnectVia = connectVia;
            Type = type ?? "WebActivity";
        }

        /// <summary> Rest API method for target endpoint. </summary>
        public WebActivityMethod Method { get; set; }
        /// <summary> Web activity target endpoint and path. Type: string (or Expression with resultType string). </summary>
        public object Url { get; set; }
        /// <summary> Represents the headers that will be sent to the request. For example, to set the language and type on a request: "headers" : { "Accept-Language": "en-us", "Content-Type": "application/json" }. Type: string (or Expression with resultType string). </summary>
        public object Headers { get; set; }
        /// <summary> Represents the payload that will be sent to the endpoint. Required for POST/PUT method, not allowed for GET method Type: string (or Expression with resultType string). </summary>
        public object Body { get; set; }
        /// <summary> Authentication method used for calling the endpoint. </summary>
        public WebActivityAuthentication Authentication { get; set; }
        /// <summary> List of datasets passed to web endpoint. </summary>
        public IList<DatasetReference> Datasets { get; }
        /// <summary> List of linked services passed to web endpoint. </summary>
        public IList<LinkedServiceReference> LinkedServices { get; }
        /// <summary> The integration runtime reference. </summary>
        public IntegrationRuntimeReference ConnectVia { get; set; }
    }
}
