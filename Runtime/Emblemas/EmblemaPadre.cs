using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Efectos;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Utiles;
using Bounds.Fisicas.Campos;
using Bounds.Fisicas.Carta;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.Emblemas {

	public abstract class EmblemaPadre {


		public static int Adversario(int jugador) {
			if (jugador == 1)
				return 2;
			return 1;
		}


		public static bool EstaOcupado(GameObject lugar) {
			return lugar.GetComponent<CampoLugar>().carta != null;
		}


		public static void ActivarEfectosDeActivacion(GameObject carta) {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			CartaInfo info = carta.GetComponent<CartaInfo>();

			int jugador = info.controlador;
			int adversario = JugadorDuelo.Adversario(jugador);

			CondicionClase condicionCriatura = new CondicionClase("CRIATURA");
			List<GameObject> criaturasEnCampo = condicionCriatura.CumpleLista(fisica.TraerCartasEnCampo(info.controlador));
			List<string> tiposDeCriatura = new List<string>();

			List<GameObject> cartasEnCampoJugador = new List<GameObject>(fisica.TraerCartasEnCampo(jugador));
			List<GameObject> cartasEnCampoAdversario = new List<GameObject>(fisica.TraerCartasEnCampo(adversario));
			List<GameObject> cartasEnCampo = new List<GameObject>();
			cartasEnCampo.AddRange(cartasEnCampoJugador);
			cartasEnCampo.AddRange(cartasEnCampoAdversario);


			foreach (GameObject criatura in criaturasEnCampo) {
				tiposDeCriatura.AddRange(criatura.GetComponent<CartaTipo>().tipos);
			}

			PilaEfectos pila = GameObject.Find("Pila").GetComponent<PilaEfectos>();
			CartaEfecto cartaEfecto = carta.GetComponent<CartaEfecto>();

			if (cartaEfecto.TieneClave("MASIVO")) {
				List<GameObject> objetivos = new SubCartasControladas(0).Generar();
				objetivos.Remove(carta);
				EmblemaEfectos.Activar(new EfectoSobreCartas(carta, new SubDestruir(), objetivos));
			}

			if (cartaEfecto.TieneClave("ACOMPAÑAR")) {
				pila.Agregar(new EfectoGanarInvocacion(carta, info.controlador, 1));
			}

			if (cartaEfecto.TieneClave("INICIAR")) {
				EmblemaEfectos.Activar(new EfectoSobreJugador(carta, jugador, new SubRobar(1), "ROBAR"));
			}

			if (cartaEfecto.TieneClave("COMANDAR_NOVATOS")) {
				CondicionMultiple condicionObjetivos = new CondicionMultiple(CondicionMultiple.Tipo.Y);
				condicionObjetivos.AgregarCondicion(new CondicionNivel(1));
				condicionObjetivos.AgregarCondicion(new CondicionClase("CRIATURA"));
				condicionObjetivos.AgregarCondicion(new CondicionEsPerfecta());
				List<GameObject> objetivos = condicionObjetivos.CumpleLista(new SubCartasEnMazo(jugador).Generar());
				if (objetivos.Count > 0) {
					int limite = BuscadorCampo.getInstancia().buscarCampoLibreN(jugador, 5).Count;
					foreach (GameObject objetivo in objetivos) {
						EmblemaEfectos.Activar(new EfectoInvocacionEspecial(carta, objetivo, jugador));
						limite--;
						if (limite == 0)
							break;
					}
				}
			}

			if (cartaEfecto.TieneClave("FATAL")) {
				EmblemaEfectos.Activar(new EfectoSobreJugador(carta, adversario, new SubDescartar(1), "VENENO"));
			}

			if (cartaEfecto.TieneClave("RECORTAR")) {
				CondicionCarta condicionFusion = new CondicionCriaturaPerfeccion(cartaEfecto.GetEfecto("RECORTAR").parametroPerfeccion);
				List<GameObject> cartasEnMazo = condicionFusion.CumpleLista(fisica.TraerCartasEnMazo(jugador));
				if (cartasEnMazo.Count > 0) {
					EmblemaEfectos.Activar(new EfectoSobreCarta(carta, new SubMolerCarta(), cartasEnMazo[0]));
				}
			}

			if (cartaEfecto.TieneClave("AUMENTAR_T") && tiposDeCriatura.Contains(cartaEfecto.GetEfecto("AUMENTAR_T").parametroTipo)) {
				EfectoBase efecto = new EfectoSobreJugador(carta, jugador, new SubRobar(1));
				efecto.AgregarEtiqueta("ROBAR");
				EmblemaEfectos.Activar(efecto);
			}

			if (cartaEfecto.TieneClave("AUMENTAR_ID")) {
				CondicionCartaID condicionCartaID = new(cartaEfecto.GetEfecto("AUMENTAR_ID").parametroID);
				if (condicionCartaID.CumpleLista(cartasEnCampoJugador).Count > 0) {
					EfectoBase efecto = new EfectoSobreJugador(carta, jugador, new SubRobar(1));
					efecto.AgregarEtiqueta("ROBAR");
					EmblemaEfectos.Activar(efecto);
				}
			}

			if (cartaEfecto.TieneClave("REVITALIZAR_T") && tiposDeCriatura.Contains(cartaEfecto.GetEfecto("REVITALIZAR_T").parametroTipo)) {
				EmblemaEfectos.Activar(new EfectoSobreJugador(carta, jugador, new SubModificarLP(500), "REVITALIZAR"));
			}

			if (cartaEfecto.TieneClave("HEROE")) {
				EmblemaEfectos.Activar(new EfectoSobreJugador(carta, jugador, new SubModificarLP(500), "REVITALIZAR"));
			}

			if (cartaEfecto.TieneClave("BOMBARDEAR")) {
				EmblemaEfectos.Activar(new EfectoBarajarFicha(carta, jugador, cartaEfecto.GetEfecto("BOMBARDEAR").parametroID, 5));
			}

			if (cartaEfecto.TieneClave("ABRACADABRA")) {
				EmblemaEfectos.Activar(new EfectoSobreJugador(carta, adversario, new SubMoler(5)));
			}

			if (cartaEfecto.TieneClave("ARDER_T") && tiposDeCriatura.Contains(cartaEfecto.GetEfecto("ARDER_T").parametroTipo)) {
				EfectoBase efectoBase = new EfectoSobreJugador(carta, Adversario(info.controlador), new SubModificarLP(-500));
				efectoBase.AgregarEtiqueta("EXPLOSION");
				EmblemaEfectos.Activar(efectoBase);
			}

			if (cartaEfecto.TieneClave("EXPLOSION")) {
				EmblemaEfectos.Activar(
					new EfectoSobreJugador(carta, Adversario(info.controlador), new SubModificarLP(-400), "EXPLOSION")
				);
			}

			if (cartaEfecto.TieneClave("DRENAR_T") && tiposDeCriatura.Contains(cartaEfecto.GetEfecto("DRENAR_T").parametroTipo)) {
				ISubSobreCarta subefecto = new SubColocarContador("debilidad", 1);
				ISubListaDeCartas lista = new SubCartasControladas(adversario, condicionCriatura);
				EmblemaEfectos.Activar(new EfectoSobreListaDeCartas(carta, subefecto, lista));
			}

			if (cartaEfecto.TieneClave("ANTI_T")) {
				CondicionTipoCriatura condicion = new CondicionTipoCriatura(cartaEfecto.GetEfecto("ANTI_T").parametroTipo);
				ISubSobreCarta subefecto = new SubColocarContador("debilidad", 1);
				pila.Agregar(new EfectoSobreCartas(carta, subefecto, condicion.CumpleLista(cartasEnCampo)));
			}

			if (cartaEfecto.TieneClave("REFORZAR_T") && tiposDeCriatura.Contains(cartaEfecto.GetEfecto("REFORZAR_T").parametroTipo)) {
				ISubSobreCarta subefecto = new SubColocarContador("poder", 1);
				ISubListaDeCartas lista = new SubCartasControladas(info.controlador, condicionCriatura);
				EmblemaEfectos.Activar(new EfectoSobreListaDeCartas(carta, subefecto, lista));
			}

			if (cartaEfecto.TieneClave("SUPERVIVENCIA_N")) {
				EfectoBD efectoBD = cartaEfecto.GetEfecto("SUPERVIVENCIA_N");
				int cantidadN = efectoBD.parametroN;
				info.ColocarContador("supervivencia", cantidadN);
			}

			if (cartaEfecto.TieneClave("SUPERVIVENCIA_GRILL")) {
				int cantidad = 0;
				foreach (GameObject material in fisica.TraerCartasEnMateriales(info.controlador)) {
					CartaInfo crinfo = material.GetComponent<CartaInfo>();
					if (crinfo.original.nombre.Contains("Grill"))
						cantidad += 1;
				}
				info.ColocarContador("supervivencia", cantidad);
			}

			if (cartaEfecto.TieneClave("CAZADOR")) {
				CartaMovimiento movimiento = carta.GetComponent<CartaMovimiento>();
				movimiento.Enderezar();
			}

			if (cartaEfecto.TieneClave("CARGA_VALIENTE")) {
				ISubSobreCarta subefecto = new SubEnderezar();
				EmblemaEfectos.Activar(new EfectoSobreCartas(carta, subefecto, criaturasEnCampo));
			}

			if (cartaEfecto.TieneClave("VINCULO_DESPERTAR") && info.criaturaEquipada != null) {
				EmblemaEfectos.Activar(new EfectoSobreCarta(carta, new SubEnderezar(), info.criaturaEquipada));
			}

			if (cartaEfecto.TieneClave("VINCULO_DORMIR")) {
				EmblemaEfectos.Activar(new EfectoSobreCarta(carta, new SubGirar(), info.criaturaEquipada));
			}

			if (cartaEfecto.TieneClave("VINCULO_MULTIPLE")) {
				EmblemaEfectos.Activar(new EfectoAuraMultiple(carta, info.criaturaEquipada));
			}

			if (cartaEfecto.TieneClave("SABIDURIA_T")) {
				CondicionTipoCriatura condicion = new CondicionTipoCriatura(tipo: cartaEfecto.GetEfecto("SABIDURIA_T").parametroTipo);
				EmblemaEfectos.Activar(new EfectoSobreJugador(carta, info.controlador, new SubRobar(condicion.CumpleLista(cartasEnCampoJugador).Count)));
			}

			if (cartaEfecto.TieneClave("TESORO_ID")) {
				List<GameObject> cartasEnMazo = fisica.TraerCartasEnMazo(info.controlador);
				int cartaID = cartaEfecto.GetEfecto("TESORO_ID").parametroID;
				GameObject tesoro = null;
				foreach (GameObject cartaEnMazo in cartasEnMazo) {
					if (cartaEnMazo.GetComponent<CartaInfo>().cartaID == cartaID) {
						tesoro = cartaEnMazo;
						break;
					}
				}
				if (tesoro != null) {
					fisica.EnviarHaciaMano(tesoro, info.controlador);
					if (info.controlador == 1) {
						tesoro.GetComponent<CartaGeneral>().ColocarBocaArriba();
					}
				}
			}

			if (cartaEfecto.TieneClave("ADQUIRIR_T")) {
				string tipo = cartaEfecto.GetEfecto("ADQUIRIR_T").parametroTipo;
				CondicionTipoCriatura condicion = new CondicionTipoCriatura(tipo);
				List<GameObject> cartasEnMazo = condicion.CumpleLista(fisica.TraerCartasEnMazo(info.controlador));
				if (cartasEnMazo.Count > 0) {
					fisica.EnviarHaciaMano(cartasEnMazo[0], info.controlador);
					if (info.controlador == 1) {
						cartasEnMazo[0].GetComponent<CartaGeneral>().ColocarBocaArriba();
					}
				}
			}

			if (cartaEfecto.TieneClave("DEVASTACION")) {
				List<GameObject> criaturas = new List<GameObject>(fisica.TraerCartasEnCampo(info.controlador));
				criaturas.AddRange(fisica.TraerCartasEnCampo(Adversario(info.controlador)));
				Condicion condicionZombi = new Condicion(tipoCarta: "CRIATURA");
				criaturas = condicionZombi.CumpleLista(criaturas);
				foreach (GameObject criatura in criaturas) {
					if (criatura == carta)
						continue;
					CartaInfo infox = criatura.GetComponent<CartaInfo>();
					infox.ColocarContador("debilidad", 4);
				}
			}

			if (cartaEfecto.TieneClave("UNIDOS")) {
				CondicionMultiple condicionMultiple = new CondicionMultiple(CondicionMultiple.Tipo.Y);
				condicionMultiple.AgregarCondicion(new CondicionNivel(minimo: 2, maximo: 3));
				condicionMultiple.AgregarCondicion(new CondicionClase(clase: "CRIATURA"));
				List<GameObject> cartas = condicionMultiple.CumpleLista(fisica.TraerCartasEnCementerio(info.controlador));
				if (cartas.Count > 0)
					EmblemaEfectos.Activar(new EfectoInvocacionEspecial(carta, cartas[0], jugador));
			}

			if (cartaEfecto.TieneClave("UNIDOS_N1")) {
				CondicionMultiple condicionMultiple = new CondicionMultiple(CondicionMultiple.Tipo.Y);
				condicionMultiple.AgregarCondicion(new CondicionNivel(1));
				condicionMultiple.AgregarCondicion(new CondicionClase(clase: "CRIATURA"));
				List<GameObject> cartas = condicionMultiple.CumpleLista(fisica.TraerCartasEnCementerio(info.controlador));
				if (cartas.Count > 0)
					EmblemaEfectos.Activar(new EfectoInvocacionEspecial(carta, cartas[0], jugador));
			}


		}


		public static List<GameObject> TraerTrampasBocaAbajo(int jugador) {
			Fisica fisica = GameObject.FindAnyObjectByType<Fisica>();
			CondicionMultiple condicionTrampa = new CondicionMultiple(CondicionMultiple.Tipo.Y);
			condicionTrampa.AgregarCondicion(new CondicionClase("TRAMPA"));
			condicionTrampa.AgregarCondicion(new CondicionEstaBocaAbajo());
			return condicionTrampa.CumpleLista(fisica.TraerCartasEnCampo(jugador));
		}


	}

}