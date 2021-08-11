using Interbloqueos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace interbloqueo
{
    class Program
    {
         public static Boolean interbloqueo = false;

        public static void Desmarcar_todo(List<Vertice> vertices)
        {
            foreach (Vertice v in vertices)
            {
                v.Marcado = false;
            }
        }
        public static void Camino(Vertice oVertice, string sangria = "")
        {
            if (oVertice != null && oVertice.Marcado == false )
            {
                oVertice.Marcado = true;
                Console.WriteLine(sangria + oVertice.Valor);
                File.AppendAllText(System.IO.Path.GetFullPath("Resultados.txt"), sangria + oVertice.Valor);

                foreach (var oV in oVertice.Aristas)
                {
                    Camino(oV, sangria + "\t");
                }

            }
            else
            {
                Console.WriteLine(sangria + oVertice.Valor);
                Console.WriteLine(sangria + "\t" + "¡INTERBLOQUEO DETECTADO! ");
                interbloqueo = true;
                File.AppendAllText(System.IO.Path.GetFullPath("Resultados.txt"), sangria + oVertice.Valor + Environment.NewLine);
                File.AppendAllText(System.IO.Path.GetFullPath("Resultados.txt"), sangria + "¡INTERBLOQUEO DETECTADO! ");
            }
        }
        public static List<Vertice> QuitarPuntas(List<Vertice> todos_vertices)
        {
            foreach (Vertice vertice in todos_vertices.ToArray())
            {
                bool es_punta = true;
                Vertice punta = vertice;
                foreach (Vertice vertice2 in todos_vertices)
                {
                    if (vertice != vertice2)
                    {
                        foreach (var arista in vertice2.Aristas)
                        {
                            if (vertice.Valor == arista.Valor)
                            {
                                es_punta = false;


                            }

                        }

                    }
                }
                if (es_punta)
                {
                    Console.WriteLine(vertice.Valor + " Es PUNTA");
                    todos_vertices.Remove(vertice); //al remover la punta da la posibilidad de nuevas puntas, que son retiradas por el mismo algortimo

                }

            }
            return todos_vertices;

        }
        static void Main(string[] args)
        {
            //Usando como modelo el grafo dirigido (a) dibujado en la diapositiva 19 del pdf Interbloqueos en la plataforma
            //Instancio todos los vertices
            Vertice oVertice_R = new Vertice("R");
            Vertice oVertice_A = new Vertice("A");
            Vertice oVertice_C = new Vertice("C");
            Vertice oVertice_W = new Vertice("W");
            Vertice oVertice_F = new Vertice("F");
            Vertice oVertice_S = new Vertice("S");
            Vertice oVertice_D = new Vertice("D");
            Vertice oVertice_U = new Vertice("U");
            Vertice oVertice_G = new Vertice("G");
            Vertice oVertice_V = new Vertice("V");
            Vertice oVertice_E = new Vertice("E");
            Vertice oVertice_T = new Vertice("T");
            Vertice oVertice_B = new Vertice("B");

            //Almaceno todos los grados en una lista
            List<Vertice> todos_vertices = new List<Vertice> { oVertice_R, oVertice_A, oVertice_C, oVertice_W, oVertice_F, oVertice_S, oVertice_D, oVertice_U, oVertice_G, oVertice_V, oVertice_E, oVertice_T, oVertice_B };
            //Arcos correspondientes
            // "R" QUIERE A "A"
            oVertice_R.Aristas.Add(oVertice_A);
            // "A" QUIERE A "S"
            oVertice_A.Aristas.Add(oVertice_S);
            // "C" QUIERE A "S"
            oVertice_C.Aristas.Add(oVertice_S);
            // "W" QUIERE A "F"
            oVertice_W.Aristas.Add(oVertice_F);
            // "F" QUIERE A "S"
            oVertice_F.Aristas.Add(oVertice_S);
            // "D" QUIERE A "S"
            oVertice_D.Aristas.Add(oVertice_S);
            // "D" QUIERE A "T"
            oVertice_D.Aristas.Add(oVertice_T);
            // "B" QUIERE A "T"
            oVertice_B.Aristas.Add(oVertice_T);
            // "T" QUIERE A "E"
            oVertice_T.Aristas.Add(oVertice_E);
            // "E" QUIERE A "V"
            oVertice_E.Aristas.Add(oVertice_V);
            // "V" QUIERE A "G"
            oVertice_V.Aristas.Add(oVertice_G);
            // "G" QUIERE A "U"
            oVertice_G.Aristas.Add(oVertice_U);
            // "U" QUIERE A "D"
            oVertice_U.Aristas.Add(oVertice_D);




            using (StreamWriter writetext = new StreamWriter(System.IO.Path.GetFullPath("Resultados.txt")))
            {
                writetext.WriteLine("---CONFIGURACION DEL GRAFO---" + Environment.NewLine);

                foreach (Vertice vertice in todos_vertices)
                {
                    writetext.WriteLine("El vertice" + " " + vertice.Valor + " " + "quiere a:" + Environment.NewLine);

                    String line = "";
                    foreach (var arista in vertice.Aristas)
                    {
                        line += arista.Valor + ", ";

                    }
                    writetext.WriteLine(line + Environment.NewLine);
                }
            }



            //Inicializo el camino desde los nodos puntas: (en este caso son R-C-W-B)
            /*
             Camino(oVertice_R);
            Desmarcar_todo(todos_vertices);
            Camino(oVertice_C);
            Desmarcar_todo(todos_vertices);
            Camino(oVertice_W);
            Desmarcar_todo(todos_vertices);
            Camino(oVertice_B);
            Desmarcar_todo(todos_vertices);
             */
            todos_vertices = QuitarPuntas(todos_vertices);
            //Si queremos recorrer ABSOLUTAMENTE TODOS LOS NODOS podemos hacerlo asi:
            File.AppendAllText(System.IO.Path.GetFullPath("Resultados.txt"), "---RECORRIDOS---" + Environment.NewLine);


            foreach (Vertice v in todos_vertices)
            {
                Console.WriteLine("\b" + "Recorrido del nodo " + v.Valor);
                File.AppendAllText(System.IO.Path.GetFullPath("Resultados.txt"), Environment.NewLine + "Recorrido del nodo " + v.Valor + Environment.NewLine);
                if (!interbloqueo)
                {
                    Camino(v);
                    Desmarcar_todo(todos_vertices);
                }
                else {

                    Console.WriteLine("FIN");
                    break;
                }
               
            }

        }
    }
}
