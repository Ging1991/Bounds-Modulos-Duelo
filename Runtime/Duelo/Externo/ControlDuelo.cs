using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Utiles;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.CPU;
using Ging1991.Persistencia.Lectores.Demandas;
using Ging1991.Dialogos;
using Ging1991.Persistencia.Direcciones;
using Ging1991.Persistencia.Lectores;
using Ging1991.Core;
using Ging1991.Interfaces;
using Bounds.Modulos.Cartas.Persistencia;
using Bounds.Modulos.Cartas.Ilustradores;
using Bounds.Infraestructura.Visores;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Paneles;
using Bounds.Modulos.Cartas.Tinteros;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Campos;
using Bounds.Modulos.Duelo;
using Bounds.Persistencia.Parametros;
using Bounds.Persistencia;
using Bounds.Modulos.Persistencia;
using Ging1991.Core.Interfaces;
using Bounds.Cofres;

namespace Bounds.Duelo {

	public class ControlDuelo : SingletonMonoBehaviour<ControlDuelo>, ICampoLugarControlador {

		private ProveedorImagenPersonaje proveedorMiniatura;
		public GestorDeSonidos gestorDeSonidos;
		public DatosDeCartas datosDeCartas;
		public IProveedor<string, Sprite> ilustradorDeCartas;
		public VisorDuelo visorDuelo;
		public PilaVisual pilaVisual;
		public PanelCartas panelCartas;
		public IFinalizarDuelo finalizarDuelo;
		public Cofre cofre;

		public ParametrosControlDuelo parametrosControl;
		public IProveedor<int, string> selectorNombres;
		public IProveedor<string, string> selectorClases;
		public IProveedor<string, string> selectorTipos;
		public IProveedor<string, string> selectorInvocaciones;
		public IProveedor<string, string> selectorSistema;

		public Configuracion configuracion;
		public Billetera billetera;
		public MusicaDeFondo musicaDeFondo;

		void Start() {

			parametrosControl.Inicializar();
			ParametrosEscena parametrosEscena = parametrosControl.parametros;

			selectorNombres = new TraductorCartaID(parametrosEscena.direcciones["CARTA_NOMBRES"]);
			selectorClases = new TraductorTexto(parametrosEscena.direcciones["CARTA_CLASES"]);
			selectorTipos = new TraductorTexto(parametrosEscena.direcciones["CARTA_TIPOS"]);
			selectorSistema = new TraductorTexto(parametrosEscena.direcciones["SISTEMA"]);
			selectorInvocaciones = new TraductorTexto(parametrosEscena.direcciones["CARTA_INVOCACIONES"]);
			configuracion = new(parametrosEscena.direcciones["CONFIGURACION"]);
			billetera = new(parametrosEscena.direcciones["BILLETERA"]);
			cofre = new(parametrosEscena.direcciones["COFRE"], parametrosEscena.direcciones["COFRE_RECURSOS"]);
			musicaDeFondo.Inicializar(parametrosEscena.direcciones["MUSICA_DE_FONDO"]);
			ilustradorDeCartas = new IlustradorDeCartas(
				parametrosControl.parametros.direcciones["CARTAS_RECURSO"],
				parametrosControl.parametros.direcciones["CARTAS_DINAMICA"]
			);


			foreach (var campo in FindObjectsByType<CampoLugar>(FindObjectsSortMode.None)) {
				campo.controlador = this;
			}

			visorDuelo.Inicializar(selectorNombres, selectorSistema, selectorClases, selectorTipos, selectorInvocaciones, ilustradorDeCartas);

			CPUReloj cpuReloj = GameObject.Find("CPU").GetComponent<CPUReloj>();
			cpuReloj.Inicializacion();

			pilaVisual.InicializarVisuales();
			panelCartas.InicializarVisuales(datosDeCartas, ilustradorDeCartas, new TinteroBounds());

			Cargador cargador = GameObject.Find("Cargador").GetComponent<Cargador>();
			Fisica fisica = GameObject.Find("Fisica").GetComponent<Fisica>();
			fisica.Inicializar();

			// MAZOS
			GlobalDuelo parametros = GlobalDuelo.GetInstancia();
			finalizarDuelo = parametros.finalizarDuelo;

			foreach (GameObject carta in cargador.CargarCartasPorCartaCofre(1, parametros.mazo1, parametros.protector1))
				fisica.EnviarHaciaMazo(carta, 1);

			foreach (GameObject carta in cargador.CargarCartasPorCartaCofre(2, parametros.mazo2, parametros.protector2))
				fisica.EnviarHaciaMazo(carta, 2);

			EmblemaMezclarMazo.Mezclar(1);
			EmblemaMezclarMazo.Mezclar(2);

			EmblemaRobo.RobarCartas(2, 4);

			EmblemaIniciarDuelo.SetLP(1, parametros.jugadorLP1);
			EmblemaIniciarDuelo.SetLP(2, parametros.jugadorLP2);
			EmblemaIniciarDuelo.SetNombre(1, parametros.jugadorNombre1);
			EmblemaIniciarDuelo.SetNombre(2, parametros.jugadorNombre2);

			proveedorMiniatura = new ProveedorImagenPersonaje(new DireccionRecursos("PERSONAJES/MINIATURAS"));
			EmblemaIniciarDuelo.SetAvatar(1, proveedorMiniatura.GetElemento(parametros.jugadorMiniatura1));
			EmblemaIniciarDuelo.SetAvatar(2, proveedorMiniatura.GetElemento(parametros.jugadorMiniatura2));

			EmblemaTurnos.GetInstancia().jugadorActivo = 1;
			EmblemaTurnos.GetInstancia().SetFase(EmblemaTurnos.Fase.FASE_MANTENIMIENTO);
			EmblemaIniciarDuelo.IniciarMulligan();
		}


		public void PresionarBotonFase() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();
			if (turnos.jugadorActivo != 1)
				return;

			GameObject mulligan = GameObject.Find("Mulligan");
			if (mulligan != null)
				return;

			Entrada entrada = Entrada.GetInstancia();
			entrada.PresionarBotonFase();
		}


		public void PresionarBotonSugerencia() {
			PresionarBotonFase();
		}


		public void PresionarBotonInvocacion() {
			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			EmblemaTurnos turnos = conocimiento.traerControlTurnos();
			if (turnos.jugadorActivo != 1)
				return;
			Entrada entrada = Entrada.GetInstancia();
			entrada.PresionarBotonInvocacion();
		}


		public void PresionarBotonAbandonar() {
			TerminarJuego componente = GameObject.Find("TerminarJuego").GetComponent<TerminarJuego>();
			componente.Terminar(false);
		}


		void Update() {
			if (Input.GetKeyDown(KeyCode.Space))
				PresionarBotonSugerencia();
			if (Input.GetKeyDown(KeyCode.R))
				EmblemaVida.DisminuirVida(2, 5000);
		}


		public void HabilitarInvocacionPerfecta() {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			List<GameObject> cartasFueraDeCampo = new List<GameObject>();
			cartasFueraDeCampo.AddRange(fisica.TraerCartasEnMazo(1));
			cartasFueraDeCampo.AddRange(fisica.TraerCartasEnCementerio(1));
			cartasFueraDeCampo.AddRange(fisica.TraerCartasEnMano(1));
			bool tieneInvocaciones = false;
			foreach (GameObject carta in cartasFueraDeCampo) {
				CartaPerfeccion cartaPerfeccion = carta.GetComponent<CartaPerfeccion>();
				if (cartaPerfeccion.PuedeInvocar()) {
					tieneInvocaciones = true;
					break;
				}
			}
			GameObject boton = GameObject.Find("BotonInvocacion");
			EmblemaTurnos controlTurnos = EmblemaTurnos.GetInstancia();
			if (tieneInvocaciones && controlTurnos.jugadorActivo == 1 && controlTurnos.fase == EmblemaTurnos.Fase.FASE_PRINCIPAL)
				boton.GetComponent<Boton>().SetColorRelleno(Color.green);
			else
				boton.GetComponent<Boton>().SetColorRelleno(Color.gray);
		}

		public void LugarPresionado(GameObject objeto) {
			Entrada entrada = Entrada.GetInstancia();
			entrada.PresionarCampo(objeto);
		}

		public void LugarSoltado(GameObject objeto) {
			if (CartaArrastrar.carta != null) {
				EmblemaJugarCarta.Jugar(CartaArrastrar.carta, objeto);
			}
		}

		private class ProveedorImagenPersonaje : LectorPorDemandaImagen, IProveedor<string, Sprite> {

			public ProveedorImagenPersonaje(Direccion direccionCarpeta) : base(direccionCarpeta, TipoLector.RECURSOS) { }

			public Sprite GetElemento(string nombre) {
				return Leer(nombre);
			}

		}


	}

}