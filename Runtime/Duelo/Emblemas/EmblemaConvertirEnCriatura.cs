using UnityEngine;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Cartas;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaConvertirEnCriatura : EmblemaPadre {

		private static readonly float DIVISOR = 255f;

		public static void ConvertirID(GameObject carta, int cartaID) {
			CartaBD datos = ControlDuelo.Instancia.datosDeCartas.lector.LeerDatos(cartaID);
			CartaInfo info = carta.GetComponent<CartaInfo>();
			info.cargar(datos);
			carta.GetComponentInChildren<CartaFrente>().Mostrar(cartaID);
			info.cartaID = cartaID;
		}

		public static void Convertir(GameObject carta) {

			CartaInfo info = carta.GetComponent<CartaInfo>();
			info.original.datoCriatura = new CriaturaBD {
				ataque = info.calcularDefensa(),
				defensa = info.calcularDefensa(),
				perfeccion = "BASICO",
				tipos = new() { "GUERRERO" },
				efectos = new()
			};
			info.original.clase = "CRIATURA";
			info.original.efectos = new();
			info.RecalcularEstadisticas();
			info.cargar(info.original);
			carta.GetComponentInChildren<CartaFrente>().SetFondo(
				new Color(230 / DIVISOR, 110 / DIVISOR, 30 / DIVISOR),
				new Color(240 / DIVISOR, 180 / DIVISOR, 80 / DIVISOR)
			);
		}


	}

}