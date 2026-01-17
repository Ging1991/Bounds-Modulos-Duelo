using Ging1991.Interfaces.Contadores;
using UnityEngine;
using UnityEngine.UI;

namespace Bounds.Modulos.Duelo.Visuales {

	public class ContadorBounds : MonoBehaviour {
		public Sprite imgVeneno;
		public Sprite imgCristal;
		public Sprite imgSangre;
		public Sprite imgEscudo;
		public ContadorIcono icono;

		public void SetTipoVisor(string tipo) {
			if (tipo == "debilidad") {
				icono.SetColorFuente(Color.white);
				icono.SetDimensiones(140, 140);
				icono.SetImagen(imgSangre);
			}
			if (tipo == "supervivencia") {
				icono.SetColorFuente(Color.black);
				icono.SetDimensiones(100, 90);
				icono.SetImagen(imgEscudo);
			}
			if (tipo == "veneno") {
				icono.SetColorFuente(Color.magenta);
				icono.SetDimensiones(100, 90);
				icono.SetImagen(imgVeneno);
			}
			if (tipo == "poder") {
				icono.SetColorFuente(Color.blue);
				icono.SetDimensiones(100, 90);
				icono.SetImagen(imgCristal);
			}
			if (tipo == "mision") {
				icono.SetColorFuente(Color.blue);
				icono.SetDimensiones(100, 90);
				icono.SetImagen(imgCristal);
			}
		}

		public void SetTipo(string tipo) {
			if (tipo == "debilidad") {
				icono.SetColorFuente(Color.white);
				icono.SetDimensiones(100, 100);
				icono.SetImagen(imgSangre);
			}
			if (tipo == "supervivencia") {
				icono.SetColorFuente(Color.black);
				icono.SetDimensiones(70, 60);
				icono.SetImagen(imgEscudo);
			}
			if (tipo == "veneno") {
				icono.SetColorFuente(Color.magenta);
				icono.SetDimensiones(70, 75);
				icono.SetImagen(imgVeneno);
			}
			if (tipo == "poder") {
				icono.SetColorFuente(Color.blue);
				icono.SetDimensiones(65, 60);
				icono.SetImagen(imgCristal);
			}
			if (tipo == "mision") {
				icono.SetColorFuente(Color.blue);
				icono.SetDimensiones(65, 60);
				icono.SetImagen(imgCristal);
			}
		}

		public void SetCantidad(int cantidad) {
			GetComponentInChildren<Text>().text = "" + cantidad;
		}

	}

}