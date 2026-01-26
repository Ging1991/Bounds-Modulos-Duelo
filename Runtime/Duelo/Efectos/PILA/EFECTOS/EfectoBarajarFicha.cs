using UnityEngine;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Carta;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Efectos {

	public class EfectoBarajarFicha : EfectoBase {

		private readonly int jugador;
		private readonly int cartaID;
		private readonly int cantidad;

		public EfectoBarajarFicha(GameObject fuente, int jugador, int cartaID, int cantidad) : base(fuente) {
			this.jugador = jugador;
			this.cartaID = cartaID;
			this.cantidad = cantidad;
		}


		public override void Resolver() {
			CreacionDeCartas creador = GameObject.Find("Fisica").GetComponent<CreacionDeCartas>();
			for (var i = 0; i < cantidad; i++) {
				GameObject campo = GameObject.Find("Cartas" + jugador);
				GameObject ficha = creador.CrearCarta(jugador, cartaID, $"J{jugador}_FICHA{cartaID}", Vector3.zero, campo, "N", "A");
				ficha.GetComponent<CartaGeneral>().ColocarBocaArriba();
				Fisica.Instancia.EnviarHaciaMazo(ficha, jugador);
			}
			EmblemaMezclarMazo.Mezclar(jugador);
		}


	}

}