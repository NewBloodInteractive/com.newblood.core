using System;
using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace NewBlood
{
    /// <summary>Provides extension methods for <see cref="SpanWriter{T}"/>.</summary>
    public static class SpanWriterExtensions
    {
        /// <summary>Write a value and advance the writer.</summary>
        public static unsafe bool TryWrite<T>(ref this SpanWriter<byte> @this, T value)
            where T : unmanaged
        {
            if (!MemoryMarshal.TryWrite(@this.RemainingSpan, ref value))
                return false;

            @this.Position += sizeof(T);
            return true;
        }

        /// <summary>Write a value and advance the writer.</summary>
        public static unsafe bool TryWrite<T>(ref this SpanWriter<byte> @this, ref T value)
            where T : unmanaged
        {
            if (!MemoryMarshal.TryWrite(@this.RemainingSpan, ref value))
                return false;

            @this.Position += sizeof(T);
            return true;
        }

        /// <summary>Write a value in little-endian and advance the writer.</summary>
        public static bool TryWriteLittleEndian(ref this SpanWriter<byte> @this, short value)
        {
            if (!BinaryPrimitives.TryWriteInt16LittleEndian(@this.RemainingSpan, value))
                return false;

            @this.Position += sizeof(short);
            return true;
        }

        /// <summary>Write a value in little-endian and advance the writer.</summary>
        public static bool TryWriteLittleEndian(ref this SpanWriter<byte> @this, ushort value)
        {
            if (!BinaryPrimitives.TryWriteUInt16LittleEndian(@this.RemainingSpan, value))
                return false;

            @this.Position += sizeof(ushort);
            return true;
        }

        /// <summary>Write a value in little-endian and advance the writer.</summary>
        public static bool TryWriteLittleEndian(ref this SpanWriter<byte> @this, int value)
        {
            if (!BinaryPrimitives.TryWriteInt32LittleEndian(@this.RemainingSpan, value))
                return false;

            @this.Position += sizeof(int);
            return true;
        }

        /// <summary>Write a value in little-endian and advance the writer.</summary>
        public static bool TryWriteLittleEndian(ref this SpanWriter<byte> @this, uint value)
        {
            if (!BinaryPrimitives.TryWriteUInt32LittleEndian(@this.RemainingSpan, value))
                return false;

            @this.Position += sizeof(uint);
            return true;
        }

        /// <summary>Write a value in little-endian and advance the writer.</summary>
        public static bool TryWriteLittleEndian(ref this SpanWriter<byte> @this, long value)
        {
            if (!BinaryPrimitives.TryWriteInt64LittleEndian(@this.RemainingSpan, value))
                return false;

            @this.Position += sizeof(long);
            return true;
        }

        /// <summary>Write a value in little-endian and advance the writer.</summary>
        public static bool TryWriteLittleEndian(ref this SpanWriter<byte> @this, ulong value)
        {
            if (!BinaryPrimitives.TryWriteUInt64LittleEndian(@this.RemainingSpan, value))
                return false;

            @this.Position += sizeof(ulong);
            return true;
        }

        /// <summary>Write a value in little-endian and advance the writer.</summary>
        public static bool TryWriteLittleEndian(ref this SpanWriter<byte> @this, float value)
        {
            return TryWriteLittleEndian(ref @this, BitConverter.SingleToInt32Bits(value));
        }

        /// <summary>Write a value in little-endian and advance the writer.</summary>
        public static bool TryWriteLittleEndian(ref this SpanWriter<byte> @this, double value)
        {
            return TryWriteLittleEndian(ref @this, BitConverter.DoubleToInt64Bits(value));
        }

        /// <summary>Write a value in big-endian and advance the writer.</summary>
        public static bool TryWriteBigEndian(ref this SpanWriter<byte> @this, short value)
        {
            if (!BinaryPrimitives.TryWriteInt16BigEndian(@this.RemainingSpan, value))
                return false;

            @this.Position += sizeof(short);
            return true;
        }

        /// <summary>Write a value in big-endian and advance the writer.</summary>
        public static bool TryWriteBigEndian(ref this SpanWriter<byte> @this, ushort value)
        {
            if (!BinaryPrimitives.TryWriteUInt16BigEndian(@this.RemainingSpan, value))
                return false;

            @this.Position += sizeof(ushort);
            return true;
        }

        /// <summary>Write a value in big-endian and advance the writer.</summary>
        public static bool TryWriteBigEndian(ref this SpanWriter<byte> @this, int value)
        {
            if (!BinaryPrimitives.TryWriteInt32BigEndian(@this.RemainingSpan, value))
                return false;

            @this.Position += sizeof(int);
            return true;
        }

        /// <summary>Write a value in big-endian and advance the writer.</summary>
        public static bool TryWriteBigEndian(ref this SpanWriter<byte> @this, uint value)
        {
            if (!BinaryPrimitives.TryWriteUInt32BigEndian(@this.RemainingSpan, value))
                return false;

            @this.Position += sizeof(uint);
            return true;
        }

        /// <summary>Write a value in big-endian and advance the writer.</summary>
        public static bool TryWriteBigEndian(ref this SpanWriter<byte> @this, long value)
        {
            if (!BinaryPrimitives.TryWriteInt64BigEndian(@this.RemainingSpan, value))
                return false;

            @this.Position += sizeof(long);
            return true;
        }

        /// <summary>Write a value in big-endian and advance the writer.</summary>
        public static bool TryWriteBigEndian(ref this SpanWriter<byte> @this, ulong value)
        {
            if (!BinaryPrimitives.TryWriteUInt64BigEndian(@this.RemainingSpan, value))
                return false;

            @this.Position += sizeof(ulong);
            return true;
        }

        /// <summary>Write a value in big-endian and advance the writer.</summary>
        public static bool TryWriteBigEndian(ref this SpanWriter<byte> @this, float value)
        {
            return TryWriteBigEndian(ref @this, BitConverter.SingleToInt32Bits(value));
        }

        /// <summary>Write a value in big-endian and advance the writer.</summary>
        public static bool TryWriteBigEndian(ref this SpanWriter<byte> @this, double value)
        {
            return TryWriteBigEndian(ref @this, BitConverter.DoubleToInt64Bits(value));
        }
    }
}
