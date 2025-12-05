using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Condiciones;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Efectos {

	public abstract class EfectoAlien : EfectoBase {

		public EfectoAlien(GameObject fuente) : base(fuente) { }


		protected GameObject EnviarFusion() {
			int jugador = fuente.GetComponent<CartaInfo>().controlador;
			Fisica fisica = Fisica.Instancia;
			CondicionCarta condicionFusion = new CondicionCriaturaPerfeccion("FUSION");
			List<GameObject> cartasEnDescarte = condicionFusion.CumpleLista(fisica.TraerCartasEnCementerio(jugador));
			cartasEnDescarte.Remove(fuente);

			if (cartasEnDescarte.Count > 0) {
				fisica.EnviarHaciaMazo(cartasEnDescarte[0], jugador);
				CartaGeneral componente = cartasEnDescarte[0].GetComponent<CartaGeneral>();
				componente.ColocarBocaAbajo();
				return cartasEnDescarte[0];
			}
			return null;
		}


	}

}