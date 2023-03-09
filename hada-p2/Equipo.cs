using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
	class Equipo
	{
		public static int minJugadores { get; set; }
		public static int maxNumeroMovimientos { get; set; }

		public int movimientos { get; private set; }
		public string nombreEquipo { get; private set; }
		List<Jugador> jugadores { get; set; }
		List<Jugador> expulsados { get; set; }
		List<Jugador> lesionados { get; set; }
		List<Jugador> retirados { get; set; }


		public Equipo(int bj, string nom)
		{
			jugadores = new List<Jugador>();
			expulsados = new List<Jugador>();
			lesionados = new List<Jugador>();
			retirados = new List<Jugador>();

			for (int i = 0; i < bj; i++)
			{
				Jugador j = new Jugador("Juagdor_" + (i + 1).ToString(), 0, 0, 50, 0);
				j.amonestacionesMaximoExcedido += cuandoAmonestacionesMaximoExcedido;
				j.faltasMaximoExcedido += cuandoFaltasMaximoExcedido;
				j.energiaMinimaExcedida += cuandoEnergiaMinimaExcedida;
				jugadores.Add(j);
			}
			nombreEquipo = nom;
		}
		public bool moverJugadores()
		{
			int movidos = 0;
			movimientos++;
			if (jugadores.Count > minJugadores)
			{
				foreach (var j in jugadores)
				{
					if (j.todoOk())
					{
						j.mover();
						movidos++;
					}
				}
			}
			if (movidos > minJugadores)
				return true;
			return false;
		}
		public void moverJugadoresEnBucle()
		{
			bool ok = true;
			while (ok == true)
			{
				ok = false;
				foreach (var j in jugadores)
				{
					if (j.todoOk())
					{
						j.mover();
						ok = true;
					}
				}
			}
		}
		public int sumarPuntos()
		{
			int puntos = 0;
			foreach (var j in jugadores)
			{
				puntos += j.puntos;
			}
			return puntos;
		}
		public List<Jugador> getJugadoresExcedenLimiteAmonestaciones()
		{
			return expulsados;
		}
		public List<Jugador> getJugadoresExcedenLimiteFaltas()
		{
			return lesionados;
		}

		public List<Jugador> getJugadoresExcedenMinimoEnergia()
		{
			return retirados;
		}

		public override string ToString()
		{
			string ss = "[" + nombreEquipo + "]Puntos: " + sumarPuntos().ToString() + "; Expulsados: " + expulsados.Count + "; Lesionados: " + lesionados.Count + "; Retirados: " + retirados.Count;
			ss += "\n";
			foreach (var j in jugadores)
			{
				ss += j.ToString() + "\n";
			}
			return ss;
		}

		private void cuandoAmonestacionesMaximoExcedido(object sender, AmonestacionesMaximoExcedidoArgs args)
		{
			Jugador j = (Hada.Jugador)sender;
			Console.WriteLine("¡¡Número máximo excedido de amonestaciones. Jugador expulsado!!");
			Console.WriteLine("Jugador: " + j.nombre);
			Console.WriteLine("Equipo: " + nombreEquipo);
			Console.WriteLine("Amonestaciones: " + args.amonestaciones);
			expulsados.Add(j);
		}
		private void cuandoFaltasMaximoExcedido(object sender, FaltasMaximoExcedidoArgs args)
		{
			Jugador j = (Hada.Jugador)sender;
			Console.WriteLine("¡¡Número máximo excedido de faltas recibidas. Jugador lesionado!!");
			Console.WriteLine("Jugador: " + j.nombre);
			Console.WriteLine("Equipo: " + nombreEquipo);
			Console.WriteLine("Faltas: " + args.faltas);
			lesionados.Add(j);
		}
		private void cuandoEnergiaMinimaExcedida(object sender, EnergiaMinimaExcedidaArgs args)
		{
			Jugador j = (Hada.Jugador)sender;
			Console.WriteLine("¡¡Energía mínima excedida. Jugador retirado!!");
			Console.WriteLine("Jugador: " + j.nombre);
			Console.WriteLine("Equipo: " + nombreEquipo);
			Console.WriteLine("Energia: " + args.energia + " %");
			retirados.Add(j);
		}
	}
}
