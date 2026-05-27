using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Pila;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Efectos {

	public class EfectoTrueno : IEfecto {

		private readonly GameObject fuente;
		private readonly List<string> etiquetas;

		public EfectoTrueno(GameObject fuente, List<string> etiquetas = null) {
			this.fuente = fuente;
			this.etiquetas = (etiquetas != null) ? etiquetas : new List<string>();
		}


		public List<string> GetEtiquetas() {
			return etiquetas;
		}


		public GameObject GetFuente() {
			return fuente;
		}


		public void Resolver() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			List<GameObject> cartas = new List<GameObject>(fisica.TraerCartasEnCampo(1));

			foreach (GameObject carta in cartas) {
				CartaInfo info = carta.GetComponent<CartaInfo>();
				CartaTipo cartaTipo = carta.GetComponent<CartaTipo>();
				if (info.original.clase == "CRIATURA" && cartaTipo.ContieneTipo("trueno")) {
					info.ColocarContador("poder", 1);
					CartaFX fx = carta.GetComponent<CartaFX>();
					fx.GanarPuntaje(500);
				}
				if (info.original.clase == "CRIATURA" && !cartaTipo.ContieneTipo("trueno")) {
					info.ColocarContador("debilidad", 1);
					CartaFX fx = carta.GetComponent<CartaFX>();
					fx.PerderPuntaje(500);
				}
			}
			cartas = new List<GameObject>(fisica.TraerCartasEnCampo(2));
			foreach (GameObject carta in cartas) {
				CartaInfo info = carta.GetComponent<CartaInfo>();
				CartaTipo cartaTipo = carta.GetComponent<CartaTipo>();
				if (info.original.clase == "CRIATURA" && cartaTipo.ContieneTipo("trueno")) {
					info.ColocarContador("poder", 1);
					CartaFX fx = carta.GetComponent<CartaFX>();
					fx.GanarPuntaje(500);
				}
				if (info.original.clase == "CRIATURA" && !cartaTipo.ContieneTipo("trueno")) {
					info.ColocarContador("debilidad", 1);
					CartaFX fx = carta.GetComponent<CartaFX>();
					fx.PerderPuntaje(500);
				}
			}
		}


	}

}