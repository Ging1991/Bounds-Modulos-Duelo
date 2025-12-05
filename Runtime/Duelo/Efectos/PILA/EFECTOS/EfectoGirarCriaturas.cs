using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Pila;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Efectos {

	public class EfectoGirarCriaturas : IEfecto {

		private readonly GameObject fuente;
		private readonly List<string> etiquetas;
		private readonly int jugador;

		public EfectoGirarCriaturas(GameObject fuente, int jugador, List<string> etiquetas = null) {
			this.fuente = fuente;
			this.jugador = jugador;
			this.etiquetas = (etiquetas != null) ? etiquetas : new List<string>();
		}


		public List<string> GetEtiquetas() {
			return etiquetas;
		}


		public GameObject GetFuente() {
			return fuente;
		}


		public void Resolver() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			List<GameObject> cartas = new List<GameObject>(fisica.TraerCartasEnCampo(jugador));
			foreach (GameObject carta in cartas) {
				CartaInfo info = carta.GetComponent<CartaInfo>();
				if (info.original.clase == "CRIATURA") {
					CartaMovimiento movimiento = carta.GetComponent<CartaMovimiento>();
					movimiento.Girar();
				}
			}
		}


	}

}