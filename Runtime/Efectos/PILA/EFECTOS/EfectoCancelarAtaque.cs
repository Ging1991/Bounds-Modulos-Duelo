using UnityEngine;
using Bounds.Duelo.Pila;

namespace Bounds.Duelo.Efectos {

	public class EfectoCancelarAtaque : EfectoBase {

		public EfectoCancelarAtaque(GameObject fuente) : base(fuente) { }


		public override void Resolver() {
			Seleccionador seleccionador = GameObject.FindAnyObjectByType<Seleccionador>();
			seleccionador.combateCancelado = true;
		}


	}

}