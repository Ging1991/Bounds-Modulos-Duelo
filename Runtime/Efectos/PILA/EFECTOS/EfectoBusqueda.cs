using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Seleccion;
using Bounds.Duelo.Paneles;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.Pila;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Duelo.Condiciones;

namespace Bounds.Duelo.Efectos {

	public class EfectoBusqueda : EfectoBase {

		private readonly int jugador;
		private readonly int cantidad;
		private readonly List<Zonas> fuentes;
		private readonly CondicionCarta condicion;

		public EfectoBusqueda(GameObject fuente, int jugador, int cantidad = 1,
				List<Zonas> fuentes = null, CondicionCarta condicion = null) : base(fuente) {
			this.jugador = jugador;
			this.cantidad = cantidad;
			this.fuentes = fuentes;
			this.condicion = condicion;
		}


		public override void Resolver() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			List<GameObject> opciones = ObtenerOpciones();

			if (opciones.Count > 0) {
				fisica.EnviarHaciaMano(opciones[0], jugador);
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