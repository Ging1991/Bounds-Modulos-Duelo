using UnityEngine;
using UnityEngine.UI;

namespace carta {

	public class Contador : MonoBehaviour {
		public Sprite img_amarillo;
		public Sprite img_morado;
		public Sprite img_azul;
		public Sprite img_rojo;

		public void setTipoContador(string tipo) {
			if (tipo == "supervivencia")
				GetComponent<Image>().sprite = img_amarillo;
			if (tipo == "veneno")
				GetComponent<Image>().sprite = img_morado;
			if (tipo == "poder")
				GetComponent<Image>().sprite = img_azul;
			if (tipo == "debilidad")
				GetComponent<Image>().sprite = img_rojo;
			if (tipo == "mision")
				GetComponent<Image>().sprite = img_rojo;
		}

		public void setNumero(int cantidad) {
			GetComponentInChildren<Text>().text = "" + cantidad;
		}

	}

}