using System.Collections.Generic;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public abstract class CondicionCarta {

		protected List<CondicionCarta> precondiciones;


		public abstract bool Cumple(GameObject carta);


		protected bool CumplePrecondiciones(GameObject carta) {
			bool cumple = true;
			foreach (CondicionCarta condicion in precondiciones)
				cumple = cumple && condicion.Cumple(carta);
			return cumple;
		}


		public List<GameObject> CumpleLista(List<GameObject> cartas) {
			List<GameObject> cumplen = new List<GameObject>();

			foreach (GameObject carta in cartas)
				if (Cumple(carta))
					cumplen.Add(carta);

			return cumplen;
		}


		public List<GameObject> NoCumpleLista(List<GameObject> cartas) {
			List<GameObject> cumplen = new List<GameObject>();

			foreach (GameObject carta in cartas)
				if (!Cumple(carta))
					cumplen.Add(carta);

			return cumplen;
		}


		protected class Acumulador {

			public bool valor;

			public Acumulador() {
				valor = true;
			}

			public void AgregarVerificacion(bool debeConsiderar, bool verificacion) {
				if (debeConsiderar)
					valor = valor && verificacion;
			}

		}


	}

}