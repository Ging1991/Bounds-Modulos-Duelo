using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Emblema;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaMezclarMazo {


		public static void Mezclar(int jugador) {
			ListadorDeZonas listador = Fisica.Instancia.listador;
			List<GameObject> cartas = listador.GenerarLista(jugador, ListadorDeZonas.Zona.MAZO);

			if (cartas.Count == 0)
				return;

			for (int i = 0; i < 100; i++) {
				int posicion1 = EmblemaAleatorio.entero(0, cartas.Count);
				int posicion2 = EmblemaAleatorio.entero(0, cartas.Count);

				GameObject auxiliar = cartas[posicion1];
				cartas[posicion1] = cartas[posicion2];
				cartas[posicion2] = auxiliar;
			}
		}


	}

}