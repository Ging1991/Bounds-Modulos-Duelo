using UnityEngine;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Pila;

namespace Bounds.Duelo.Efectos {

	public class EfectoInvocacionEspecial : EfectoBase {

		private readonly GameObject criatura;
		private readonly int jugador;

		public EfectoInvocacionEspecial(GameObject fuente, GameObject criatura, int jugador) : base(fuente) {
			this.criatura = criatura;
			this.jugador = jugador;
		}


		public override void Resolver() {
			GameObject campo = BuscadorCampo.getInstancia().buscarCampoLibre(jugador);
			if (campo != null) {
				EmblemaInvocacionEspecial.Invocar(jugador, criatura, campo);
			} else {
				//EfectosDeSonido.Tocar("FxRebote");
			}
		}

    }

}