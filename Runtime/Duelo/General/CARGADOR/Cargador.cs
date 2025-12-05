using System.Collections.Generic;
using UnityEngine;
using Ging1991.Relojes;
using Bounds.Global.Mazos;

namespace Bounds.Duelo {

	public class Cargador : MonoBehaviour {


		public List<GameObject> CargarCartasPorCartaCofre(int jugador, List<CartaMazo> cartas, Sprite protector) {
			List<GameObject> ret = new List<GameObject>();

			int posicion = 1;
			foreach (var carta in cartas) {
				ret.AddRange(CargarCartasPorLinea(jugador, carta, protector));
				posicion++;
			}

			return ret;
		}


		public List<GameObject> CargarCartasPorLinea(int jugador, CartaMazo carta, Sprite protector) {
			List<GameObject> cartas = new List<GameObject>();
			GameObject campo = GameObject.Find("Cartas" + jugador);
			CreacionDeCartas creacion = FindAnyObjectByType<CreacionDeCartas>();
			Reloj.GetInstanciaGlobal();

			int posicion = 1;
			for (int i = 0; i < carta.cantidad; i++) {
				cartas.Add(
					creacion.CrearCarta(
						jugador,
						carta.cartaID,
						$"J{jugador}_POS{posicion}_ID{carta.cartaID}",
						Vector3.zero,
						campo,
						carta.rareza,
						carta.imagen,
						protector
					)
				);
				posicion++;
			}

			return cartas;
		}


	}

}