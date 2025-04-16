namespace transconnect {
    public class Lien<T> : IEquatable<Lien<T>> where T : notnull, IComparable<T>, IEquatable<T> {
        public Noeud<T> origin { get; }
        public Noeud<T> dest { get; }
        public int weight { get; }

        /// <summary>
        /// Constructor with all parameters
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="weight"></param>
        public Lien(Noeud<T> origin, Noeud<T> dest, int weight) {
            this.origin = origin;
            this.dest = dest;
            this.weight = weight;
        }

        public static bool operator == (Lien<T> left, Lien<T> right) {
            return left.origin == right.origin && left.dest == right.dest && left.weight == right.weight;
        }

        public static bool operator != (Lien<T> left, Lien<T> right) {
            return left.origin != right.origin || left.dest != right.dest || left.weight != right.weight;
        }

        /// <summary>
        /// String representation of this edge
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{origin.data} to {dest.data}";
        }

        /// <summary>
        /// Detemines if two instances are equal
        /// </summary>
        /// <param name="other"></param>
        /// <returns>True if both instances they have the same origin, same destination and if they
        /// have the same weight, false otherwise</returns>
        public bool Equals(Lien<T>? other)
        {
            if (other is null) return false;
            return this == other;
        }

        public override bool Equals(object? obj) => Equals(obj as Lien<T>);
        public override int GetHashCode() => weight.GetHashCode();
    }
}
