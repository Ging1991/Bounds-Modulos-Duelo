using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Duelo.Fisicas;
using Ging1991.Animaciones;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubPagoEnSangre : ISubSobreCarta {

		public void AplicarEfecto(GameObject carta) {
			CartaInfo info = carta.GetComponent<CartaInfo>();
			if (EmblemaVida.VidaActual(info.controlador) > 500)
				EmblemaVida.DisminuirVida(info.controlador, 500, "VENENO");
			else {
				Fisica fisica = GameObject.FindAnyObjectByType<Fisica>();
				fisica.EnviarHaciaDescarte(carta, info.controlador);
				//carta.GetComponentInChildren<EfectoVisual>().Animar("VENENO");
			}
		}


	}

}