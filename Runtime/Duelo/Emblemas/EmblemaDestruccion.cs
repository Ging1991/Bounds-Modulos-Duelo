using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Efectos;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Emblemas.Trampas;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Visuales;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaDestruccion : EmblemaPadre {


		private static void DisminuirVida(GameObject carta, string visual = "GOLPE") {
			CartaInfo info = carta.GetComponent<CartaInfo>();
			EmblemaVida.DisminuirVida(info.controlador, info.original.nivel * 100, visual);
		}


		private static bool SalvadoPorProteccion(GameObject carta) {

			foreach (GameObject protector in new SubCartasControladas(0).Generar()) {
				if (protector.GetComponent<CartaEfecto>().TieneClave("ESCUDO") && protector.GetComponent<CartaInfo>().criaturaEquipada == carta) {
					if (DestruirPorReemplazo(protector))
						return true;
				}
			}

			foreach (GameObject protector in new SubCartasControladas(carta.GetComponent<CartaInfo>().controlador).Generar()) {
				if (protector.GetComponent<CartaEfecto>().TieneClave("GUARDIAN")) {
					if (DestruirPorReemplazo(protector))
						return true;
				}
			}

			foreach (GameObject protector in new SubCartasEnCementerio(carta.GetComponent<CartaInfo>().controlador).Generar()) {
				if (protector.GetComponent<CartaEfecto>().TieneClave("PROTECTOR")) {
					EmblemaEnviarMaterial.EnviarMateriales(new List<GameObject>() { protector });
					protector.GetComponentInChildren<GestorVisual>().Animar("NUBE");
					carta.GetComponentInChildren<GestorVisual>().Animar("NUBE");
					return true;
				}
			}

			CartaInfo info = carta.GetComponent<CartaInfo>();
			if (info.original.clase == "CRIATURA") {
				CondicionClase condicion = new("TRAMPA");
				foreach (GameObject trampa in condicion.CumpleLista(new SubCartasControladas(info.controlador).Generar())) {
					CartaInfo infoTrampa = trampa.GetComponent<CartaInfo>();
					CartaGeneral componenteTrampa = trampa.GetComponent<CartaGeneral>();
					if (!componenteTrampa.bocaArriba) {
						if (infoTrampa.original.datoTrampa.tipo == "perfecta") {
							componenteTrampa.ColocarBocaArriba();
							return true;
						}
					}
				}
			}

			return false;
		}


		public static bool DestruirPorBatalla(GameObject carta) {
			CartaInfo cartaInfo = carta.GetComponent<CartaInfo>();
			if (cartaInfo.original.clase != "CRIATURA" && cartaInfo.original.clase != "EQUIPO")
				return false;

			Fisica fisica = Fisica.Instancia;
			if (!fisica.TraerCartasEnCampo(cartaInfo.controlador).Contains(carta))
				return false;

			if (SalvadoPorProteccion(carta))
				return false;

			DisminuirVida(carta);

			if (cartaInfo.TraerContadores("supervivencia") > 0) {
				cartaInfo.RemoverContador("supervivencia", 1);
			}
			else {
				EmblemaDescarte.EnviarDesdeCampo(carta);
				if (cartaInfo.original.clase == "AURA" || cartaInfo.original.clase == "EQUIPO")
					EmblemaDesequipar.Desequipar(carta);
			}

			ActivarEfectosPropios(carta);
			ActivarEfectosDeOtrasCartas(carta);
			ActivarTrampas(carta);
			Desanexar(carta);
			return true;
		}


		public static bool DestruirPorEfectos(GameObject carta) {
			CartaInfo cartaInfo = carta.GetComponent<CartaInfo>();
			Fisica fisica = Fisica.Instancia;
			if (!fisica.TraerCartasEnCampo(cartaInfo.controlador).Contains(carta))
				return false;

			CartaEfecto cartaEfecto = carta.GetComponent<CartaEfecto>();
			if (cartaEfecto.TieneClave("ARKANO")) {
				ControlDuelo.Instancia.gestorDeSonidos.ReproducirSonido("FxRebote");
				return false;
			}

			if (SalvadoPorProteccion(carta))
				return false;

			carta.GetComponentInChildren<GestorVisual>().Animar("GOLPE");
			DisminuirVida(carta);

			if (cartaInfo.TraerContadores("supervivencia") > 0) {
				cartaInfo.RemoverContador("supervivencia", 1);
				return true;
			}

			EmblemaDescarte.EnviarDesdeCampo(carta);

			if (cartaInfo.original.clase == "AURA" || cartaInfo.original.clase == "EQUIPO")
				EmblemaDesequipar.Desequipar(carta);

			ControlDuelo duelo = ControlDuelo.Instancia;
			duelo.HabilitarInvocacionPerfecta();
			ActivarEfectosPropios(carta);
			ActivarEfectosDeOtrasCartas(carta);
			ActivarTrampas(carta);
			Desanexar(carta);
			return true;
		}


		public static bool DestruccionContinua(GameObject carta) {
			CartaInfo cartaInfo = carta.GetComponent<CartaInfo>();
			Fisica fisica = Fisica.Instancia;
			if (!fisica.TraerCartasEnCampo(cartaInfo.controlador).Contains(carta))
				return false;

			if (SalvadoPorProteccion(carta))
				return false;

			carta.GetComponentInChildren<GestorVisual>().Animar("GOLPE");
			DisminuirVida(carta);

			if (cartaInfo.TraerContadores("supervivencia") > 0) {
				cartaInfo.RemoverContador("supervivencia", 1);
				return true;
			}
			Desanexar(carta);
			ActivarEfectosPropios(carta);
			ActivarEfectosDeOtrasCartas(carta);
			ActivarTrampas(carta);

			EmblemaDescarte.EnviarDesdeCampo(carta);
			EmblemaDesequipar.Desequipar(carta);
			return true;
		}


		private static bool DestruirPorReemplazo(GameObject carta) {
			CartaInfo cartaInfo = carta.GetComponent<CartaInfo>();
			Fisica fisica = Fisica.Instancia;
			if (!fisica.TraerCartasEnCampo(cartaInfo.controlador).Contains(carta))
				return false;

			carta.GetComponentInChildren<GestorVisual>().Animar("GOLPE");
			DisminuirVida(carta);

			if (cartaInfo.TraerContadores("supervivencia") > 0) {
				cartaInfo.RemoverContador("supervivencia", 1);
				return true;
			}
			ActivarEfectosPropios(carta);

			EmblemaDescarte.EnviarDesdeCampo(carta);
			EmblemaDesequipar.Desequipar(carta);
			return true;
		}


		private static void ActivarEfectosDeOtrasCartas(GameObject carta) {

			CondicionClase condicionVacio = new CondicionClase(clase: "VACIO");
			Fisica fisica = Fisica.Instancia;
			CartaInfo info = carta.GetComponent<CartaInfo>();
			CartaTipo cartaTipo = carta.GetComponent<CartaTipo>();

			foreach (GameObject vacio in condicionVacio.CumpleLista(new SubCartasControladas(0).Generar())) {
				CartaInfo infoVacio = vacio.GetComponent<CartaInfo>();

				if (infoVacio.original.datoVacio.tipo == "EJERCITO") {
					if (cartaTipo.ContieneTipo(infoVacio.original.datoVacio.parametro)) {
						EmblemaEfectos.Activar(new EfectoSobreJugador(vacio, info.controlador, new SubRobar(1)));
					}
				}

				if (infoVacio.original.datoVacio.tipo == "ALMA_DRAGON") {
					if (!cartaTipo.ContieneTipo("dragon")) {
						EmblemaEfectos.Activar(new EfectoCrearFicha(vacio, info.controlador, 393, 1));
					}
				}

				if (infoVacio.original.datoVacio.tipo == "IMPERIO_REPTIL" && cartaTipo.ContieneTipo("reptil")) {
					List<GameObject> repiles = new List<GameObject>(fisica.TraerCartasEnMazo(info.controlador));
					Condicion condicionReptil = new Condicion(tipoCarta: "CRIATURA", tipoCriatura: new List<string> { "reptil" });
					repiles = condicionReptil.CumpleLista(repiles);
					if (repiles.Count > 0) {
						fisica.EnviarHaciaMano(repiles[0], info.controlador);
						if (info.controlador == 1) {
							CartaGeneral componente = repiles[0].GetComponent<CartaGeneral>();
							componente.ColocarBocaArriba();
						}
					}
				}
				if (infoVacio.original.datoVacio.tipo == "imperio_zombi" && info.original.datoCriatura.perfeccion != "FICHA") {
					EmblemaEfectos.Activar(new EfectoCrearFicha(vacio, info.controlador, 121, 1));
				}
			}

		}


		private static void Desanexar(GameObject carta) {
			foreach (GameObject vinculo in new SubCartasControladas(0).Generar()) {
				CartaInfo infoAura = vinculo.GetComponent<CartaInfo>();
				if (infoAura.original.clase == "AURA" && infoAura.criaturaEquipada == carta)
					DestruccionContinua(vinculo);
				if (infoAura.original.clase == "EQUIPO" && infoAura.criaturaEquipada == carta)
					infoAura.criaturaEquipada = null;
			}
		}


		private static void ActivarTrampas(GameObject criatura) {
			int controlador = criatura.GetComponent<CartaInfo>().controlador;
			int adversario = Adversario(controlador);
			Fisica fisica = Fisica.Instancia;

			foreach (var trampa in TraerTrampasBocaAbajo(controlador)) {
				CartaInfo infoTrampa = trampa.GetComponent<CartaInfo>();
				CartaGeneral generalTrampa = trampa.GetComponent<CartaGeneral>();

				if (infoTrampa.original.datoTrampa.tipo == "AGUILA_DE_FUEGO") {

					List<GameObject> mazoControlador = new List<GameObject>(fisica.TraerCartasEnMazo(controlador));
					GameObject criaturaAguilaDeFuego = null;
					GameObject materialAprendiz = null;
					GameObject materialAguila = null;

					foreach (GameObject cartaEnMazoControlador in mazoControlador) {
						CartaInfo infoCartaEnMazoControlador = cartaEnMazoControlador.GetComponent<CartaInfo>();
						if (infoCartaEnMazoControlador.original.nombre == "Aguila rapaz")
							materialAguila = cartaEnMazoControlador;
						if (infoCartaEnMazoControlador.original.nombre == "Aprendiz de pyro")
							materialAprendiz = cartaEnMazoControlador;
						if (infoCartaEnMazoControlador.original.nombre == "Aguila de fuego")
							criaturaAguilaDeFuego = cartaEnMazoControlador;
					}

					if (criaturaAguilaDeFuego != null && materialAprendiz != null && materialAguila != null) {
						EmblemaTrampa.ActivarTrampa(trampa);
						EmblemaInvocacionPerfecta.Invocar(controlador, criaturaAguilaDeFuego, new List<GameObject>() { materialAguila, materialAprendiz });
						break;
					}
				}

				if (infoTrampa.original.datoTrampa.tipo == "COMBUSTION") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoSobreJugador(trampa, adversario, new SubModificarLP(-500)));
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "REVIVIR") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoInvocacionEspecial(trampa, criatura, controlador));
					break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "VENGANZA") {
					CondicionClase condicionCriatura = new CondicionClase("CRIATURA");
					List<GameObject> criaturasDelAdversario = condicionCriatura.CumpleLista(fisica.TraerCartasEnCampo(adversario));
					if (criaturasDelAdversario.Count > 0) {
						EmblemaTrampa.ActivarTrampa(trampa);
						EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubDestruir(), criaturasDelAdversario[0]));
						break;
					}
				}

				if (infoTrampa.original.datoTrampa.tipo == "FICHA_INSECTO") {
					EmblemaTrampa.ActivarTrampa(trampa);
					EmblemaEfectos.Activar(new EfectoCrearFicha(trampa, controlador, 227, 2));
					break;
				}

				if (infoTrampa.original.clase == "TRAMPA"
						&& infoTrampa.original.datoTrampa.tipo == "destruye_venganza"
						&& !generalTrampa.bocaArriba) {

					generalTrampa.ColocarBocaArriba();
					// destruyo una criatura del oponente
					List<GameObject> criaturas = new List<GameObject>(fisica.TraerCartasEnCampo(JugadorDuelo.Adversario(controlador)));
					Condicion condicion = new Condicion(tipoCarta: "CRIATURA");
					criaturas = condicion.CumpleLista(criaturas);
					if (criaturas.Count > 0)
						//Destruir(criaturas[0], 0);
						break;
				}

				if (infoTrampa.original.datoTrampa.tipo == "VENGANZA_TRUENO") {
					if (criatura.GetComponent<CartaTipo>().ContieneTipo("trueno")) {
						CondicionClase condicionCriatura = new CondicionClase("CRIATURA");
						List<GameObject> criaturasDelAdversario = condicionCriatura.CumpleLista(fisica.TraerCartasEnCampo(adversario));
						if (criaturasDelAdversario.Count > 0) {
							EmblemaTrampa.ActivarTrampa(trampa);
							EmblemaEfectos.Activar(new EfectoSobreCarta(trampa, new SubDestruir(), criaturasDelAdversario[0]));
							EmblemaEfectos.Activar(new EfectoSobreJugador(trampa, adversario, new SubModificarLP(-500)));
							break;
						}
					}
				}

				if (infoTrampa.original.datoTrampa.tipo == "VENGANZA_NIVEL") {
					CondicionClase condicionCriatura = new CondicionClase("CRIATURA");
					List<GameObject> cartasEnCampo = new List<GameObject>(fisica.TraerCartasEnCampo(controlador));
					cartasEnCampo.AddRange(fisica.TraerCartasEnCampo(adversario));
					List<GameObject> criaturasParaDestruir = new List<GameObject>();

					foreach (GameObject criaturaEnCampo in condicionCriatura.CumpleLista(cartasEnCampo)) {
						CartaInfo infoCriaturaEnCampo = criaturaEnCampo.GetComponent<CartaInfo>();
						if (infoCriaturaEnCampo.original.nivel <= criatura.GetComponent<CartaInfo>().original.nivel)
							criaturasParaDestruir.Add(criaturaEnCampo);
					}

					if (criaturasParaDestruir.Count > 0) {
						EmblemaTrampa.ActivarTrampa(trampa);
						EmblemaEfectos.Activar(new EfectoSobreCartas(trampa, new SubDestruir(), criaturasParaDestruir));
						break;
					}
				}
			}


		}


		private static void ActivarEfectosPropios(GameObject carta) {

			Fisica fisica = Fisica.Instancia;
			CartaInfo info = carta.GetComponent<CartaInfo>();
			CartaEfecto cartaEfecto = carta.GetComponent<CartaEfecto>();
			int adversario = Adversario(info.controlador);
			CreacionDeCartas creacion = GameObject.Find("Fisica").GetComponent<CreacionDeCartas>();

			if (cartaEfecto.TieneClave("RECICLAR")) {
				EmblemaEfectos.Activar(new EfectoSobreJugador(carta, info.controlador, new SubRobar(1), "ROBAR"));
			}

			if (cartaEfecto.TieneClave("VENGANZA")) {
				EmblemaEfectos.Activar(new EfectoSobreJugador(carta, adversario, new SubModificarLP(-500)));
			}

			if (cartaEfecto.TieneClave("ALIEN_491")) {
				EmblemaEfectos.Activar(new EfectoAlienEncuentraMateriales(carta));
			}

			if (cartaEfecto.TieneClave("ALIEN_492")) {
				EmblemaEfectos.Activar(new EfectoAlienInvocaMateriales(carta));
			}

			if (cartaEfecto.TieneClave("ALIEN_493")) {
				EmblemaEfectos.Activar(new EfectoAlienEncuentraFusion(carta));
			}

			if (cartaEfecto.TieneClave("CENIZAS")) {
				CondicionArquetipo arquetipo = new("ilusionista");
				EmblemaEfectos.Activar(
					new EfectoSobreJugador(
						carta,
						adversario,
						new SubModificarLP(
							arquetipo.CumpleLista(fisica.TraerCartasEnCementerio(info.controlador)).Count * (-500)
						),
						"VENENO"
					)
				);
			}

			if (cartaEfecto.TieneClave("DUPLICAR")) {
				GameObject campo = GameObject.Find("Cartas" + info.controlador);
				GameObject ficha = creacion.CrearCarta(
					info.controlador,
					info.cartaID,
					$"J{info.controlador}_FICHA{info.cartaID}",
					Vector3.zero, campo, "N", "A"
				);
				fisica.EnviarHaciaMano(ficha, info.controlador);
			}

			if (cartaEfecto.TieneClave("CREAR_FICHA_ID")) {
				EfectoBD efecto = cartaEfecto.GetEfecto("CREAR_FICHA_ID");
				GameObject campo = GameObject.Find("Cartas" + info.controlador);
				GameObject ficha = creacion.CrearCarta(
					info.controlador,
					efecto.parametroID,
					$"J{info.controlador}_FICHA{efecto.parametroID}",
					Vector3.zero, campo, "N", "A"
				);
				EmblemaEfectos.Activar(new EfectoInvocacionEspecial(carta, ficha, info.controlador));
			}

		}


	}

}