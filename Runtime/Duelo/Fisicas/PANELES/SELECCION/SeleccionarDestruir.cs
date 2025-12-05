using Bounds.Duelo.Emblemas;
using UnityEngine;

namespace Bounds.Duelo.Paneles.Seleccion {

	public class SeleccionarDestruir : ISeleccionarCarta {

		public void Seleccionar(GameObject carta) {
			EmblemaDestruccion.DestruirPorEfectos(carta);
		}

	}

}