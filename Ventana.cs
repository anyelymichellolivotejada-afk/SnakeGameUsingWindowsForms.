using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// para uso interno del proyecto: definición simple de Point evitando dependencia a System.Drawing
//namespace JuegoSnake
using System.Reflection;
using System.Threading;
//para poder usar la clase pont  inv
namespace JuegoSnake
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y) { X = x; Y = y; }
    }

    internal class Ventana
    {


        public string Titulo { get; set; }
        public int Ancho { get; set; }
        public int Altura { get; set; }
        public ConsoleColor ColorFondo { get; set; }

        public ConsoleColor ColorLetra { get; set; }
        public Point Limitesuperior { get; set; }
        public Point Limiteinferior { get; set; }
        public int Area { get; set; }


        public Ventana(string Titulo, int Ancho, int Altura, ConsoleColor ColorFondo, ConsoleColor ColorLetra, Point limiteinferior, Point limitesuperior)
        {
            this.Titulo = Titulo;
            this.Ancho = Ancho;
            this.Altura = Altura;
            this.ColorFondo = ColorFondo;
            this.ColorLetra = ColorLetra;
            // usar límites proporcionados
            this.Limitesuperior = limitesuperior;
            this.Limiteinferior = limiteinferior;
            Area = ((limiteinferior.X - limitesuperior.X)-1) * ((limiteinferior.Y - limitesuperior.Y)-1);
            Init();
            //se coloca aca para que se ejecute al crear la ventana y no tener que llamar al metodo Init() desde otro lugar
        }
        
        //metodo privado para  la conrfiguracion inicial de la ventana
        public void Init() 
        
        {
            Console.SetWindowSize(Ancho, Altura);
            Console.Title = Titulo;
            Console.BackgroundColor = ColorFondo;
            Console.CursorVisible = false;
            Console.Clear();
            //el clear se hace para que se vea el color de fondo en toda la ventana



        }

        public void dibujarMarco() 
        {
            Console.ForegroundColor = ColorLetra;
            for (int i = Limitesuperior.X; i < Limiteinferior.X; i++) 
            {
                Console.SetCursorPosition(i, Limitesuperior.Y);
                Console.Write("=");
                Console.SetCursorPosition(i, Limiteinferior.Y);
                Console.Write("=");
              
            
            }
            for (int i = Limitesuperior.Y; i< Limiteinferior.Y;i++) 
            {

                Console.SetCursorPosition(Limitesuperior.X, i);
                Console.Write("|");
                Console.SetCursorPosition(Limiteinferior.X, i);
                Console.Write("|");

            }

            Console.SetCursorPosition(Limitesuperior.X, Limitesuperior.Y);
            Console.Write("╔");

            // Esquina inferior izquierda
            Console.SetCursorPosition(Limitesuperior.X, Limiteinferior.Y);
            Console.Write("╚");

            // Esquina superior derecha
            Console.SetCursorPosition(Limiteinferior.X, Limitesuperior.Y);
            Console.Write("╗");

            // Esquina inferior derecha
            Console.SetCursorPosition(Limiteinferior.X, Limiteinferior.Y);
            Console.Write("╝");









        }



        //metodo del menu 
        // Método del menú

        public void menu()
        {
            Console.ForegroundColor = ConsoleColor.White;

            int centroX = (Limitesuperior.X + Limiteinferior.X) / 2;
            int centroY = (Limitesuperior.Y + Limiteinferior.Y) / 2;

            // Título
            string titulo = "J U E G O  S N A K E";
            Console.SetCursorPosition(centroX - titulo.Length / 2, centroY - 3);
            Console.Write(titulo);

            // Opción para jugar
            string opcionJugar = "ENTER - JUGAR";
            Console.SetCursorPosition(centroX - opcionJugar.Length / 2, centroY);
            Console.Write(opcionJugar);

            // Opción para salir
            string opcionSalir = "ESC - SALIR";
            Console.SetCursorPosition(centroX - opcionSalir.Length / 2, centroY + 2);
            Console.Write(opcionSalir);
        }































        //teclaado 
        public void Teclado(ref bool ejecucion, ref bool jugar, Snake snake) 
        {
            if (Console.KeyAvailable) 
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.Enter) 
                {

                    jugar = true;
                    Console.Clear();
                    dibujarMarco();
                    snake.Init();
                                
                }
                if (tecla.Key == ConsoleKey.Escape) 
                {
                    ejecucion = false;
                
                
                
                }

            
            
            
            
            }
        
        
        
        
        }


        //metodo para las alertas 
        public void Gameover(string text) 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            dibujarMarco();
            Console.SetCursorPosition(Limitesuperior.X + (Limitesuperior.X / 2) - 10,
                Limitesuperior.Y + (Limiteinferior.Y / 2) - 2);
            Console.Write("text");
            Thread.Sleep(3000);
            Console.Clear();
            dibujarMarco();

        
        
        
        
        
        }













    }
}
