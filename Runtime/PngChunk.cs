namespace NewBlood
{
    /// <summary>A chunk in a PNG image.</summary>
    public readonly struct PngChunk
    {
        /// <summary>The chunk's identification code.</summary>
        public uint Id { get; }

        /// <summary>The CRC value of the bytes following the chunk length (including the ID).</summary>
        public uint Crc { get; }

        /// <summary>The offset of the chunk data in the source image.</summary>
        public int Offset { get; }

        /// <summary>The size of the chunk data in the source image.</summary>
        public int Length { get; }

        /// <summary>Returns a value indicating whether this chunk is critical.</summary>
        public bool IsCritical => (Id & (1 << 5)) == 0;

        /// <summary>Returns a value indicating whether this chunk is public.</summary>
        public bool IsPublic => (Id & (1 << 13)) == 0;

        /// <summary>Returns a value indicating whether this chunk is safe to copy.</summary>
        public bool IsCopyable => (Id & (1 << 29)) != 0;

        /// <summary>Initializes a new <see cref="PngChunk"/> instance.</summary>
        public PngChunk(uint id, uint crc, int offset, int length)
        {
            Id     = id;
            Crc    = crc;
            Offset = offset;
            Length = length;
        }
    }
}
