using System.Collections.Generic;
using Bounds.Duelo.Condiciones;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubCartasEnMazo : ISubListaDeCartas {

		private readonly int jugador;
		private readonly CondicionCarta condicion;
		private readonly int nPrimeras;

		public SubCartasEnMazo(int jugador, CondicionCarta condicion = null, int nPrimeras = -1) {
			this.jugador = jugador;
			this.condicion = condicion;
			this.nPrimeras = nPrimeras;
		}


		public List<GameObject> Generar() {
			Fisica fisica = GameObject.FindAnyObjectByType<Fisica>();
			List<GameObject> lista = new List<GameObject>();

			if (nPrimeras > 0) {

				if (jugador == 0 || jugador == 1)
					lista.AddRange(fisica.TraerSiguientesCartasEnMazo(1, nPrimeras));

				if (jugador == 0 || jugador == 2) {
					lista.AddRange(fisica.TraerSiguientesCartasEnMazo(2, nPrimeras));
				}

			}
			else {

				if (jugador == 0 || jugador == 1)
					lista.AddRange(fisica.TraerCartasEnMazo(1));

				if (jugador == 0 || jugador == 2)
					lista.AddRange(fisica.TraerCartasEnMazo(2));

			}

			if (condicion != null) {
				lista = condicion.CumpleLista(lista);
			}
			return lista;
		}


	}

}