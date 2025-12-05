using System.Collections.Generic;
using Ging1991.Core;
using UnityEngine;

namespace Bounds.Modulos.Duelo.Fisicas {

	public class ListadorDeZonas : Listador<GameObject> {

		public enum Zona { MAZO, MANO, CAMPO, CEMENTERIO, MATERIALES }

		public override void Inicializar() {
			base.Inicializar();
			AgregarLista("MAZO1", new List<GameObject>());
			AgregarLista("MAZO2", new List<GameObject>());
			AgregarLista("MANO1", new List<GameObject>());
			AgregarLista("MANO2", new List<GameObject>());
			AgregarLista("CAMPO1", new List<GameObject>());
			AgregarLista("CAMPO2", new List<GameObject>());
			AgregarLista("CEMENTERIO1", new List<GameObject>());
			AgregarLista("CEMENTERIO2", new List<GameObject>());
			AgregarLista("MATERIALES1", new List<GameObject>());
			AgregarLista("MATERIALES2", new List<GameObject>());
		}


		public void AgregarCarta(GameObject carta, int jugador, Zona zona) {
			AgregarElemento(carta, $"{zona.ToString()}{jugador}");
		}


		public List<GameObject> GenerarListaNueva(int jugador, Zona zona) {
			return GetListaNueva($"{zona.ToString()}{jugador}");
		}


		public List<GameObject> GenerarLista(int jugador, Zona zona) {
			return GetLista($"{zona.ToString()}{jugador}");
		}


		public GameObject SiguienteCarta(int jugador, Zona zona) {
			return SiguienteElemento($"{zona.ToString()}{jugador}");
		}


		public GameObject SiguienteCarta(int jugador, Zona zona, int posicion) {
			return SiguienteElemento($"{zona.ToString()}{jugador}", posicion);
		}


	}

}