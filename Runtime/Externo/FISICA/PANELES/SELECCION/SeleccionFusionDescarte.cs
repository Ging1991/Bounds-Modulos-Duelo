using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas;
using System.Collections.Generic;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;
using Bounds.Fisicas.Campos;

namespace Bounds.Duelo.Paneles.Seleccion {

	public class SeleccionFusionDescarte : ISeleccionarCarta {

		public int jugador;

		public SeleccionFusionDescarte(int jugador) {
			this.jugador = jugador;
		}

		public void Seleccionar(GameObject carta) {
			GameObject campo = BuscadorCampo.getInstancia().buscarCampoLibre(jugador);

			if (campo != null && campo.GetComponent<CampoLugar>().carta == null) {
				CartaInfo info = carta.GetComponent<CartaInfo>();
				Fisica fisica = Fisica.Instancia;
				List<GameObject> materiales = new();

				foreach (var material in info.original.materiales) {
					foreach (GameObject cartaDescarte in fisica.TraerCartasEnCementerio(jugador)) {
						CartaInfo infoDescarte = cartaDescarte.GetComponent<CartaInfo>();
						if (infoDescarte.original.cartaID == material.parametroID) {
							materiales.Add(cartaDescarte);
							break;
						}
					}
				}
				EmblemaInvocacionPerfecta.Invocar(jugador, carta, materiales);
			}
			else {
				//EfectosDeSonido.Tocar("FxRebote");
			}
		}

	}

}