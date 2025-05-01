namespace transconnect {
    public class Noeud<T> : IComparable<Noeud<T>>, IEquatable<Noeud<T>> where T : notnull, IComparable<T>, IEquatable<T> {
        public T data { get; }

        /// <summary>
        /// Constructor with data only
        /// </summary>
        /// <param name="data"></param>
        public Noeud(T data) {
            this.data = data;
        }

        public static bool operator == (Noeud<T> left, Noeud<T> right) {
            return left.data.Equals(right.data);
        }

        public static bool operator != (Noeud<T> left, Noeud<T> right) {
            return !(left == right);
        }

        /// <summary>
        /// String representation of this vertex
        /// </summary>
        /// <returns></returns>
        public override string? ToString() {
            return data.ToString();
        }

        /// <summary>
        /// Detemines if two instances are equal
        /// </summary>
        /// <param name="other"></param>
        /// <returns>True if both instances share the same data, false otherwise</returns>
        public bool Equals(Noeud<T>? other)
        {
            if (other is null) return false;
            return this == other;
        }

        public override bool Equals(object? obj) => Equals(obj as Noeud<T>);
        public override int GetHashCode() => data.GetHashCode();

        /// <summary>
        /// Determines how to sort two instances based on data comparison
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Noeud<T>? other)
        {
            if (other is null) return -1;
            return data.CompareTo(other.data);
        }
    }
}
