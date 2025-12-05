using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubLevantar : ISubSobreCarta {

		public SubLevantar() {}


		public void AplicarEfecto(GameObject carta) {
			EmblemaLevantar.Levantar(carta);
		}


	}

}