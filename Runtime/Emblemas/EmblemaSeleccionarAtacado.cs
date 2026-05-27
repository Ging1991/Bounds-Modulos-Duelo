using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Emblemas.Fases;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Emblema {

	public class EmblemaSeleccionarAtacado {

		public static bool Seleccionar(GameObject carta) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();

			if (Seleccionador.Instancia.atacante == null)
				return false;

			if (turnos.fase != EmblemaTurnos.Fase.FASE_DE_BATALLA)
				return false;

			CartaInfo info = carta.GetComponent<CartaInfo>();
			if (info.controlador == turnos.jugadorActivo)
				return false;

			if (info.original.clase != "CRIATURA" && info.original.clase != "EQUIPO")
				return false;

			bool tieneMuros = false;

			foreach (GameObject c in fisica.TraerCartasEnCampo(info.controlador))
				tieneMuros = tieneMuros || c.GetComponent<CartaEfecto>().TieneClave("MURO");

			if (tieneMuros && !info.GetComponent<CartaEfecto>().TieneClave("MURO"))
				return false;

			EmblemaDeclararAtaque.Declarar(GameObject.FindAnyObjectByType<Seleccionador>().atacante, carta);

			return true;
		}


	}

}