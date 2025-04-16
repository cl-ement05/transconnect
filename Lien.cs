namespace transconnect {
    public class Lien<T> : IEquatable<Lien<T>> where T : notnull, IComparable<T>, IEquatable<T> {
        public Noeud<T> origin { get; }
        public Noeud<T> dest { get; }
        public int weight { get; }

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

        public override string ToString()
        {
            return $"{origin.data} to {dest.data}";
        }

        public bool Equals(Lien<T>? other)
        {
            if (other is null) return false;
            return this == other;
        }

        public override bool Equals(object? obj) => Equals(obj as Lien<T>);
        public override int GetHashCode() => weight.GetHashCode();
    }
}
