using System;
using System.Buffers;
using System.Buffers.Binary;

namespace NewBlood
{
    /// <summary>Provides methods for writing PNG images.</summary>
    public readonly struct PngWriter
    {
        /// <summary>The PNG format magic number.</summary>
        const ulong Magic = 0x89504E470D0A1A0A;

        /// <summary>The <see cref="IBufferWriter{T}"/> used by this writer.</summary>
        public IBufferWriter<byte> Writer { get; }

        /// <summary>Initializes a new <see cref="PngWriter"/> instance.</summary>
        public PngWriter(IBufferWriter<byte> writer)
        {
            Writer = writer;
        }

        /// <summary>Writes a PNG header to the buffer writer.</summary>
        public void WriteHeader()
        {
            BinaryPrimitives.WriteUInt64BigEndian(Writer.GetSpan(8), Magic);
            Writer.Advance(8);
        }

        /// <summary>Calculates the CRC and writes a chunk to the buffer writer.</summary>
        public void WriteChunk(uint id, ReadOnlySpan<byte> buffer)
        {
            WriteChunk(id, PngCrcAlgorithm.Compute(PngCrcAlgorithm.Compute(id), buffer), buffer);
        }

        /// <summary>Writes a chunk to the buffer writer.</summary>
        public void WriteChunk(uint id, uint crc, ReadOnlySpan<byte> buffer)
        {
            var destination = Writer.GetSpan(buffer.Length + 12);
            BinaryPrimitives.WriteInt32BigEndian(destination, buffer.Length);
            BinaryPrimitives.WriteUInt32BigEndian(destination.Slice(4), id);
            buffer.CopyTo(destination.Slice(8));
            BinaryPrimitives.WriteUInt32BigEndian(destination.Slice(8 + buffer.Length), ~crc);
            Writer.Advance(buffer.Length + 12);
        }
    }
}
