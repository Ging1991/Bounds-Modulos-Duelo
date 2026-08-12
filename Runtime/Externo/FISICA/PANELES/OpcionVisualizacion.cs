using UnityEngine;
using Bounds.Fisicas.Carta;
using Bounds.Cartas;

namespace Bounds.Duelo.Paneles {

	public class OpcionVisualizacion : MonoBehaviour {

		private GameObject carta;
		public CartaImagenID cartaImagenID;
		public PanelZona padre;

		public void Iniciar(GameObject carta, PanelZona padre) {
			this.carta = carta;
			this.padre = padre;
			CartaInfo cartaInfo = carta.GetComponent<CartaInfo>();
			cartaImagenID.MostrarCartaID(cartaInfo.cartaID, cartaInfo.imagen, cartaInfo.rareza);
		}

		public void OnMouseDown() {
			padre.SeleccionarCarta(carta);
		}

	}

}