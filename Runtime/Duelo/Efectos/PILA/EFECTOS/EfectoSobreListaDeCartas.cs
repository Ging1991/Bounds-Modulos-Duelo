using UnityEngine;
using Bounds.Duelo.Pila.Subefectos;

namespace Bounds.Duelo.Pila.Efectos {

	public class EfectoSobreListaDeCartas : EfectoBase {

		private readonly ISubSobreCarta subefecto;
		private readonly ISubListaDeCartas lista;

		public EfectoSobreListaDeCartas(GameObject fuente, ISubSobreCarta subefecto, ISubListaDeCartas lista) : base(fuente) {
			this.lista = lista;
			this.subefecto = subefecto;
		}


		public override void Resolver() {
			foreach (var carta in lista.Generar()) {
				subefecto.AplicarEfecto(carta);
			}
		}


    }

}