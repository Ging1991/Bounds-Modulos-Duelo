using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Ging1991.Animaciones;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Pila;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Efectos {

	public class EfectoEnvenenar : IEfecto {

		private readonly int jugador;
		private readonly GameObject fuente;
		private readonly List<string> etiquetas;

		public EfectoEnvenenar(GameObject fuente, int jugador, List<string> etiquetas = null) {
			this.fuente = fuente;
			this.jugador = jugador;
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

			CondicionClase condicionCriatura = new CondicionClase(clase: "CRIATURA");
			List<GameObject> criaturas = condicionCriatura.CumpleLista(fisica.TraerCartasEnCampo(jugador));

			foreach (GameObject criatura in criaturas) {
				CartaInfo info = criatura.GetComponent<CartaInfo>();
				//info.colocarHabilidad("envenenado");
				//criatura.GetComponentInChildren<EfectoVisual>().Animar("VENENO");
			}
		}


	}

}