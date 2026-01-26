using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Ging1991.Animaciones;
using Bounds.Duelo.Pila;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Efectos {

	public class EfectoEnvenenarCriatura : IEfecto {

		private readonly GameObject objetivo;
		private readonly GameObject fuente;
		private readonly List<string> etiquetas;

		public EfectoEnvenenarCriatura(GameObject fuente, GameObject objetivo, List<string> etiquetas = null) {
			this.fuente = fuente;
			this.objetivo = objetivo;
			this.etiquetas = (etiquetas != null) ? etiquetas : new List<string>();
		}


		public List<string> GetEtiquetas() {
			return etiquetas;
		}


		public GameObject GetFuente() {
			return fuente;
		}


		public void Resolver() {
			CartaInfo info = objetivo.GetComponent<CartaInfo>();
			//info.colocarHabilidad("envenenado");
			//objetivo.GetComponentInChildren<EfectoVisual>().Animar("VENENO");
		}


	}

}