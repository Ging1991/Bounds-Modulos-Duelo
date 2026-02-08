using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubConvertir : ISubSobreCarta {

		public SubConvertir() { }


		public void AplicarEfecto(GameObject carta) {
			EmblemaConvertirEnCriatura.Convertir(carta);
		}


	}

}