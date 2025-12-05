using UnityEngine;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Pila;

namespace Bounds.Duelo.Efectos {

	public class EfectoCrearFicha : EfectoBase {

		private readonly int jugador;
		private readonly int cartaID;
		private readonly int cantidad;

		public EfectoCrearFicha(GameObject fuente, int jugador, int cartaID, int cantidad) : base(fuente) {
			this.jugador = jugador;
			this.cartaID = cartaID;
			this.cantidad = cantidad;
		}


		public override void Resolver() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			BuscadorCampo buscador = BuscadorCampo.getInstancia();
			CreacionDeCartas creador = GameObject.Find("Fisica").GetComponent<CreacionDeCartas>();

			for (var i = 0; i < cantidad; i++) {
				GameObject campoLibre = buscador.buscarCampoLibre(jugador);
				if (campoLibre != null) {
					GameObject campo = GameObject.Find("Cartas"+jugador);
					GameObject ficha = creador.CrearCarta(jugador, cartaID, $"J{jugador}_FICHA{cartaID}", Vector3.zero, campo, "N", "A");
					EmblemaInvocacionEspecial.Invocar(jugador, ficha, campoLibre);
				}
			}
			conocimiento.traerDuelo().HabilitarInvocacionPerfecta();
		}


    }

}