using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Emblemas.Jugar;
using Bounds.Duelo.Emblemas.Vinculos;
using Bounds.Fisicas.Carta;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubPoseer : ISubSobreCarta {

		private GameObject cartaPoseedora;

		public SubPoseer(GameObject cartaPoseedora) {
			this.cartaPoseedora = cartaPoseedora;
		}

		public void AplicarEfecto(GameObject cartaPoseida) {
			int controlador = cartaPoseida.GetComponent<CartaInfo>().controlador;
			GameObject campoLibre = BuscadorCampo.getInstancia().buscarCampoLibre(controlador);
			if (campoLibre == null)
				return;

			if (cartaPoseedora.GetComponent<CartaInfo>().original.clase != "AURA") {
				cartaPoseedora.GetComponent<CartaInfo>().original.clase = "AURA";
				CartaInfo info = cartaPoseedora.GetComponent<CartaInfo>();
				cartaPoseedora.GetComponentInChildren<CartaGeneral>().Mostrar(
					info.cartaID,
					info.imagen,
					info.rareza,
					info.original.clase,
					info.original.clase,
					info.calcularAtaque(),
					info.calcularDefensa(),
					info.original.nivel
				);
				cartaPoseedora.GetComponent<CartaPerfeccion>().CalcularPerfeccion();
			}

			if (EmblemaVinculo.CumpleRestricciones(cartaPoseedora, cartaPoseida)) {
				EmblemaJuegoActivar.ColocarSobreElCampo(controlador, cartaPoseedora, campoLibre, "AURA");
				EmblemaVinculo.Vincular(cartaPoseedora, cartaPoseida);
				cartaPoseedora.GetComponent<CartaInfo>().criaturaEquipada = cartaPoseida;
			}

		}

	}

}