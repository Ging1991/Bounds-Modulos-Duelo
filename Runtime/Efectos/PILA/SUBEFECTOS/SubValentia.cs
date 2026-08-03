using Bounds.Duelo.Emblemas.Jugar;
using Bounds.Fisicas.Carta;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubValentia : ISubSobreCarta {

		public void AplicarEfecto(GameObject carta) {
			int controlador = carta.GetComponent<CartaInfo>().controlador;

			GameObject campoLibre = BuscadorCampo.getInstancia().buscarCampoLibre(controlador);
			if (campoLibre != null) {
				EmblemaJuegoActivar.ColocarSobreElCampo(controlador, carta, campoLibre, carta.GetComponent<CartaInfo>().original.clase);
				Seleccionador seleccionador = GameObject.FindAnyObjectByType<Seleccionador>();
				seleccionador.combateCancelado = true;
			}

		}

	}

}