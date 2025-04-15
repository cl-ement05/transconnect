namespace transconnect {
    public class Graph<T> where T : notnull {
        public List<Noeud<T>> verticies { get; }

        public Graph(List<Noeud<T>> verticies) {
            this.verticies = verticies;
        }

        public Graph(int[,] matrix, T[] labels) {
            verticies = new List<Noeud<T>>();
            if (matrix.GetLength(0) != labels.Length) {
                throw new ArgumentException("Numbers of labels must match nbr of rows in matrix");
            } else if (matrix.GetLength(0) != matrix.GetLength(1)) {
                throw new ArgumentException("Must be a square matrix");
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

        public Graph(Dictionary<T, List<(T data, int weight)>> adjacencyList) {
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

        public List<Noeud<T>> DFS(Noeud<T> start, Stack<Noeud<T>>? visiting = null, bool searchCycles = false) {
            if (visiting == null) {
                var visited2 = new Stack<Noeud<T>>();
                visited2.Push(start);
                return DFS(start, visited2, searchCycles);
            } else {
                if (searchCycles) {
                    Stack<Noeud<T>> dfs = new Stack<Noeud<T>>();
                    HashSet<Noeud<T>> visited = new HashSet<Noeud<T>>();
                    dfs.Push(start);
                    bool found = false;
                    while (dfs.Count > 0 || !found) {
                        Noeud<T> cur = dfs.Peek();
                        bool successor = false;
                        foreach(Lien<T> lien in cur.edges) {
                            if (dfs.Contains(lien.dest)) {
                                dfs.Push(lien.dest);
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
                } else {
                    List<Noeud<T>> dfs = new List<Noeud<T>>();
                    foreach(Lien<T> lien in start.edges) {
                        if (!visiting.Contains(lien.dest)) {
                            visiting.Push(lien.dest);
                            dfs.Concat(DFS(lien.dest, visiting));
                        }
                    }
                    visiting.Pop();
                    return (List<Noeud<T>>)dfs.Concat([start]);
                }
            }
        }

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

        public bool isConnected() {
            return DFS(verticies.First()).SequenceEqual(verticies);
        }

        public List<Noeud<T>> hasCycle() {
            return DFS(verticies.First(), searchCycles: true);
        }

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
