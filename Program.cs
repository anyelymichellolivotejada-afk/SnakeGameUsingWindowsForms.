using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JuegoSnake;
namespace JuegoSnake
{
    static class ProgramS
    {
        [STAThread]
        static void Main()
        {
            Ventana ventana;
            Snake snake;
            bool jugar = false;
            Comida comida;
            bool Ejecucion =true;
             

            void iniciar()
            {
                ventana = new Ventana("Snake", 65, 20, ConsoleColor.Black, ConsoleColor.Blue, new Point(59, 18),new Point(5, 3));
                ventana.dibujarMarco();
                comida = new Comida(ConsoleColor.DarkRed, ventana);
                snake = new Snake(new Point(8, 4), ConsoleColor.Red, ConsoleColor.White, ventana, comida);
                //snake.inicialCuerpo(2);
                //comida.GenerarComida(snake);
                
            }

            void Game() 
            {

                while (Ejecucion) 
                {

                    ventana.menu();
                    ventana.Teclado(ref Ejecucion, ref jugar, snake);

                    while (jugar)
                    {

                        snake.mover();
                        snake.Informacion(0, 84);
                        if (!snake.vivo) 
                        { jugar = false;
                            snake.puntaje = 0;
                        }
                        Thread.Sleep(130);


                    }


                }
                
            
            
            }


            iniciar();
            Game();







        }
    }
}


