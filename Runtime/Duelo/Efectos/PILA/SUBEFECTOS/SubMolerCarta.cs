using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Fisicas.Carta;
using Ging1991.Animaciones;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubMolerCarta : ISubSobreCarta {

		public void AplicarEfecto(GameObject carta1) {
			BloqueJugador bloque = BloqueJugador.getInstancia("BloqueJugador" + carta1.GetComponent<CartaInfo>().controlador);
			//bloque.GetComponentInChildren<EfectoVisual>().Animar("VENENO");
			EmblemaDescartarCarta.Descartar(carta1);
		}

	}

}