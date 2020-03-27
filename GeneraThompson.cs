using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;

namespace Proyecto1
{
    class AFN
    {
        static ArrayList listaTokken = new ArrayList();
        static ArrayList listaExxpresion = new ArrayList();
        public static List<Image> ImageRetu = new List<Image>();
        List<TK_Expresion> recorrido;
        List<State> states;
        public static int imagesCount = 0;
        private string[] Reservadas = { "conj", "año", "mes", "documento", "nombre" };
        List<string> agregar;
        List<Transition> agregarTransicion;
        List<Image> listaImagenTransformada;
        private string paths;
        private int contError = 0;
        private int contToken = 1;
        List<Image> listaImagenTablas;
        
        
        //llena expresiones del analizador lexico
        public void verExpresiones(List<string> vecto)
        {

            // vector[i].Equals(".")
            recorrido = new List<TK_Expresion>();
            agregar = new List<string>();
            Form1 form1 = new Form1();
            Free free = new Free();
            // string[] vector2 = free.vecto2.ToArray();
            string[] vector = vecto.ToArray();
            Console.Write("ddddd" + vector.Length);
            for (int i = 0; i < vector.Length; i++)
            {

                if (vector[i].Equals(".") || vector[i].Equals("|"))
                {
                    Console.WriteLine(vector[i]);
                    recorrido.Add(new TK_Expresion(vector[i], vector[i], false, false, true));
                }
                else if (vector[i].Equals("*") || vector[i].Equals("+") || vector[i].Equals("?"))
                {
                    Console.WriteLine(vector[i]);
                    //Console.WriteLine(vector[k
                    //Console.WriteLine(vector[k]);
                    recorrido.Add(new TK_Expresion(vector[i], vector[i], false, false, false));
                }
                else
                {
                    Console.WriteLine(vector[i]);
                    recorrido.Add(new TK_Expresion(vector[i], vector[i], true, false, false));
                    Boolean genrado = false;

                    if (agregar.Count() > 0)
                    {
                        for (int j = 0; j < agregar.Count(); j++)
                        {
                            //for (int i = 0; i < Reservadas.Length; i++)
                            //{
                            //    nombre = Reservadas[i];
                            //    if (token.ToLower().Equals(nombre))
                            //    {
                            //        i = Reservadas.Length + 1;
                            //        uno = true;
                            //    }
                            //}
                            if (vector[i].Equals(agregar[j]))
                            {
                                //if (uno == true)
                                //{
                                //    AnalizarToken(token, fila, columna, "RESERVADA");
                                //}
                                //else if (uno == false)
                                //{
                                //    Errores(token, fila, columna);
                                //}
                                genrado = true;
                                break;
                            }
                        }
                        if (!genrado)
                        {
                            //{
                            //    Errores(token, fila, columna);
                            //}
                            agregar.Add(vector[i]);
                        }
                    }
                    else
                    {
                        agregar.Add(vector[i]);
                    }

                }
            }

        }
        //private void LlenarTokens()
        //{
        //    string nombre = "";
        //    string comparar = "";
        //    string tipo = "";
        //    foreach (Token i in listaToken)
        //    {
        //        nombre = i.Lexema;
        //        tipo = i.Tipo;
        //        FindMyText(nombre, tipo);
        //        comparar = nombre;
        //    }
        //}
        public void generarAFN(int cuantas)
        {
            for (int i = 0; i < cuantas; i++)
            {
                int StCount = 0;
                states = new List<State>();
                agregarTransicion = new List<Transition>();
                for (int k = 0; k < recorrido.Count() - 1; k++)
                {
                    //private Boolean IsSC(int code)
                    //{
                    //    if (code == 126 | code == 44 | code == 59)
                    //    {
                   
                                {
                                    if (recorrido[k].getToken().Equals("|") && !recorrido[k].isValidate())
                                    {
                                        for (int cont = 0; cont < 6; cont++)
                                        {
                                            states.Add(new State(StCount));
                                            StCount++;
                                        }
                                        //if (transition.character.Contains("{") && transition.character.Contains("}") && !transition.character.Contains("\""))
                                        //{
                                        agregarTransicion.Add(new Transition(states[StCount - 6], states[StCount - 5], "ε"));
                                        agregarTransicion.Add(new Transition(states[StCount - 6], states[StCount - 4], "ε"));
                                        //Transicion transicion = new Transicion(estadoInicial, operando.Inicial, "epsilon");
                                        //Transicion inicial_final = new Transicion(estadoInicial, estadoFinal, "epsilon");
                                        agregarTransicion.Add(new Transition(states[StCount - 5], states[StCount - 3], recorrido[k + 1].getLexema()));
                                        agregarTransicion.Add(new Transition(states[StCount - 4], states[StCount - 2], recorrido[k + 2].getLexema()));
                                        agregarTransicion.Add(new Transition(states[StCount - 3], states[StCount - 1], "ε"));
                                        agregarTransicion.Add(new Transition(states[StCount - 2], states[StCount - 1], "ε"));
                                        recorrido[k].setValidate(true);
                                        recorrido[k].setInicial(states[StCount - 6]);
                                        recorrido[k].setFinal(states[StCount - 1]);
                                        regenerate(k, 2);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        k = -1;
                                    }
                                    else if (recorrido[k].getToken().Equals(".") && !recorrido[k].isValidate())
                                    {
                                        for (int cont = 0; cont < 3; cont++)
                                        {
                                            states.Add(new State(StCount));
                                            StCount++;
                                        }
                                        //if (transition.character.Contains("*") && transition.character.Contains("+") && !transition.character.Contains("\""))
                                        //{
                                        agregarTransicion.Add(new Transition(states[StCount - 3], states[StCount - 2], recorrido[k + 1].getLexema()));
                                        agregarTransicion.Add(new Transition(states[StCount - 2], states[StCount - 1], recorrido[k + 2].getLexema()));
                                        recorrido[k].setValidate(true);
                                        recorrido[k].setInicial(states[StCount - 3]);
                                        recorrido[k].setFinal(states[StCount - 1]);
                                        regenerate(k, 2);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        k = -1;
                                    }
                                }
                                else if (recorrido[k + 1].isTerminal() && recorrido[k + 2].isValidate())
                                {
                                    if (recorrido[k].getToken().Equals("|") && !recorrido[k].isValidate())
                                    {
                                        for (int cont = 0; cont < 4; cont++)
                                        {
                                            states.Add(new State(StCount));
                                            StCount++;
                                        }
                                        //if (transition.character.Contains("|") && transition.character.Contains(".") && !transition.character.Contains("\""))
                                        //{
                                        agregarTransicion.Add(new Transition(states[StCount - 4], recorrido[k + 2].getInicial(), "ε"));
                                        agregarTransicion.Add(new Transition(states[StCount - 4], states[StCount - 3], "ε"));
                                        //Transicion transicion = new Transicion(estadoInicial, operando.Inicial, "epsilon");
                                        //Transicion inicial_final = new Transicion(estadoInicial, estadoFinal, "epsilon");
                                        agregarTransicion.Add(new Transition(states[StCount - 3], states[StCount - 2], recorrido[k + 1].getLexema()));
                                        agregarTransicion.Add(new Transition(states[StCount - 2], states[StCount - 1], "ε"));
                                        agregarTransicion.Add(new Transition(recorrido[k + 2].getFinal(), states[StCount - 1], "ε"));
                                        recorrido[k].setValidate(true);
                                        recorrido[k].setInicial(states[StCount - 4]);
                                        recorrido[k].setFinal(states[StCount - 1]);
                                        //Transicion transicion = new Transicion(estadoInicial, operando.Inicial, "epsilon");
                                        //estadoInicial.SetTransicionIzq(transicion);
                                        regenerate(k, 2);
                                        //Transicion transicion = new Transicion(estadoInicial, operando.Inicial, "epsilon");
                                        //estadoInicial.SetTransicionIzq(transicion);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        k = -1;
                                    }
                                    else if (recorrido[k].getToken().Equals(".") && !recorrido[k].isValidate())
                                    {
                                        states.Add(new State(StCount));
                                        StCount++;
                                        agregarTransicion.Add(new Transition(states[StCount - 1], recorrido[k + 2].getInicial(), recorrido[k + 1].getLexema()));
                                        recorrido[k].setValidate(true);
                                        recorrido[k].setInicial(states[StCount - 1]);
                                        recorrido[k].setFinal(recorrido[k + 2].getFinal());
                                        regenerate(k, 2);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        k = -1;
                                    }
                                }
                                else if (recorrido[k + 1].isValidate() && recorrido[k + 2].isTerminal())
                                {
                                    if (recorrido[k].getToken().Equals("|") && !recorrido[k].isValidate())
                                    {
                                        for (int cont = 0; cont < 4; cont++)
                                        {
                                            states.Add(new State(StCount));
                                            StCount++;
                                        }
                                        //if (transition.character.Contains("or") && transition.character.Contains("and") && !transition.character.Contains("\""))
                                        //{
                                        agregarTransicion.Add(new Transition(states[StCount - 4], recorrido[k + 1].getInicial(), "ε"));
                                        agregarTransicion.Add(new Transition(states[StCount - 4], states[StCount - 3], "ε"));
                                        //Transicion transicion = new Transicion(estadoInicial, operando.Inicial, "epsilon");
                                        //Transicion inicial_final = new Transicion(estadoInicial, estadoFinal, "epsilon");
                                        agregarTransicion.Add(new Transition(states[StCount - 3], states[StCount - 2], recorrido[k + 2].getLexema()));
                                        agregarTransicion.Add(new Transition(states[StCount - 2], states[StCount - 1], "ε"));
                                        agregarTransicion.Add(new Transition(recorrido[k + 1].getFinal(), states[StCount - 1], "ε"));
                                        recorrido[k].setValidate(true);
                                        recorrido[k].setInicial(states[StCount - 4]);
                                        recorrido[k].setFinal(states[StCount - 1]);
                                        regenerate(k, 2);
                                        //Transicion transicion = new Transicion(estadoInicial, operando.Inicial, "epsilon");
                                        //estadoInicial.SetTransicionIzq(transicion);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        k = -1;
                                    }
                                    else if (recorrido[k].getToken().Equals(".") && !recorrido[k].isValidate())
                                    {
                                        states.Add(new State(StCount));
                                        StCount++;
                                        agregarTransicion.Add(new Transition(recorrido[k + 1].getFinal(), states[StCount - 1], recorrido[k + 2].getLexema()));
                                        recorrido[k].setValidate(true);
                                        recorrido[k].setInicial(recorrido[k + 1].getInicial());
                                        recorrido[k].setFinal(states[StCount - 1]);
                                        regenerate(k, 2);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        k = -1;
                                    }
                                }
                                else if (recorrido[k + 1].isValidate() && recorrido[k + 2].isValidate())
                                {
                                    if (recorrido[k].getToken().Equals("|") && !recorrido[k].isValidate())
                                    {
                                        for (int cont = 0; cont < 2; cont++)
                                        {
                                            states.Add(new State(StCount));
                                            StCount++;
                                        }
                                        //if (transition.character.Contains("?") && transition.character.Contains(".") && !transition.character.Contains("\""))
                                        //{
                                        agregarTransicion.Add(new Transition(states[StCount - 2], recorrido[k + 1].getInicial(), "ε"));
                                        agregarTransicion.Add(new Transition(states[StCount - 2], recorrido[k + 2].getInicial(), "ε"));
                                        agregarTransicion.Add(new Transition(recorrido[k + 1].getFinal(), states[StCount - 1], "ε"));
                                        //Transicion inicial_final = new Transicion(estadoInicial, estadoFinal, "epsilon");
                                        agregarTransicion.Add(new Transition(recorrido[k + 2].getFinal(), states[StCount - 1], "ε"));
                                        recorrido[k].setValidate(true);
                                        recorrido[k].setInicial(states[StCount - 2]);
                                        recorrido[k].setFinal(states[StCount - 1]);
                                        regenerate(k, 2);
                                        //Transicion transicion = new Transicion(estadoInicial, operando.Inicial, "epsilon");
                                        //estadoInicial.SetTransicionIzq(transicion);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        k = -1;
                                    }
                                    else if (recorrido[k].getToken().Equals(".") && !recorrido[k].isValidate())
                                    {
                                        agregarTransicion.Add(new Transition(recorrido[k + 1].getFinal(), recorrido[k + 2].getInicial(), "ε"));
                                        recorrido[k].setValidate(true);
                                        recorrido[k].setInicial(recorrido[k + 1].getInicial());
                                        recorrido[k].setFinal(recorrido[k + 2].getFinal());
                                        regenerate(k, 2);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        recorrido.RemoveAt(recorrido.Count() - 1);
                                        k = -1;
                                    }

                                }

                            }
                        }
                        else
								}
							if (recorrido[k].getToken().Equals("?"))
                                {
                                    for (int cont = 0; cont < 6; cont++)
                                    {
                                        states.Add(new State(StCount));
                                        StCount++;
                                    }

                                    agregarTransicion.Add(new Transition(states[StCount - 6], states[StCount - 5], "ε"));
                                    agregarTransicion.Add(new Transition(states[StCount - 6], states[StCount - 4], "ε"));
                                    agregarTransicion.Add(new Transition(states[StCount - 5], states[StCount - 3], recorrido[k + 1].getLexema()));
                                    agregarTransicion.Add(new Transition(states[StCount - 4], states[StCount - 2], "ε"));
                                    //Transicion transicion = new Transicion(estadoInicial, operando.Inicial, "epsilon");
                                    //Transicion transicion = new Transicion(estadoInicial, operando.Inicial, "epsilon");
                                    //estadoInicial.SetTransicionIzq(transicion);
                                    //Transicion inicial_final = new Transicion(estadoInicial, estadoFinal, "epsilon");
                                    agregarTransicion.Add(new Transition(states[StCount - 3], states[StCount - 1], "ε"));
                                    agregarTransicion.Add(new Transition(states[StCount - 2], states[StCount - 1], "ε"));
                                    //Transicion cambioIzq = operando1.GetEstadoInicial().GetTransicionIzq();
                                    //Transicion cambioDer = operando1.GetEstadoInicial().GetTransicionDer();
                                    recorrido[k].setValidate(true);
                                    recorrido[k].setInicial(states[StCount - 6]);
                                    recorrido[k].setFinal(states[StCount - 1]);
                                    regenerate(k, 1);
                                    recorrido.RemoveAt(recorrido.Count() - 1);
                                    k = -1;
                                }
                            }
                            else if (!recorrido[k].isTerminal() && recorrido[k + 1].isValidate())
                            {
                                if (recorrido[k].getToken().Equals("*"))
                                {
                                    for (int cont = 0; cont < 2; cont++)
                                    {
                                        states.Add(new State(StCount));
                                        StCount++;
                                    }
                                    agregarTransicion.Add(new Transition(states[StCount - 2], recorrido[k + 1].getInicial(), "ε"));
                                    agregarTransicion.Add(new Transition(states[StCount - 2], states[StCount - 1], "ε"));
                                    //Transicion transicion = new Transicion(estadoInicial, operando.Inicial, "epsilon");
                                    //estadoInicial.SetTransicionIzq(transicion);
                                    agregarTransicion.Add(new Transition(recorrido[k + 1].getFinal(), states[StCount - 1], "ε"));
                                    agregarTransicion.Add(new Transition(recorrido[k + 1].getFinal(), recorrido[k + 1].getInicial(), "ε"));
                                    //Transicion cambioIzq = operando1.GetEstadoInicial().GetTransicionIzq();
                                    //Transicion cambioDer = operando1.GetEstadoInicial().GetTransicionDer();
                                    recorrido[k].setValidate(true);
                                    recorrido[k].setInicial(states[StCount - 2]);
                                    recorrido[k].setFinal(states[StCount - 1]);
                                    regenerate(k, 1);
                                    recorrido.RemoveAt(recorrido.Count() - 1);
                                    k = -1;
                                }
                                else if (recorrido[k].getToken().Equals("+"))
                                {
                                    for (int cont = 0; cont < 2; cont++)
                                    {
                                        states.Add(new State(StCount));
                                        StCount++;
                                    }
                                    agregarTransicion.Add(new Transition(states[StCount - 2], recorrido[k + 1].getInicial(), "ε"));
                                    agregarTransicion.Add(new Transition(recorrido[k + 1].getFinal(), states[StCount - 1], "ε"));
                                    agregarTransicion.Add(new Transition(recorrido[k + 1].getFinal(), recorrido[k + 1].getInicial(), "ε"));
                                    //Transicion cambioIzq = operando1.GetEstadoInicial().GetTransicionIzq();
                                    //Transicion cambioDer = operando1.GetEstadoInicial().GetTransicionDer();
                                    recorrido[k].setValidate(true);
                                    recorrido[k].setInicial(states[StCount - 2]);
                                    recorrido[k].setFinal(states[StCount - 1]);
                                    regenerate(k, 1);
                                    recorrido.RemoveAt(recorrido.Count() - 1);
                                    k = -1;
                                }
                                else if (recorrido[k].getToken().Equals("?"))
                                {
                                    for (int cont = 0; cont < 4; cont++)
                                    {
                                        states.Add(new State(StCount));
                                        StCount++;
                                    }
                                    agregarTransicion.Add(new Transition(states[StCount - 4], recorrido[k + 1].getInicial(), "ε"));
                                    //Transicion transicion = new Transicion(estadoInicial, operando.Inicial, "epsilon");
                                    //estadoInicial.SetTransicionIzq(transicion);
                                    agregarTransicion.Add(new Transition(states[StCount - 4], states[StCount - 3], "ε"));
                                    agregarTransicion.Add(new Transition(states[StCount - 3], states[StCount - 2], "ε"));
                                    agregarTransicion.Add(new Transition(states[StCount - 2], states[StCount - 1], "ε"));
                                    //Transicion transicion = new Transicion(estadoInicial, operando.Inicial, "epsilon");
                                    //Transicion inicial_final = new Transicion(estadoInicial, estadoFinal, "epsilon");
                                    agregarTransicion.Add(new Transition(recorrido[k + 1].getFinal(), states[StCount - 1], "ε"));
                                    //Transicion cambioIzq = operando1.GetEstadoInicial().GetTransicionIzq();
                                    //Transicion cambioDer = operando1.GetEstadoInicial().GetTransicionDer();
                                    recorrido[k].setValidate(true);
                                    recorrido[k].setInicial(states[StCount - 4]);
                                    recorrido[k].setFinal(states[StCount - 1]);
                                    regenerate(k, 1);
                                    recorrido.RemoveAt(recorrido.Count() - 1);
                                    k = -1;
                                }
                            }
                        }
                        if (recorrido.Count() == 1)
                        {
                            recorrido[0].getInicial().setStateInicial(true);
                            recorrido[0].getFinal().setStateAceptacion(true);
                        }
                    }
                    string codigo = generateBody();
                    GenerateImages(codigo, "Thompson" + imagesCount);
                    
                
            }
        }
        public void generaGrafo(string nombre)
        {
            int contador=1;
            nodo inicial;
            string exeFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            string replacement = Regex.Replace(nombre, @"\t|\n|\r", "");
            string bodyGraph = "";
            System.IO.StreamWriter file = new System.IO.StreamWriter(exeFolder + "\\" + replacement + ".dot");
            try
            {

                int cont = 0;
                bodyGraph += "digraph G{\n"
                    + "rankdir=LR\n"
                    + "node[shape=box];\n";
                cont = contador;
               // nodo n = inicial;
                // while (cont != 0)
                //  {
                /*  cont--;
                  if (cont == 0) bodyGraph += "\" " + n.Num + " \";\n\n";
                  else bodyGraph += "\" " + n.Num + " \" ->";
                  n = n.primero;*/
                //  cuerpo = "\""+ estados+ "\"" + " -> " + "\"" + "."+ "\"" + "[label="+ "\"" + "."+ "\"" + "];";
                //bodyGraph += body;
                //}
                bodyGraph += "\n\n}\n\n";


                file.WriteLine(bodyGraph);
                file.Close();

                ProcessStartInfo startInfo = new ProcessStartInfo("dot.exe");
                startInfo.Arguments = "-Tpng " + replacement + ".dot -o " + replacement + ".png";
                Process.Start(startInfo);

            }
            catch (Exception e)
            {
                MessageBox.Show("Error grafo: " + e.Message);
                file.Close();
                //throw;
            }
        }
        public void GenerateImages(string body, string nombre)
        {

            string path = Application.StartupPath;
            if (File.Exists(path + "\\" + nombre + ".dot"))
            {
                File.Delete(path + "\\" + nombre + ".dot");
            }
            File.WriteAllText(path + "\\" + nombre + ".dot", body);
            var comand = "dot -Tjpg \"" + path + "\\" + nombre + ".dot\" -o \"" + path + "\\" + nombre + ".jpg\" ";
            var procStartInfo = new ProcessStartInfo("cmd", "/C" + comand);
            var proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            proc.WaitForExit();

            string var = path + "\\" + nombre + ".jpg";

            //file.WriteLine(bodyGraph);
            //file.Close();
            if (File.Exists(var))
            {

                string pathImg = var;

                Image img = Image.FromFile(var.Replace("\"", ""));

                ImageRetu.Add(img);
                //ProcessStartInfo startInfo = new ProcessStartInfo("dot.exe");
                //startInfo.Arguments = "-Tpng " + replacement + ".dot -o " + replacement + ".png";
                //Process.Start(startInfo);
            }
            else
            {
                Console.WriteLine("Error");
            }

        }
        public void Analyzer_Lexico()
        {
            //Tokens = new ArrayList();
            //Mistakes = new ArrayList();
            //ReservedWords = new String[1];

            //Lexema = "";
            //Indice = 0;
            //Estado = 0;

            //Load_Words_Reserved();
        }
        public void GenerarTransiciones()
        {
            for (int i = 0; i < agregarTransicion.Count(); i++)
            {
                agregarTransicion[i].getInicial().getListTrans().Add(agregarTransicion[i]);
            }

        }
        public List<Image> getImageRetu()
        {
            return ImageRetu;
        }

       


        public List<Image> getListaImagenTransformada()
        {
            return listaImagenTransformada;
        }

        public void crearDot(string[,] datos)
        {

            string nombre = "";
            string aux = "";

            try
            {
                for (int i = 0; i < 500; i++)
                {
                    if (datos[9, i] != null)
                    {
                        aux = datos[0, i];
                        char[] espacios = datos[0, i].ToCharArray();
                        for (int v = 0; v < espacios.Length; v++)
                        {
                            if (Char.IsLetter(espacios[v]))
                            {
                                nombre += espacios[v];
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
               // System.out.println(ex);
            }
}
                public string generateBody()
        {
            string body = "digraph G { \n rankdir = LR;\n";

            for (int i = 0; i < states.Count(); i++)
            {
                //String Gcode = "digraph trans{\nGraph[label = \"" + name + " Transitions\"];\n" +
                //"\tnodeT [shape=plaintext, label=<\n\t<table border='1'>\n" + header + codeStates + "\n\t</table>>];\n\n}";
                body = body + "nodo" + states[i].getNumero() + "[label = \"" + states[i].getNumero() + "\"];\n";
                if (states[i].isStateAceptacion())
                {
                    body = body + "nodo" + states[i].getNumero() + "[style=filled fillcolor = gray];\n ";
                }
                else if (states[i].isStateAceptacion())
                {
                    body = body + "nodo" + states[i].getNumero() + "[style=filled fillcolor = gray, shape = doublecircle];\n ";
                }
            }

            for (int i = 0; i < agregarTransicion.Count(); i++)
            {
                //String Gcode = "digraph trans{\nGraph[label = \"" + name + " Transitions\"];\n" +
                //"\tnodeT [shape=plaintext, label=<\n\t<table border='1'>\n" + header + codeStates + "\n\t</table>>];\n\n}";
                body = body + "nodo" + agregarTransicion[i].getInicial().getNumero() + "-> nodo" + agregarTransicion[i].getFinal().getNumero() + "[label=\"" + agregarTransicion[i].getSimbolo() + "\"];\n";
            }
            body = body + "}";
            return body;
        }

        //private Boolean IsLetter(int code)
        //{
        //    if ((code >= 65 && code <= 90) | (code >= 97 && code <= 122)
        //            | (code >= 160 && code <= 165) || code == 130 || code == 181
        //            || code == 144 || code == 214 || code == 224 || code == 233)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public List<Image> getListaImagenTablas()
        {
            return listaImagenTablas;
        }


        
       
    }
}
