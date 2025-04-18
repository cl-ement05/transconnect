namespace transconnect {
    public class Graph<T> where T : notnull, IComparable<T>, IEquatable<T> {
        public List<Noeud<T>> verticies { get; }

        /// <summary>
        /// Natural constructor
        /// </summary>
        /// <param name="verticies"></param>
        public Graph(List<Noeud<T>> verticies) {
            this.verticies = verticies;
        }

        /// <summary>
        /// Constructor using adjacency matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="labels"></param>
        /// <exception cref="ArgumentException"></exception>
        public Graph(int[,] matrix, T[] labels) {
            verticies = new List<Noeud<T>>();
            if (matrix.GetLength(0) != labels.Length) {
                throw new ArgumentException("Numbers of labels must match nbr of rows in matrix");
            }
            if (matrix.GetLength(0) != matrix.GetLength(1)) {
                throw new ArgumentException("Must be a square matrix");
            }
            if (labels.Distinct().Count() != labels.Count()) {
                throw new ArgumentException("Graph cannot contain 2 verticies with the same label");
            }
            for(int i = 0; i < matrix.GetLength(0); i++) {
                verticies.Add(new Noeud<T>(labels[i]));
            }
            for(int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = 0; j < matrix.GetLength(1); j++) {
                    if (matrix[i, j] != 0) verticies[i].addEdge(verticies[j], matrix[i, j]);
                }
            }
        }

        /// <summary>
        /// Constructor taking adjacency list as parameter
        /// </summary>
        /// <param name="adjacencyList"></param>
        /// <exception cref="ArgumentException"></exception>
        public Graph(Dictionary<T, List<(T data, int weight)>> adjacencyList) {
            if (adjacencyList.Keys.Distinct().Count() != adjacencyList.Keys.Count()) {
                throw new ArgumentException("Graph cannot contain 2 verticies with the same label");
            }
            verticies = new List<Noeud<T>>();
            foreach(T val in adjacencyList.Keys) {
                verticies.Add(new Noeud<T>(val));
            }
            int counter = 0;
            foreach(T val in adjacencyList.Keys) {
                foreach(var adjacencies in adjacencyList[val]) {
                    verticies[counter].addEdge(verticies.Find((e) => e.data.Equals(adjacencies.Item1))!, adjacencies.Item2);
                }
                counter++;
            }
        }

        /// <summary>
        /// Itertative version of DFS which looks for cycle
        /// </summary>
        /// <param name="start"></param>
        /// <returns>List of verticies included in the cycle if one was found</returns>
        private List<Noeud<T>> DFSSearchCycle(Noeud<T> start) {
            Stack<Noeud<T>> dfs = new Stack<Noeud<T>>();
            HashSet<Noeud<T>> visited = new HashSet<Noeud<T>>();
            dfs.Push(start);
            bool found = false;
            while (dfs.Count > 0 && !found) {
                Noeud<T> cur = dfs.Peek();
                bool successor = false;
                foreach(Lien<T> lien in cur.edges) {
                    if (dfs.Contains(lien.dest) && dfs.Count > 2) { // > 2 because otherwise round-trip A-B-A is returned
                        dfs.Push(lien.dest);
                        found = true;
                        break;
                    }
                    if (!dfs.Contains(lien.dest) && !visited.Contains(lien.dest)) {
                        dfs.Push(lien.dest);
                        successor = true;
                        break;
                    }
                }
                if (!successor && !found) {
                    visited.Add(dfs.Pop());
                }
            }
            if (found) {
                return dfs.ToList();
            } else {
                return [];
            }
        }

        /// <summary>
        /// Internal mechanism for DFS algo
        /// </summary>
        /// <param name="start"></param>
        /// <param name="visiting"></param>
        /// <param name="visited"></param>
        /// <returns>List of visited verticies, ordered by "date" visited by algo</returns>
        private List<Noeud<T>> DFS(Noeud<T> start, Stack<Noeud<T>> visiting, HashSet<Noeud<T>> visited) {
            List<Noeud<T>> dfs = new List<Noeud<T>>();
            foreach(Lien<T> lien in start.edges) {
                if (!visiting.Contains(lien.dest) && !visited.Contains(lien.dest)) {
                    visiting.Push(lien.dest);
                    dfs = dfs.Concat(DFS(lien.dest, visiting, visited)).ToList();
                }
            }
            Noeud<T> justVisited = visiting.Pop();
            visited.Add(justVisited);
            return dfs.Concat([justVisited]).ToList();
        }

        /// <summary>
        /// DFS algorithm entry point
        /// </summary>
        /// <param name="start">Starting point</param>
        /// <returns>List of visited verticies, ordered by "date" visited by algo</returns>
        public List<Noeud<T>> DFS(Noeud<T> start) {
            var visiting2 = new Stack<Noeud<T>>();
            visiting2.Push(start);
            var visited2 = new HashSet<Noeud<T>>();
            return DFS(start, visiting2, visited2);
        }

        /// <summary>
        /// BFS algo
        /// </summary>
        /// <param name="start">Starting point</param>
        /// <returns>List of visited verticies, ordered by "date" visited by algo</returns>
        public List<Noeud<T>> BFS(Noeud<T> start) {
            Queue<Noeud<T>> queue = new Queue<Noeud<T>>();
            HashSet<Noeud<T>> visited = new HashSet<Noeud<T>>();
            List<Noeud<T>> result = new List<Noeud<T>>();
            queue.Enqueue(start);
            visited.Add(start);
            while (queue.Count > 0) {
                Noeud<T> vertex = queue.Dequeue();
                result.Add(vertex);
                foreach(Lien<T> edge in vertex.edges) {
                    if (!visited.Contains(edge.dest)) {
                        visited.Add(edge.dest);
                        queue.Enqueue(edge.dest);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Draws visual representation of this instance
        /// </summary>
        [STAThread]
        public void drawGraph() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new GraphDrawer<T>(this));
        }

        /// <summary>
        /// Checks if this graph is connected
        /// </summary>
        /// <returns>True if graph is connected</returns>
        public bool isConnected() {
            List<Noeud<T>> a = DFS(verticies.First());
            a.Sort();
            return a.SequenceEqual(verticies);
        }

        /// <summary>
        /// Dijkstra algo for finding shortest path
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Item1: list of verticies making the path from start to end, Item2: path length</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public (List<Noeud<T>>, int) Dijkstra(Noeud<T> start, Noeud<T> end) {
            if (verticies.Any((e) => e.edges.Any((edge) => edge.weight < 0))) {
                throw new InvalidOperationException("Cannot use Dijkstra if weights are < 0");
            }
            List<Noeud<T>> result = new List<Noeud<T>>();
            HashSet<Noeud<T>> visited = new HashSet<Noeud<T>>();
            Dictionary<Noeud<T>, Noeud<T>?> predicitions = new Dictionary<Noeud<T>, Noeud<T>?>();
            Dictionary<Noeud<T>, int> distances = new Dictionary<Noeud<T>, int>();
            
            foreach (Noeud<T> noeud in verticies) {
                if (noeud.Equals(start)) {
                    predicitions[noeud] = start;
                    distances[noeud] = 0;
                } else {
                    predicitions[noeud] = null;
                    distances[noeud] = int.MaxValue;
                }
            }

            while (!visited.Contains(end)) {
                Noeud<T> minNoeud = distances.First((e) => !visited.Contains(e.Key)).Key;
                int minDistance = distances[minNoeud];
                foreach (Noeud<T> noeud in distances.Keys) {
                    if (!visited.Contains(noeud)) {
                        if (minDistance > distances[noeud]) {
                            minDistance = distances[noeud];
                            minNoeud = noeud;
                        }
                    }
                }
                visited.Add(minNoeud);
                foreach(Lien<T> lien in minNoeud.edges) {
                    if (!visited.Contains(lien.dest)) {
                        if (minDistance + lien.weight < distances[lien.dest]) {
                            distances[lien.dest] = minDistance + lien.weight;
                            predicitions[lien.dest] = minNoeud;
                        }
                    }
                }
            }

            Noeud<T> current = end;
            while (current != start) {
                result.Add(current);
                current = predicitions[current]!;
            }
            result.Add(start);
            result.Reverse();

            return (result, distances[end]);
        }

        /// <summary>
        /// Bellman-Ford algo for finding shortest path
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Item1: list of verticies making the path from start to end, Item2: path length</returns>
        public (List<Noeud<T>>, int) BellmanFord(Noeud<T> start, Noeud<T> end) {
            List<Noeud<T>> result = new List<Noeud<T>>();
            Dictionary<Noeud<T>, Noeud<T>?> predicitions = new Dictionary<Noeud<T>, Noeud<T>?>();
            Dictionary<Noeud<T>, int> distances = new Dictionary<Noeud<T>, int>();
            
            foreach (Noeud<T> noeud in verticies) {
                if (noeud.Equals(start)) {
                    predicitions[noeud] = start;
                    distances[noeud] = 0;
                } else {
                    predicitions[noeud] = null;
                    distances[noeud] = int.MaxValue;
                }
            }

            HashSet<Lien<T>> liens = new HashSet<Lien<T>>();
            verticies.ForEach((noeud) => liens = liens.Union(noeud.edges).ToHashSet());
            for (int i = 0; i < verticies.Count()-1; i++) {
                foreach (Lien<T> lien in liens) {
                    if (distances[lien.origin] + lien.weight < distances[lien.dest]) {
                        distances[lien.dest] = distances[lien.origin] + lien.weight;
                        predicitions[lien.dest] = lien.origin;
                    }
                }
            }
            //checking for negative cycle
            foreach (Lien<T> lien in liens) {
                if (distances[lien.origin] + lien.weight < distances[lien.dest]) {
                    throw new InvalidOperationException("Cannot use Bellman-Ford when there is a negative cycle");
                }
            }

            Noeud<T> current = end;
            while (current != start) {
                result.Add(current);
                current = predicitions[current]!;
            }
            result.Add(start);
            result.Reverse();

            return (result, distances[end]);
        }

        /// <summary>
        /// Floyd-Warshall algo for finding shortest path
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>path length</returns>
        public int FloydWarshall(Noeud<T> start, Noeud<T> end) {
            //first we need to match each vertex with an arbitrary index nbr since we have no way to figure out
            //how to sort verticies in the matrix, this won't affect the result
            int n = verticies.Count();
            Dictionary<Noeud<T>, int> associations = new Dictionary<Noeud<T>, int>();
            associations[start] = 0;
            associations[end] = n-1;
            int nbr = 1;
            foreach (Noeud<T> vertex in verticies.Except([start, end])) {
                associations[vertex] = nbr;
                nbr++;
            }

            //building matrix for index 0 ie adjacency matrix
            int[,] matrix = new int[n,n];
            for (int i = 0; i < n ; i++) {
                for (int j = 0; j < n; j++) {
                    matrix[i,j] = int.MaxValue;
                }
            }
            foreach (Noeud<T> noeud in verticies) {
                foreach (Lien<T> lien in noeud.edges) {
                    matrix[associations[noeud], associations[lien.dest]] = lien.weight;
                }
            }

            for (int k = 1; k < n; k++) {
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < n; j++) {
                        if (matrix[i,k] + matrix[k,j] > 0 ) { //checking bc int.max+int.max returns -2
                            matrix[i,j] = Math.Min(matrix[i,j], matrix[i,k] + matrix[k,j]);
                        }   
                    }
                }
            }

            return matrix[0,n-1];
        }

        /// <summary>
        /// Checks if this instance has a cycle
        /// </summary>
        /// <returns>List of verticies making the cycle if one was found, empty list otherwise</returns>
        public List<Noeud<T>> hasCycle() {
            return DFSSearchCycle(verticies.First());
        }

        /// <summary>
        /// String representation of this graph
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "";
            foreach (Noeud<T> vert in verticies) {
                str += vert.ToString()+"\n";
            }
            return str;
        }
    }
}
