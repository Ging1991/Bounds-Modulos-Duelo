using Bounds.Duelo.Emblema;
using Bounds.Duelo.Emblemas.Fases;
using Bounds.Duelo.Fila.Fases.Subfases;
using UnityEngine;

namespace Bounds.Duelo.Fila.Fases {

	public class FaseMantenimiento : IFase {

		private readonly int jugador;

		public FaseMantenimiento(int jugador) {
			this.jugador = jugador;
		}

		void IFase.Resolver() {
			EmblemaMantenimiento.ActivarEfectos(jugador);
			FilaFases fila = GameObject.FindAnyObjectByType<FilaFases>();

			if (EmblemaEnderezarPermanentes.TraerCartas(jugador).Count > 0)
				fila.Agregar(new SubfaseEnderezar(jugador));

			fila.Agregar(new SubfaseRobo(jugador));
			fila.Agregar(new SubfaseInvocaciones(jugador));
		}

	}

}