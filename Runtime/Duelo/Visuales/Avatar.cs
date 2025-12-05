using UnityEngine;
using UnityEngine.UI;
using Bounds.Persistencia;
using Ging1991.Dialogos;

namespace Bounds.Tutorial {

	public class Avatar : MonoBehaviour {


		public void setAvatar(Personaje personaje) {
			Image imagen = GetComponent<Image>();
			imagen.sprite = LectorAvatar.GetInstancia().GetMiniatura(personaje);
		}


		public static Avatar getInstancia(string nombre) {
			GameObject instancia = GameObject.Find(nombre);
			return instancia.GetComponent<Avatar>();
		}


	}
}