using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace JuegoSnake
{
    internal class Snake
    {
        enum direccion 
        { 
            Arriba, Abajo,Derecha,Izquierda
        
        
        }

        public bool vivo { get; set; }
        public  ConsoleColor  ColorCabeza { get; set; }
        public ConsoleColor ColorCuerpo { get; set; }

        public Ventana ventanaC { get; set; }
        public List<Point> Cuerpo { get; set; }
        public Point Cabeza { get; set; }
        private direccion _direccion;
        public Comida comidaC { get; set; }

        public bool comiendo { get; set; }
        public int puntaje  { get; set; }
        public int puntajeMaximo { get; set; }
        public Point PosicionInicial { get; set; }





        //no comprendo al 100
        public Snake(Point posicion, ConsoleColor colorCabeza, ConsoleColor colorCuerpo, Ventana ventana,
            Comida comida)
        {
            ColorCabeza = colorCabeza;
            ColorCuerpo = colorCuerpo;
            ventanaC = ventana;
            Cabeza = posicion;
            comidaC = comida;
            Cuerpo = new List<Point>();
            _direccion = direccion.Derecha;
            puntaje = 0;
            puntajeMaximo = 0;
            PosicionInicial = posicion;





        }

        public void inicialCuerpo(int numPartes) 
        {
            int X = Cabeza.X - 1;
            for (int i = 0; i < numPartes;i ++) 
            {
                Console.SetCursorPosition(X,Cabeza.Y);
                Console.Write(" ");
                Cuerpo.Add(new Point(X, Cabeza.Y));
                X--;
            
            
            
            }
            

        
        
        
        
        
        }





        //metodo mover 
        public void mover() 
        {
            teclado();
            Point posCabezaAnterior = Cabeza;
            MoverCabeza();
            moverCuerpo(posCabezaAnterior);
            CollisionesComida();
            if (colisionesCuerpo()) 
            {
                Muerte();
                ventanaC.Gameover("G A M E   O V E R");
            
            }
        
        }
        public void MoverCabeza() 
        {
            Console.ForegroundColor = ColorCabeza;
            Console.SetCursorPosition(Cabeza.X, Cabeza.Y);
            Console.Write(" ");
            switch (_direccion)
            {

                case direccion.Derecha:
                    Cabeza = new Point(Cabeza.X + 1, Cabeza.Y);
                    break;
                case direccion.Izquierda:
                    Cabeza = new Point(Cabeza.X - 1, Cabeza.Y);
                    break;
                case direccion.Arriba:
                    Cabeza = new Point(Cabeza.X, Cabeza.Y - 1);
                    break;
                case direccion.Abajo:
                    Cabeza = new Point(Cabeza.X, Cabeza.Y + 1);
                    break;

            
            }
            colisionesMarco();
            Console.SetCursorPosition(Cabeza.X, Cabeza.Y);
            Console.Write("█");
           
        }
        private void moverCuerpo(Point posCabezaAnterior)
        {

            Console.ForegroundColor = ColorCabeza;
            Console.SetCursorPosition(posCabezaAnterior.X, posCabezaAnterior.Y);
            Console.Write("█");
            Cuerpo.Insert(0, posCabezaAnterior);

            if (comiendo)
            {
                comiendo = false;
                return;
            }

            Console.SetCursorPosition(Cuerpo[Cuerpo.Count - 1].X, Cuerpo[Cuerpo.Count - 1].Y);
            Console.Write(" ");
            Cuerpo.Remove(Cuerpo[Cuerpo.Count - 1]);
        

            
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           }
       
        //metodo      para las coliciones 
        //para que cuando la cabeza coliicone con una parte del marco esta se pase al otro lado.
        private void colisionesMarco()
        {
            if (Cabeza.X <= ventanaC.Limitesuperior.X)
                Cabeza = new Point(ventanaC.Limiteinferior.X - 1, Cabeza.Y);
            if (Cabeza.X >= ventanaC.Limiteinferior.X)
                Cabeza = new Point(ventanaC.Limitesuperior.X + 1, Cabeza.Y);
            if (Cabeza.Y <= ventanaC.Limitesuperior.Y)
                Cabeza = new Point(Cabeza.X, ventanaC.Limiteinferior.Y - 1);
            if (Cabeza.Y >= ventanaC.Limiteinferior.Y)
                Cabeza = new Point(Cabeza.X, ventanaC.Limitesuperior.Y + 1);


        }


        public void Init() 
        {
            Cuerpo.Clear();
            Cabeza = PosicionInicial;
            vivo = true;
            inicialCuerpo(2);
            _direccion = direccion.Derecha;
            comidaC.GenerarComida(this);
        
        
        
        
        }
        
        








        private void teclado() 
        { if (Console.KeyAvailable) 
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.RightArrow && (_direccion!=direccion.Izquierda))
                    _direccion = direccion.Derecha;
                if (tecla.Key == ConsoleKey.LeftArrow  && (_direccion!=direccion.Derecha))
                    _direccion = direccion.Izquierda;
                if (tecla.Key == ConsoleKey.UpArrow    && (_direccion!=direccion.Abajo))
                    _direccion = direccion.Arriba;
                if (tecla.Key == ConsoleKey.DownArrow && (_direccion!=direccion.Arriba))
                    _direccion = direccion.Abajo;

            }

        
        }





        //colisiones comida para cuando colicione con la comida 
        //y se la coma aparezca en otra parte 
        public void CollisionesComida()
        {
            // comparar coordenadas en lugar de usar == para evitar operador no definido
            if (Cabeza.X == comidaC.Posicion.X && Cabeza.Y == comidaC.Posicion.Y)
            {
                if (!comidaC.GenerarComida(this)) 
                {
                    vivo = false;
                    ventanaC.Gameover("J U E G O   C O M P L E T A D O ");
                
                
                }
                comiendo = true;
                puntaje++;
                if (puntaje > puntajeMaximo) 
                {
                    puntajeMaximo = puntaje;
                }
            }
        }



        public bool colisionesCuerpo()
        {
            foreach (Point item in Cuerpo)
            {
                if (Cabeza.X == item.X && Cabeza.Y == item.Y)
                {
                    vivo = false;

                    return true;


                }

            }
            return false;

        }

        //metodo de muerte
        public void Muerte() 
        {
            Console.ForegroundColor = ColorCuerpo;
            foreach (Point item in Cuerpo) 
            {

                if (item.X==Cabeza.X && item.Y== Cabeza.Y) continue;
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write("░");
                Thread.Sleep(150);
            
            
            
            
            }
        
        }


        public void Informacion(int distanciaX1, int distanciax2)
        {
            Console.ForegroundColor = ConsoleColor.White;

            int posicionY = ventanaC.Limiteinferior.Y - 1;

            Console.SetCursorPosition(
                ventanaC.Limitesuperior.X + distanciaX1,
                ventanaC.Limitesuperior.Y-1);

            Console.Write("Puntaje: " + puntaje + "   ");

            Console.SetCursorPosition(
               ventanaC.Limitesuperior.X + distanciax2,
                ventanaC.Limitesuperior.Y - 1);

            Console.Write("Puntaje máximo: " + puntajeMaximo + "   ");
        }








    }






    
}
