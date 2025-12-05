using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Modulos.Duelo.Fisicas;
using System.Collections.Generic;
using UnityEngine;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaDestruccionImperfectos {

		public static List<GameObject> TraerCartas(int jugador) {

			Fisica fisica = Fisica.Instancia;
			List<GameObject> cartasParaDestruir = new List<GameObject>();

			foreach (GameObject carta in fisica.TraerCartasEnCampo(jugador)) {
				CartaEfecto cartaEfecto = carta.GetComponent<CartaEfecto>();
				CartaPerfeccion cartaPerfeccion = carta.GetComponent<CartaPerfeccion>();
				if (!cartaPerfeccion.EsPerfecta() && !cartaEfecto.TieneClave("RESISTENCIA"))
					cartasParaDestruir.Add(carta);
			}
			return cartasParaDestruir;
		}


		public static void Destruir(int jugador) {
			foreach (GameObject carta in TraerCartas(jugador))
				EmblemaDestruccion.DestruccionContinua(carta);
		}


	}

}