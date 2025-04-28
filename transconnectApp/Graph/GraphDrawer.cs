using System.Drawing.Drawing2D;

namespace transconnect {
    public partial class GraphDrawer<T> : Form where T : notnull, IComparable<T>, IEquatable<T>
    {
        Graph<T> graph;
        
        /// <summary>
        /// Natural constructor
        /// </summary>
        /// <param name="graph"></param>
        public GraphDrawer(Graph<T> graph)
        {
            InitializeComponent();
            this.graph = graph;
            this.Paint += new PaintEventHandler(this.OnPaint);
        }

        /// <summary>
        /// Event handler which draws graph on screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPaint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int Noeudsize = 80;
            int margin = 150;
            int cst = 5;
            int cols = (int) Math.Ceiling(Math.Sqrt(graph.verticies.Count));
            int rows = (int) Math.Ceiling((double)graph.verticies.Count / cols);
            Dictionary<T, (int, int)> positions = new Dictionary<T, (int, int)>();
            Pen pen = new Pen(Color.Black);

            int index = 0;
            foreach(Noeud<T> noeud in graph.verticies) {
                int row = index / cols;
                int col = index % cols;
                int x = cst + col * (Noeudsize + margin);
                int y = cst + row * (Noeudsize + margin);
                positions[noeud.data] = (x, y);

                g.FillEllipse(Brushes.Coral, x, y, Noeudsize, Noeudsize);
                g.DrawString(noeud.data.ToString(), new Font("Arial", 13), Brushes.Black, x+30, y+30);

                index++;
            }

            foreach (Noeud<T> noeud in graph.verticies) {
                foreach(Lien<T> lien in noeud.edges) {
                    (int, int) coordsOrg = positions[noeud.data];
                    (int, int) coordsDst = positions[lien.dest.data];
                    g.DrawLine(pen, coordsOrg.Item1+Noeudsize/2, coordsOrg.Item2+Noeudsize/2, 
                    coordsDst.Item1+Noeudsize/2, coordsDst.Item2+Noeudsize/2);
                }
            }
        }
    }
}
