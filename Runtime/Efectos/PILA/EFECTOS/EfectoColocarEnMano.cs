using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Seleccion;
using Bounds.Duelo.Paneles;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.Pila;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Efectos {

	public class EfectoColocarEnMano : EfectoBase {

		private readonly int jugador;
		private readonly int carta_id;
		private readonly int cantidad;
		private readonly List<GameObject> opciones;
		private readonly bool descartarResto;

		public EfectoColocarEnMano(GameObject fuente, int jugador, int carta_id, int cantidad = 1,
				List<GameObject> opciones = null, bool descartarResto = false) : base(fuente) {

			this.jugador = jugador;
			this.carta_id = carta_id;
			this.cantidad = cantidad;
			this.opciones = opciones;
			this.descartarResto = descartarResto;
		}


		public override void Resolver() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			Instanciador instanciador = GameObject.Find("Instanciador").GetComponent<Instanciador>();
			if (opciones.Count > 0) {
				if (jugador == 2) {
					fisica.EnviarHaciaMano(opciones[0], 2);
				}
				else {
					PanelSeleccion panel = instanciador.CrearPanelSeleccionarCarta().GetComponent<PanelSeleccion>();
					//panel.Iniciar(
					//	new SeleccionRecuperar(
					//		jugador, descartarResto:this.descartarResto, opciones:this.opciones), "Selecciona una carta para agregar a tu mano.");
					//panel.AgregarOpciones(opciones);
				}
			}
		}


	}

}