namespace transconnect {
    public class Noeud<T> where T : notnull {
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
            return left.data.Equals(right.data) && left.edges.Equals(right.edges);
        }

        public static bool operator != (Noeud<T> left, Noeud<T> right) {
            return !left.data.Equals(right.data) || !left.edges.Equals(right.edges);
        }

        public override string ToString() {
            string str = "";
            foreach (Lien<T> edge in edges) {
                str += edge.ToString() + ", ";
            }
            return data.ToString() + ", edges : " + str;
        }
    }
}
