using System.Collections.Generic;
using Bounds.Duelo.Pila;
using UnityEngine;

namespace Bounds.Duelo.Fila {

	public class FilaFases : MonoBehaviour {

		public List<IFase> fases = new List<IFase>();

		public void Agregar(IFase fase) {
			fases.Add(fase);
			GetComponent<FilaReloj>().Reanudar();
		}


		public void Remover(IFase fase) {
			fases.Remove(fase);
		}


		public bool EstaVacia() {
			return fases.Count == 0;
		}


		private bool EstaLaPilaVacia() {
			return GameObject.FindAnyObjectByType<PilaEfectos>().EstaVacia();
		}


		public void Resolver() {
			if (EstaLaPilaVacia()) {
				if (!EstaVacia()) {
					IFase efecto = fases[0];
					efecto.Resolver();
					Remover(efecto);

				}
				else {
					GetComponent<FilaReloj>().Pausar();
				}
			}
		}


	}

}