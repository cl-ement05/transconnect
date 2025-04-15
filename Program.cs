namespace transconnect {
    public class Program {
        public static void Main(string[] args) 
        {
            int[,] data = { 
                {0, 3, 5, 0, 0, 0},
                {3, 0, 7, 0, 0, 6},
                {5, 7, 0, 2, 0, 0},
                {0, 0, 2, 0, 2, 0},
                {0, 0, 0, 2, 0, 0},
                {0, 6, 0, 0, 0, 0} };
            char[] labels = {'A', 'B', 'C', 'D', 'E', 'F'};
            Graph<char> graph = new Graph<char>(data, labels);
            graph.BFS(graph.verticies.First()).ForEach(Console.WriteLine);
        }
    }
}
