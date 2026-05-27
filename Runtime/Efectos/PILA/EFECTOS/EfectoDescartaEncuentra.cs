using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Paneles;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Paneles.Seleccion;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Efectos {

	public class EfectoDescartaEncuentra : EfectoBase, ISeleccionarCarta {

		private readonly int jugador;
		private readonly List<GameObject> opciones;
		public enum TipoEfecto {
			ENCUENTRA_MATERIALES_ID,
			ENCUENTRA_BASICOS_7MAS
		}
		private readonly TipoEfecto tipo;

		public EfectoDescartaEncuentra(GameObject fuente, int jugador, List<GameObject> opciones, TipoEfecto tipo) : base(fuente) {
			this.jugador = jugador;
			this.opciones = opciones;
			this.tipo = tipo;
		}


		public override void Resolver() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			Instanciador instanciador = GameObject.Find("Instanciador").GetComponent<Instanciador>();
			if (opciones.Count > 0) {
				if (jugador == 2) {
					fisica.EnviarHaciaDescarte(opciones[0], 2);
					EncontrarCartas(opciones[0], 2);

				}
				else {
					PanelSeleccion panel = instanciador.CrearPanelSeleccionarCarta().GetComponent<PanelSeleccion>();
					panel.Iniciar(this, "Selecciona una carta para descartar.");
					panel.AgregarOpciones(opciones);
				}
			}
		}


		public void Seleccionar(GameObject cartaSeleccionada) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			fisica.EnviarHaciaDescarte(cartaSeleccionada, 1);
			EncontrarCartas(cartaSeleccionada, 1);
		}


		private void EncontrarCartas(GameObject carta, int jugador) {/*
			if (tipo == TipoEfecto.ENCUENTRA_MATERIALES_ID) {
				List<int> materiales = carta.GetComponent<CartaInfo>().original.datoCriatura.materiales;
				EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
				Fisica fisica = conocimiento.traerFisica();
				List<GameObject> encontradas = new List<GameObject>();
				foreach (int cartaID in materiales) {
					foreach(GameObject cartaMazo in fisica.TraerCartasEnMazo(jugador)) {
						if (!encontradas.Contains(cartaMazo)){
							if (cartaMazo.GetComponent<CartaInfo>().cartaID == cartaID) {
								encontradas.Add(cartaMazo);
								break;
							}
						}
					}
				}
				foreach (GameObject encontrada in encontradas) {
					fisica.EnviarHaciaMano(encontrada, jugador);
					if (jugador == 1) {
						CartaGeneral componente = encontrada.GetComponent<CartaGeneral>();
						componente.ColocarBocaArriba();
					}
				}
			}*/

			if (tipo == TipoEfecto.ENCUENTRA_BASICOS_7MAS) {
			}

		}


	}

}