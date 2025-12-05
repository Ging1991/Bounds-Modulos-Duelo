using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Utiles;
using UnityEngine;

namespace Bounds.Persistencia.Carta {

	public class CondicionMapper : MonoBehaviour {
		
		public static CondicionCarta GenerarCondicion(DatoBloqueCondicion datoBloque, int jugador = -1) {
			if (datoBloque == null || datoBloque.condicion == null)
				return null;
			
			if (datoBloque.condicion.tipo != "" && datoBloque.condicion.tipo != null) {
				return GenerarCondicion(datoBloque.condicion, jugador);
			}
			if (datoBloque.condicionesY != null && datoBloque.condicionesY.Count > 0) {
				CondicionMultiple condicionMultiple = new CondicionMultiple(CondicionMultiple.Tipo.Y);
				foreach(DatoCondicion condicion in datoBloque.condicionesY) {
					condicionMultiple.AgregarCondicion(GenerarCondicion(condicion, jugador));
				}
				return condicionMultiple;
			}
			if (datoBloque.condicionesO != null && datoBloque.condicionesO.Count > 0) {
				CondicionMultiple condicionMultiple = new CondicionMultiple(CondicionMultiple.Tipo.O);
				foreach(DatoCondicion condicion in datoBloque.condicionesO) {
					condicionMultiple.AgregarCondicion(GenerarCondicion(condicion, jugador));
				}
				return condicionMultiple;
			}
			return null;
		}


		public static CondicionCarta GenerarCondicion(DatoCondicion condicion, int jugador = -1) {
			
			if (condicion.tipo == "nivel") {
				return new CondicionNivel(nivel:condicion.nivel, minimo:condicion.minimo, maximo:condicion.maximo);
			}
			if (condicion.tipo == "clase") {
				return new CondicionClase(clase:condicion.clase, clases:condicion.clases);
			}
			if (condicion.tipo == "tipoCriatura") {
				return new CondicionTipoCriatura(tipo:condicion.tipoCriatura, tipos:condicion.tiposCriatura);
			}
			if (condicion.tipo == "criaturaPerfeccion") {
				return new CondicionCriaturaPerfeccion(perfeccion:condicion.perfeccion, perfecciones:condicion.perfecciones);
			}
			if (condicion.tipo == "perfeccion") {
				return new CondicionPerfeccion(soloImperfectos:condicion.soloImperfectos);
			}
			if (condicion.tipo == "esPerfecta") {
				return new CondicionEsPerfecta(esPerfecta:condicion.esPerfecta);
			}
			if (condicion.tipo == "estadisticaATK") {
				return new CondicionAtaque(minimo:condicion.minimo, maximo:condicion.maximo);
			}
			if (condicion.tipo == "estadisticaDEF") {
				return new CondicionDefensa(minimo:condicion.minimo, maximo:condicion.maximo);
			}
			if (condicion.tipo == "jugador") {
				int controlador = -1;
				if (jugador != -1)
					controlador = (condicion.controlador == "ADVERSARIO") ? JugadorDuelo.Adversario(jugador) : jugador;

				return new CondicionJugador(controlador:controlador);
			}
			return null;
		}


	}

}