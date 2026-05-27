using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bounds.Duelo {
	
	public class CargaLigaControl : MonoBehaviour {


		void Start() {
			StartCoroutine(Cargar());
		}


		IEnumerator Cargar() {
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Liga");

			while (!asyncLoad.isDone) {
				yield return null;
			}

		}


	}

}