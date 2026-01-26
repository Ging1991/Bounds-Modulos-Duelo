using UnityEngine;
using Bounds.Duelo.Carta;
using System;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Emblema {

	public class EmblemaDesequipar {

		public static bool Desequipar(GameObject carta) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();

			if (carta == null)
				return false;

			// Debe ser un aura o equipo
			if (info.original.clase != "EQUIPO" && info.original.clase != "AURA")
				return false;

			// Debe estar en el campo
			if (!fisica.TraerCartasEnCampo(info.controlador).Contains(carta))
				return false;

			GameObject criaturaEquipada = info.criaturaEquipada;
			if (criaturaEquipada == null)
				return false;

			CartaInfo infoCriatura = criaturaEquipada.GetComponent<CartaInfo>();

			info.criaturaEquipada = null;
			try {
				//infoCriatura.removerBonoAtaque(carta, info.original.datoEquipo.bono_ataque);
				//infoCriatura.removerBonoDefensa(carta, info.original.datoEquipo.bono_defensa);
			}
			catch (Exception e) {
				Debug.Log(e.StackTrace);
			}

			// si es aura ademas le saco la habilidad
			if (info.original.clase == "AURA") {
				//infoCriatura.removerHabilidad(info.original.datoAura.habilidad);
			}

			// enderezo el equipo
			CartaMovimiento movimiento = carta.GetComponent<CartaMovimiento>();
			movimiento.Enderezar();

			// actualizo el visor
			//Visor visor = GameObject.Find("Visor").GetComponent<Visor>();
			//visor.Mostrar(criaturaEquipada);

			return true;
		}


	}

}