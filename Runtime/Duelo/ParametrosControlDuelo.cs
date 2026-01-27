using Bounds.Modulos.Persistencia;
using Ging1991.Persistencia.Direcciones;
using UnityEngine;

namespace Bounds.Modulos.Duelo {

	public class ParametrosControlDuelo : MonoBehaviour {

		public ParametrosEscena parametros;

		public void Inicializar() {
			if (parametros.inicializado == false) {
				parametros.Inicializar();
				parametros.SetDireccion("DIRECCION_NOMBRES", new DireccionRecursos("Cartas", "Nombres").Generar());
			}
		}

	}

}