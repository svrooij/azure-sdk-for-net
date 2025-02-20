// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.ResourceManager.Network.Models
{
    /// <summary> Individual port mappings for inbound NAT rule created for backend pool. </summary>
    public partial class InboundNatRulePortMapping
    {
        /// <summary> Initializes a new instance of InboundNatRulePortMapping. </summary>
        internal InboundNatRulePortMapping()
        {
        }

        /// <summary> Initializes a new instance of InboundNatRulePortMapping. </summary>
        /// <param name="inboundNatRuleName"> Name of inbound NAT rule. </param>
        /// <param name="protocol"> The reference to the transport protocol used by the inbound NAT rule. </param>
        /// <param name="frontendPort"> Frontend port. </param>
        /// <param name="backendPort"> Backend port. </param>
        internal InboundNatRulePortMapping(string inboundNatRuleName, LoadBalancingTransportProtocol? protocol, int? frontendPort, int? backendPort)
        {
            InboundNatRuleName = inboundNatRuleName;
            Protocol = protocol;
            FrontendPort = frontendPort;
            BackendPort = backendPort;
        }

        /// <summary> Name of inbound NAT rule. </summary>
        public string InboundNatRuleName { get; }
        /// <summary> The reference to the transport protocol used by the inbound NAT rule. </summary>
        public LoadBalancingTransportProtocol? Protocol { get; }
        /// <summary> Frontend port. </summary>
        public int? FrontendPort { get; }
        /// <summary> Backend port. </summary>
        public int? BackendPort { get; }
    }
}
