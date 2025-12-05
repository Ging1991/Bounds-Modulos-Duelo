
using UnityEngine;

namespace Bounds.Cofres {

	public class LineaReceta {

		public int cartaID;
		public int cantidad;
		public string rareza;
		public string imagen;

		public LineaReceta(string codigo) {
//			Debug.Log(codigo);
			var partes = codigo.Split('_');
			cartaID = int.Parse(partes[0]);
			imagen = partes[1];
			rareza = partes[2];
			cantidad = int.Parse(partes[3]);
		}


		public string GetCodigo() {
			return $"{GetCartaIDFormateada()}_{imagen}_{rareza}_{cantidad}";
		}


		public string GetCodigoParcial() {
			return $"{GetCartaIDFormateada()}_{imagen}_{rareza}";
		}


		protected string GetCartaIDFormateada() {
			string cadenaID = $"{cartaID}";
			if (cartaID < 100)
				cadenaID = $"0{cartaID}";
			if (cartaID < 10)
				cadenaID = $"00{cartaID}";
			return cadenaID;
		}


	}

}