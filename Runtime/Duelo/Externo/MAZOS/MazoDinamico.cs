
using System;
using UnityEngine;

namespace Bounds.Global.Mazos {

	public abstract class MazoDinamico : Mazo {

		private readonly string direccion;

		public MazoDinamico(string direccion, string codigo="") {
			this.direccion = direccion;
			LectorDinamico lector = new LectorDinamico(direccion);
			if (lector.ExistenDatos()) {
				try {
//					Debug.Log("Existe datos: " + direccion);
					Inicializar(lector.Leer());

				} catch (Exception e) {
					Debug.Log("Hubo un problema con el mazo: " + e.Message);
					InicializarDesdeRecursos(codigo);
				}
			} else {
				Debug.Log("2 - Mazo dinamico, iniciando desde recursos: " + direccion);
				InicializarDesdeRecursos(codigo);
			}
		}

		public abstract void InicializarDesdeRecursos(string codigo);


		public void Guardar() {
			LectorDinamico lector = new LectorDinamico(direccion);
			lector.Guardar(Serializar());
		}


	}

}