using System; using System.Collections; using System.Collections.Generic; using System.ComponentModel; using System.Data; using System.Drawing; using System.Linq; using System.Text; using System.Threading.Tasks; using System.Windows.Forms;  namespace Proyecto1 {     public partial class ListaTokens : Form     {         private string[] grid = new string[5];         public ListaTokens(ArrayList list)         {             InitializeComponent();             idListView.View = View.Details;             idListView.GridLines = true;             idListView.FullRowSelect = true;              string[] grid = new string[6];             ListViewItem itm;              //Add first item             foreach (Tokenn i in list) {                 grid[0] = Convert.ToString(i.No);                 grid[1] = Convert.ToString(i.Tokens);                 grid[2] = i.Lexema;                 grid[3] = i.Tipo;                 grid[4] = Convert.ToString(i.Fila);                 grid[5] = Convert.ToString(i.Columna);                 itm = new ListViewItem(grid);                 itm.Font = new System.Drawing.Font(         "Candara", 14, System.Drawing.FontStyle.Regular);                 idListView.Items.Add(itm);             }           }          private void idListView_SelectedIndexChanged(object sender, EventArgs e)         {          }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    } } 