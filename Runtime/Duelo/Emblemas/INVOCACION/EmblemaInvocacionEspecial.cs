using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaInvocacionEspecial : EmblemaPadre {

		public static void Invocar(int jugador, GameObject carta, GameObject lugar) {
			EmblemaInvocacion.Invocar(jugador, carta, lugar);
			ActivarTrampas(jugador, carta);
		}


		private static void ActivarTrampas(int jugador, GameObject carta) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();

			List<GameObject> cartasAdversario = fisica.TraerCartasEnCampo(Adversario(jugador));
			CartaInfo info = carta.GetComponent<CartaInfo>();
			if (info.original.clase == "CRIATURA" && info.original.datoCriatura.perfeccion == "MAGICO") {
				foreach (GameObject cartaAdversario in cartasAdversario) {
					CartaInfo infoAdversario = cartaAdversario.GetComponent<CartaInfo>();
					CartaGeneral scrAdversario = cartaAdversario.GetComponent<CartaGeneral>();
					if (infoAdversario.original.clase == "TRAMPA" && !scrAdversario.bocaArriba) {
						if (infoAdversario.original.datoTrampa.tipo == "destruye_prisma") {
							scrAdversario.ColocarBocaArriba();
							//EmblemaDestruccion.Destruir(carta, 0);
							EmblemaRobo.RobarCartas(infoAdversario.controlador, 1);
						}
					}
				}

			}


		}

	}


}