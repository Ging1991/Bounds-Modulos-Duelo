using Bounds.Duelo.Carta;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Emblemas.Fases {

	public class EmblemaFinalDelCombate {

		public static void Final(GameObject atacante, GameObject atacado) {
			Seleccionador seleccionador = Seleccionador.Instancia;
			seleccionador.SeleccionarParaCombate();

			if (atacante.GetComponent<CartaEfecto>().TieneClave("FILOSO_N")) {
				CartaInfo infoAtacado = atacado.GetComponent<CartaInfo>();
				if (infoAtacado.original.clase == "CRIATURA" && new SubCartasControladas(infoAtacado.controlador).Generar().Contains(atacado)) {
					EmblemaEfectos.Activar(
						new EfectoSobreCarta(atacante, new SubColocarContador("debilidad", 2), atacado)
					);
				}
			}

			if (atacante.GetComponent<CartaEfecto>().TieneClave("OLVIDO")) {
				CartaInfo infoAtacado = atacado.GetComponent<CartaInfo>();
				if (infoAtacado.original.clase == "CRIATURA" && new SubCartasControladas(infoAtacado.controlador).Generar().Contains(atacado)) {
					EmblemaEfectos.Activar(
						new EfectoSobreCarta(atacante, new SubReciclar(), atacado)
					);
				}
			}

			ActivarVacios(atacante, atacado);
		}

		private static void ActivarVacios(GameObject atacante, GameObject atacado) {

			Fisica fisica = Fisica.Instancia;
			if (fisica.TraerCartasEnCampo(atacado.GetComponent<CartaInfo>().controlador).Contains(atacado)) {
				foreach (GameObject vacio in new SubCartasControladas(0, new CondicionClase("VACIO")).Generar()) {
					CartaInfo infoVacio = vacio.GetComponent<CartaInfo>();
					if (infoVacio.original.datoVacio.tipo == "MAÑANA")
						EmblemaEfectos.Activar(new EfectoSobreCarta(vacio, new SubColocarContador("debilidad", 2), atacado));
				}
			}
		}

	}

}