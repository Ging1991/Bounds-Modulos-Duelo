using Bounds.Duelo.Emblema;
using Bounds.Duelo.Emblemas;
using Bounds.Fisicas.Carta;
using UnityEngine;

namespace Bounds.Duelo.Pila.Subefectos {

	public class SubReflejar : ISubSobreCarta {

		public void AplicarEfecto(GameObject carta) {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			BuscadorCampo buscador = BuscadorCampo.getInstancia();
			CreacionDeCartas creador = GameObject.Find("Fisica").GetComponent<CreacionDeCartas>();
			CartaInfo info = carta.GetComponent<CartaInfo>();

			GameObject campoLibre = buscador.buscarCampoLibre(info.controlador);
			if (campoLibre != null && info.original.clase == "CRIATURA") {
				GameObject campo = GameObject.Find("Cartas" + info.controlador);
				GameObject ficha = creador.CrearCarta(info.controlador, info.cartaID, $"J{info.controlador}_FICHA{info.cartaID}", Vector3.zero, campo, info.rareza, info.imagen);
				//ficha.GetComponent<CartaGeneral>().ColocarBocaArriba();
				ficha.GetComponent<CartaGeneral>().ColocarBocaAbajo();
				ficha.GetComponent<CartaInfo>().original.datoCriatura.efectos.RemoveAll(efecto => efecto.clave == "REFLEJO");
				EmblemaInvocacionEspecial.Invocar(info.controlador, ficha, campoLibre);
			}
			conocimiento.traerDuelo().HabilitarInvocacionPerfecta();
		}

	}

}