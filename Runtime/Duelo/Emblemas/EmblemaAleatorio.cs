using System.Collections.Generic;
using UnityEngine;

namespace Bounds.Duelo.Emblema {

	public class EmblemaAleatorio {

		private static System.Random random;


		public static int entero(int minimo, int maximo) {

			if (random == null)
				random = new System.Random();
			
			return random.Next(minimo, maximo);
		}


		public static GameObject carta(List<GameObject> cartas) {
			if (cartas.Count == 0)
				return null;
			return cartas[entero(0, cartas.Count)];
		}


	}

}