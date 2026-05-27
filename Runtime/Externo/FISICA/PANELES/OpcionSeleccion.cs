using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Paneles {

	public class OpcionSeleccion : MonoBehaviour {

		private GameObject carta;


		public void Iniciar1(GameObject carta) {
			this.carta = carta;
			CartaInfo info = carta.GetComponent<CartaInfo>();
			//GetComponentInChildren<CartaImagen>().InicializarCartaID(info.cartaID);
		}


		public void Deseleccionar() {
			CartaFX fx = carta.GetComponent<CartaFX>();
			fx.deseleccionar();
			CartaFX fx1 = GetComponent<CartaFX>();
			fx1.deseleccionar();
		}


		public void OnMouseDown() {
			PanelSeleccion panel = GameObject.Find("PanelSeleccionar").GetComponent<PanelSeleccion>();
			panel.PresionarSeleccionar(carta);

			//Visor visor = GameObject.Find("Visor").GetComponent<Visor>();
			//visor.Mostrar(carta, forzarVisible:true);

			CartaFX fx = GetComponent<CartaFX>();
			fx.seleccionar(usar2: true);
		}


	}

}