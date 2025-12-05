using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Seleccion;
using Bounds.Duelo.Paneles;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.Pila;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Efectos {

	public class EfectoBusqueda : EfectoBase {

		private readonly int jugador;
		private readonly int cantidad;
		private readonly List<Zonas> fuentes;
		private readonly Condicion condicion;

		public EfectoBusqueda(GameObject fuente, int jugador, int cantidad = 1,
				List<Zonas> fuentes = null, Condicion condicion = null) : base(fuente) {
			this.jugador = jugador;
			this.cantidad = cantidad;
			this.fuentes = fuentes;
			this.condicion = condicion;
		}


		public override void Resolver() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			Instanciador instanciador = GameObject.Find("Instanciador").GetComponent<Instanciador>();
			List<GameObject> opciones = ObtenerOpciones();

			if (opciones.Count > 0) {
				if (jugador == 2) {
					fisica.EnviarHaciaMano(opciones[0], 2);
				}
				else {
					PanelSeleccion panel = instanciador.CrearPanelSeleccionarCarta().GetComponent<PanelSeleccion>();
					//panel.Iniciar(
					//new SeleccionRecuperar(
					//		jugador, opciones:opciones), "Selecciona una carta para agregar a tu mano.");
					panel.AgregarOpciones(opciones);
				}
			}
		}


		private List<GameObject> ObtenerOpciones() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();

			List<GameObject> opciones = new List<GameObject>();
			if (fuentes.Contains(Zonas.MAZO))
				opciones.AddRange(fisica.TraerCartasEnMazo(jugador));
			if (fuentes.Contains(Zonas.DESCARTE))
				opciones.AddRange(fisica.TraerCartasEnCementerio(jugador));
			if (fuentes.Contains(Zonas.MATERIALES))
				opciones.AddRange(fisica.TraerCartasEnMateriales(jugador));
			return condicion.CumpleLista(opciones);
		}


	}

}