using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{

public class Jugador
	{
		public static int maxAmonestaciones { get; set; }
		public static int maxFaltas { get; set; }
		public static int minEnergia { get; set; }
		public static Random rand { private get; set; }
		public string nombre { get; set; }
		public int puntos { get; set; }

		private int _amonestaciones;

		public int amonestaciones {
            get
            {
				return _amonestaciones;
            }

			set
			{
				if (0 > value)
					amonestaciones = 0;
				else
					_amonestaciones = value;
				if (value > maxAmonestaciones)
					amonestacionesMaximoExcedido(this, new AmonestacionesMaximoExcedidoArgs(value));
			}
			}
		private int _faltas;

		public int faltas
        {
            set
            {
				if(value > maxFaltas)
                {
					faltasMaximoExcedido(this, new FaltasMaximoExcedidoArgs(value));
                }
					_faltas = value;
            }
            get
            {
				return _faltas;
            }
        }

		private int _energia;

		public int energia
		{
			set
			{
				if (value > 100)
					_energia = 100;
				else
					_energia = value;

				if (value < minEnergia)
					energiaMinimaExcedida(this, new EnergiaMinimaExcedidaArgs(value));
			}
			get
            {
				return _energia;
            }
		}



		public Jugador(string nombre, int amonestaciones, int faltas, int energia, int puntos)
		{
			this.nombre = nombre;
			this.amonestaciones = amonestaciones;
			this.faltas = faltas;
			this.energia = energia;
			this.puntos = puntos;
		}
		public void incAmonestaciones()
		{
			amonestaciones += rand.Next(0, 3);
		}

		public void incFaltas()
		{
			faltas+= rand.Next(0,4);
		}
		public void decEnergia()
		{
			energia-=rand.Next(0,8);
		}

		public void incPuntos()
		{
			puntos+= rand.Next(0,4);
		}

		public bool todoOk()
		{
			if (maxAmonestaciones < amonestaciones || faltas > maxFaltas || minEnergia > energia)
				return false;
			else
				return true;
					}

		public void mover()
		{
            if (todoOk())
            {
				incAmonestaciones();
				incFaltas();
				incPuntos();
				decEnergia();
            }
		}
		override
		public string ToString()
		{
			return ("[" + nombre + "] Puntos: " + puntos + ";Amonestaciones: " + amonestaciones + "; Faltas: " + faltas + "; Energia: " + energia + "%; OK: " + todoOk());

		}

		public event EventHandler<AmonestacionesMaximoExcedidoArgs> amonestacionesMaximoExcedido;
		public event EventHandler<FaltasMaximoExcedidoArgs> faltasMaximoExcedido;

		public event EventHandler<EnergiaMinimaExcedidaArgs> energiaMinimaExcedida;


	}


	public class AmonestacionesMaximoExcedidoArgs : EventArgs
	{
		public int amonestaciones { get; set; }
		public AmonestacionesMaximoExcedidoArgs(int amonestaciones)
		{
			this.amonestaciones = amonestaciones;
		}
	}
	public class FaltasMaximoExcedidoArgs : EventArgs
	{
		public int faltas { get; set; }
		public FaltasMaximoExcedidoArgs(int value)
		{
			this.faltas = faltas;
		}
	}
	public class EnergiaMinimaExcedidaArgs : EventArgs
	{
		public int energia { get; set; }
		public EnergiaMinimaExcedidaArgs(int value)
		{
			this.energia = energia;
		}
	}
}
