using UnityEngine;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Emblemas;

namespace Bounds.Duelo.Efectos {

	public class EfectoCancelarEfecto : EfectoBase {

		private readonly IEfecto efecto;

		public EfectoCancelarEfecto(GameObject fuente, IEfecto efecto) : base(fuente) {
			this.efecto = efecto;
		}


		public override void Resolver() {
			EmblemaEfectos.Cancelar(efecto);
			EmblemaDestruccion.DestruirPorEfectos(efecto.GetFuente());
		}


	}

}