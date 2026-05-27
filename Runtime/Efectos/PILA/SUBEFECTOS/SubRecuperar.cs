using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubRecuperar : ISubSobreCarta {

		public SubRecuperar() {}


		public void AplicarEfecto(GameObject carta) {
			EmblemaRecuperar.Levantar(carta);
		}


	}

}