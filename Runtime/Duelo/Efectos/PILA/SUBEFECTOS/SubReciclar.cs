using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubReciclar : ISubSobreCarta {

		public SubReciclar() { }


		public void AplicarEfecto(GameObject carta) {
			EmblemaReciclar.Reciclar(carta);
		}


	}

}