using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using System.Collections.Generic;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Condiciones;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Utiles {

	public class BuscadorCartas {

		private static BuscadorCartas instancia;

		private BuscadorCartas() { }


		public static BuscadorCartas GetInstancia() {
			if (instancia == null)
				instancia = new BuscadorCartas();
			return instancia;
		}


		public GameObject BuscarMayorAtaqueEnMano(int jugador) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();

			GameObject mayor = null;
			int ataque = -1;
			foreach (GameObject carta in fisica.TraerCartasEnMano(jugador)) {
				CartaInfo info = carta.GetComponent<CartaInfo>();
				if (info.original.clase == "CRIATURA" && info.calcularAtaque() > ataque) {
					ataque = info.calcularAtaque();
					mayor = carta;
				}
			}
			return mayor;
		}


		public GameObject BuscarMejorAtacante(int jugador) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();

			GameObject mayor = null;
			int ataque = -1;
			foreach (GameObject carta in fisica.TraerCartasEnCampo(jugador)) {
				CartaInfo info = carta.GetComponent<CartaInfo>();
				CartaEfecto cartaEfecto = carta.GetComponent<CartaEfecto>();

				if (cartaEfecto.TieneClave("ENCADENADO"))
					continue;

				CartaMovimiento movimiento = carta.GetComponent<CartaMovimiento>();
				if (!movimiento.estaGirado && info.calcularAtaque() > ataque && info.original.clase == "CRIATURA") {
					ataque = info.calcularAtaque();
					mayor = carta;
				}
			}
			return mayor;
		}


		public GameObject BuscarMejorAtacado(int jugador, int ataqueActual) {

			CondicionMultiple condicionObjetivos = new CondicionMultiple(CondicionMultiple.Tipo.O);
			condicionObjetivos.AgregarCondicion(new CondicionClase("CRIATURA"));
			condicionObjetivos.AgregarCondicion(new CondicionClase("EQUIPO"));

			List<GameObject> objetivosPotenciales = new SubCartasControladas(jugador, condicionObjetivos).Generar();

			List<GameObject> objetivosPotencialesConMuro = new();
			foreach (GameObject carta in objetivosPotenciales)
				if (carta.GetComponent<CartaEfecto>().TieneClave("MURO"))
					objetivosPotencialesConMuro.Add(carta);

			if (objetivosPotencialesConMuro.Count > 0)
				objetivosPotenciales = objetivosPotencialesConMuro;

			GameObject resultado = null;
			int mayorDefensa = -1;
			foreach (GameObject carta in objetivosPotenciales) {
				CartaInfo info = carta.GetComponent<CartaInfo>();
				if (info.calcularDefensa() > mayorDefensa && ataqueActual > info.calcularDefensa()) {
					mayorDefensa = info.calcularDefensa();
					resultado = carta;
				}
			}

			return resultado;
		}


	}

}