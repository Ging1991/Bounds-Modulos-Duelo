using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Pila.Subefectos;
using UnityEngine;

namespace Bounds.Duelo.Utiles {

	public class Tester : MonoBehaviour {

		private static void DestruyeTodasLasCartas() {
			foreach (var carta in new SubCartasControladas(0).Generar()) {
				EmblemaDestruccion.DestruirPorEfectos(carta);
			}
		}

		private static void RobaUnaCarta() {
			EmblemaRobo.RobarCartas(1, 1);
		}


		void Update() {
			if (Input.GetKey("a"))
				RobaUnaCarta();

			if (Input.GetKey("d"))
				DestruyeTodasLasCartas();
		}


	}

}