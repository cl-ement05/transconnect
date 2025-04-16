namespace transconnect {
    public class Program {
        public static void Main(string[] args) 
        {
            int[,] data = { 
                {0, 3, 5, 0, 0, 0},
                {3, 0, 1, 0, 0, 6},
                {5, 1, 0, 2, 0, 0},
                {0, 0, 2, 0, 2, 0},
                {0, 0, 0, 2, 0, 0},
                {0, 6, 0, 0, 0, 0} };
            char[] labels = {'A', 'B', 'C', 'D', 'E', 'F'};
            Graph<char> graph = new Graph<char>(data, labels);
            Dictionary<char, List<(char data, int weight)>> dict = new Dictionary<char, List<(char data, int weight)>>() {
                {'A', [('B', 3), ('C', 5)]},
                {'B', [('A', 3), ('C', 7), ('F', 6)]},
                {'C', [('A', 5), ('B', 7), ('D', 2)]},
                {'D', [('C', 2), ('E', 3)]},
                {'E', [('D', 3)]},
                {'F', [('B', 6)]},
            };
            Graph<char> graph2 = new Graph<char>(dict);
            Noeud<char> noeud = graph2.verticies.Find((e) => e.data == 'A')!;
            Noeud<char> noeud2 = graph2.verticies.Find((e) => e.data == 'E')!;
            int var = graph2.FloydWarshall(noeud, noeud2);
            Console.WriteLine(var);
        }
    }
}
