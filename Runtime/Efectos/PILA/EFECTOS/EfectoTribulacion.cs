using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Emblemas;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Efectos {

	public class EfectoTribulacion : IEfecto {

		private readonly GameObject fuente;
		private readonly List<string> etiquetas;
		private readonly int jugador;
		private readonly string tipo;

		public EfectoTribulacion(GameObject fuente, int jugador, string tipo, List<string> etiquetas = null) {
			this.fuente = fuente;
			this.jugador = jugador;
			this.tipo = tipo;
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
				CartaTipo cartaTipo = carta.GetComponent<CartaTipo>();
				if (cartaTipo.ContieneTipo(tipo)) {
					EmblemaRobo.RobarCartas(jugador, 1);
					break;
				}
			}
		}


	}

}