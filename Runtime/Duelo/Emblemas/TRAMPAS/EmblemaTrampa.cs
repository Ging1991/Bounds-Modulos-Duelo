using Bounds.Duelo.Carta;
using UnityEngine;

namespace Bounds.Duelo.Emblemas.Trampas {

	public class EmblemaTrampa {

		public static void ActivarTrampa(GameObject trampa) {
			trampa.GetComponent<CartaGeneral>().ColocarBocaArriba();
			EmblemaPadre.ActivarEfectosDeActivacion(trampa);
		}


	}

}