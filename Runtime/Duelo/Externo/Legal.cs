using System.Collections.Generic;
using System;

namespace Bounds.Infraestructura {

	public class Legal {

		public static int CARTAS_MAX_EN_MAZO = 40;
		public static int CARTAS_MIN_EN_MAZO = 1;
		public static int CARTAS_ID_MIN = 1;
		public static int CARTAS_ID_MAX = 580;
		public static List<string> ENEMIGOS = new List<string> { "reflejo", "mika", "miguel", "marcos", "florena", "launix", "luna", "rey" };


		public static void ValidarCartaID(int cartaID) {
			if (cartaID < CARTAS_ID_MIN || cartaID > CARTAS_ID_MAX)
				throw new ArgumentException("CartaID inválida: " + cartaID);
		}


		public static void ValidarPosicionCartaEnMazo(int posicion) {
			if (posicion < CARTAS_MIN_EN_MAZO || posicion > CARTAS_MAX_EN_MAZO)
				throw new ArgumentException("Posición inválida: " + posicion);
		}


		public static void ValidarClase(string clase) {
			List<string> clasesValidas = new List<string> {
				"BASICO", "EVOLUCION", "FUSION",
				"AURA", "EQUIPO", "HECHIZO",
				"MAGICO" , "TRAMPA", "FANTASMA",
				"VACIO", "FICHA", "GEMINIS",
				"REFLEJO", "VECTOR", "SOMBRA"
			};
			if (!clasesValidas.Contains(clase))
				throw new ArgumentException("Clase inválida: " + clase);
		}


	}

}