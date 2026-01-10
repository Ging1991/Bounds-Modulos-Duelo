using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Modulos.Cartas;

namespace Bounds.Duelo.Paneles {

	public class OpcionVisualizacion : MonoBehaviour {

		private GameObject carta;
		public bool tieneHabilidad;
		public PanelZona padre;

		public void Iniciar(GameObject carta) {
			this.carta = carta;
			CartaInfo info = carta.GetComponent<CartaInfo>();
			GetComponentInChildren<CartaFrente>().Mostrar(info.cartaID);
			//tieneHabilidad = info.tieneHabilidad("recobrar");
		}


		public void OnMouseDown() {
			padre.seleccionarCarta(carta);
		}


	}

}