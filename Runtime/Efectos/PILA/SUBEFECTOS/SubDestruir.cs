using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubDestruir : ISubSobreCarta {

		public void AplicarEfecto(GameObject carta) {
			EmblemaDestruccion.DestruirPorEfectos(carta);
		}


	}

}