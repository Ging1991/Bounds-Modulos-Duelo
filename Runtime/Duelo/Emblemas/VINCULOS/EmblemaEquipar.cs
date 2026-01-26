using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Emblemas.Vinculos;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Emblema {

	public class EmblemaEquipar {

		public GameObject cartaAtacada;

		private static EmblemaEquipar instancia;

		private EmblemaEquipar() { }


		public static EmblemaEquipar GetInstancia() {
			if (instancia == null)
				instancia = new EmblemaEquipar();
			return instancia;
		}


		public bool Seleccionar(GameObject carta) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();
			CartaInfo info = carta.GetComponent<CartaInfo>();
			EmblemaSeleccionarEquipar equipar = EmblemaSeleccionarEquipar.GetInstancia();
			GameObject equipo = equipar.cartaSeleccionada;

			// Debe haber seleccionado previamente un equipo
			if (equipo == null)
				return false;

			// Debe ser fase principal
			if (turnos.fase != EmblemaTurnos.Fase.FASE_PRINCIPAL)
				return false;

			// Debe ser una criatura
			if (info.original.clase != "CRIATURA")
				return false;

			// Debe pertenecer al jugador
			if (info.controlador != turnos.jugadorActivo)
				return false;

			// Debe estar en el campo
			if (!fisica.TraerCartasEnCampo(turnos.jugadorActivo).Contains(carta))
				return false;

			EquiparCriatura(equipo, carta);
			equipar.Deseleccionar();

			// actualizo el visor
			//Visor visor = GameObject.Find("Visor").GetComponent<Visor>();
			//visor.Mostrar(carta);
			ControlDuelo.Instancia.gestorDeSonidos.ReproducirSonido("FxEspadas");
			return true;
		}


		private void EquiparCriatura(GameObject equipo, GameObject criatura) {
			EmblemaVinculo.Vincular(equipo, criatura);
			equipo.GetComponent<CartaInfo>().criaturaEquipada = criatura;
			equipo.GetComponent<CartaMovimiento>().Girar();
			//criatura.GetComponent<CartaImagen>().ColocarBocaAbajo(true);
		}


	}

}