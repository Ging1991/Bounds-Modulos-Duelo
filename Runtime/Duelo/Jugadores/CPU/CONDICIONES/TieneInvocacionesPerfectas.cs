using System;
using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.CPU.Condiciones {

	public class TieneInvocacionesPerfectas : ICondicionDeJuego {

		private readonly int jugador;

		public TieneInvocacionesPerfectas(int jugador) {
			this.jugador = jugador;
		}


		public bool SeCumple() {
			return GetCartas().Count > 0;
		}


		public List<GameObject> GetCartas() {

			try {
				Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
				List<GameObject> cartas = new List<GameObject>();
				cartas.AddRange(fisica.TraerCartasEnMazo(jugador));
				cartas.AddRange(fisica.TraerCartasEnMano(jugador));
				cartas.AddRange(fisica.TraerCartasEnCementerio(jugador));

				List<GameObject> opciones = new List<GameObject>();

				foreach (GameObject carta in cartas) {
					try {
						if (carta.GetComponent<CartaPerfeccion>().PuedeInvocar()) {
							opciones.Add(carta);
						}
					}
					catch (Exception eCarta) {
						Debug.LogError("[GetCartas] Error evaluando carta " + carta?.name + ": " + eCarta);
					}
				}

				return opciones;
			}
			catch (Exception e) {
				Debug.LogError("[GetCartas] EXCEPCIÓN general: " + e);
				return new List<GameObject>();
			}
		}


	}

}