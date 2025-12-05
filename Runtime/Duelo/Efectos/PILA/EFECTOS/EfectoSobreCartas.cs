using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Pila.Subefectos;

namespace Bounds.Duelo.Pila.Efectos {

	public class EfectoSobreCartas : EfectoBase {

		private readonly ISubSobreCarta subefecto;
		private readonly List<GameObject> cartas;

		public EfectoSobreCartas(GameObject fuente, ISubSobreCarta subefecto, List<GameObject> cartas) : base(fuente) {
			this.cartas = cartas;
			this.subefecto = subefecto;
		}


		public override void Resolver() {
			foreach (var carta in cartas) {
				subefecto.AplicarEfecto(carta);
			}
		}


    }

}