// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.ResourceManager.RecoveryServicesBackup.Models
{
    /// <summary> Additional information on Azure Workload for SQL specific backup item. </summary>
    public partial class VmWorkloadProtectedItemExtendedInfo
    {
        /// <summary> Initializes a new instance of VmWorkloadProtectedItemExtendedInfo. </summary>
        public VmWorkloadProtectedItemExtendedInfo()
        {
        }

        /// <summary> Initializes a new instance of VmWorkloadProtectedItemExtendedInfo. </summary>
        /// <param name="oldestRecoverOn"> The oldest backup copy available for this backup item across all tiers. </param>
        /// <param name="oldestRecoveryPointInVault"> The oldest backup copy available for this backup item in vault tier. </param>
        /// <param name="oldestRecoveryPointInArchive"> The oldest backup copy available for this backup item in archive tier. </param>
        /// <param name="newestRecoveryPointInArchive"> The latest backup copy available for this backup item in archive tier. </param>
        /// <param name="recoveryPointCount"> Number of backup copies available for this backup item. </param>
        /// <param name="policyState"> Indicates consistency of policy object and policy applied to this backup item. </param>
        /// <param name="recoveryModel"> Indicates consistency of policy object and policy applied to this backup item. </param>
        internal VmWorkloadProtectedItemExtendedInfo(DateTimeOffset? oldestRecoverOn, DateTimeOffset? oldestRecoveryPointInVault, DateTimeOffset? oldestRecoveryPointInArchive, DateTimeOffset? newestRecoveryPointInArchive, int? recoveryPointCount, string policyState, string recoveryModel)
        {
            OldestRecoverOn = oldestRecoverOn;
            OldestRecoveryPointInVault = oldestRecoveryPointInVault;
            OldestRecoveryPointInArchive = oldestRecoveryPointInArchive;
            NewestRecoveryPointInArchive = newestRecoveryPointInArchive;
            RecoveryPointCount = recoveryPointCount;
            PolicyState = policyState;
            RecoveryModel = recoveryModel;
        }

        /// <summary> The oldest backup copy available for this backup item across all tiers. </summary>
        public DateTimeOffset? OldestRecoverOn { get; set; }
        /// <summary> The oldest backup copy available for this backup item in vault tier. </summary>
        public DateTimeOffset? OldestRecoveryPointInVault { get; set; }
        /// <summary> The oldest backup copy available for this backup item in archive tier. </summary>
        public DateTimeOffset? OldestRecoveryPointInArchive { get; set; }
        /// <summary> The latest backup copy available for this backup item in archive tier. </summary>
        public DateTimeOffset? NewestRecoveryPointInArchive { get; set; }
        /// <summary> Number of backup copies available for this backup item. </summary>
        public int? RecoveryPointCount { get; set; }
        /// <summary> Indicates consistency of policy object and policy applied to this backup item. </summary>
        public string PolicyState { get; set; }
        /// <summary> Indicates consistency of policy object and policy applied to this backup item. </summary>
        public string RecoveryModel { get; set; }
    }
}
