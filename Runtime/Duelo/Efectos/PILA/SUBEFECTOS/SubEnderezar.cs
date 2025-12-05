using Bounds.Duelo.Carta;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubEnderezar : ISubSobreCarta {

		public void AplicarEfecto(GameObject carta) {
			carta.GetComponent<CartaMovimiento>().Enderezar();
		}


	}

}