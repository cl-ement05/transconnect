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
            graph.DFS(graph.verticies.First()).ForEach(Console.WriteLine);
            Dictionary<char, List<(char data, int weight)>> dict = new Dictionary<char, List<(char data, int weight)>>() {
                {'A', [('B', 3), ('C', 5)]},
                {'B', [('A', 3), ('C', 7), ('F', 6)]},
                {'C', [('A', 5), ('B', 7)]},
                {'D', [('C', 2), ('E', 3)]},
                {'E', [('D', 3)]},
                {'F', [('B', 6)]},
            };
            Graph<char> graph2 = new Graph<char>(dict);
            graph2.DFS(graph.verticies.First()).ForEach(Console.WriteLine);
        }
    }
}
