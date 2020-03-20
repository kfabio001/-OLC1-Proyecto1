using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1
{
    public partial class Form1 : Form
    {
        public string[] ruta = { "ruta" };
        List<string> rutas = new List<string>();
        public List<string> root = new List<string>();
        public int contadorRutas;
        string auxlex;
        private string[] Reservadas = { "conj", "año", "mes", "documento",  "nombre" };
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private string texto;
        static ArrayList listaToken = new ArrayList();
        static ArrayList listaExpresion = new ArrayList();
        static ArrayList listaerror = new ArrayList();
        private string path;
        private int contError = 0;
        private int contToken = 1;
        int no = 1;
        int noExp = 1;

         grafo grap;
         nodo mensajeNo;
        nodo inicial, final;
        int contador;

        public static string cuerpo="";
        public static int estados;
        //string [] vector;
        List<string> vecto = new List<string>();
        //vector= new string[100];
        int cont = 0;


        public Form1()
        {
            InitializeComponent();
        }
        
        private void Analizador(string texto)
        {
            string token = "";
            int columna = 0;
            int fila = 1;
            string palabra = "";
            string tokenPunto = "";
            char letra;
            int contadorPalabra = 0;
            int comentarioLinea = 0;
            int comentarioMulti1 = 0;
            int comentarioMulti2 = 0;
            int conjunto = 0;
            int defConjunto = 0;
            int defConjunto2 = 0;
            int expresion = 0;
            int expresion2 = 0;
            int estado = 0;
            int cantPunto = 0;
            int estadoMover = 0;
            char comillas = '"';

            string exp = "";
            



            for (estado = 0; estado < texto.Length; estado++)
            {
                letra = texto[estado];

                if (cantPunto > 0)
                {
                    if (letra == '”')
                    {
                        letra = '"';

                    }
                    if (letra != ':')
                    {
                        if (cantPunto == 1)
                        {
                            estadoMover = 1;
                            token = tokenPunto;
                            estado = estado - 1;
                            tokenPunto = "";
                            cantPunto = 0;
                        }
                        else if (letra == '=')
                        {
                            if (cantPunto == 2)
                            {
                                token = tokenPunto + letra;
                                estadoMover = 1;
                                estado = estado - 1;

                                cantPunto = 0;
                            }
                        }
                        else if (cantPunto >= 2)
                        {
                            int i = 0;
                            columna++;
                            while (i < tokenPunto.Length)
                            {
                                AnalizarToken(":", fila, columna++, "DOS PUNTOS");
                                i++;
                            }
                            columna = columna - 1;
                            tokenPunto = "";
                            token = "";
                            cantPunto = 0;
                        }
                    }
                }

                switch (estadoMover)
                {
                    case 0:
                        switch (letra)
                        {
                            case ' ':
                            case '\r':
                            case '\t':
                            case '\f':
                                estadoMover = 0;
                                break;
                            case '\n':
                                fila++;
                                columna = 0;
                                comentarioLinea = 0;
                                estadoMover = 0;
                                break;

                            case '/':
                                token += letra;
                                estadoMover = 1;
                                comentarioLinea = 1;
                                estado = estado - 1;
                                break;
                            case '-':
                                
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case '.':
                                exp += letra;
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case '|':
                                exp += letra;
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case '*':
                                exp += letra;
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case '+':
                                exp += letra;
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case '?':
                                exp += letra;
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;

                            case '{':
                                exp += letra;
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case '}':
                                exp += letra;
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case '>':
                                
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                             case '<':

                                 token += letra;
                                 estadoMover = 1;
                                 estado = estado - 1;
                                 break;
                          
                            case '!':

                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case ':':
                                tokenPunto += letra;
                                cantPunto++;
                                estadoMover = 0;
                              
                                break;
                            case ';':
                                
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case '=':
                                if (tokenPunto.Equals("::"))
                                {
                                    tokenPunto = "";
                                }
                                else
                                {
                                    estadoMover = 8;
                                    estado = estado - 1;
                                }
                                break;
                            case '"':
                                token += letra;
                                estadoMover = 4;
                                break;
                            case '”':
                                letra = comillas;
                                token += letra;
                                estadoMover = 4;
                                break;
                            default:
                                if (Char.IsNumber(letra))
                                {
                                    token += letra;
                                    estadoMover = 3;
                                }
                                else if (Char.IsLetter(letra))
                                {
                                    estadoMover = 3;
                                    estado = estado - 1;
                                }
                                else
                                {
                                    estadoMover = 8;
                                    estado = estado - 1;
                                }
                                break;
                        }
                        break;
                    case 1:
                    
                        if (token.Equals("/"))
                        {
                            columna++;
                            AnalizarToken(token, fila, columna, "DIAGONAL");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("-"))
                        {
                            expresion2 = 1;
                            defConjunto2 = 1;
                            columna++;
                            AnalizarToken(token, fila, columna, "GUION");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals(":"))
                        {
                            conjunto = 1;
                            columna++;
                            AnalizarToken(token, fila, columna, "DOS PUNTOS");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("::="))
                        {
                            string dospuntos = "=";
                            columna++;
                            AnalizarToken(token, fila, columna, "SIGNO IGUAL");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals(";"))
                        {
                            
                            defConjunto = 0;
                            defConjunto2 = 0;
                            columna++;
                            if ((expresion == 1)&&(expresion2==1)) {
                                AnalizarToken(exp, fila, columna, "EXPRESION");
                                AnalizarExpresion(exp, fila, columna, "EXPRESION");
                                noExp++;
                                //AnalizarExpresion(noExp, exp, fila, columna, "EXPRESION");
                                //Console.WriteLine(noExp + exp);
                            }
                            AnalizarToken(token, fila, columna, "PUNTO Y COMA");
                            expresion = 0;
                            expresion2 = 0;
                            exp = "";
                            token = "";
                            estadoMover = 0;
                            exp = "";
                        }
                        else if (token.Equals(">"))
                        {
                            comentarioMulti1 = 0;
                            comentarioMulti2 = 0;
                            defConjunto = 1;
                            expresion = 1;
                            columna++;
                            AnalizarToken(token, fila, columna, "MAYOR QUE");
                            exp = "";
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("<"))
                        {
                            comentarioMulti1 = 1;
                            columna++;
                            AnalizarToken(token, fila, columna, "MENOR QUE");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("!"))
                        {
                            comentarioMulti2 = 1;
                            columna++;
                            AnalizarToken(token, fila, columna, "ADMIRACION");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("."))
                        {
                            columna++;
                            AnalizarToken(token, fila, columna, "CONCATENACION");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("*"))
                        {
                            columna++;
                            AnalizarToken(token, fila, columna, "CERRADURA *");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("+"))
                        {
                            columna++;
                            AnalizarToken(token, fila, columna, "CERRADURA +");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("|"))
                        {
                            columna++;
                            AnalizarToken(token, fila, columna, "DISYUNCION");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("?"))
                        {
                            columna++;
                            AnalizarToken(token, fila, columna, "CERRADURA ?");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("{"))
                        {
                            defConjunto = 0;
                            conjunto = 1;
                            columna++;
                            AnalizarToken(token, fila, columna, "CORCHETE IZQUIERDO");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("}"))
                        {
                            conjunto = 0;
                            columna++;
                            AnalizarToken(token, fila, columna, "CORCHETE DERECHO");
                            token = "";
                            estadoMover = 0;
                        }

                        break;
                    case 2:
                        columna++;
                        AnalizarToken(token, fila, columna, "DIGITO");
                        token = "";
                        estadoMover = 0;
                        break;
                    case 3:
                        if (Char.IsLetterOrDigit(letra) || Char.IsSymbol(letra) || letra == ' ' || letra == '_' || letra == ',' || letra == '"' || letra == '”')
                        {
                            exp += letra;
                            token += letra;
                            columna++;
                        }
                        else
                        {
                            if (token.ToLower().Equals("conj"))
                            {
                                palabra = token;
                                contadorPalabra = 0;
                            }
                            else 
                            {
                                palabra = token;
                                contadorPalabra = 1;
                            }
                            if (contadorPalabra == 0)
                            {
                                PalabrasReservadas(token, fila, columna, estado);
                                token = "";
                                estado = estado - 1;
                                estadoMover = 0;
                            }
                            else if (contadorPalabra == 1)
                            {
                                estadoMover = 5;
                            }
                            else if (contadorPalabra == 3)
                            {
                                estado = estado - 1;
                                estadoMover = 5;
                                palabra = "";
                            }
                        }
                        break;
                    case 4:
                        if (letra == '”')
                        {
                            letra = '"';
                        }
                        if (letra != '"')
                        {

                            token += letra;
                        }
                        else if (letra == '"')
                        {
                            estado = estado - 1;
                            estadoMover = 5;
                        }
                        break;
                    case 5:
                        if (contadorPalabra == 0)
                        {
                             columna++;
                            exp += token;
                            exp += "\"";
                            AnalizarToken(token + "\"", fila, columna, "CADENA");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (contadorPalabra == 1)
                        {
                            if (comentarioLinea == 1)
                            {
                                AnalizarToken(token, fila, columna, "COMENTARIO");
                                contadorPalabra = 0;
                                expresion = 0;
                                token = "";
                                palabra = "";
                                estado = estado - 2;
                                estadoMover = 0;
                            } else if ((comentarioMulti1 == 1) && (comentarioMulti2 == 1)) {
                                AnalizarToken(token, fila, columna, "COMENTARIO MULTI");
                                contadorPalabra = 0;
                                expresion = 0;
                                token = "";
                                palabra = "";
                                estado = estado - 2;
                                estadoMover = 0;
                            } else if (conjunto == 1)
                            {
                                AnalizarToken(token, fila, columna, "CONJUNTO");
                                contadorPalabra = 0;
                                conjunto = 0;
                                token = "";
                                palabra = "";
                                estado = estado - 2;
                                estadoMover = 0;

                            } else if ((defConjunto == 1) && (defConjunto2 == 1))
                            {
                                AnalizarToken(token, fila, columna, "DEFINICION CONJUNTO");
                                contadorPalabra = 0;
                                expresion = 0;
                                defConjunto = 0;
                                token = "";
                                palabra = "";
                                estado = estado - 2;
                                estadoMover = 0;
                            }
                            else { 
                            AnalizarToken(token, fila, columna, "IDENTIFICADOR");
                            contadorPalabra = 0;
                                expresion = 0;
                                expresion2 = 1;
                            token = "";
                            palabra = "";
                            estado = estado - 2;
                            estadoMover = 0;
                            }

                        }
                       
                        break;
                                  
                    case 8:
                        columna++;
                        Errores(token += letra, fila, columna);
                        contError++;
                        token = "";
                        estadoMover = 0;
                        break;
                    
                }
            }

        }
        public void AnalizarExpresion( string token, int fila, int columna, string tipo)
        {
            if (tipo.Equals("EXPRESION"))
            {
                listaExpresion.Add(new Expresiones(noExp, 1, token, tipo, fila, columna));
 
            }
                Console.WriteLine("EXP " +" "+ noExp.ToString() + " " + 1 + " " + token + " " + tipo + " " + fila + " " + columna);
        }
        public void Analizador2()
        {
            int estadoMover = 0 ;
            char letra;
            string token = "";
            int columna = 0;
            int fila = 1;
            string palabra = "";
            string nombre = "";
            string comparar = "";
            string tipo = "";
            foreach (Expresiones i in listaExpresion)
            {
                nombre = i.Lexema;
                tipo = i.Tipo;
                switch (estadoMover)
                {


                }
            }           
        }
        private void PalabrasReservadas(string token, int fila, int columna, int inicio)
        {
            string nombre = "";
            bool uno = false;
            for (int i = 0; i < Reservadas.Length; i++)
            {
                nombre = Reservadas[i];
                if (token.ToLower().Equals(nombre))
                {
                    i = Reservadas.Length + 1;
                    uno = true;
                }
            }
            if (uno == true)
            {
                AnalizarToken(token, fila, columna, "RESERVADA");
            }
            else if (uno == false)
            {
                Errores(token, fila, columna);
            }
        }
        public void obtenerRuta()
        {

            var ods = new Token();
            string[] arrayElementos = rutas.ToArray();
            for (int i = 0; i < contadorRutas; i++)
            {


                char[] caracteres = arrayElementos[i].ToCharArray();


                for (int h = 0; h < caracteres.Length; h++)
                {


                    // if (Char.IsLetter(caracteres[h]))
                    if (caracteres[h] != '"')
                    {
                        auxlex += caracteres[h];

                    }
                }

                root.Add(auxlex);

                auxlex = "";
            }
        }
        public void enviarExpresion()
        {
            foreach (Expresiones i in listaExpresion)
            {
                string enviar = i.Lexema;
                Analizador3(enviar);
               // Console.WriteLine(enviar);
            }

        }
        public void recibeExpresiones()
        {
            string[] vector = vecto.ToArray();
            string aux,aux2,aux3,aux4,aux5,aux6;
            int estados = 0;
            int estadoIni=0;
            int estadoFin = 1;
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i] != ".") { 
                string[] vector2 = vecto.ToArray();
                }
            }
                for (int i = 0; i < vector.Length; i++)
            {
                Console.WriteLine(vector[i] + i);
                if (vector[i].Equals("."))
                {
                    estados++;
                    estadoIni = estadoFin;
                    estadoFin = estadoFin + 2;
                    Console.WriteLine(estadoIni+" ini"+estadoFin+" fin");
                    aux = "\"" + estadoIni + "\"" + " -> " + "\"" + (estados + 1) + "\"" + "[label=" + "\"" + vector[i + 1] + "\"" + "];\n\n";
                    estados++;
                    aux2 = "\"" + (estados) + "\"" + " -> " + "\"" + (estadoFin) + "\"" + "[label=" + "\"" + vector[i + 2] + "\"" + "];\n\n";
                    estados++;
                    Console.WriteLine(aux);
                    Console.WriteLine(aux2);
                    cuerpo += aux;
                    cuerpo += aux2;
                    aux = "";
                    aux2 = "";
                }
                else if (vector[i].Equals("|"))
                {
                    estadoIni = estadoFin;
                    estadoFin = estadoFin + 3;
                    Console.WriteLine(estadoIni + " ini" + estadoFin + " fin");
                    //  estados++;
                    // aux = "\"" + (estados) + "\"" + " -> " + "\"" + (estados + 1) + "\"" + "[label=" + "\"" + vector[i + 1] + "\"" + "];\n\n";
                    aux = "\"" + (estadoIni) + "\"" + " -> " + "\"" + (estados + 1) + "\"" + "[label=" + "\"" + "e" + "\"" + "];\n\n";
                    estados++;
                    aux2 = "\"" + (estados) + "\"" + " -> " + "\"" + (estados + 1) + "\"" + "[label=" + "\"" + vector[i + 1] + "\"" + "];\n\n";
                    estados++;
                    aux3 = "\"" + (estados) + "\"" + " -> " + "\"" + (estadoFin) + "\"" + "[label=" + "\"" + "e" + "\"" + "];\n\n";
                    estados++;
                    aux4 = "\"" + (estados - 3) + "\"" + " -> " + "\"" + (estados + 1) + "\"" + "[label=" + "\"" + "e" + "\"" + "];\n\n";
                    estados++;
                    aux5 = "\"" + (estados) + "\"" + " -> " + "\"" + (estados + 1) + "\"" + "[label=" + "\"" + vector[i + 2] + "\"" + "];\n\n";
                    estados++;
                    aux6 = "\"" + (estados ) + "\"" + " -> " + "\"" + (estados - 2) + "\"" + "[label=" + "\"" + "e" + "\"" + "];\n\n";
                   // estados++;
                    // aux6 = "\"" + (estados) + "\"" + " -> " + "\"" + (estados + 1) + "\"" + "[label=" + "\"" + "e" + "\"" + "];\n\n";
                    //cuerpo += aux;
                    Console.WriteLine(aux);
                    Console.WriteLine(aux2);
                    Console.WriteLine(aux3);
                    Console.WriteLine(aux4);
                    Console.WriteLine(aux5);
                    Console.WriteLine(aux6);
                    cuerpo += aux;
                    cuerpo += aux2;
                    cuerpo += aux3;
                    cuerpo += aux4;
                    cuerpo += aux5;
                    cuerpo += aux6;
                    Console.WriteLine(cuerpo);
                    aux = "";
                    aux2 = "";
                    aux3 = "";
                    aux4 = "";
                    aux5 = "";
                    aux6 = "";
                    

                }
                else if (vector[i].Equals("*"))
                {
                    estadoIni = estadoFin;
                    estadoFin = estadoFin + 3;
                    Console.WriteLine(estadoIni + " ini" + estadoFin + " fin");
                    //  estados++;
                    // aux = "\"" + (estados) + "\"" + " -> " + "\"" + (estados + 1) + "\"" + "[label=" + "\"" + vector[i + 1] + "\"" + "];\n\n";
                    aux = "\"" + (estadoIni) + "\"" + " -> " + "\"" + (estados + 1) + "\"" + "[label=" + "\"" + "e" + "\"" + "];\n\n";
                    estados++;
                    aux2 = "\"" + (estados) + "\"" + " -> " + "\"" + (estados + 1) + "\"" + "[label=" + "\"" + vector[i + 1] + "\"" + "];\n\n";
                    estados++;
                    aux3 = "\"" + (estados) + "\"" + " -> " + "\"" + (estadoFin) + "\"" + "[label=" + "\"" + "e" + "\"" + "];\n\n";
                    estados++;
                    aux4 = "\"" + (estados - 1) + "\"" + " -> " + "\"" + (estados - 2) + "\"" + "[label=" + "\"" + "e" + "\"" + "];\n\n";
                    estados++;
                    aux5 = "\"" + (estadoIni) + "\"" + " -> " + "\"" + (estadoFin) + "\"" + "[label=" + "\"" + vector[i + 2] + "\"" + "];\n\n";
                    //estados++;
                    //aux6 = "\"" + (estados) + "\"" + " -> " + "\"" + (estados - 2) + "\"" + "[label=" + "\"" + "e" + "\"" + "];\n\n";
                    // estados++;
                    // aux6 = "\"" + (estados) + "\"" + " -> " + "\"" + (estados + 1) + "\"" + "[label=" + "\"" + "e" + "\"" + "];\n\n";
                    //cuerpo += aux;
                    Console.WriteLine(aux);
                    Console.WriteLine(aux2);
                    Console.WriteLine(aux3);
                    Console.WriteLine(aux4);
                    Console.WriteLine(aux5);
                    //Console.WriteLine(aux6);
                    cuerpo += aux;
                    cuerpo += aux2;
                    cuerpo += aux3;
                    cuerpo += aux4;
                    cuerpo += aux5;
                   // cuerpo += aux6;
                    Console.WriteLine(cuerpo);
                    aux = "";
                    aux2 = "";
                    aux3 = "";
                    aux4 = "";
                    aux5 = "";
                   // aux6 = "";
                }
            }
        }
        public void Analizador3(string texto)
        {
            string token = "";
            int columna = 0;
            int fila = 1;
            string palabra = "";
            string tokenPunto = "";
            char letra;
            int contadorPalabra = 0;
            int comentarioLinea = 0;
            int comentarioMulti1 = 0;
            int comentarioMulti2 = 0;
            int conjunto = 0;
            int defConjunto = 0;
            int defConjunto2 = 0;
            int expresion = 0;
            int expresion2 = 0;
            int estado = 0;
            int cantPunto = 0;
            int estadoMover = 0;
            char comillas = '"';

            string exp = "";

            string aux = "";
            string hijo1 = "";
            string hijo2 = "";






            for (estado = 0; estado < texto.Length; estado++)
            {
                letra = texto[estado];

                

                switch (estadoMover)
                {
                    case 0:
                        switch (letra)
                        {
                            case ' ':
                            case '\r':
                            case '\t':
                            case '\f':
                                estadoMover = 0;
                                break;
                            case '\n':
                                fila++;
                                columna = 0;
                                comentarioLinea = 0;
                                estadoMover = 0;
                                break;
                  
                            case '.':
                                exp += letra;
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case '|':
                                exp += letra;
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case '*':
                                exp += letra;
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case '+':
                                exp += letra;
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case '?':
                                exp += letra;
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;

                            case '{':
                                exp += letra;
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case '}':
                                exp += letra;
                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            
                            case ';':

                                token += letra;
                                estadoMover = 1;
                                estado = estado - 1;
                                break;
                            case '=':
                                if (tokenPunto.Equals("::"))
                                {
                                    tokenPunto = "";
                                }
                                else
                                {
                                    estadoMover = 8;
                                    estado = estado - 1;
                                }
                                break;
                            case '"':
                               // token += letra;
                                estadoMover = 4;
                                break;
                            case '”':
                                letra = comillas;
                                //token += letra;
                                estadoMover = 4;
                                break;
                            default:
                                if (Char.IsNumber(letra))
                                {
                                    token += letra;
                                    estadoMover = 3;
                                }
                                else if (Char.IsLetter(letra))
                                {
                                    estadoMover = 3;
                                    estado = estado - 1;
                                }
                                else
                                {
                                    estadoMover = 8;
                                    estado = estado - 1;
                                }
                                break;
                        }
                        break;
                    case 1:

                         if (token.Equals(";"))
                        {
                            //cuerpo = "";
                            
                            columna++;
                            if ((expresion == 1) && (expresion2 == 1))
                            {
                                //AnalizarToken(exp, fila, columna, "EXPRESION");
                                //AnalizarExpresion(exp, fila, columna, "EXPRESION");
                               // noExp++;
                                //AnalizarExpresion(noExp, exp, fila, columna, "EXPRESION");
                                //Console.WriteLine(noExp + exp);
                            }
                            AnalizarToken(token, fila, columna, "PUNTO Y COMA");
                            expresion = 0;
                            expresion2 = 0;
                            exp = "";
                            token = "";
                            estadoMover = 0;
                            exp = "";
                        }//  cuerpo = "\""+ estados+ "\"" + " -> " + "\"" + "."+ "\"" + "[label="+ "\"" + "."+ "\"" + "];";

                        else if (token.Equals("."))
                        {
                            vecto.Add(token);
                            cont++;
                           // estados++;
                            //aux = "\""+ estados+ "\"" + " -> " + "\"" + (estados+1)+ "\"" + "[label="+ "\"" + hijo1+ "\""+ "];\n\n";
                          //  aux = "\"" + estados + "\"" + " -> " + "\"" + (estados + 1) + "\"" + "[label=" + "\"" ;

                           // cuerpo += aux;
                            
                           // aux = "";
                            columna++;
                           // AnalizarToken(token, fila, columna, "CONCATENACION");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("*"))
                        {
                            vecto.Add(token);
                            cont++;
                            columna++;
                           // AnalizarToken(token, fila, columna, "CERRADURA *");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("+"))
                        {
                            vecto.Add(token);
                            cont++;
                            columna++;
                            //AnalizarToken(token, fila, columna, "CERRADURA +");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("|"))
                        {
                            vecto.Add(token);
                            cont++;
                            columna++;
                            // AnalizarToken(token, fila, columna, "DISYUNCION");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("?"))
                        {
                            vecto.Add(token);
                            cont++;
                            columna++;
                            // AnalizarToken(token, fila, columna, "CERRADURA ?");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("{"))
                        {
                          //  vecto.Add(token);
                            cont++;
                            defConjunto = 0;
                            conjunto = 1;
                            columna++;
                            // AnalizarToken(token, fila, columna, "CORCHETE IZQUIERDO");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (token.Equals("}"))
                        {
                          //  vecto.Add(token);
                            cont++;
                            conjunto = 0;
                            columna++;
                            //  AnalizarToken(token, fila, columna, "CORCHETE DERECHO");
                            token = "";
                            estadoMover = 0;
                        }

                        break;
                    case 2:
                        columna++;
                        vecto.Add(token);
                        cont++;
                        //  AnalizarToken(token, fila, columna, "DIGITO");
                        token = "";
                        estadoMover = 0;
                        break;
                    case 3:
                        if (Char.IsLetterOrDigit(letra) || Char.IsSymbol(letra) || letra == ' ' || letra == '_' || letra == ',' || letra == '"' || letra == '”')
                        {
                            exp += letra;
                            token += letra;
                            columna++;
                        }
                        else
                        {
                            if (token.ToLower().Equals("conj"))
                            {
                                palabra = token;
                                contadorPalabra = 0;
                            }
                            else
                            {
                                palabra = token;
                                contadorPalabra = 1;
                            }
                            if (contadorPalabra == 0)
                            {
                                //   PalabrasReservadas(token, fila, columna, estado);
                                token = "";
                                estado = estado - 1;
                                estadoMover = 0;
                            }
                            else if (contadorPalabra == 1)
                            {
                                estadoMover = 5;
                            }
                            else if (contadorPalabra == 3)
                            {
                                estado = estado - 1;
                                estadoMover = 5;
                                palabra = "";
                            }
                        }
                        break;
                    case 4:
                        if (letra == '”')
                        {
                            letra = '"';
                        }
                        if (letra != '"')
                        {

                            token += letra;
                        }
                        else if (letra == '"')
                        {
                            estado = estado - 1;
                            estadoMover = 5;
                        }
                        break;
                    case 5:
                        if (contadorPalabra == 0)
                        {
                            vecto.Add(token);
                            cont++;
                           // Console.WriteLine("cadena " + token);
                            columna++;
                            exp += token;
                            exp += "\"";
                            // AnalizarToken(token + "\"", fila, columna, "CADENA");

                            //  Console.WriteLine(token+ "kaska");
                            token = "";
                            estadoMover = 0;
                        }
                        else if (contadorPalabra == 1)
                        {
                             if (conjunto == 1)
                            {

                                vecto.Add(token);
                                cont++;

                                contadorPalabra = 0;
                                // expresion = 0;
                                conjunto = 0;
                                token = "";
                                palabra = "";
                                estado = estado - 2;
                                estadoMover = 0;

                            }
                            
                            else
                            {
                                // AnalizarToken(token, fila, columna, "IDENTIFICADOR");
                                contadorPalabra = 0;
                                expresion = 0;
                                expresion2 = 1;
                                token = "";
                                palabra = "";
                                estado = estado - 2;
                                estadoMover = 0;
                            }

                        }

                        break;

                    case 8:
                        columna++;
                        //  Errores(token += letra, fila, columna);
                        contError++;
                        token = "";
                        estadoMover = 0;
                        break;

                }
            }
        }
        public void generaGrafo(string nombre)
        {

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
                nodo n = inicial;
               // while (cont != 0)
              //  {
                    /*  cont--;
                      if (cont == 0) bodyGraph += "\" " + n.Num + " \";\n\n";
                      else bodyGraph += "\" " + n.Num + " \" ->";
                      n = n.primero;*/
                  //  cuerpo = "\""+ estados+ "\"" + " -> " + "\"" + "."+ "\"" + "[label="+ "\"" + "."+ "\"" + "];";
                bodyGraph +=cuerpo;
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

            public void LlenarExpresion()
        {
            string token = "";
            string tipo = "";
            int  fila, columna;
            foreach (Expresiones i in listaExpresion)
            {
              //  Console.WriteLine("EXP" + noExp.ToString() + 1 + token + tipo + fila + columna);
                //Console.WriteLine("EXP" + noExp.ToString(), 1, token, tipo, fila.ToString(), columna.ToString());
            }
        }
            public void AnalizarToken(string token, int fila, int columna, string tipo)
        {
            if (tipo.Equals("RESERVADA"))
            {
                listaToken.Add(new Token(contToken, 1, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("DIGITO"))
            {
                listaToken.Add(new Token(contToken, 2, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("CADENA"))
            {
                listaToken.Add(new Token(contToken, 3, token, tipo, fila, columna));
                rutas.Add(token);
                contadorRutas++;
                
                contToken++;
            }
            else if (tipo.Equals("IDENTIFICADOR"))
            {
                listaToken.Add(new Token(contToken, 4, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("COMENTARIO"))
            {
                listaToken.Add(new Token(contToken, 5, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("DOS PUNTOS"))
            {
                listaToken.Add(new Token(contToken, 6, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("PUNTO Y COMA"))
            {
                listaToken.Add(new Token(contToken, 7, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("CORCHETE IZQUIERDO"))
            {
                listaToken.Add(new Token(contToken, 8, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("CORCHETE DERECHO"))
            {
                listaToken.Add(new Token(contToken, 9, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("SIGNO IGUAL"))
            {
                listaToken.Add(new Token(contToken, 10, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("MAYOR QUE"))
            {
                listaToken.Add(new Token(contToken, 11, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("MENOR QUE"))
            {
                listaToken.Add(new Token(contToken, 12, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("ADMIRACION"))
            {
                listaToken.Add(new Token(contToken, 13, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("COMENTARIO MULTI"))
            {
                listaToken.Add(new Token(contToken, 14, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("CONJUNTO"))
            {
                listaToken.Add(new Token(contToken, 15, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("DEFINICION CONJUNTO"))
            {
                listaToken.Add(new Token(contToken, 15, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("EXPRESION"))
            {
                listaToken.Add(new Token(contToken, 15, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("GUION"))
            {
                listaToken.Add(new Token(contToken, 16, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("DIAGONAL"))
            {
                listaToken.Add(new Token(contToken, 17, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("CONCATENACION"))
            {
                listaToken.Add(new Token(contToken, 18, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("DISYUNCION"))
            {
                listaToken.Add(new Token(contToken, 19, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("CERRADURA *"))
            {
                listaToken.Add(new Token(contToken, 20, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("CERRADURA +"))
            {
                listaToken.Add(new Token(contToken, 21, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("CERRADURA ?"))
            {
                listaToken.Add(new Token(contToken, 22, token, tipo, fila, columna));
                contToken++;
            }
            else if (tipo.Equals("EXPRESION"))
            {
                listaToken.Add(new Token(contToken, 22, token, tipo, fila, columna));
                contToken++;
            }
        }
        
        private void GenerarArbol()
        {
            Tree_View tree = new Tree_View(texto);
            tree.Show();
        }
        private void LlenarTokens()
        {
            string nombre = "";
            string comparar = "";
            string tipo = "";
            foreach (Token i in listaToken)
            {
                nombre = i.Lexema;
                tipo = i.Tipo;
                FindMyText(nombre, tipo);
                comparar = nombre;
            }
        }
        private void LlenarErrores()
        {
            string nombre = "";
            int fila = 0;
            foreach (ErrorToken i in listaerror)
            {
                nombre = i.Tokens;
                fila = i.Fila;
                Pint(nombre);
            }
        }
        private void acercaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nombre: Fabio Andre Sanchez Chavez" + "\n" + "Carnet:    201709075 ", "Acerca de:", MessageBoxButtons.OKCancel, MessageBoxIcon.None);
        }
        

        private void PintarElementos(string palabra)
        {

            int inicio = 0;
            while (inicio < idTexto.Text.LastIndexOf(palabra))
            {
                idTexto.Find(palabra, inicio, idTexto.TextLength, RichTextBoxFinds.MatchCase);
                idTexto.SelectionColor = Color.Black;
                inicio = idTexto.Text.IndexOf(palabra, inicio) + 1;
            }

        }
        private void Errores(string token, int fila, int columna)
        {

            listaerror.Add(new ErrorToken(no, token, "Caracter Lexico Desconocido", fila, columna));
            no++;
            contError++;

        }
       

        


        
        
        public bool Pint(string text)
        {
            bool returnValue = false;
            if (text.Length > 0)
            {
                int indexToText = idTexto.Find(text);
                idTexto.SelectionColor = Color.Black;

                if (indexToText >= 0)
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }
       

        

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "er files (*.er)|*.er|All files (*.*)|*.*";
            if (file.ShowDialog() == DialogResult.OK)
            {
                path = file.FileName;
                String texto = File.ReadAllText(path);
                idTexto.Text = "\t" + texto;
            }
        }
        private void IdTexto_TextChanged(object sender, EventArgs e)
        {
              
        }
        private void GuardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();

            saveFileDialog1.Filter = "er files (*.er)|*.er|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sav = saveFileDialog1.FileName;
                saveFileDialog1.InitialDirectory = @"c:\temp\";
                File.WriteAllText(saveFileDialog1.FileName, idTexto.Text);
            }
        }

        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter sr = new StreamWriter(path);
            sr.Write(idTexto.Text);
            sr.Close();
            MessageBox.Show("Se ha guardado satisfactoriamente");
        }

       
        public bool FindMyText(string text, string tipo)
        {
            bool returnValue = false;
            if (tipo.Equals("RESERVADA"))
            {
                if (text.Length > 0)
                {
                    int num = Math.Min(idTexto.SelectionStart + 1, idTexto.TextLength);
                    int indexToText = idTexto.Find(text, num, RichTextBoxFinds.MatchCase);
                    idTexto.SelectionColor = Color.LightBlue;
                    if (indexToText >= 0)
                    {
                        returnValue = true;
                    }
                }
            }
            else if (tipo.Equals("IDENTIFICADOR"))
            {
                if (text.Length > 0)
                {
                    int num = Math.Min(idTexto.SelectionStart + 1, idTexto.TextLength);
                    int indexToText = idTexto.Find(text, num, RichTextBoxFinds.MatchCase);
                    idTexto.SelectionColor = Color.Orange;
                    if (indexToText >= 0)
                    {
                        returnValue = true;
                    }
                }
            }
            else if (tipo.Equals("CADENA"))
            {
                if (text.Length > 0)
                {
                    int num = Math.Min(idTexto.SelectionStart + 1, idTexto.TextLength);
                    int indexToText = idTexto.Find(text, num, RichTextBoxFinds.MatchCase);
                    idTexto.SelectionColor = Color.Green;
                    if (indexToText >= 0)
                    {
                        returnValue = true;
                    }
                }
            }
            else if (tipo.Equals("DIGITO"))
            {
                if (text.Length > 0)
                {
                    int num = Math.Min(idTexto.SelectionStart + 1, idTexto.TextLength);
                    int indexToText = idTexto.Find(text, num, RichTextBoxFinds.MatchCase);
                    idTexto.SelectionColor = Color.Yellow;
                    if (indexToText >= 0)
                    {
                        returnValue = true;
                    }
                }
            }
            else if (tipo.Equals("DOS PUNTOS"))
            {
                if (text.Length > 0)
                {
                    int num = Math.Min(idTexto.SelectionStart + 1, idTexto.TextLength);
                    int indexToText = idTexto.Find(text, num, RichTextBoxFinds.MatchCase);
                    idTexto.SelectionColor = Color.Pink;
                    if (indexToText >= 0)
                    {
                        returnValue = true;
                    }
                }
            }
            else if (tipo.Equals("PUNTO Y COMA"))
            {
                if (text.Length > 0)
                {
                    int num = Math.Min(idTexto.SelectionStart + 1, idTexto.TextLength);
                    int indexToText = idTexto.Find(text, num, RichTextBoxFinds.MatchCase);
                    idTexto.SelectionColor = Color.Red;
                    if (indexToText >= 0)
                    {
                        returnValue = true;
                    }
                }
            }
            else if (tipo.Equals("SIGNO ASIGNACION"))
            {
                if (text.Length > 0)
                {
                    int num = Math.Min(idTexto.SelectionStart + 1, idTexto.TextLength);
                    int indexToText = idTexto.Find(text, num, RichTextBoxFinds.MatchCase);
                    idTexto.SelectionColor = Color.Blue;
                    if (indexToText >= 0)
                    {
                        returnValue = true;
                    }
                }
            }
            else if (tipo.Equals("CORCHETE APERTURA") || tipo.Equals("CORCHETE CIERRE"))
            {
                if (text.Length > 0)
                {
                    int num = Math.Min(idTexto.SelectionStart + 1, idTexto.TextLength);
                    int indexToText = idTexto.Find(text, num, RichTextBoxFinds.MatchCase);
                    idTexto.SelectionColor = Color.Purple;
                    if (indexToText >= 0)
                    {
                        returnValue = true;
                    }
                }
            }

            return returnValue;
        }

        
       

        private void button1_Click(object sender, EventArgs e)
        {

            listaToken.Clear();
            listaerror.Clear();
            obtenerRuta();
            contError = 0;
            contToken = 1;
            no = 0;
            texto = idTexto.Text;
            Analizador(texto);

            if (contError == 0)
            {
               // GenerarArbol();
                LlenarTokens();
                obtenerRuta();

            }
            else
            {
                MessageBox.Show("Error lexico");
                LlenarTokens();
                LlenarErrores();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ListaTokens lista = new ListaTokens(listaToken);

            lista.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListaError lista = new ListaError(listaerror);
            lista.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            enviarExpresion();
            recibeExpresiones();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string edd = "xp";
                generaGrafo(edd);
        }
        }
    }

