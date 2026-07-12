using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Modulos.Cartas;
using Bounds.Fisicas.Carta;
using Bounds.Cartas;

namespace Bounds.Duelo.Paneles {

	public class OpcionVisualizacion : MonoBehaviour {

		private GameObject carta;
		public bool tieneHabilidad;
		public PanelZona padre;

		public void Iniciar(GameObject carta) {
			this.carta = carta;
			CartaInfo info = carta.GetComponent<CartaInfo>();
			GetComponentInChildren<CartaImagenID>().MostrarCartaID(info.cartaID);
			//tieneHabilidad = info.tieneHabilidad("recobrar");
		}


		public void OnMouseDown() {
			padre.SeleccionarCarta(carta);
		}


	}

}