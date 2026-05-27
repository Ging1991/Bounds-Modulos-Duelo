using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubEnviarMazo : ISubSobreCarta {

		public void AplicarEfecto(GameObject carta) {
			EmblemaReciclar.Reciclar(carta);
		}

	}

}