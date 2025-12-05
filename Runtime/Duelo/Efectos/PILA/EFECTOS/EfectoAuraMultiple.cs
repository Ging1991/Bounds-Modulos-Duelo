using UnityEngine;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas.Jugar;

namespace Bounds.Duelo.Efectos {

	public class EfectoAuraMultiple : EfectoBase {

		private readonly GameObject criatura;

		public EfectoAuraMultiple(GameObject fuente, GameObject criatura) : base(fuente) {
			this.criatura = criatura;
		}


		public override void Resolver() {
			int jugador = fuente.GetComponent<CartaInfo>().controlador;
			int cartaID = fuente.GetComponent<CartaInfo>().cartaID;
			BuscadorCampo buscador = BuscadorCampo.getInstancia();
			GameObject campoLibre = buscador.buscarCampoLibre(jugador);

			if (campoLibre != null) {
				CreacionDeCartas creador = GameObject.Find("Fisica").GetComponent<CreacionDeCartas>();
				GameObject campo = GameObject.Find($"Cartas{jugador}");
				GameObject copia = creador.CrearCarta(jugador, cartaID, $"J{jugador}_FICHA{cartaID}", Vector3.zero, campo, "N", "A");

				EmblemaJuegoSeleccionar.Seleccionar(copia, forzarSeleccion: true);
				EmblemaJuegoSeleccionar.SeleccionarParaVincular(criatura);
				EmblemaJuegoJugarAura.Jugar(jugador, campoLibre);
				EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
				copia.GetComponent<CartaGeneral>().ColocarBocaArriba();
				conocimiento.traerDuelo().HabilitarInvocacionPerfecta();
			}
		}


	}

}