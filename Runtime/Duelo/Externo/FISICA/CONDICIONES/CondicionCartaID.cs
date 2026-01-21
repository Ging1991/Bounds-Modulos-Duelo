using System.Collections.Generic;
using System.Linq;
using Bounds.Duelo.Carta;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionCartaID : CondicionCarta {

		private readonly int valor;
		private readonly List<int> cartasID;

		public CondicionCartaID(int cartaID = -1, List<int> cartasID = null) {
			valor = cartaID;
			this.cartasID = cartasID;
			precondiciones = new List<CondicionCarta>();
		}


		public override bool Cumple(GameObject carta) {

			if (!CumplePrecondiciones(carta))
				return false;
			if (cartasID != null) {
				return CartaPerfeccion.ExtenderID(carta).Intersect<int>(cartasID).Count<int>() > 0;
			}
			return CartaPerfeccion.ExtenderID(carta).Contains(valor);
		}


	}

}