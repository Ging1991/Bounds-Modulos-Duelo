using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaReciclar : EmblemaPadre {

		public static bool Reciclar(GameObject carta) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();

			fisica.EnviarHaciaMazo(carta, info.propietario);
			info.restablecer();
			carta.GetComponent<CartaMovimiento>().Enderezar();

			CartaGeneral componente = carta.GetComponent<CartaGeneral>();
			componente.ColocarBocaAbajo();

			// AURAS Y EQUIPOS
			List<GameObject> cartas = new List<GameObject>();
			List<GameObject> cartasDestruir = new List<GameObject>();
			cartas.AddRange(fisica.TraerCartasEnCampo(1));
			cartas.AddRange(fisica.TraerCartasEnCampo(2));

			foreach (GameObject anexo in cartas) {
				CartaInfo infoAura = anexo.GetComponent<CartaInfo>();
				if (infoAura.original.clase == "AURA" && infoAura.criaturaEquipada == carta)
					cartasDestruir.Add(anexo);
				if (infoAura.original.clase == "EQUIPO" && infoAura.criaturaEquipada == carta)
					infoAura.criaturaEquipada = null;
			}

			foreach (GameObject aura in cartasDestruir) {
				EmblemaDestruccion.DestruccionContinua(aura);
			}

			ControlDuelo duelo = conocimiento.traerDuelo();
			duelo.HabilitarInvocacionPerfecta();
			return true;

		}


	}

}