using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Efectos {

	public class EfectoAlienEncuentraMateriales : EfectoAlien {

		public EfectoAlienEncuentraMateriales(GameObject fuente) : base(fuente) { }


		public override void Resolver() {

			GameObject fusion = EnviarFusion();
			if (fusion != null) {
				int jugador = fuente.GetComponent<CartaInfo>().controlador;
				List<MaterialBD> materiales = fusion.GetComponent<CartaInfo>().original.materiales;
				Fisica fisica = Fisica.Instancia;

				foreach (var material in materiales) {
					foreach (GameObject cartaMazo in fisica.TraerCartasEnMazo(jugador)) {
						if (cartaMazo.GetComponent<CartaInfo>().cartaID == material.parametroID) {
							fisica.EnviarHaciaMano(cartaMazo, jugador);
							if (jugador == 1) {
								CartaGeneral componente = cartaMazo.GetComponent<CartaGeneral>();
								componente.ColocarBocaArriba();
							}
							break;
						}
					}
				}
			}
		}


	}

}