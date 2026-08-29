using System.Linq;
using Bounds.Duelo.Emblemas;
using Bounds.Fisicas.Carta;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubGuia : ISubSobreCarta {

		private string habilidad;

		public SubGuia(string habilidad) {
			this.habilidad = habilidad;
		}

		public void AplicarEfecto(GameObject carta) {
			CartaInfo cartaInfo = carta.GetComponent<CartaInfo>();
			GameObject objetivo = null;
			foreach (var cartaEnMazo in new SubCartasEnMazo(cartaInfo.controlador).Generar()) {
				CartaInfo cartaEnMazoInfo = cartaEnMazo.GetComponent<CartaInfo>();
				if (cartaEnMazoInfo.original.nivel != cartaInfo.original.nivel + 1)
					continue;
				if (cartaEnMazoInfo.original.clase != "CRIATURA")
					continue;
				if (cartaEnMazoInfo.original.datoCriatura.perfeccion != "BASICO")
					continue;
				if (cartaInfo.original.datoCriatura.tipos.Intersect(cartaEnMazoInfo.original.datoCriatura.tipos).Count<string>() > 0) {
					objetivo = cartaEnMazo;
				}
			}
			GameObject lugarLibre = BuscadorCampo.getInstancia().buscarCampoLibre(cartaInfo.controlador);
			if (objetivo != null && lugarLibre != null) {
				EmblemaInvocacionEspecial.Invocar(cartaInfo.controlador, objetivo, lugarLibre);
				if (habilidad == "FLORENA")
					EmblemaRobo.RobarCartas(cartaInfo.controlador, 2);
				if (habilidad == "LAUNIX")
					EmblemaVida.DisminuirVida(EmblemaPadre.Adversario(cartaInfo.controlador), 1000);
			}
			else {
				ControlDuelo.Instancia.gestorDeSonidos.ReproducirSonido("FxRebote");
			}
		}


	}

}