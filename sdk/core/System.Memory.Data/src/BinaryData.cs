﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// A lightweight abstraction for a payload of bytes that supports converting between string, stream, JSON, and bytes.
    /// </summary>
    public class BinaryData
    {
        private const int CopyToBufferSize = 81920;

        /// <summary>
        /// The backing store for the <see cref="BinaryData"/> instance.
        /// </summary>
        private readonly ReadOnlyMemory<byte> _bytes;

        /// <summary>
        /// Returns an empty BinaryData.
        /// </summary>
        public static BinaryData Empty { get; } = new BinaryData(ReadOnlyMemory<byte>.Empty);

        /// <summary>
        /// Creates a <see cref="BinaryData"/> instance by wrapping the
        /// provided byte array.
        /// </summary>
        /// <param name="data">The array to wrap.</param>
        public BinaryData(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            _bytes = data;
        }

        /// <summary>
        /// Creates a <see cref="BinaryData"/> instance by serializing the provided object to JSON
        /// using <see cref="JsonSerializer"/>.
        /// </summary>
        ///
        /// <param name="jsonSerializable">The object that will be serialized to JSON using
        /// <see cref="JsonSerializer"/>.</param>
        /// <param name="options">The options to use when serializing to JSON.</param>
        /// <param name="type">The type to use when serializing the data. If not specified, <see cref="object.GetType"/> will
        /// be used to determine the type.</param>
#pragma warning disable AZC0014 // Avoid using banned types in public API
        public BinaryData(object? jsonSerializable, JsonSerializerOptions? options = default, Type? type = default)
#pragma warning restore AZC0014 // Avoid using banned types in public API
        {
            _bytes = JsonSerializer.SerializeToUtf8Bytes(jsonSerializable, type ?? jsonSerializable?.GetType() ?? typeof(object), options);
        }

        /// <summary>
        /// Creates a <see cref="BinaryData"/> instance by wrapping the
        /// provided bytes.
        /// </summary>
        /// <param name="data">Byte data to wrap.</param>
        public BinaryData(ReadOnlyMemory<byte> data)
        {
            _bytes = data;
        }

        /// <summary>
        /// Creates a <see cref="BinaryData"/> instance from a string by converting
        /// the string to bytes using the UTF-8 encoding.
        /// </summary>
        /// <param name="data">The string data.</param>
        public BinaryData(string data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            _bytes = Encoding.UTF8.GetBytes(data);
        }

        /// <summary>
        /// Creates a <see cref="BinaryData"/> instance by wrapping the provided
        /// <see cref="ReadOnlyMemory{Byte}"/>.
        /// </summary>
        /// <param name="data">Byte data to wrap.</param>
        /// <returns>A wrapper over <paramref name="data"/>.</returns>
        public static BinaryData FromBytes(ReadOnlyMemory<byte> data) =>
            new BinaryData(data);

        /// <summary>
        /// Creates a <see cref="BinaryData"/> instance by wrapping the provided
        /// byte array.
        /// </summary>
        /// <param name="data">The array to wrap.</param>
        /// <returns>A wrapper over <paramref name="data"/>.</returns>
        public static BinaryData FromBytes(byte[] data) =>
            new BinaryData(data);

        /// <summary>
        /// Creates a <see cref="BinaryData"/> instance from a string by converting
        /// the string to bytes using the UTF-8 encoding.
        /// </summary>
        /// <param name="data">The string data.</param>
        /// <returns>A value representing the UTF-8 encoding of <paramref name="data"/>.</returns>
        public static BinaryData FromString(string data) =>
            new BinaryData(data);

        /// <summary>
        /// Creates a <see cref="BinaryData"/> instance from the specified stream.
        /// The stream is not disposed by this method.
        /// </summary>
        /// <param name="stream">Stream containing the data.</param>
        /// <returns>A value representing all of the data remaining in <paramref name="stream"/>.</returns>
        public static BinaryData FromStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

#pragma warning disable AZC0102 // Do not use GetAwaiter().GetResult().
            return FromStreamAsync(stream, false).GetAwaiter().GetResult();
#pragma warning restore AZC0102 // Do not use GetAwaiter().GetResult().
        }

        /// <summary>
        /// Creates a <see cref="BinaryData"/> instance from the specified stream.
        /// The stream is not disposed by this method.
        /// </summary>
        /// <param name="stream">Stream containing the data.</param>
        /// <param name="cancellationToken">A token that may be used to cancel the operation.</param>
        /// <returns>A value representing all of the data remaining in <paramref name="stream"/>.</returns>
        public static Task<BinaryData> FromStreamAsync(
            Stream stream,
            CancellationToken cancellationToken = default)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return FromStreamAsync(stream, true, cancellationToken);
        }

        private static async Task<BinaryData> FromStreamAsync(
            Stream stream,
            bool async,
            CancellationToken cancellationToken = default)
        {
            int streamLength = 0;
            if (stream.CanSeek)
            {
                long longLength = stream.Length - stream.Position;
                if (longLength > int.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(stream),
                        "Stream length must be less than Int32.MaxValue");
                }
                streamLength = (int)longLength;
            }

            using MemoryStream memoryStream = stream.CanSeek ? new MemoryStream(streamLength) : new MemoryStream();
            if (async)
            {
                await stream.CopyToAsync(memoryStream, CopyToBufferSize, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                stream.CopyTo(memoryStream);
            }
            return new BinaryData(memoryStream.GetBuffer().AsMemory(0, (int)memoryStream.Position));
        }

        /// <summary>
        /// Creates a <see cref="BinaryData"/> instance by serializing the provided object using
        /// the <see cref="JsonSerializer"/>.
        /// </summary>
        ///
        /// <typeparam name="T">The type to use when serializing the data.</typeparam>
        /// <param name="jsonSerializable">The data to use.</param>
        /// <param name="options">The options to use when serializing to JSON.</param>
        ///
        /// <returns>A value representing the UTF-8 encoding of the JSON representation of <paramref name="jsonSerializable" />.</returns>
        public static BinaryData FromObjectAsJson<T>(
            T jsonSerializable,
#pragma warning disable AZC0014 // Avoid using banned types in public API
            JsonSerializerOptions? options = default)
#pragma warning restore AZC0014 // Avoid using banned types in public API
        {
            byte[] buffer = JsonSerializer.SerializeToUtf8Bytes(jsonSerializable, typeof(T), options);
            return new BinaryData(buffer);
        }

        /// <summary>
        /// Converts the value of this instance to a string using UTF-8.
        /// </summary>
        /// <remarks>
        /// If the underlying object is a JSON string, calling <see cref="ToString"/> will retain the wrapping double quotes in the
        /// resulting string. If you want to deserialize the JSON string to a string, thereby removing the wrapping double quotes,
        /// call <see cref="ToObjectFromJson{String}"/> instead.
        /// </remarks>
        /// <returns>
        /// A string from the value of this instance, using UTF-8 to decode the bytes.
        /// </returns>
        public override string ToString()
        {
            if (MemoryMarshal.TryGetArray(
                _bytes,
                out ArraySegment<byte> data))
            {
                return Encoding.UTF8.GetString(data.Array, data.Offset, data.Count);
            }
            return Encoding.UTF8.GetString(_bytes.ToArray());
        }

        /// <summary>
        /// Converts the <see cref="BinaryData"/> to a read-only stream.
        /// </summary>
        /// <returns>A stream representing the data.</returns>
        public Stream ToStream() =>
            new ReadOnlyMemoryStream(_bytes);

        /// <summary>
        /// Gets the value of this instance as bytes without any further interpretation.
        /// </summary>
        /// <returns>The value of this instance as bytes without any further interpretation.</returns>
        public ReadOnlyMemory<byte> ToMemory() =>
            _bytes;

        /// <summary>
        /// Converts the <see cref="BinaryData"/> to a byte array.
        /// </summary>
        /// <returns>A byte array representing the data.</returns>
        public byte[] ToArray() =>
            _bytes.ToArray();

        /// <summary>
        /// Converts the <see cref="BinaryData"/> to the specified type using
        /// <see cref="JsonSerializer"/>.
        /// </summary>
        /// <typeparam name="T">The type that the data should be
        /// converted to.</typeparam>
        /// <param name="options">The <see cref="JsonSerializerOptions"/> to use when serializing to JSON.</param>
        /// <returns>The data converted to the specified type.</returns>
#pragma warning disable AZC0014 // Avoid using banned types in public API
        public T? ToObjectFromJson<T>(JsonSerializerOptions? options = default)
#pragma warning restore AZC0014 // Avoid using banned types in public API
        {
            return (T?)JsonSerializer.Deserialize(_bytes.Span, typeof(T), options);
        }

        /// <summary>
        /// Defines an implicit conversion from a <see cref="BinaryData" /> to a <see cref="ReadOnlyMemory{Byte}"/>.
        /// </summary>
        /// <param name="data">The value to be converted.</param>
        public static implicit operator ReadOnlyMemory<byte>(
            BinaryData? data) =>
            data?._bytes ?? default;

        /// <summary>
        /// Defines an implicit conversion from a <see cref="BinaryData" /> to a <see cref="ReadOnlySpan{Byte}"/>.
        /// </summary>
        /// <param name="data">The value to be converted.</param>
        public static implicit operator ReadOnlySpan<byte>(
            BinaryData? data)
        {
            if (data == null)
            {
                return default;
            }
            return data._bytes.Span;
        }
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true" /> if the specified object is equal to the current object; otherwise, <see langword="false" />.
        /// </returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj);
        }

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() =>
            base.GetHashCode();
    }
}
