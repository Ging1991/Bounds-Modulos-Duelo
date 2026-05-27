using System.Collections.Generic;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Utiles;
using UnityEngine;

namespace Bounds.Duelo.Efectos {

	public class EfectoGanarInvocacion : IEfecto {

		private readonly int jugador;
		private readonly int cantidad;
		private readonly GameObject fuente;
		private readonly List<string> etiquetas;


		public EfectoGanarInvocacion(GameObject fuente, int jugador, int cantidad, List<string> etiquetas = null) {
			this.fuente = fuente;
			this.jugador = jugador;
			this.cantidad = cantidad;
			this.etiquetas = (etiquetas != null) ? etiquetas : new List<string>();
		}

		public void Resolver() {
			JugadorDuelo scr = JugadorDuelo.GetInstancia(jugador);
			scr.invocaciones_normales += cantidad;
		}

		public List<string> GetEtiquetas() {
			return etiquetas;
		}


		public GameObject GetFuente() {
			return fuente;
		}


    }

}