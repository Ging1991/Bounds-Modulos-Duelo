using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubConvertirID : ISubSobreCarta {

		private int cartaID;

		public SubConvertirID(int cartaID) {
			this.cartaID = cartaID;
		}


		public void AplicarEfecto(GameObject carta) {
			EmblemaConvertirEnCriatura.ConvertirID(carta, cartaID);
		}


	}

}