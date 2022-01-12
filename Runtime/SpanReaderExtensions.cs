using System;
using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace NewBlood
{
    /// <summary>Provides extension methods for <see cref="SpanReader{T}"/>.</summary>
    public static class SpanReaderExtensions
    {
        /// <summary>Read the next value and advance the reader.</summary>
        public static unsafe bool TryRead<T>(ref this SpanReader<byte> @this, out T value)
            where T : unmanaged
        {
            if (!MemoryMarshal.TryRead(@this.UnreadSpan, out value))
                return false;

            @this.Position += sizeof(T);
            return true;
        }

        /// <summary>Read the next value in little-endian and advance the reader.</summary>
        public static bool TryReadLittleEndian(ref this SpanReader<byte> @this, out short value)
        {
            if (!BinaryPrimitives.TryReadInt16LittleEndian(@this.UnreadSpan, out value))
                return false;

            @this.Position += sizeof(short);
            return true;
        }

        /// <summary>Read the next value in little-endian and advance the reader.</summary>
        public static bool TryReadLittleEndian(ref this SpanReader<byte> @this, out ushort value)
        {
            if (!BinaryPrimitives.TryReadUInt16LittleEndian(@this.UnreadSpan, out value))
                return false;

            @this.Position += sizeof(ushort);
            return true;
        }

        /// <summary>Read the next value in little-endian and advance the reader.</summary>
        public static bool TryReadLittleEndian(ref this SpanReader<byte> @this, out int value)
        {
            if (!BinaryPrimitives.TryReadInt32LittleEndian(@this.UnreadSpan, out value))
                return false;

            @this.Position += sizeof(int);
            return true;
        }

        /// <summary>Read the next value in little-endian and advance the reader.</summary>
        public static bool TryReadLittleEndian(ref this SpanReader<byte> @this, out uint value)
        {
            if (!BinaryPrimitives.TryReadUInt32LittleEndian(@this.UnreadSpan, out value))
                return false;

            @this.Position += sizeof(uint);
            return true;
        }

        /// <summary>Read the next value in little-endian and advance the reader.</summary>
        public static bool TryReadLittleEndian(ref this SpanReader<byte> @this, out long value)
        {
            if (!BinaryPrimitives.TryReadInt64LittleEndian(@this.UnreadSpan, out value))
                return false;

            @this.Position += sizeof(long);
            return true;
        }

        /// <summary>Read the next value in little-endian and advance the reader.</summary>
        public static bool TryReadLittleEndian(ref this SpanReader<byte> @this, out ulong value)
        {
            if (!BinaryPrimitives.TryReadUInt64LittleEndian(@this.UnreadSpan, out value))
                return false;

            @this.Position += sizeof(ulong);
            return true;
        }

        /// <summary>Read the next value in little-endian and advance the reader.</summary>
        public static bool TryReadLittleEndian(ref this SpanReader<byte> @this, out float value)
        {
            if (!BinaryPrimitives.TryReadInt32LittleEndian(@this.UnreadSpan, out int raw))
            {
                value = default;
                return false;
            }

            value = BitConverter.Int32BitsToSingle(raw);
            @this.Position += sizeof(float);
            return true;
        }

        /// <summary>Read the next value in little-endian and advance the reader.</summary>
        public static bool TryReadLittleEndian(ref this SpanReader<byte> @this, out double value)
        {
            if (!BinaryPrimitives.TryReadInt64LittleEndian(@this.UnreadSpan, out long raw))
            {
                value = default;
                return false;
            }

            value = BitConverter.Int64BitsToDouble(raw);
            @this.Position += sizeof(double);
            return true;
        }

        /// <summary>Read the next value in big-endian and advance the reader.</summary>
        public static bool TryReadBigEndian(ref this SpanReader<byte> @this, out short value)
        {
            if (!BinaryPrimitives.TryReadInt16BigEndian(@this.UnreadSpan, out value))
                return false;

            @this.Position += sizeof(short);
            return true;
        }

        /// <summary>Read the next value in big-endian and advance the reader.</summary>
        public static bool TryReadBigEndian(ref this SpanReader<byte> @this, out ushort value)
        {
            if (!BinaryPrimitives.TryReadUInt16BigEndian(@this.UnreadSpan, out value))
                return false;

            @this.Position += sizeof(ushort);
            return true;
        }

        /// <summary>Read the next value in big-endian and advance the reader.</summary>
        public static bool TryReadBigEndian(ref this SpanReader<byte> @this, out int value)
        {
            if (!BinaryPrimitives.TryReadInt32BigEndian(@this.UnreadSpan, out value))
                return false;

            @this.Position += sizeof(int);
            return true;
        }

        /// <summary>Read the next value in big-endian and advance the reader.</summary>
        public static bool TryReadBigEndian(ref this SpanReader<byte> @this, out uint value)
        {
            if (!BinaryPrimitives.TryReadUInt32BigEndian(@this.UnreadSpan, out value))
                return false;

            @this.Position += sizeof(uint);
            return true;
        }

        /// <summary>Read the next value in big-endian and advance the reader.</summary>
        public static bool TryReadBigEndian(ref this SpanReader<byte> @this, out long value)
        {
            if (!BinaryPrimitives.TryReadInt64BigEndian(@this.UnreadSpan, out value))
                return false;

            @this.Position += sizeof(long);
            return true;
        }

        /// <summary>Read the next value in big-endian and advance the reader.</summary>
        public static bool TryReadBigEndian(ref this SpanReader<byte> @this, out ulong value)
        {
            if (!BinaryPrimitives.TryReadUInt64BigEndian(@this.UnreadSpan, out value))
                return false;

            @this.Position += sizeof(ulong);
            return true;
        }

        /// <summary>Read the next value in big-endian and advance the reader.</summary>
        public static bool TryReadBigEndian(ref this SpanReader<byte> @this, out float value)
        {
            if (!BinaryPrimitives.TryReadInt32BigEndian(@this.UnreadSpan, out int raw))
            {
                value = default;
                return false;
            }

            value = BitConverter.Int32BitsToSingle(raw);
            @this.Position += sizeof(float);
            return true;
        }

        /// <summary>Read the next value in big-endian and advance the reader.</summary>
        public static bool TryReadBigEndian(ref this SpanReader<byte> @this, out double value)
        {
            if (!BinaryPrimitives.TryReadInt64BigEndian(@this.UnreadSpan, out long raw))
            {
                value = default;
                return false;
            }

            value = BitConverter.Int64BitsToDouble(raw);
            @this.Position += sizeof(double);
            return true;
        }
    }
}
