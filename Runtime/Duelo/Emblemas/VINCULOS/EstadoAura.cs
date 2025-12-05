using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Emblemas;
using Bounds.Modulos.Duelo.Fisicas;

public class EstadoAura {

	// revisa todas las auras en el campo y destruye aquellas que no esten equipando a una criatura en el campo
	public static void RevisarEstado() {
		EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
		Fisica fisica = conocimiento.traerFisica();

		List<GameObject> cartas = new List<GameObject>(fisica.TraerCartasEnCampo(1));
		foreach (GameObject carta in cartas) {
			CartaInfo info = carta.GetComponent<CartaInfo>();
			if (info.original.clase == "AURA" && info.criaturaEquipada == null)
				EmblemaDestruccion.DestruccionContinua(carta);
		}

		cartas = new List<GameObject>(fisica.TraerCartasEnCampo(2));
		foreach (GameObject carta in cartas) {
			CartaInfo info = carta.GetComponent<CartaInfo>();
			if (info.original.clase == "AURA" && info.criaturaEquipada == null)
				EmblemaDestruccion.DestruccionContinua(carta);
		}

	}

}