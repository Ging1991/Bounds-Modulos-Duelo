using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas;

namespace Bounds.Duelo.Emblema {

	public class EmblemaAtaqueDirecto {

		private static EmblemaAtaqueDirecto instancia;
		private int jugadorAtacado = 0;
		private GameObject auxiliar;
		private EmblemaAtaqueDirecto() {}
		

		public static EmblemaAtaqueDirecto GetInstancia(){
			if (instancia == null)
				instancia = new EmblemaAtaqueDirecto();
			return instancia;
		}
		

		public bool Atacar(GameObject carta, int jugadorAtacado) {
			this.jugadorAtacado = jugadorAtacado;
/*
			EmblemaSeleccionarAtacante atacante = EmblemaSeleccionarAtacante.GetInstancia();
			if (atacante.cartaSeleccionada == null)
				return false;

			CartaMovimiento movimiento = atacante.cartaSeleccionada.GetComponent<CartaMovimiento>();
			movimiento.girar();
			CartaFX fx = atacante.cartaSeleccionada.GetComponent<CartaFX>();
			fx.PotencialAtacante(false);

			auxiliar = atacante.cartaSeleccionada;
			atacante.Deseleccionar();

			ResolverAtaque();*/
			return true;
		}


		public void ResolverAtaque() {
			CartaInfo infoAtacante = auxiliar.GetComponent<CartaInfo>();
			EmblemaVida.DisminuirVida(jugadorAtacado, infoAtacante.original.nivel * 100);

			GameObject cpu = GameObject.FindGameObjectWithTag("cpu");
			if (cpu != null) {
				//CPU scr = cpu.GetComponent<CPU>();
				//scr.Continuar();
			}

		}

	}

}