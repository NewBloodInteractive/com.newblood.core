using System;

namespace NewBlood
{
    /// <summary>Provides methods for reading from <see cref="ReadOnlySpan{T}"/> instances.</summary>
    public ref struct SpanReader<T>
    {
        /// <summary>The underlying <see cref="ReadOnlySpan{T}"/> for the reader.</summary>
        public ReadOnlySpan<T> Span { get; }

        /// <summary>The current position in the span.</summary>
        public int Position { get; set; }

        /// <summary>Gets a value indicating whether there is no more data to read.</summary>
        public bool End => Position >= Span.Length;

        /// <summary>The unread portion of <see cref="Span"/>.</summary>
        public ReadOnlySpan<T> UnreadSpan => Span.Slice(Position);

        /// <summary>Initializes a new <see cref="SpanReader{T}"/> instance.</summary>
        public SpanReader(ReadOnlySpan<T> span)
        {
            Span     = span;
            Position = 0;
        }

        /// <summary>Peeks at the next value without advancing the reader.</summary>
        public bool TryPeek(out T value)
        {
            if (End)
            {
                value = default;
                return false;
            }

            value = Span[Position];
            return true;
        }

        /// <summary>Peeks at the next value at specific offset without advancing the reader.</summary>
        public bool TryPeek(int offset, out T value)
        {
            if (Position + offset >= Span.Length)
            {
                value = default;
                return false;
            }

            value = Span[Position + offset];
            return true;
        }

        /// <summary>Read the next value and advance the reader.</summary>
        public bool TryRead(out T value)
        {
            if (End)
            {
                value = default;
                return false;
            }

            value = Span[Position++];
            return true;
        }

        /// <summary>Copies data from the current <see cref="Position"/> to the given <paramref name="destination"/> span if there is enough data to fill it.</summary>
        public bool TryCopyTo(Span<T> destination)
        {
            return UnreadSpan.TryCopyTo(destination);
        }
    }
}
