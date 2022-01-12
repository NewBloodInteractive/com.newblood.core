namespace NewBlood
{
    /// <summary>Specifies the type of a PNG chunk.</summary>
    public enum PngChunkId : uint
    {
        /// <summary>Image header chunk.</summary>
        IHDR = ('I' << 24) | ('H' << 16) | ('D' << 8) | 'R',

        /// <summary>Palette chunk.</summary>
        PLTE = ('P' << 24) | ('L' << 16) | ('T' << 8) | 'E',

        /// <summary>Image data chunk.</summary>
        IDAT = ('I' << 24) | ('D' << 16) | ('A' << 8) | 'T',

        /// <summary>Image trailer chunk.</summary>
        IEND = ('I' << 24) | ('E' << 16) | ('N' << 8) | 'D',

        /// <summary>Primary chromaticities chunk.</summary>
        cHRM = ('c' << 24) | ('H' << 16) | ('R' << 8) | 'M',

        /// <summary>Image gamma chunk.</summary>
        gAMA = ('g' << 24) | ('A' << 16) | ('M' << 8) | 'A',

        /// <summary>Embedded ICC profile chunk.</summary>
        iCCP = ('i' << 24) | ('C' << 16) | ('C' << 8) | 'P',

        /// <summary>Significant bits chunk.</summary>
        sBIT = ('s' << 24) | ('B' << 16) | ('I' << 8) | 'T',

        /// <summary>Standard RGB color space chunk.</summary>
        sRGB = ('s' << 24) | ('R' << 16) | ('G' << 8) | 'B',

        /// <summary>Background color chunk.</summary>
        bKGD = ('b' << 24) | ('K' << 16) | ('G' << 8) | 'D',

        /// <summary>Palette histogram chunk.</summary>
        hIST = ('h' << 24) | ('I' << 16) | ('S' << 8) | 'T',

        /// <summary>Transparency chunk.</summary>
        tRNS = ('t' << 24) | ('R' << 16) | ('N' << 8) | 'S',

        /// <summary>Physical pixel dimensions chunk.</summary>
        pHYs = ('p' << 24) | ('H' << 16) | ('Y' << 8) | 's',

        /// <summary>Suggested palette chunk.</summary>
        sPLT = ('s' << 24) | ('P' << 16) | ('L' << 8) | 'T',

        /// <summary>Image last-modification time chunk.</summary>
        tIME = ('t' << 24) | ('I' << 16) | ('M' << 8) | 'E',

        /// <summary>International textual data chunk.</summary>
        iTXt = ('i' << 24) | ('T' << 16) | ('X' << 8) | 't',

        /// <summary>Textual data chunk.</summary>
        tEXt = ('t' << 24) | ('E' << 16) | ('X' << 8) | 't',

        /// <summary>Compressed textual data chunk.</summary>
        zTXt = ('z' << 24) | ('T' << 16) | ('X' << 8) | 't',
    }
}
