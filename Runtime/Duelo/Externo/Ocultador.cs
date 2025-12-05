using UnityEngine;

namespace Caballero.Infraestructura {

	public class Ocultador : MonoBehaviour {


		public void OcultarObjeto(bool ocultar) {
			transform.GetChild(0).gameObject.SetActive(!ocultar);
		}


	}

}