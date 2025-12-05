using System;
using System.Collections.Generic;

namespace Bounds.Duelo.Utiles {

	public class JugadorDuelo {
		
		public enum Jugador {
			AMBOS,
			CONTROLADOR,
			ADVERSARIO
		}

		private static List<JugadorDuelo> jugadores = new List<JugadorDuelo>();
		private int jugador = -1;
		public int invocaciones_normales = 0;


		private JugadorDuelo(int jugador) {
			this.jugador = jugador;
		}


		public static JugadorDuelo GetInstancia(int jugador) {
			
			if (jugador < 1 || jugador > 2)
				throw new ArgumentException("Argumento inválido para jugador: " + jugador);
			
			JugadorDuelo ret = null;
			foreach(JugadorDuelo j in jugadores)
				if (j.jugador == jugador)
					ret = j;
			if (ret == null) {
				ret = new JugadorDuelo(jugador);
				jugadores.Add(ret);
			}
			return ret;
		}


		public static int Adversario(int jugador) {
			if (jugador == 1)
				return 2;
			return 1;
		}


		public int Adversario() {
			return Adversario(jugador);
		}


	}

}