using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas.Jugar;
using Bounds.Modulos.Cartas.Persistencia.Datos;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaSeleccionInvocacionPerfecta {

		public GameObject cartaSeleccionada;
		public List<int> materiales = new List<int>();
		public List<MaterialBD> materialesOBJ = new List<MaterialBD>();
		public List<int> materiales_vector_atk = new List<int>();
		public List<int> materiales_vector_def = new List<int>();
		public List<string> materiales_geminis = new List<string>();

		private static EmblemaSeleccionInvocacionPerfecta instancia;

		private EmblemaSeleccionInvocacionPerfecta() { }


		public static EmblemaSeleccionInvocacionPerfecta GetInstancia() {
			if (instancia == null)
				instancia = new EmblemaSeleccionInvocacionPerfecta();
			return instancia;
		}


		public void Seleccionar(int jugador, GameObject carta) {

			CartaInfo info = carta.GetComponent<CartaInfo>();
			CartaPerfeccion cartaPerfeccion = carta.GetComponent<CartaPerfeccion>();


			// La carta tiene que ser una criatura
			if (info.original.clase != "CRIATURA")
				return;

			// La carta tiene que ser una perfecta
			if (!cartaPerfeccion.EsPerfeccionable())
				return;

			EmblemaJuegoSeleccionar.Deseleccionar();
			CartaFX fx = carta.GetComponent<CartaFX>();
			fx.seleccionar();
			cartaSeleccionada = carta;
			materialesOBJ = info.original.materiales;
		}


		public void Deseleccionar() {
			if (cartaSeleccionada != null) {
				CartaFX fx = cartaSeleccionada.GetComponent<CartaFX>();
				fx.deseleccionar();
			}
			cartaSeleccionada = null;
		}


	}

}