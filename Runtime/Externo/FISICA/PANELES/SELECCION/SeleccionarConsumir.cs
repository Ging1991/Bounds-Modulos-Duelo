using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Paneles.Seleccion {

	public class SeleccionarConsumir : ISeleccionarCarta {

		private int jugador;
		private GameObject fuente;

		public SeleccionarConsumir(int jugador, GameObject fuente) {
			this.jugador = jugador;
			this.fuente = fuente;
		}

		public void Seleccionar(GameObject carta) {
			CartaInfo cartaInfo = carta.GetComponent<CartaInfo>();
			Fisica fisica = Fisica.Instancia;
			fisica.EnviarHaciaDescarte(carta, cartaInfo.controlador);
			EmblemaEfectos.Activar(new EfectoSobreJugador(fuente, jugador, new SubModificarLP(-1000)));
		}

	}

}