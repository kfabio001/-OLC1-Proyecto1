using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1
{
    public partial class Tree_View : Form
    {
        //int columna = 0;
       // int fila = 1;
        string auxNomb;
        string lex;
        private ArrayList ruta = new ArrayList();
        private List<int> Palabras = new List<int>();
        private Match match;
        string regex = "";
        string aux = "";


        TextBox textBox1 = new TextBox();
        List<string> nom = new List<string>();
       public List<string> ruTree = new List<string>();
        int contadorNombres;
       



        

        private void Agregar(string cadena) {
            char cadenas;
            string palabra = "";
            TreeNode Node;
            int estado = 0;
            //int prueba;
            int MMes = 0;
            int pos = 0;
            int contAnio = 0;
            int contNomPath =0;
            string alo = "", mes = "", path = "", nombre = "", paths = "C:";
            
            for (int j = 0; j < cadena.Length; j++) {
                cadenas = cadena[j];
                palabra += cadenas;

                if (palabra.Equals("\n") || palabra.Equals(";") || palabra.Equals("=") || palabra.Equals(":") || palabra.ToLower().Equals("documento{") ||   palabra.Equals("\r") || palabra.Equals("\t") || palabra.Equals("\f") || palabra.Equals(" ") || palabra.ToLower().Equals("path"))
                {
                    palabra = "";

                } else if (palabra.Equals("path")) {
                    //Console.WriteLine(palabra);
                    palabra = "";
                }
                else if (palabra.Equals("}"))
                {
                    if (contNomPath==1) {
                        estado = 5;
                        contNomPath = 0;
                    }
                    palabra = "";
                }
                //||(cadenas!='"'))//Char.IsLetter(cadenas)||Char.IsSymbol(cadenas) )//|| cadenas == '/' || cadenas == '.' || Char.IsDigit(cadenas) || cadenas == '_' || cadenas == ':' || cadenas == slash)
                switch (estado) {
                    case 0:
                        switch (palabra.ToLower()) {

                            case " ":
                            case "\r":
                            case "\t":
                            case "\f":
                            case "\n":
                            case ":":
                            case "=":
                            case "}":
                                estado = 0;
                                break;
                            case "año":
                               // Console.WriteLine(palabra);
                                palabra = "";
                                estado = 1;
                                break;
                            case "mes":
                              //  Console.WriteLine(palabra);
                                palabra = "";
                                estado = 2;
                                break;
                            case "\"c:":
                               // Console.WriteLine(palabra);
                                // palabra = "";
                                estado = 3;
                                break;
                            case "nombre":
                               // Console.WriteLine(palabra);
                                palabra = "";
                                estado = 4;
                                break;
                            
                            default:

                                break;
                        }
                        break;
                    case 1:
                        if (Char.IsDigit(cadenas)) {
                            alo += cadenas;
                            estado = 1;
                        } else if (cadenas == '{') {
                            estado = 0;
                            palabra = "";
                            Node = idTreeView.Nodes.Add(alo);
                            alo = "";
                            contAnio++;
                            if (contAnio >= 2) {
                                pos++;
                                MMes = 0;
                            }
                        }
                        break;
                    case 2:
                        if (Char.IsLetter(cadenas))
                        {
                            mes += cadenas;
                            estado = 2;
                        }
                        else if (cadenas == '{')
                        {
                            estado = 0;
                            palabra = "";

                            idTreeView.Nodes[pos].Nodes.Add(mes);
                            mes = "";
                            MMes++;
                        }
                        break;
                    case 3:
                        
                       
                        //path += cadenas;
                        if (cadenas!='"')//||(cadenas!='"'))//Char.IsLetter(cadenas)||Char.IsSymbol(cadenas) )//|| cadenas == '/' || cadenas == '.' || Char.IsDigit(cadenas) || cadenas == '_' || cadenas == ':' || cadenas == slash)
                        {
                            
                            path += cadenas;

                            estado = 3;
                        }
                        else if (cadenas == '"')
                        {
                            //path = c + path;
                           // Console.WriteLine("ruta  "+path);
                            ruTree.Add(path);
                           // if (nombre.Equals(""))
                           // {
                                estado = 0;
                                palabra = "";
                           // }
                           // else {
                            //    estado = 5;
                           // }
                        }
                        break;
                    case 4:
                        if (Char.IsLetter(cadenas) || Char.IsDigit(cadenas) || cadenas == '_')
                        {
                            nombre += cadenas;
                            estado = 4;
                        }
                        else if (cadenas == ';')
                        {
                           // if (path.Equals("")) {
                                estado = 0;
                                palabra = "";
                                contNomPath++;
                           // } else {
                           //     estado = 5;
                           // }
                        }
                        break;
                    case 5:
                       estado = 0;
                        palabra = "";
                        if (MMes == 1)
                        {
                            idTreeView.Nodes[pos].Nodes[0].Nodes.Add(nombre);
                            ruta.Add(new Path(nombre, path));
                            Console.WriteLine(nombre);
                            nom.Add(nombre);
                            contadorNombres++;
                            nombre = "";
                            path = "";
                        }
                        else if (MMes > 0)
                        {
                            idTreeView.Nodes[pos].Nodes[MMes - 1].Nodes.Add(nombre);
                            ruta.Add(new Path(nombre, path));
                            Console.WriteLine(nombre);
                            nom.Add(nombre);
                            contadorNombres++;
                            nombre = "";
                            path = "";
                        }
                        break;
                }
            }
        }

        public Tree_View(string cadena)
        {
            InitializeComponent();
            Agregar(cadena);
            obtenerNombre();

        }
        public void obtenerNombre()
        {
         
            string[] arrayNombres = nom.ToArray();
            string[] arrayRutas = ruTree.ToArray();
            

            for (int i = 0; i < contadorNombres; i++)
            {
                // Console.WriteLine("{0} ", arrayNombres[i]+ arrayRutas[i]);
                // Console.WriteLine("{0} ", arrayNombres[i]);
                string ro = @"C:" + @arrayRutas[i]; //+ arrayNombres[i];
                

                // Console.WriteLine(ro);
                if (File.Exists(ro))
                {
                    ro = "";
                }
                else
                {
                    
                    StreamWriter sw = new StreamWriter(ro);
                }
            
                
        }
        }
        private void IdTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            int posNode = idTreeView.SelectedNode.Index;
            string nombreDoc = idTreeView.SelectedNode.Text;
            string path = "";
            string texto = "";
            foreach (Path p in ruta)
            {
                if (nombreDoc.Equals(p.Nombre)) {
                    path = p.Url;
                  
                }
            }
            try {

                try {
                    try {
                        texto = File.ReadAllText(path);
                        idTexto.Text = texto;
                    }
                    catch (System.IO.FileNotFoundException) {
                        MessageBox.Show("Path erronea");
                    }
                 
                }
                catch (System.IO.DirectoryNotFoundException) {
                    MessageBox.Show("Path erronea");
                }
            }
            catch (System.ArgumentException) {
                idTexto.Text = "";
            }
        }

        private void IdBuscar_Click(object sender, EventArgs e)
        {
            
            int i;
            char aux;
            string palabra = "";
            int op = 0;
            for ( i = 0; i < idTexto.Text.Length; i++) {
                aux = idTexto.Text[i];
                switch (op) {
                    case 0:
                        switch (aux) {
                            case ' ':
                            case '\r':
                            case '\t':
                            case '\f':
                            case '\n':
                                AnalizarP(palabra);
                                palabra = "";
                                break;
                            default:
                                op = 1;
                                i = i - 1;
                                break;
                        }
                        break;
                    case 1:
                        if (aux == ' ' || aux == '\n')
                        {
                            op = 0;
                            i = i - 1;
                        }
                        else {
                            palabra += aux;
                            op = 1;
                        }
                        break;
                }
            }

        }
        private void AnalizarP(string palabra) {
            Regex rx;
            rx = new Regex(@IdExpresion.Text);
            bool isMatch = false;
            isMatch = rx.IsMatch(palabra);
            if (isMatch == true)
            {
                Buscar(palabra);
            }
            else if (isMatch == false)
            {
            }

        }
        private void Buscar(string palabra)
        {
            string input = palabra;
            match = Regex.Match(input,@IdExpresion.Text, RegexOptions.Compiled);

            if (match.Success)
            {
                regex = match.Value;
                if (regex.Equals(aux))
                {

                }
                else
                {
                    Pintar(match.Value);
                    aux = match.Value;
                }
            }
        }
        private void Pintar(string palabra)
        {
            int inicio = 0;
            while (inicio <= idTexto.Text.LastIndexOf(palabra))
            {
                idTexto.Find(palabra, inicio, idTexto.TextLength, RichTextBoxFinds.MatchCase);
                idTexto.SelectionBackColor = Color.Yellow;
                inicio = idTexto.Text.IndexOf(palabra, inicio) + 1;
            }
        }

        private void IdTexto_TextChanged(object sender, EventArgs e)
        {

        }
        public void abririArchivo()
        {
            string[] arrayRutas = ruTree.ToArray();
            string[] arrayNombres = nom.ToArray();
            for (int i = 0; i < contadorNombres; i++)
            {
                string name = idTreeView.SelectedNode.Text;
                if (name.Equals(arrayNombres[i]))
                {
                    Console.WriteLine("hola");
                   // Console.WriteLine(i + " " + name + " " + arrayNombres[i] + " " + arrayRutas[i]);
                    using (StreamReader sr = new StreamReader(@"C:" + arrayRutas[i]))
                    {
                        string linea;
                        while ((linea = sr.ReadLine()) != null)
                        {
                            idTexto.AppendText(linea + "\n");
                        }

                    }
                    StreamWriter srt = new StreamWriter(@"C:" + arrayRutas[i]);
                    srt.Write(idTexto.Text);
                    srt.Close();
                }

            }
        }

        public void guardarArchivo()
        {
            string[] arrayRutas = ruTree.ToArray();
            string[] arrayNombres = nom.ToArray();
            for (int i = 0; i < contadorNombres; i++)
            {
                string name = idTreeView.SelectedNode.Text;
                if (name.Equals(arrayNombres[i]))
                {
                    Console.WriteLine("hola");
                   // Console.WriteLine(i + " " + name + " " + arrayNombres[i] + " " + arrayRutas[i]);
                    
                    StreamWriter srt = new StreamWriter(@"C:" + arrayRutas[i]);
                    srt.Write(idTexto.Text);
                    srt.Close();
                }

            }
        }
        private void idTreeView_DoubleClick(object sender, EventArgs e)
             
        {

            abririArchivo();
            }

        private void button1_Click(object sender, EventArgs e)
        {
            guardarArchivo();
        }

        private void Tree_View_Load(object sender, EventArgs e)
        {

        }
    }

}

