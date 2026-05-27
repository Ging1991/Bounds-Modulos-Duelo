using UnityEngine;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Emblemas.Jugar;
using Bounds.Duelo.Paneles.Seleccion;

namespace Bounds.Duelo.Seleccion {

	public class SeleccionarCriaturaVinculada : ISeleccionarCarta {

		public int jugador;
		public GameObject aura, lugar1, lugar2;


		public SeleccionarCriaturaVinculada (int jugador, GameObject aura, GameObject lugar1, GameObject lugar2) {
			this.jugador = jugador;
			this.aura = aura;
			this.lugar1 = lugar1;
			this.lugar2 = lugar2;
		}


		public void Seleccionar(GameObject criatura) {
			EmblemaInvocacion.Invocar(jugador, criatura, lugar1);
			EmblemaJuegoSeleccionar.Seleccionar(aura);
			EmblemaJuegoSeleccionar.SeleccionarParaVincular(criatura);
			EmblemaJuegoJugarAura.Jugar(jugador, lugar2);
		}


	}
	
}