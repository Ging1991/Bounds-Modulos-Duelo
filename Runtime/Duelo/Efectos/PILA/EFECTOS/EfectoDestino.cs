using UnityEngine;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Carta;
using System.Collections.Generic;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Efectos {

	public class EfectoDestino : EfectoBase {

		public EfectoDestino(GameObject fuente) : base(fuente) { }


		public override void Resolver() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			int jugador = fuente.GetComponent<CartaInfo>().controlador;
			foreach (var carta in new SubCartasEnMano(jugador).Generar()) {
				EmblemaDescartarCarta.Descartar(carta);
			}
			List<GameObject> cartasEnCampo = new SubCartasControladas(jugador).Generar();
			cartasEnCampo.Remove(fuente);
			foreach (var carta in cartasEnCampo) {
				EmblemaEnviarAlCementerio.Enviar(carta);
			}
			EmblemaRobo.RobarCartas(jugador, 5);
			CondicionCartaID condicion = new CondicionCartaID(497);
			int cantidad = new SubCartasEnMano(jugador, condicion).Generar().Count;
			if (cantidad > 0) {
				EmblemaEfectos.Activar(new EfectoSobreJugador(fuente, JugadorDuelo.Adversario(jugador), new SubModificarLP(-cantidad * 1000)));
			}
			else {
				EmblemaEfectos.Activar(new EfectoSobreJugador(fuente, jugador, new SubModificarLP(-2000)));
			}
		}


	}

}