using System;
using System.Buffers.Binary;

namespace NewBlood
{
    /// <summary>Provides methods for reading PNG image files.</summary>
    public struct PngReader
    {
        /// <summary>The reader's current offset into <see cref="Buffer"/>.</summary>
        public int Offset { get; private set; }

        /// <summary>The buffer containing the PNG image to read.</summary>
        public ArraySegment<byte> Buffer { get; }

        /// <summary>The PNG format magic number.</summary>
        const ulong Magic = 0x89504E470D0A1A0A;

        /// <summary>Initializes a new <see cref="PngReader"/> instance.</summary>
        public PngReader(ArraySegment<byte> buffer)
        {
            if (!TryCreate(buffer, out this))
            {
                throw new ArgumentException("Buffer does not contain a valid PNG image.", nameof(buffer));
            }
        }

        /// <summary>Initializes a new <see cref="PngReader"/> instance.</summary>
        PngReader(ArraySegment<byte> buffer, int offset)
        {
            Buffer = buffer;
            Offset = offset;
        }

        /// <summary>Initializes a new <see cref="PngReader"/> instance.</summary>
        public static bool TryCreate(ArraySegment<byte> buffer, out PngReader reader)
        {
            if (!BinaryPrimitives.TryReadUInt64BigEndian(buffer, out ulong magic))
                goto Failure;

            if (magic != Magic)
                goto Failure;

            reader = new PngReader(buffer, 8);
            return true;

        Failure:
            reader = default;
            return false;
        }

        /// <summary>Reads the next chunk from the image.</summary>
        public bool TryReadChunk(out PngChunk chunk)
        {
            if (!TryPeekChunk(out chunk))
                return false;

            Offset += chunk.Length + 12;
            return true;
        }

        /// <summary>Peeks the next chunk from the image.</summary>
        public bool TryPeekChunk(out PngChunk chunk)
        {
            var slice = new ArraySegment<byte>(Buffer.Array, Buffer.Offset + Offset, Buffer.Count - Offset);

            // A chunk contains a 32-bit length, a 32-bit ID, and a 32-bit CRC after the data.
            // The minimum size of a chunk is therefore 12 bytes, when the data length is zero.
            if (slice.Count < 12)
                goto Failure;

            if (!BinaryPrimitives.TryReadInt32BigEndian(slice, out int length))
                goto Failure;

            if (!BinaryPrimitives.TryReadUInt32BigEndian(slice.AsSpan(4), out uint id))
                goto Failure;

            if (!BinaryPrimitives.TryReadUInt32BigEndian(slice.AsSpan(8 + length), out uint crc))
                goto Failure;

            chunk = new PngChunk(id, crc, Offset + 8, length);
            return true;

        Failure:
            chunk = default;
            return false;
        }

        /// <summary>Returns a slice of <see cref="Buffer"/> containing the chunk data.</summary>
        public ArraySegment<byte> GetChunkBuffer(PngChunk chunk)
        {
            return new ArraySegment<byte>(Buffer.Array, Buffer.Offset + chunk.Offset, chunk.Length);
        }
    }
}
