using System.Collections.Generic;
using System.Linq;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubInvocacionDesdeCementerio : ISubSobreCarta {

		private int jugador;

		public SubInvocacionDesdeCementerio(int jugador) {
			this.jugador = jugador;
		}

		private bool EsMaterialOBJ(GameObject carta, MaterialBD material) {
			if (material.tipo == "CLASE" && carta.GetComponent<CartaInfo>().original.clase == material.parametroClase)
				return true;
			if (material.tipo == "TIPO" && carta.GetComponent<CartaTipo>().ContieneTipo(material.parametroTipo))
				return true;
			if (material.tipo == "CARTA_ID" && CartaPerfeccion.ExtenderID(carta).Contains(material.parametroID))
				return true;
			if (material.tipo == "VECTOR_ATK" && carta.GetComponent<CartaInfo>().calcularAtaque() == material.parametroATK)
				return true;
			if (material.tipo == "VECTOR_DEF" && carta.GetComponent<CartaInfo>().calcularDefensa() == material.parametroDEF)
				return true;
			return false;
		}


		public void AplicarEfecto(GameObject carta) {
			CartaInfo cartaInfo = carta.GetComponent<CartaInfo>();
			GameObject lugarLibre = BuscadorCampo.getInstancia().buscarCampoLibre(cartaInfo.controlador);
			List<GameObject> cartasEnCementerio = new SubCartasEnCementerio(jugador).Generar();
			List<GameObject> materialesUsados = new();
			foreach (MaterialBD materialBD in cartaInfo.original.materiales) {
				foreach (GameObject cartaEnCementerio in cartasEnCementerio) {
					if (!materialesUsados.Contains(cartaEnCementerio) && EsMaterialOBJ(cartaEnCementerio, materialBD)) {
						materialesUsados.Add(cartaEnCementerio);
						break;
					}
				}
			}

			if (lugarLibre != null && materialesUsados.Count == cartaInfo.original.materiales.Count) {
				EmblemaInvocacionPerfecta.Invocar(jugador, carta, materialesUsados);
			}
			else {
				ControlDuelo.Instancia.gestorDeSonidos.ReproducirSonido("FxRebote");
			}

		}


	}

}