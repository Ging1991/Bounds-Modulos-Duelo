using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaSeleccionMaterial {

		public List<GameObject> cartaSeleccionadas = new List<GameObject>();
		public List<int> cartaSeleccionadasID = new List<int>();

		private static EmblemaSeleccionMaterial instancia;

		private EmblemaSeleccionMaterial() { }


		public static EmblemaSeleccionMaterial GetInstancia() {
			if (instancia == null)
				instancia = new EmblemaSeleccionMaterial();
			return instancia;
		}


		public bool Seleccionar(GameObject carta) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			EmblemaSeleccionInvocacionPerfecta perfecta = EmblemaSeleccionInvocacionPerfecta.GetInstancia();
			GameObject criatura = perfecta.cartaSeleccionada;

			if (criatura == null)
				return false;

			CartaInfo infoCriatura = criatura.GetComponent<CartaInfo>();

			if (turnos.fase != EmblemaTurnos.Fase.FASE_PRINCIPAL)
				return false;

			if (EstaCompleto())
				return false;

			// Debe pertenecer al jugador
			bool materialRef = info.controlador != turnos.jugadorActivo && infoCriatura.original.datoCriatura.perfeccion == "REFLEJO";
			if (info.controlador != turnos.jugadorActivo && !materialRef)
				return false;

			// no debe haber sido ya seleccionado por id
			if (cartaSeleccionadas.Contains(carta))
				return false;

			if (infoCriatura.original.datoCriatura.perfeccion == "GEMINIS" && info.original.clase != "CRIATURA")
				return false;

			if (infoCriatura.original.datoCriatura.perfeccion == "SAGRADO"
					|| infoCriatura.original.datoCriatura.perfeccion == "MAGICO"
					|| infoCriatura.original.datoCriatura.perfeccion == "GEMINIS"
					|| infoCriatura.original.datoCriatura.perfeccion == "ROMPECABEZAS"
					|| infoCriatura.original.datoCriatura.perfeccion == "EVOLUCION"
					|| infoCriatura.original.datoCriatura.perfeccion == "FANTASMA"
					|| infoCriatura.original.datoCriatura.perfeccion == "VECTOR"
					|| infoCriatura.original.datoCriatura.perfeccion == "FUSION") {

				List<MaterialBD> materiales = infoCriatura.original.materiales;
				bool esMaterial = false;
				foreach (var material in materiales) {

					if (material.tipo == "TIPO" && carta.GetComponent<CartaTipo>().ContieneTipo(material.parametroTipo)) {
						esMaterial = true;
					}

					if (material.tipo == "CLASE" && material.parametroClase == info.original.clase) {
						esMaterial = true;
					}

					if (material.tipo == "CARTA_ID" && CartaPerfeccion.ExtenderID(carta).Contains(material.parametroID)) {
						esMaterial = true;
					}

					if (material.tipo == "VECTOR_ATK" && material.parametroATK == info.calcularAtaque()) {
						esMaterial = true;
					}

					if (material.tipo == "VECTOR_DEF" && material.parametroDEF == info.calcularDefensa()) {
						esMaterial = true;
					}

				}
				if (!esMaterial)
					return false;

			}
			else if (infoCriatura.original.datoCriatura.perfeccion == "REFLEJO") {
				if (infoCriatura.controlador == info.controlador) {
					return false;
				}
			}
			else {

				bool esUnID = false;
				foreach (int i in CartaPerfeccion.ExtenderID(carta))
					esUnID = esUnID || (perfecta.materiales.Contains(i) && !cartaSeleccionadasID.Contains(i));

				if (!esUnID)
					return false;

			}

			foreach (int i in CartaPerfeccion.ExtenderID(carta))
				cartaSeleccionadasID.Add(i);
			cartaSeleccionadas.Add(carta);

			CartaFX fx = carta.GetComponent<CartaFX>();
			fx.seleccionar();

			if (EstaCompleto()) {
				EmblemaInvocacionPerfecta.Invocar(infoCriatura.controlador,
					EmblemaSeleccionInvocacionPerfecta.GetInstancia().cartaSeleccionada,
					cartaSeleccionadas
				);
				cartaSeleccionadas = new List<GameObject>();
				cartaSeleccionadasID = new List<int>();
			}
			return true;
		}


		public bool EstaCompleto() {

			EmblemaSeleccionInvocacionPerfecta perfecta = EmblemaSeleccionInvocacionPerfecta.GetInstancia();
			if (perfecta.cartaSeleccionada == null)
				return false;
			return perfecta.materialesOBJ.Count == cartaSeleccionadas.Count;
		}


		public void Deseleccionar() {
			foreach (GameObject carta in cartaSeleccionadas) {
				CartaFX fx = carta.GetComponent<CartaFX>();
				fx.deseleccionar();
			}
			cartaSeleccionadas = new List<GameObject>();
			cartaSeleccionadasID = new List<int>();
		}


	}

}