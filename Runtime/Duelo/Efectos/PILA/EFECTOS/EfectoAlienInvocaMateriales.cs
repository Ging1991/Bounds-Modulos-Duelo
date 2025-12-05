using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;

namespace Bounds.Duelo.Efectos {

	public class EfectoAlienInvocaMateriales : EfectoAlien {

		public EfectoAlienInvocaMateriales(GameObject fuente) : base(fuente) { }


		public override void Resolver() {

			int jugador = fuente.GetComponent<CartaInfo>().controlador;
			GameObject fusion = EnviarFusion();
			GameObject lugar = BuscadorCampo.getInstancia().buscarCampoLibre(jugador);
			if (fusion != null && lugar != null) {
				List<MaterialBD> materiales = fusion.GetComponent<CartaInfo>().original.materiales;
				Fisica fisica = Fisica.Instancia;

				foreach (var material in materiales) {
					foreach (GameObject carta in fisica.TraerCartasEnMazo(jugador)) {
						if (carta.GetComponent<CartaInfo>().cartaID == material.parametroID) {
							EmblemaInvocacionEspecial.Invocar(jugador, carta, lugar);
							return;
						}
					}
				}
			}
		}


	}

}