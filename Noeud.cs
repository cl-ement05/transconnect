namespace transconnect {
    public class Noeud<T> : IComparable<Noeud<T>>, IEquatable<Noeud<T>> where T : notnull, IComparable<T>, IEquatable<T> {
        public T data { get; }
        public HashSet<Lien<T>> edges { get; }

        /// <summary>
        /// Constructor with data only
        /// </summary>
        /// <param name="data"></param>
        public Noeud(T data) {
            this.data = data;
            edges = new HashSet<Lien<T>>();
        }

        /// <summary>
        /// Constructor with all attributes : data and edges which must be provided via a HashSet
        /// </summary>
        /// <param name="data"></param>
        /// <param name="edges"></param>
        public Noeud(T data, HashSet<Lien<T>> edges) {
            this.data = data;
            this.edges = edges;
        }

        /// <summary>
        /// Adds an edge ie connects this vertex to another
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="weight"></param>
        public void addEdge(Noeud<T> dest, int weight) {
            Lien<T> lien = new Lien<T>(this, dest, weight);
            edges.Add(lien);
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
        public override string ToString() {
            string str = "";
            foreach (Lien<T> edge in edges) {
                str += edge.ToString() + ", ";
            }
            return data.ToString() + ", edges : " + str;
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
