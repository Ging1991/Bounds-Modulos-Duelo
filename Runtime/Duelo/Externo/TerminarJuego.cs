using System.Collections;
using UnityEngine;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Utiles;
using System.Collections.Generic;
using Ging1991.Relojes;
using Bounds.Modulos.Cartas.Persistencia;
using Bounds.Modulos.Cartas.Ilustradores;
using Bounds.Infraestructura;

namespace Bounds.Duelo {

	public class TerminarJuego : MonoBehaviour, IEjecutable {
		public IlustradorDeCartas ilustradorDeCartas;


		public void Ejecutar() {
			GlobalDuelo duelo = GlobalDuelo.GetInstancia();
			ControlEscena escena = ControlEscena.GetInstancia();
			if (duelo.modo == "ENTRENAMIENTO")
				escena.CambiarEscena_entrenamiento();
			else if (duelo.modo == "HISTORIA")
				escena.CambiarEscena("HISTORIA");
			//else if (duelo.dueloModo == Infraestructura.Constantes.DueloConstantes.Modo.TUTORIAL)
			//	escena.CambiarEscena("TUTORIAL");
			else if (duelo.modo == "LIMITADO")
				escena.CambiarEscena("LIMITADO SELECCION ENEMIGOS");
			else escena.CambiarEscena_menu();
		}


		public void terminar(bool ganar) {
			GameObject cpu = GameObject.Find("CPU");
			if (cpu != null)
				Destroy(cpu);
			StartCoroutine(terminarJuego(ganar));
		}


		IEnumerator terminarJuego(bool haGanado) {
			yield return new WaitForSeconds(1);
			/*
						Configuracion configuracion = new Configuracion();
						GlobalDuelo parametros = GlobalDuelo.GetInstancia();

						if (parametros.modo == "LIGA") {
							ProcesarModoLiga(parametros, haGanado);
						}

						if (parametros.modo == "LIMITADO") {
							LectorLimitado lector = new LectorLimitado();
							lector.GuardarResultado(parametros.jugadorMiniatura2, haGanado ? "VICTORIA" : "DERROTA");
						}

						if (haGanado) {

							if (parametros.dueloModo == Infraestructura.Constantes.DueloConstantes.Modo.TUTORIAL) {
								int capituloActual = configuracion.LeerCapituloLeccion();
								configuracion.GuardarCapituloLeccion(capituloActual + 1);
							}

							if (parametros.modo == "HISTORIA") {
								LectorEntrenamiento lectorEntrenamiento = LectorEntrenamiento.GetInstancia();
								lectorEntrenamiento.Habilitar(parametros.jugadorMiniatura2);
								//ClasificatoriasControl.MejorarRarezasDeMazo();
								int capituloActual = configuracion.LeerCapituloHistoria();
								configuracion.GuardarCapituloHistoria(capituloActual + 1);
							}

						}

						if (parametros.modo == "ENTRENAMIENTO") {
							LectorEntrenamiento lectorEntrenamiento = LectorEntrenamiento.GetInstancia();
							if (haGanado)
								lectorEntrenamiento.IncrementarVictorias(parametros.jugadorNombre2);
							else
								lectorEntrenamiento.IncrementarDerrotas(parametros.jugadorNombre2);
						}

						yield return new WaitForSeconds(1);
						EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
						Instanciador instanciador = conocimiento.traerInstanciador();
						GameObject cuadro = instanciador.CrearCuadroFinalizarDuelo(new Vector3(0, 0, 0));

						if (haGanado) {
							BloqueJugador bloque1 = BloqueJugador.getInstancia("BloqueJugador" + 1);
							cuadro.GetComponent<CuadroFinalizarDuelo>().Iniciar("Resultado: VICTORIA", $"Recompensa ${bloque1.vida / 10}");
							cuadro.GetComponent<CuadroFinalizarDuelo>().Iniciar(this, true, DatosDeCartas.Instancia, ilustradorDeCartas);
							configuracion.GanarOro(bloque1.vida / 10);
						}

						else {
							cuadro.GetComponent<CuadroFinalizarDuelo>().Iniciar("Resultado: DERROTA", "Recompensa $100");
							cuadro.GetComponent<CuadroFinalizarDuelo>().Iniciar(this, false, DatosDeCartas.Instancia, ilustradorDeCartas);
							configuracion.GanarOro(100);
						}*/
		}

		/*
				private async void ProcesarModoLiga(GlobalDuelo duelo, bool haGanado) {/*
					List<int> cartasID1 = new List<int>();
					foreach (var carta in duelo.mazo1) {
						cartasID1.Add(carta.cartaID);
					}

					List<int> cartasID2 = new List<int>();
					foreach (var carta in duelo.mazo2) {
						cartasID2.Add(carta.cartaID);
					}

					string jugador1 = "Jugador";
					string jugador2 = duelo.jugadorNombre2;

					ServicioGuardarResultadoV2 servicio = new ServicioGuardarResultadoV2();
					if (await servicio.AutorizarAsync()) {
						if (haGanado) {
							servicio.LlamarAsync(jugador1, jugador2, cartasID1, cartasID2);
						}
						else {
							servicio.LlamarAsync(jugador2, jugador1, cartasID2, cartasID1);
						}
					}
				}*/

	}

}