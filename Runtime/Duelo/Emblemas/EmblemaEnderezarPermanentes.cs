using UnityEngine;
using Bounds.Duelo.Carta;
using System.Collections.Generic;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Emblema {

	public class EmblemaEnderezarPermanentes {

		public static List<GameObject> TraerCartas(int jugador) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			List<GameObject> cartasGiradas = new List<GameObject>();
			foreach (GameObject carta in fisica.TraerCartasEnCampo(jugador)) {
				if (carta.GetComponent<CartaMovimiento>().estaGirado)
					cartasGiradas.Add(carta);
			}
			return cartasGiradas;
		}


		public static void Enderezar(int jugador) {
			foreach (GameObject carta in TraerCartas(jugador)) {
				carta.GetComponent<CartaMovimiento>().Enderezar();
			}
		}


	}

}