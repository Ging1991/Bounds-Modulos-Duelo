using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Paneles;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.Efectos;
using Bounds.Duelo.Condiciones;
using Bounds.Persistencia.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Paneles.Seleccion;
using Ging1991.Core;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaHechizoActivacion {


		public static void ActivarConDescarte(int jugador, GameObject hechizo, GameObject descarte) {
			int adversario = JugadorDuelo.Adversario(jugador);
			Fisica fisica = Fisica.Instancia;

			HechizoBD dato = hechizo.GetComponent<CartaInfo>().original.datoHechizo;
			int jugadorObjetivo = jugador;
			if (dato.jugador == "ADVERSARIO")
				jugadorObjetivo = adversario;

			if (dato.tipo == "DESCARTE_ENCUENTRA_BASICO") {
				CartaInfo costeCartaInfo = descarte.GetComponent<CartaInfo>();
				CondicionMultiple condicion = new CondicionMultiple(CondicionMultiple.Tipo.Y);
				condicion.AgregarCondicion(new CondicionNivel(costeCartaInfo.original.nivel));
				condicion.AgregarCondicion(new CondicionCriaturaPerfeccion("BASICO"));
				List<GameObject> cartasPosibles = condicion.CumpleLista(fisica.TraerCartasEnMazo(jugador));
				if (cartasPosibles.Count > 1) {
					EmblemaEfectos.Activar(new EfectoSobreCarta(hechizo, new SubEnviarMano(), cartasPosibles[0]));
					EmblemaEfectos.Activar(new EfectoSobreCarta(hechizo, new SubEnviarMano(), cartasPosibles[1]));
				}
			}


			if (dato.tipo == "DESCARTE_BUSCA_MATERIALES_ID") {
				EmblemaEfectos.Activar(new EfectoEncuentraMaterialesID(hechizo, jugador, descarte));
			}

			if (dato.tipo == "DESCARTE_ROBAR_CARTAS") {
				EmblemaEfectos.Activar(new EfectoSobreJugador(hechizo, jugadorObjetivo, new SubRobar(dato.cantidad)));
			}

			if (dato.tipo == "DESCARTE_DEMONIO") {
				CartaInfo infoDescarte = descarte.GetComponent<CartaInfo>();
				EfectoBase efecto = new EfectoSobreJugador(hechizo, adversario, new SubModificarLP(-1 * infoDescarte.calcularAtaque()));
				efecto.AgregarEtiqueta("VENENO");
				EmblemaEfectos.Activar(efecto);
			}

			if (dato.tipo == "DESCARTE_ENCUENTRA_CARTAS_BASICO_N8_MAS") {
				CondicionCriaturaPerfeccion condicion = new CondicionCriaturaPerfeccion("BASICO");
				List<GameObject> cartas = new List<GameObject>();
				foreach (GameObject cartaMazo in condicion.CumpleLista(fisica.TraerCartasEnMazo(jugador))) {
					if (!cartas.Contains(cartaMazo)) {
						if (cartaMazo.GetComponent<CartaInfo>().original.nivel >= 8) {
							cartas.Add(cartaMazo);
						}
					}
					if (cartas.Count >= 2)
						break;
				}
				foreach (GameObject encontrada in cartas) {
					fisica.EnviarHaciaMano(encontrada, jugador);
					if (jugador == 1) {
						encontrada.GetComponent<CartaGeneral>().ColocarBocaArriba();
					}
				}
			}

			HechizoActivadoCorrectamente(jugador);
		}


		public static void ActivarConPagoLP(int jugador, GameObject hechizo, int costeLP) {
			int adversario = JugadorDuelo.Adversario(jugador);
			HechizoBD dato = hechizo.GetComponent<CartaInfo>().original.datoHechizo;
			Fisica fisica = Fisica.Instancia;

			int jugadorObjetivo = jugador;
			if (dato.jugador == "ADVERSARIO")
				jugadorObjetivo = adversario;


			if (dato.tipo == "COSTE_LP_ROBAR_CARTAS") {
				EmblemaEfectos.Activar(new EfectoSobreJugador(hechizo, jugadorObjetivo, new SubRobar(dato.cantidad)));
			}

			if (dato.tipo == "COSTE_LP_INVOCA_4000") {
				CondicionMultiple condicion4000 = new CondicionMultiple(CondicionMultiple.Tipo.Y);
				condicion4000.AgregarCondicion(new CondicionCriaturaPerfeccion("BASICO"));
				condicion4000.AgregarCondicion(new CondicionAtaque(minimo: 4000));
				List<GameObject> opciones = condicion4000.CumpleLista(fisica.TraerCartasEnMazo(jugador));
				if (opciones.Count > 0) {
					EmblemaEfectos.Activar(new EfectoInvocacionEspecial(hechizo, opciones[0], jugador));
				}
			}

			HechizoActivadoCorrectamente(jugador);
		}


		public static void ActivarConSacrificio(int jugador, GameObject hechizo, GameObject sacrificio) {
			Fisica fisica = Fisica.Instancia;
			int adversario = JugadorDuelo.Adversario(jugador);
			HechizoBD dato = hechizo.GetComponent<CartaInfo>().original.datoHechizo;
			int jugadorObjetivo = jugador;
			if (dato.jugador == "ADVERSARIO")
				jugadorObjetivo = adversario;


			if (dato.tipo == "SACRIFICIO_DRAGONES_GEMELOS") {
				CondicionTipoCriatura condicionDragon = new CondicionTipoCriatura("dragon");
				List<GameObject> dragones = condicionDragon.CumpleLista(fisica.TraerCartasEnMazo(jugador));
				Dictionary<int, GameObject> mapaNivelCarta = new Dictionary<int, GameObject>();

				foreach (GameObject dragon in dragones) {
					CartaInfo infoDragon = dragon.GetComponent<CartaInfo>();
					if (!mapaNivelCarta.ContainsKey(infoDragon.original.nivel)) {
						mapaNivelCarta.Add(infoDragon.original.nivel, dragon);
						continue;
					}
					GameObject dragonAnterior = mapaNivelCarta[infoDragon.original.nivel];
					CartaInfo infoDragonAnterior = dragonAnterior.GetComponent<CartaInfo>();
					if (infoDragon.original.nombre != infoDragonAnterior.original.nombre) {
						EmblemaEfectos.Activar(new EfectoInvocacionEspecial(hechizo, dragon, jugador));
						EmblemaEfectos.Activar(new EfectoInvocacionEspecial(hechizo, dragonAnterior, jugador));
						break;
					}
				}
			}

			if (dato.tipo == "SACRIFICIO_MODIFICA_VIDA") {
				CartaInfo sacrificioInfo = sacrificio.GetComponent<CartaInfo>();
				EmblemaEfectos.Activar(new EfectoSobreJugador(hechizo, jugador, new SubModificarLP(sacrificioInfo.calcularDefensa()), "REVITALIZAR"));
			}

			if (dato.tipo == "SACRIFICIO_DAÑO_MAGICO") {
				EmblemaEfectos.Activar(new EfectoSobreJugador(hechizo, adversario, new SubModificarLP(-1000), "VENENO"));
			}

			if (dato.tipo == "SACRIFICIO_ROBAR_CARTAS") {
				EmblemaEfectos.Activar(new EfectoSobreJugador(hechizo, jugadorObjetivo, new SubRobar(dato.cantidad)));
			}

			if (dato.tipo == "SACRIFICIO_CREAR_FICHA") {
				EmblemaEfectos.Activar(new EfectoCrearFicha(hechizo, jugador, dato.parametroID, 1));
			}

			HechizoActivadoCorrectamente(jugador);
		}


		public static void ActivarConObjetivo(GameObject hechizo, GameObject objetivo) {

			HechizoBD dato = hechizo.GetComponent<CartaInfo>().original.datoHechizo;
			CartaInfo info = hechizo.GetComponent<CartaInfo>();
			int controlador = info.controlador;

			if (dato.tipo == "DESTRUIR_OBJETIVO") {
				EmblemaEfectos.Activar(new EfectoSobreCarta(hechizo, new SubDestruir(), objetivo));
			}

			if (dato.tipo == "OBJETIVO_LEVANTAR") {
				EmblemaEfectos.Activar(new EfectoSobreCarta(hechizo, new SubLevantar(), objetivo));
			}

			if (dato.tipo == "INVOCAR_OBJETIVO") {
				EmblemaEfectos.Activar(new EfectoInvocacionEspecial(hechizo, objetivo, controlador));
			}

			if (dato.tipo == "CONTADOR_OBJETIVO") {
				EmblemaEfectos.Activar(new EfectoSobreCarta(hechizo, new SubColocarContador("poder", 1), objetivo));
			}

			if (dato.tipo == "RECUPERAR_OBJETIVO") {
				EmblemaEfectos.Activar(new EfectoSobreCarta(hechizo, new SubRecuperar(), objetivo));
			}

			if (dato.tipo == "CONTROLAR_OBJETIVO") {
				EmblemaEfectos.Activar(new EfectoSobreCarta(hechizo, new SubControlar(controlador), objetivo));
			}

			if (dato.tipo == "RECICLAR_OBJETIVO") {
				EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
				Fisica fisica = conocimiento.traerFisica();
				fisica.EnviarHaciaMazo(objetivo, objetivo.GetComponent<CartaInfo>().propietario);
			}

			if (dato.tipo == "CLONAR_OBJETIVO") {
				EmblemaEfectos.Activar(new EfectoCrearFicha(hechizo, controlador, objetivo.GetComponent<CartaInfo>().cartaID, 1));
			}

			HechizoActivadoCorrectamente(controlador);
		}


		public static void Activar(int jugador, GameObject hechizo) {

			int adversario = JugadorDuelo.Adversario(jugador);
			HechizoBD dato = hechizo.GetComponent<CartaInfo>().original.datoHechizo;
			Fisica fisica = Fisica.Instancia;
			List<GameObject> cartasEnCampoJugador = new List<GameObject>(fisica.TraerCartasEnCampo(jugador));

			int jugadorObjetivo = 0;
			if (dato.jugador == "ADVERSARIO")
				jugadorObjetivo = adversario;
			if (dato.jugador == "JUGADOR")
				jugadorObjetivo = jugador;

			if (dato.tipo == "DESTINO_FINAL") {
				EmblemaEfectos.Activar(new EfectoDestino(hechizo));
			}

			if (dato.tipo == "ONDAS_DAÑO") {
				SubCartasControladas sub = new SubCartasControladas(jugador);
				CondicionCartaID condicion = new CondicionCartaID(cartasID: new List<int>() { 457, 458 });
				EfectoBase efecto = new EfectoSobreJugador(
					hechizo,
					adversario,
					new SubModificarLP(-300 * condicion.CumpleLista(sub.Generar()).Count));
				efecto.AgregarEtiqueta("EXPLOSION");
				EmblemaEfectos.Activar(efecto);
			}

			if (dato.tipo == "ONDAS_BUSQUEDA") {
				CondicionCartaID condicion = new CondicionCartaID(cartasID: new List<int>() { 457, 458 });
				List<GameObject> ondas = condicion.CumpleLista(fisica.TraerCartasEnMazo(jugador));
				if (ondas.Count > 0) {
					EmblemaEfectos.Activar(new EfectoSobreCarta(hechizo, new SubEnviarMano(), ondas[0]));
				}
			}

			if (dato.tipo == "LLEGADA") {
				EmblemaEfectos.Activar(
					new EfectoSobreJugador(
						hechizo,
						adversario,
						new SubModificarLP(-500 * fisica.TraerCartasEnMateriales(jugador).Count),
						"VENENO"
					)
				);
			}

			if (dato.tipo == "SIRVIENTES") {
				Condicion condicionSirviente = new Condicion(textoParcial: "Sirviente");
				int contador = 0;
				foreach (GameObject sirviente in condicionSirviente.CumpleLista(fisica.TraerSiguientesCartasEnMazo(jugador, 5))) {
					if (fisica.TraerCartasEnMano(jugador).Count < 5) {
						fisica.EnviarHaciaMano(sirviente, jugador);
						contador++;
						if (jugador == 1)
							sirviente.GetComponent<CartaGeneral>().ColocarBocaArriba();
					}
				}
				EfectoBase efectoBase = new EfectoSobreJugador(hechizo, adversario, new SubModificarLP(-500 * contador));
				efectoBase.AgregarEtiqueta("VENENO");
				EmblemaEfectos.Activar(efectoBase);
			}

			if (dato.tipo == "OFRENDA") {
				List<GameObject> cartas = new List<GameObject>(fisica.TraerCartasEnMano(adversario));
				ActivarTutor(jugador, cartas, fisica, false);
			}

			if (dato.tipo == "METEORO") {
				CondicionClase condicionCriatura = new("CRIATURA");
				List<GameObject> criaturas1 = condicionCriatura.CumpleLista(fisica.TraerCartasEnCampo(jugador));
				List<GameObject> criaturas2 = condicionCriatura.CumpleLista(fisica.TraerCartasEnCampo(adversario));
				if (criaturas1.Count > 0 && criaturas2.Count == 0)
					EmblemaEfectos.Activar(new EfectoSobreJugador(hechizo, adversario, new SubModificarLP(-1000), "EXPLOSION"));
				//else
				//EfectosDeSonido.Tocar("FxRebote");
			}

			if (dato.tipo == "MODIFICAR_VIDA") {
				EmblemaEfectos.Activar(
					new EfectoSobreJugador(hechizo, jugadorObjetivo, new SubModificarLP(dato.cantidad), dato.etiqueta)
				);
			}

			if (dato.tipo == "MOLER") {
				EmblemaEfectos.Activar(new EfectoSobreJugador(hechizo, adversario, new SubMoler(dato.cantidad)));
			}

			if (dato.tipo == "TUTOR") {
				CondicionCarta condicion = CondicionMapper.GenerarCondicion(dato.condicionCarta);
				if (dato.nPrimeras > 0) {
					List<GameObject> cartas = condicion.CumpleLista(fisica.TraerSiguientesCartasEnMazo(jugador, dato.nPrimeras));
					ActivarTutor(jugador, cartas, fisica, dato.esAleatorio);
				}
				else {
					List<GameObject> cartas = condicion.CumpleLista(fisica.TraerCartasEnMazo(jugador));
					ActivarTutor(jugador, cartas, fisica, dato.esAleatorio);
				}
			}

			if (dato.tipo == "EXTINCION") {
				CondicionClase condicionCriatura = new CondicionClase("CRIATURA");
				List<GameObject> cartasEnMazos = new List<GameObject>();
				cartasEnMazos.AddRange(fisica.TraerCartasEnMazo(jugador));
				cartasEnMazos.AddRange(fisica.TraerCartasEnMazo(adversario));
				foreach (GameObject cartaEnMazo in condicionCriatura.CumpleLista(cartasEnMazos)) {
					if (cartaEnMazo.GetComponent<CartaInfo>().original.datoCriatura.perfeccion != "BASICO")
						EmblemaDescartarCarta.Descartar(cartaEnMazo);
				}
			}

			if (dato.tipo == "EXPEDICION") {
				List<GameObject> cartasSiguientes = new List<GameObject>(fisica.TraerSiguientesCartasEnMazo(jugador, 10));
				foreach (var cartaSuperior in cartasSiguientes) {
					CartaInfo infoSuperior = cartaSuperior.GetComponent<CartaInfo>();
					if (infoSuperior.original.clase == "CRIATURA") {
						EmblemaEfectos.Activar(new EfectoInvocacionEspecial(hechizo, cartaSuperior, jugador));
						break;
					}
					else {
						fisica.EnviarHaciaDescarte(cartaSuperior, jugador);
					}
				}
			}

			if (dato.tipo == "NUCLEAR_FUSION") {
				ActivarInvocacionNuclear(jugador, fisica, "FUSION");
			}

			if (dato.tipo == "NUCLEAR_EVOLUCION") {
				ActivarInvocacionNuclear(jugador, fisica, "EVOLUCION");
			}

			if (dato.tipo == "DESTRUIR_CARTAS") {
				CondicionCarta condicion = CondicionMapper.GenerarCondicion(dato.condicionCarta, jugador);
				List<GameObject> cartasEnCampo = new();
				cartasEnCampo.AddRange(fisica.TraerCartasEnCampo(1));
				cartasEnCampo.AddRange(fisica.TraerCartasEnCampo(2));
				if (condicion != null)
					cartasEnCampo = condicion.CumpleLista(cartasEnCampo);
				EmblemaEfectos.Activar(new EfectoSobreCartas(hechizo, new SubDestruir(), cartasEnCampo));
			}

			if (dato.tipo == "CONTADOR_CARTAS_DEBILIDAD") {
				CondicionClase condicionCriatura = new("CRIATURA");
				EmblemaEfectos.Activar(
					new EfectoSobreCartas(
						hechizo,
						new SubColocarContador("debilidad", 4),
						condicionCriatura.CumpleLista(new SubCartasControladas(jugadorObjetivo).Generar())
					)
				);
			}

			if (dato.tipo == "SEGUNDA_INVOCACION") {
				EmblemaEfectos.Activar(new EfectoGanarInvocacion(hechizo, jugador, 1));
			}

			if (dato.tipo == "ENDEREZAR_CRIATURAS") {
				EmblemaEfectos.Activar(
					new EfectoSobreListaDeCartas(
						hechizo,
						new SubEnderezar(),
						new SubCartasControladas(jugador, new CondicionClase("CRIATURA"))
					)
				);
			}

			if (dato.tipo == "DESTRUIR_TODO") {
				List<GameObject> lista = new SubCartasControladas(0).Generar();
				lista.Remove(hechizo);
				EmblemaEfectos.Activar(new EfectoSobreCartas(hechizo, new SubDestruir(), lista));
			}

			if (dato.tipo == "CEMENTERIO_MATERIAL") {
				EmblemaEnviarMaterial.EnviarMateriales(new List<GameObject>(fisica.TraerCartasEnCementerio(jugador)));
			}

			if (dato.tipo == "APOCALIPSIS") {

				List<GameObject> listaNoCriaturas = new();
				foreach (GameObject cartaEnCampo in new SubCartasControladas(0).Generar()) {
					if (cartaEnCampo.GetComponent<CartaInfo>().original.clase != "CRIATURA")
						listaNoCriaturas.Add(cartaEnCampo);
				}
				listaNoCriaturas.Remove(hechizo);
				EmblemaEfectos.Activar(new EfectoSobreCartas(hechizo, new SubDestruir(), listaNoCriaturas));
			}

			if (dato.tipo == "ENVENENAR") {
				CondicionClase condicionClase = new CondicionClase("CRIATURA");
				EmblemaEfectos.Activar(
					new EfectoSobreCartas(
						hechizo,
						new SubColocarContador("veneno", 1),
						condicionClase.CumpleLista(new SubCartasControladas(adversario).Generar())
					)
				);
			}

			if (dato.tipo == "DRAGONES") {
				CondicionTipoCriatura condicionDragon = new("dragon");
				foreach (var dragon in condicionDragon.CumpleLista(fisica.TraerCartasEnCementerio(jugador))) {
					EmblemaEfectos.Activar(new EfectoInvocacionEspecial(hechizo, dragon, jugador));
				}
			}

			if (dato.tipo == "BENDICION") {
				CondicionClase condicionClase = new CondicionClase("CRIATURA");
				EmblemaEfectos.Activar(
					new EfectoSobreCartas(hechizo, new SubColocarContador("poder", 2), condicionClase.CumpleLista(cartasEnCampoJugador))
				);
			}

			if (dato.tipo == "ADIVINA_N") {
				List<GameObject> opciones = fisica.TraerSiguientesCartasEnMazo(jugador, dato.nPrimeras);
				ActivarTutor(jugador, opciones, Fisica.Instancia, false);
			}

			if (dato.tipo == "ROBAR_CARTAS") {
				EmblemaEfectos.Activar(new EfectoSobreJugador(hechizo, jugadorObjetivo, new SubRobar(dato.cantidad)));
			}

			if (dato.tipo == "ROBAR_CARTAS_CRIATURA") {
				CondicionClase condicion = new("CRIATURA");
				EmblemaEfectos.Activar(
					new EfectoSobreJugador(hechizo, jugador, new SubRobar(condicion.CumpleLista(fisica.TraerCartasEnCampo(jugador)).Count))
				);
			}

			if (dato.tipo == "RENOVAR_CARTAS") {
				List<GameObject> cartasEnMano = new(fisica.TraerCartasEnMano(jugador));
				int cantidad = cartasEnMano.Count;
				if (cantidad > 0) {
					foreach (GameObject cartaEnMano in cartasEnMano) {
						EmblemaDescartarCarta.Descartar(cartaEnMano);
					}
					EmblemaRobo.RobarCartas(jugador, cantidad);
				}
			}

			if (dato.tipo == "EXPLOSION_FINAL") {
				CondicionTipoCriatura condicionPyro = new("pyro");
				List<GameObject> pyros = condicionPyro.CumpleLista(fisica.TraerCartasEnCampo(jugador));
				EmblemaEfectos.Activar(new EfectoSobreJugador(hechizo, adversario, new SubModificarLP(-500 * pyros.Count)));
				foreach (GameObject pyro in pyros) {
					EmblemaEnviarAlCementerio.DesdeElCampo(pyro);
				}
			}

			if (dato.tipo == "INVOCA_MAZO") {
				CondicionCarta condicionInvocable = CondicionMapper.GenerarCondicion(dato.condicionCarta, jugador);
				List<GameObject> invocables = condicionInvocable.CumpleLista(fisica.TraerCartasEnMazo(jugador));
				if (invocables.Count > 0) {
					EmblemaEfectos.Activar(new EfectoInvocacionEspecial(hechizo, invocables[0], jugador));
				}
				else {
					//EfectosDeSonido.Tocar("FxRebote");
				}
			}

			if (dato.tipo == "TUTOR_CONTRATAQUE") {
				foreach (GameObject cartaEnMano in new SubCartasEnMano(jugador).Generar()) {
					EmblemaDescartarCarta.Descartar(cartaEnMano);
				}

				int contador = 0;
				Condicion condicionContrataques = new Condicion(tipoCarta: "TRAMPA", textoParcial: "Contrataque");
				foreach (GameObject contrataque in condicionContrataques.CumpleLista(fisica.TraerCartasEnMazo(jugador))) {
					EmblemaEfectos.Activar(new EfectoSobreCarta(hechizo, new SubColocarBocaAbajo(jugador), contrataque));
					contador++;
					if (contador > 3)
						break;
				}
			}

			if (dato.tipo == "NUCLEAR_MAGICO") {
				ActivarInvocacionNuclear(jugador, fisica, "MAGICO");
			}

			if (dato.tipo == "NUCLEAR_GEMINIS") {
				ActivarInvocacionNuclear(jugador, fisica, "GEMINIS");
			}

			if (dato.tipo == "NUCLEAR_VECTOR") {
				ActivarInvocacionNuclear(jugador, fisica, "VECTOR");
			}

			if (dato.tipo == "MASIFICACION_ZOMBI") {
				CondicionClase condicionCriatura = new("CRIATURA");
				foreach (var carta in condicionCriatura.CumpleLista(new SubCartasControladas(0).Generar())) {
					carta.GetComponent<CartaTipo>().AgregarTipo("zombi");
				}
			}

			if (dato.tipo == "FUSION_CEMENTERIO") {
				CondicionCriaturaPerfeccion condicionFusion = new("FUSION");
				List<GameObject> cartas = new List<GameObject>(fisica.TraerCartasEnMazo(jugador));
				cartas.AddRange(new List<GameObject>(fisica.TraerCartasEnMano(jugador)));
				cartas.AddRange(new List<GameObject>(fisica.TraerCartasEnCementerio(jugador)));

				List<GameObject> opciones = new List<GameObject>();
				foreach (GameObject fusion in condicionFusion.CumpleLista(cartas)) {
					//if (fusion.GetComponent<CartaPerfeccion>().ListaCompletaParaInvocar(fisica.TraerCartasEnCementerio(jugador)))
					//	opciones.Add(fusion);
				}

				if (opciones.Count > 0) {
					SeleccionFusionDescarte accion = new(jugador);
					if (jugador == 1 && opciones.Count > 1) {
						fisica.panel.SetActive(true);
						PanelCartas panel = fisica.panel.GetComponent<PanelCartas>();
						panel.Iniciar(opciones, accion, texto: "Selecciona una fusion para invocar.");

					}
					else {
						accion.Seleccionar(opciones[0]);
					}
				}
				else {
					//EfectosDeSonido.Tocar("FxRebote");
				}

			}


			if (dato.tipo == "FUSION_HEREJE") {
				//pila.Agregar(new EfectoCrearFicha(hechizo, jugador, dato.parametroID, 1));
			}

			HechizoActivadoCorrectamente(jugador);
		}


		private static void ActivarTutor(int jugador, List<GameObject> opciones, Fisica fisica, bool esAleatorio) {
			if (opciones.Count > 0) {
				SeleccionarRecuperar accion = new SeleccionarRecuperar(jugador);
				if (jugador == 1 && opciones.Count > 1 && !esAleatorio) {
					fisica.panel.SetActive(true);
					PanelCartas panel = fisica.panel.GetComponent<PanelCartas>();
					panel.Iniciar(opciones, accion, texto: "Selecciona una carta para traer a tu mano.");

				}
				else {
					accion.Seleccionar(opciones[0]);
				}
			}
			else {
				//EfectosDeSonido.Tocar("FxRebote");
			}
		}


		private static void ActivarInvocacionNuclear(int jugador, Fisica fisica, string perfeccion) {

			CondicionCriaturaPerfeccion condicion = new CondicionCriaturaPerfeccion(perfeccion);
			List<GameObject> cartasEnMazo = new List<GameObject>(fisica.TraerCartasEnMazo(jugador));
			List<GameObject> invocables = condicion.CumpleLista(cartasEnMazo);

			if (invocables.Count > 0) {
				GameObject invocable = invocables[0];
				List<GameObject> materiales = TraerMateriales(invocable, cartasEnMazo);
				fisica.EnviarHaciaMano(invocable, jugador);
				EmblemaEnviarMaterial.EnviarMateriales(materiales);
				if (jugador == 1) {
					invocable.GetComponent<CartaGeneral>().ColocarBocaArriba();
				}
			}

		}


		private static List<GameObject> TraerMateriales(GameObject objetivo, List<GameObject> cartasEnMazo) {

			List<GameObject> materialesEncontrados = new List<GameObject>();
			CartaInfo infoObjetivo = objetivo.GetComponent<CartaInfo>();

			foreach (var materialBD in infoObjetivo.original.materiales) {
				foreach (GameObject cartaEnMazo in cartasEnMazo) {
					if (!materialesEncontrados.Contains(cartaEnMazo) && cartaEnMazo != objetivo) {

						if (materialBD.tipo == "CARTA_ID") {
							if (CartaPerfeccion.ExtenderID(cartaEnMazo).Contains(materialBD.parametroID)) {
								materialesEncontrados.Add(cartaEnMazo);
								break;
							}
						}

						if (materialBD.tipo == "TIPO") {
							if (cartaEnMazo.GetComponent<CartaTipo>().ContieneTipo(materialBD.parametroTipo)) {
								materialesEncontrados.Add(cartaEnMazo);
								break;
							}
						}

						if (materialBD.tipo == "CLASE") {
							if (cartaEnMazo.GetComponent<CartaInfo>().original.clase == materialBD.parametroClase) {
								materialesEncontrados.Add(cartaEnMazo);
								break;
							}
						}

						if (materialBD.tipo == "VECTOR_ATK") {
							if (cartaEnMazo.GetComponent<CartaInfo>().calcularAtaque() == materialBD.parametroATK) {
								materialesEncontrados.Add(cartaEnMazo);
								break;
							}
						}

						if (materialBD.tipo == "VECTOR_DEF") {
							if (cartaEnMazo.GetComponent<CartaInfo>().calcularDefensa() == materialBD.parametroDEF) {
								materialesEncontrados.Add(cartaEnMazo);
								break;
							}
						}
					}
				}
			}
			return materialesEncontrados;
		}


		private static void HechizoActivadoCorrectamente(int jugador) {
			ControlDuelo duelo = Object.FindAnyObjectByType<ControlDuelo>();
			duelo.HabilitarInvocacionPerfecta();
			EstadisticasSingleton.Instancia.Incrementar($"HECHIZO_{jugador}_jugadas");
		}

	}

}