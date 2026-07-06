using UnityEngine;
using Bounds.Duelo.Paneles.Seleccion;
using Bounds.Fisicas.Carta;
using Bounds.Cartas;

namespace Bounds.Duelo.Paneles {

	public class PanelOpcionCarta : MonoBehaviour {

		private GameObject carta;
		public ISeleccionarCarta accion;

		public void Iniciar(GameObject carta, ISeleccionarCarta accion = null) {
			this.carta = carta;
			this.accion = accion;
			CartaInfo info = carta.GetComponent<CartaInfo>();
			GetComponentInChildren<CartaImagenID>().MostrarCartaID(info.cartaID, info.imagen, info.rareza);
		}


		public void OnMouseDown() {
			if (accion != null)
				accion.Seleccionar(carta);
		}


	}

}