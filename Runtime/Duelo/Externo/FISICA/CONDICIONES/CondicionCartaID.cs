using System.Collections.Generic;
using Bounds.Duelo.Carta;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionCartaID : CondicionCarta {

		private readonly int valor;

		public CondicionCartaID(int cartaID = -1, List<int> cartasID = null) {
			valor = cartaID;
			precondiciones = new List<CondicionCarta>();
		}


		public override bool Cumple(GameObject carta) {

			if (!CumplePrecondiciones(carta))
				return false;

			return CartaPerfeccion.ExtenderID(carta).Contains(valor);
		}


	}

}