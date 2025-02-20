// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.ContainerService.Models
{
    public partial class ManagedClusterSecurityProfileImageCleaner : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(IsEnabled))
            {
                writer.WritePropertyName("enabled"u8);
                writer.WriteBooleanValue(IsEnabled.Value);
            }
            if (Optional.IsDefined(IntervalHours))
            {
                writer.WritePropertyName("intervalHours"u8);
                writer.WriteNumberValue(IntervalHours.Value);
            }
            writer.WriteEndObject();
        }

        internal static ManagedClusterSecurityProfileImageCleaner DeserializeManagedClusterSecurityProfileImageCleaner(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            Optional<bool> enabled = default;
            Optional<int> intervalHours = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("enabled"u8))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    enabled = property.Value.GetBoolean();
                    continue;
                }
                if (property.NameEquals("intervalHours"u8))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    intervalHours = property.Value.GetInt32();
                    continue;
                }
            }
            return new ManagedClusterSecurityProfileImageCleaner(Optional.ToNullable(enabled), Optional.ToNullable(intervalHours));
        }
    }
}
