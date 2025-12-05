using System.Collections.Generic;
using Bounds.Duelo.Condiciones;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubCartasEnMano : ISubListaDeCartas {

		private readonly int jugador;
		private readonly CondicionCarta condicion;

		public SubCartasEnMano(int jugador, CondicionCarta condicion = null) {
			this.jugador = jugador;
			this.condicion = condicion;
		}


		public List<GameObject> Generar() {
			Fisica fisica = GameObject.FindAnyObjectByType<Fisica>();
			List<GameObject> lista = new List<GameObject>();
			if (jugador == 0 || jugador == 1) {
				lista.AddRange(fisica.TraerCartasEnMano(1));
			}
			if (jugador == 0 || jugador == 2) {
				lista.AddRange(fisica.TraerCartasEnMano(2));
			}
			if (condicion != null) {
				lista = condicion.CumpleLista(lista);
			}
			return lista;
		}


	}

}