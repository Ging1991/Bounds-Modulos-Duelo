using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Pila;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Efectos {

	public class EfectoEncontrar : EfectoBase {

		private readonly int jugador;
		private readonly CondicionCarta condicion;

		public EfectoEncontrar(GameObject fuente, int jugador, CondicionCarta condicion) : base(fuente) {
			this.jugador = jugador;
			this.condicion = condicion;
		}


		public override void Resolver() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			List<GameObject> cartasEnMazo = new List<GameObject>(fisica.TraerCartasEnMazo(jugador));

			foreach (GameObject encontrada in condicion.CumpleLista(cartasEnMazo)) {
				fisica.EnviarHaciaMano(encontrada, jugador);
				if (jugador == 1) {
					CartaGeneral componente = encontrada.GetComponent<CartaGeneral>();
					componente.ColocarBocaArriba();
				}
			}

		}


	}

}