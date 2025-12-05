using UnityEngine;

namespace Bounds.Infraestructura {

	public class Colores {

		private static readonly float DIVISOR = 255f;

		public static Color BORDE = Generar(0, 80, 50);
		public static Color LETRA = Generar(5, 45, 25);
		public static Color RELLENO_GLOBAL = Generar(10, 115, 85);
		public static Color RELLENO_BOTON = Generar(80, 240, 190);
		public static Color RELLENO_CARTEL = Generar(15, 190, 140);


		private static Color Generar(int valor1, int valor2, int valor3) {
			return new Color(valor1 / DIVISOR, valor2 / DIVISOR, valor3  / DIVISOR);
		}


	}

}