using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaEnviarMaterial {

		public static void EnviarMateriales(List<GameObject> materiales) {
			Fisica fisica = Fisica.Instancia;

			foreach (GameObject material in materiales) {
				EnviarMaterial(material, material.GetComponent<CartaInfo>().propietario);
			}
			ZonaJugador zona1 = ZonaJugador.GetInstancia("ZonaJugador1");
			zona1.SetMateriales(fisica.TraerCartasEnMateriales(1).Count);
			ZonaJugador zona2 = ZonaJugador.GetInstancia("ZonaJugador2");
			zona2.SetMateriales(fisica.TraerCartasEnMateriales(2).Count);

			CalcularPerfecciones(1);
			CalcularPerfecciones(2);
		}


		private static void EnviarMaterial(GameObject carta, int jugador) {

			EmblemaDesequipar.Desequipar(carta);
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			fisica.EnviarHaciaMateriales(carta, jugador);

			List<GameObject> cartasVinculadas = new List<GameObject>(fisica.TraerCartasEnCampo(jugador));
			foreach (GameObject cartaVinculada in cartasVinculadas) {
				CartaInfo infoVinculada = cartaVinculada.GetComponent<CartaInfo>();
				if (infoVinculada.original.clase == "AURA" && infoVinculada.criaturaEquipada == carta)
					infoVinculada.criaturaEquipada = null;
				if (infoVinculada.original.clase == "EQUIPO" && infoVinculada.criaturaEquipada == carta)
					infoVinculada.criaturaEquipada = null;
			}
		}


		private static void CalcularPerfecciones(int jugador) {
			Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
			List<GameObject> objetivos = new List<GameObject>(fisica.TraerCartasEnMazo(jugador));
			objetivos.AddRange(new List<GameObject>(fisica.TraerCartasEnMano(jugador)));
			objetivos.AddRange(new List<GameObject>(fisica.TraerCartasEnCampo(jugador)));
			objetivos.AddRange(new List<GameObject>(fisica.TraerCartasEnCementerio(jugador)));
			foreach (GameObject objetivo in objetivos) {
				objetivo.GetComponent<CartaPerfeccion>().CalcularPerfeccion();
			}
		}


	}

}