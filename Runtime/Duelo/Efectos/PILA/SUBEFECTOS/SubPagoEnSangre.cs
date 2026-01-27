using Bounds.Duelo.Emblemas;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubPagoEnSangre : ISubSobreCarta {

		private readonly int COSTE = 300;

		public void AplicarEfecto(GameObject carta) {
			CartaInfo info = carta.GetComponent<CartaInfo>();
			if (EmblemaVida.VidaActual(info.controlador) > COSTE)
				EmblemaVida.DisminuirVida(info.controlador, COSTE, "VENENO");
			else {
				Fisica fisica = GameObject.FindAnyObjectByType<Fisica>();
				fisica.EnviarHaciaDescarte(carta, info.controlador);
				//carta.GetComponentInChildren<EfectoVisual>().Animar("VENENO");
			}
		}


	}

}