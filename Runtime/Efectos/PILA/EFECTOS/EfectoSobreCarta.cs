using Bounds.Duelo.Pila.Subefectos;
using UnityEngine;

namespace Bounds.Duelo.Pila.Efectos {

	public class EfectoSobreCarta : EfectoBase {

		private readonly ISubSobreCarta subefecto;
		private readonly GameObject carta;

		public EfectoSobreCarta(GameObject fuente, ISubSobreCarta subefecto, GameObject carta) : base(fuente) {
			this.carta = carta;
			this.subefecto = subefecto;
		}


		public override void Resolver() {
			subefecto.AplicarEfecto(carta);
		}


    }

}