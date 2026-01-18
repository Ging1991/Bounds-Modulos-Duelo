using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Paneles.Seleccion;
using Bounds.Modulos.Cartas;

namespace Bounds.Duelo.Paneles {

	public class PanelOpcionCarta : MonoBehaviour {

		private GameObject carta;
		public ISeleccionarCarta accion;

		public void Iniciar(GameObject carta, ISeleccionarCarta accion = null) {
			this.carta = carta;
			this.accion = accion;
			CartaInfo info = carta.GetComponent<CartaInfo>();
			GetComponentInChildren<CartaFrente>().Mostrar(info.cartaID, info.imagen);
		}


		public void OnMouseDown() {
			if (accion != null)
				accion.Seleccionar(carta);
		}


	}

}