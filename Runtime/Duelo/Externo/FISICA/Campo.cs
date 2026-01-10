using UnityEngine;
using UnityEngine.EventSystems;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Utiles;

namespace Bounds.Modulos.Duelo.Fisicas {

	public class Campo : MonoBehaviour, IDropHandler {

		public GameObject carta;
		public int jugador;
		public int lugarID;


		public void Ocupar(GameObject carta) {
			this.carta = carta;
			GetComponent<BoxCollider2D>().enabled = false;
		}


		public void Desocupar() {
			carta = null;
			GetComponent<BoxCollider2D>().enabled = true;
		}


		public bool EstaOcupado() {
			return carta != null;
		}


		void OnMouseDown() {
			Entrada entrada = Entrada.GetInstancia();
			entrada.PresionarCampo(gameObject);
		}


		public void OnDrop(PointerEventData eventData) {
			if (CartaArrastrar.carta != null) {
				EmblemaJugarCarta.Jugar(CartaArrastrar.carta, gameObject);
			}
		}


	}

}