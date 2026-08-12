using UnityEngine;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Emblemas;

namespace Bounds.Duelo.Efectos {

	public class EfectoCancelarEfecto : EfectoBase {

		private readonly IEfecto efecto;
		private bool destruirFuente;

		public EfectoCancelarEfecto(GameObject fuente, IEfecto efecto, bool destruirFuente) : base(fuente) {
			this.efecto = efecto;
			this.destruirFuente = destruirFuente;
		}


		public override void Resolver() {
			EmblemaEfectos.Cancelar(efecto);
			if (destruirFuente)
				EmblemaDestruccion.DestruirPorEfectos(efecto.GetFuente());
		}


	}

}