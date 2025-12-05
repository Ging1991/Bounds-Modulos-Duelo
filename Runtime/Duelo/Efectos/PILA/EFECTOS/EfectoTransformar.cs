using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.Paneles;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Paneles.Seleccion;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Efectos {

	public class EfectoTransformar : IEfecto, ISeleccionarCarta {

		private readonly GameObject fuente;
		private readonly List<string> etiquetas;
		private readonly int jugador;
		private readonly int fichaID;
		private readonly List<GameObject> opciones;

		public EfectoTransformar(GameObject fuente, int jugador, int fichaID, List<GameObject> opciones = null,
				List<string> etiquetas = null) {

			this.fuente = fuente;
			this.jugador = jugador;
			this.fichaID = fichaID;
			this.opciones = opciones;
			this.etiquetas = etiquetas ?? new List<string>();
		}


		public List<string> GetEtiquetas() {
			return etiquetas;
		}


		public GameObject GetFuente() {
			return fuente;
		}


		public void Resolver() {
			Instanciador instanciador = GameObject.Find("Instanciador").GetComponent<Instanciador>();
			if (opciones.Count > 0) {
				if (jugador == 2) {
					Seleccionar(opciones[0]);
				}
				else {
					PanelSeleccion panel = instanciador.CrearPanelSeleccionarCarta().GetComponent<PanelSeleccion>();
					panel.Iniciar(this, "Selecciona una carta para sacrificar.");
					panel.AgregarOpciones(opciones);
				}
			}
		}


		public void Seleccionar(GameObject cartaSeleccionada) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			fisica.EnviarHaciaDescarte(cartaSeleccionada, jugador);
			PilaEfectos pila = GameObject.Find("Pila").GetComponent<PilaEfectos>();
			pila.Agregar(new EfectoCrearFicha(fuente, jugador, fichaID, 1));
			pila.Resolver();
		}


	}

}