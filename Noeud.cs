namespace transconnect {
    public class Noeud<T> : IComparable<Noeud<T>>, IEquatable<Noeud<T>> where T : notnull, IComparable<T> {
        public T data { get; }
        public HashSet<Lien<T>> edges { get; }

        public Noeud(T data) {
            this.data = data;
            edges = new HashSet<Lien<T>>();
        }

        public Noeud(T data, HashSet<Lien<T>> edges) {
            this.data = data;
            this.edges = edges;
        }

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

        public override string ToString() {
            string str = "";
            foreach (Lien<T> edge in edges) {
                str += edge.ToString() + ", ";
            }
            return data.ToString() + ", edges : " + str;
        }

        public bool Equals(Noeud<T>? other)
        {
            if (other is null) return false;
            return this == other;
        }

        public override bool Equals(object? obj) => Equals(obj as Noeud<T>);
        public override int GetHashCode() => data.GetHashCode();

        public int CompareTo(Noeud<T>? other)
        {
            if (other is null) return -1;
            return data.CompareTo(other.data);
        }
    }
}
