using UnityEngine;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Cartas.Persistencia.Datos;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaParadoja : EmblemaPadre {

		public static void Aplicar(int jugador, GameObject carta, int material) {
			EmblemaTutor.Agregar(jugador, carta);
			CartaInfo info = carta.GetComponent<CartaInfo>();
			info.propietario = jugador;
			info.controlador = jugador;
			MaterialBD materialBD = new() {
				tipo = "CARTA_ID",
				parametroID = material
			};
			info.original.materiales = new() { materialBD };
		}


	}

}