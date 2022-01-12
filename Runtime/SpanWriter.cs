using System;

namespace NewBlood
{
    /// <summary>Provides methods for writing to <see cref="Span{T}"/> instances.</summary>
    public ref struct SpanWriter<T>
    {
        /// <summary>The underlying <see cref="Span{T}"/> for the writer.</summary>
        public Span<T> Span { get; }

        /// <summary>The current position in the span.</summary>
        public int Position { get; set; }

        /// <summary>Gets a value indicating whether there is no more space left in the span.</summary>
        public bool End => Position >= Span.Length;

        /// <summary>The unwritten portion of <see cref="Span"/>.</summary>
        public Span<T> RemainingSpan => Span.Slice(Position);

        /// <summary>Initializes a new <see cref="SpanWriter{T}"/> instance.</summary>
        public SpanWriter(Span<T> span)
        {
            Span     = span;
            Position = 0;
        }

        /// <summary>Write a value and advance the writer.</summary>
        public bool TryWrite(T value)
        {
            if (End)
                return false;

            Span[Position++] = value;
            return true;
        }
    }
}
