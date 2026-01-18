using System.Collections.Generic;
using Bounds.Duelo.Carta;
using UnityEngine;

namespace Bounds.Duelo.Pila {

	public class PilaEfectos : MonoBehaviour {

		public List<IEfecto> efectos = new List<IEfecto>();
		public GameObject pilaVisual;

		public void Agregar(IEfecto efecto) {
			efectos.Add(efecto);
			ActualizarVista();
			GetComponent<PilaReloj>().Pausar();
			GetComponent<PilaReloj>().Reanudar();
		}


		public void Remover(IEfecto efecto) {
			efectos.Remove(efecto);
			ActualizarVista();
		}


		public bool EstaVacia() {
			return efectos.Count == 0;
		}


		public void Resolver() {
			if (!EstaVacia()) {
				IEfecto efecto = efectos[efectos.Count - 1];
				efecto.Resolver();
				Remover(efecto);

			}
			else {
				GetComponent<PilaReloj>().Pausar();
			}
		}


		private void ActualizarVista() {
			pilaVisual.SetActive(false);
			if (!EstaVacia()) {
				pilaVisual.SetActive(true);
				List<PilaEfectos.CartaPila> efectosID = new List<PilaEfectos.CartaPila>();
				foreach (IEfecto efecto in efectos) {
					CartaPila cartaPila = new CartaPila();
					cartaPila.cartaID = efecto.GetFuente().GetComponent<CartaInfo>().cartaID;
					cartaPila.imagen = efecto.GetFuente().GetComponent<CartaInfo>().imagen;
					efectosID.Add(cartaPila);
				}
				pilaVisual.GetComponentInChildren<PilaVisual>().SetEfectos(efectosID);
			}
		}


		public class CartaPila {
			public int cartaID;
			public string imagen;
		}


	}

}