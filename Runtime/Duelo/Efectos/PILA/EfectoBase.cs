using System.Collections.Generic;
using UnityEngine;

namespace Bounds.Duelo.Pila {

	public abstract class EfectoBase : IEfecto {

		protected readonly GameObject fuente;
		protected readonly List<string> etiquetas;

		public EfectoBase(GameObject fuente) {
			this.fuente = fuente;
			etiquetas = new List<string>();
		}


		public void AgregarEtiqueta(string etiqueta) {
			etiquetas.Add(etiqueta);
		}


		public List<string> GetEtiquetas() {
			return etiquetas;
		}


		public GameObject GetFuente() {
			return fuente;
		}
		

        public abstract void Resolver();


    }

}