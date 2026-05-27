using UnityEngine;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Emblemas.Vinculos;
using Bounds.Duelo.Emblemas.Jugar;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Efectos {

	public class EfectoAuraPendulo : EfectoBase {


		public EfectoAuraPendulo(GameObject fuente) : base(fuente) { }


		public override void Resolver() {
			int jugador = fuente.GetComponent<CartaInfo>().controlador;
			BuscadorCampo buscador = BuscadorCampo.getInstancia();
			GameObject campoLibre = buscador.buscarCampoLibre(jugador);
			EmblemaConocimiento emblema = EmblemaConocimiento.getInstancia();
			Fisica fisica = emblema.traerFisica();
			CondicionClase condicionClase = new CondicionClase("CRIATURA");

			if (campoLibre != null) {
				foreach (var carta in condicionClase.CumpleLista(fisica.TraerCartasEnCampo(jugador))) {
					if (EmblemaVinculo.CumpleRestricciones(fuente, carta)) {
						EmblemaJuegoSeleccionar.Seleccionar(fuente);
						EmblemaJuegoSeleccionar.SeleccionarParaVincular(carta);
						EmblemaJuegoJugarAura.Jugar(jugador, campoLibre, true);
						EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
						conocimiento.traerDuelo().HabilitarInvocacionPerfecta();
						break;
					}
				}
			}
		}


	}

}