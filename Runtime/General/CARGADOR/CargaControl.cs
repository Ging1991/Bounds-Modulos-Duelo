using System.Collections;
using Bounds.Persistencia;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Bounds.Duelo {

	public class CargaControl : MonoBehaviour {


		void Start() {
			StartCoroutine(Cargar());
			GlobalDuelo global = GlobalDuelo.GetInstancia();
			LectorAvatar lector = LectorAvatar.GetInstancia();
			GameObject avatarEnemigo = GameObject.Find("AvatarEnemigo");
			//avatarEnemigo.GetComponent<Image>().sprite = lector.GetAvatar(global.jugadorMiniatura2);
		}


		IEnumerator Cargar() {
			AsyncOperation operacion = SceneManager.LoadSceneAsync("Duelo");
			while (!operacion.isDone) {
				yield return null;
			}
		}

	}
	
}