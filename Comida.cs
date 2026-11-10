using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace JuegoSnake
{
    internal class Comida
    {
        public Point Posicion { get; set; }
        public ConsoleColor Color { get; set; }
        public Ventana Ventanac { get; set; }

        public Comida(ConsoleColor color, Ventana ventana) 
        {
            Color = color;
            Ventanac = ventana;
    
        
        }
        private void Dibujar()
        {

            Console.ForegroundColor = Color;
            Console.SetCursorPosition(Posicion.X, Posicion.Y);
            Console.Write("*");
        }

        public bool GenerarComida(Snake snake)

        {

            int longsnake = snake.Cuerpo.Count + 1;
            if ((Ventanac.Area = longsnake) <= 0)
            { return false; }

            Random random = new Random();

            int x = random.Next(
                Ventanac.Limitesuperior.X + 1,
                Ventanac.Limiteinferior.X
            );

            int y = random.Next(
                Ventanac.Limitesuperior.Y + 1,
                Ventanac.Limiteinferior.Y
            );

            Posicion = new Point(x, y);


            foreach (Point item in snake.Cuerpo) 
            {
                if ((x == item.X && y == item.Y) || (x == snake.Cabeza.X && y == snake.Cabeza.Y)) 
                { if (GenerarComida(snake)) { return true; }
                
                
                
                
                }
            
            
            
            
            
            }

            Dibujar();
            return true;
        }









    }
}
