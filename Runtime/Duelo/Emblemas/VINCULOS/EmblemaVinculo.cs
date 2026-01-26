using Bounds.Duelo.Carta;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Utiles;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using UnityEngine;

namespace Bounds.Duelo.Emblemas.Vinculos {

	public class EmblemaVinculo {

		public static bool CumpleRestricciones(GameObject cartaVinculante, GameObject cartaVinculada) {

			CartaEfecto vinculanteEfecto = cartaVinculante.GetComponent<CartaEfecto>();
			int jugador = cartaVinculante.GetComponent<CartaInfo>().controlador;
			int adversario = JugadorDuelo.Adversario(jugador);

			if (vinculanteEfecto.TieneClave("VINCULO_RESTRICCION_CONTROLADOR_JUGADOR")) {
				if (cartaVinculada.GetComponent<CartaInfo>().controlador != jugador)
					return false;
			}

			if (vinculanteEfecto.TieneClave("VINCULO_RESTRICCION_CONTROLADOR_ADVERSARIO")) {
				if (cartaVinculada.GetComponent<CartaInfo>().controlador != adversario)
					return false;
			}

			if (vinculanteEfecto.TieneClave("VINCULO_RESTRICCION_TIPO")) {
				string tipo = vinculanteEfecto.GetEfecto("VINCULO_RESTRICCION_TIPO").parametroTipo;
				if (!cartaVinculada.GetComponent<CartaTipo>().tipos.Contains(tipo))
					return false;
			}

			if (vinculanteEfecto.TieneClave("VINCULO_RESTRICCION_PERFECCION")) {
				string valor = vinculanteEfecto.GetEfecto("VINCULO_RESTRICCION_PERFECCION").parametroPerfeccion;
				if (cartaVinculada.GetComponent<CartaInfo>().original.datoCriatura.perfeccion != valor)
					return false;
			}

			if (vinculanteEfecto.TieneClave("VINCULO_RESTRICCION_NIVEL_MAXIMO")) {
				int valor = vinculanteEfecto.GetEfecto("VINCULO_RESTRICCION_NIVEL_MAXIMO").parametroNMaximo;
				if (cartaVinculada.GetComponent<CartaInfo>().original.nivel > valor)
					return false;
			}

			if (vinculanteEfecto.TieneClave("VINCULO_RESTRICCION_ATK_MAXIMO")) {
				int valor = vinculanteEfecto.GetEfecto("VINCULO_RESTRICCION_ATK_MAXIMO").parametroNMaximo;
				if (cartaVinculada.GetComponent<CartaInfo>().calcularAtaque() > valor)
					return false;
			}

			if (vinculanteEfecto.TieneClave("VINCULO_RESTRICCION_NIVEL_MINIMO")) {
				int valor = vinculanteEfecto.GetEfecto("VINCULO_RESTRICCION_NIVEL_MINIMO").parametroNMinimo;
				if (cartaVinculada.GetComponent<CartaInfo>().original.nivel < valor)
					return false;
			}

			return true;
		}


		public static void Vincular(GameObject cartaVinculante, GameObject cartaVinculada) {

			CartaEfecto vinculanteEfecto = cartaVinculante.GetComponent<CartaEfecto>();
			CartaInfo vinculadaInfo = cartaVinculada.GetComponent<CartaInfo>();
			CartaMovimiento vinculadaMovimiento = cartaVinculada.GetComponent<CartaMovimiento>();

			if (vinculanteEfecto.TieneClave("VINCULO_DESPERTAR")) {
				EmblemaEfectos.Activar(new EfectoSobreCarta(cartaVinculante, new SubEnderezar(), cartaVinculada));
			}

			if (vinculanteEfecto.TieneClave("VINCULO_HABILIDAD")) {
				string habilidad = vinculanteEfecto.GetEfecto("VINCULO_HABILIDAD").parametroHabilidad;
				EfectoBD efecto = new EfectoBD();
				efecto.clave = habilidad;
				cartaVinculada.GetComponent<CartaEfecto>().ColocarEfecto(efecto);

				if (habilidad == "SUPERVIVENCIA_N") {
					efecto.parametroN = vinculanteEfecto.GetEfecto("VINCULO_HABILIDAD").parametroN;
					vinculadaInfo.ColocarContador("supervivencia", efecto.parametroN);
				}
				if (habilidad == "CAZADOR")
					vinculadaMovimiento.Enderezar();
			}

			if (vinculanteEfecto.TieneClave("VINCULO_ESTADISTICA_ATK_DEF")) {
				vinculadaInfo.colocarBonoAtaque(
					cartaVinculante,
					vinculanteEfecto.GetEfecto("VINCULO_ESTADISTICA_ATK_DEF").parametroATK
				);
				vinculadaInfo.colocarBonoDefensa(
					cartaVinculante,
					vinculanteEfecto.GetEfecto("VINCULO_ESTADISTICA_ATK_DEF").parametroDEF
				);
			}

			if (vinculanteEfecto.TieneClave("VINCULO_ESTADISTICA_ATK")) {
				vinculadaInfo.colocarBonoAtaque(
					cartaVinculante,
					vinculanteEfecto.GetEfecto("VINCULO_ESTADISTICA_ATK").parametroATK
				);
			}

			if (vinculanteEfecto.TieneClave("VINCULO_ESTADISTICA_DEF")) {
				vinculadaInfo.colocarBonoDefensa(
					cartaVinculante,
					vinculanteEfecto.GetEfecto("VINCULO_ESTADISTICA_DEF").parametroDEF
				);
			}

			if (vinculanteEfecto.TieneClave("VINCULO_TIPO")) {
				cartaVinculada.GetComponent<CartaTipo>().AgregarTipo(vinculanteEfecto.GetEfecto("VINCULO_TIPO").parametroTipo);
			}

		}


	}

}