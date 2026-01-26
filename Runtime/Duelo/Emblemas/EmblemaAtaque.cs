using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Duelo.Pila.Efectos;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Fila;
using Bounds.Duelo.Fila.Fases.Subfases;
using Bounds.Duelo.Emblemas.Fases;
using Ging1991.Core;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Emblemas {

	public class EmblemaAtaque : EmblemaPadre {

		public static void ResolverCombate() {

			EmblemaConocimiento conocimiento = EmblemaConocimiento.getInstancia();
			Fisica fisica = conocimiento.traerFisica();
			Seleccionador seleccionador = Seleccionador.Instancia;
			List<GameObject> cartasEnCampo = new List<GameObject>();
			cartasEnCampo.AddRange(fisica.TraerCartasEnCampo(1));
			cartasEnCampo.AddRange(fisica.TraerCartasEnCampo(2));

			if (seleccionador.atacante == null || !cartasEnCampo.Contains(seleccionador.atacante))
				return;

			if (seleccionador.atacado == null || !cartasEnCampo.Contains(seleccionador.atacado))
				return;

			if (seleccionador.combateCancelado) {
				seleccionador.SeleccionarParaCombate();
				ControlDuelo.Instancia.GetComponent<GestorDeSonidos>().ReproducirSonido("FxRebote");
				return;
			}

			CartaInfo infoAtacante = seleccionador.atacante.GetComponent<CartaInfo>();
			CartaInfo infoAtacada = seleccionador.atacado.GetComponent<CartaInfo>();
			bool ataqueExitoso = false;

			if (infoAtacada.original.clase == "CRIATURA")
				ataqueExitoso = infoAtacante.calcularAtaque() > infoAtacada.calcularDefensa();
			if (infoAtacada.original.clase == "EQUIPO")
				ataqueExitoso = infoAtacante.calcularAtaque() > infoAtacada.calcularDefensa();
			/*
						if (infoAtacante.habilidades.Contains("contagio") && !infoAtacada.habilidades.Contains("contagio"))
							infoAtacada.colocarHabilidad("contagio");
						if (!infoAtacante.habilidades.Contains("contagio") && infoAtacada.habilidades.Contains("contagio"))
							infoAtacante.colocarHabilidad("contagio");
			*/
			int jugadorAtacado = infoAtacada.controlador;
			//atacante.Deseleccionar();

			if (ataqueExitoso) {
				// si lo fue, destruyo al moustro atacado
				bool criaturaDestruida = EmblemaDestruccion.DestruirPorBatalla(seleccionador.atacado);

				if (criaturaDestruida) {
					if (infoAtacante.GetComponent<CartaEfecto>().TieneClave("DESTRUCTOR_MONTAÑAS")) {
						List<GameObject> materiales = fisica.TraerCartasEnMateriales(infoAtacante.controlador);
						int cantidad = 0;
						foreach (GameObject material in materiales) {
							CartaInfo infoMaterial = material.GetComponent<CartaInfo>();
							if (infoMaterial.original.nombre == "Gólem ensamblado" || infoMaterial.original.nombre == "Proto Gólem")
								cantidad += 1000;
						}
						EmblemaEfectos.Activar(new EfectoSobreJugador(infoAtacante.gameObject, jugadorAtacado, new SubModificarLP(-cantidad)));
					}
					if (infoAtacante.GetComponent<CartaEfecto>().TieneClave("BRUTAL_MANO")) {
						EmblemaEfectos.Activar(
							new EfectoSobreJugador(
								infoAtacante.gameObject,
								jugadorAtacado,
								new SubModificarLP(new SubCartasEnMano(jugadorAtacado).Generar().Count * (-500)),
								"CRITICO"
							)
						);
					}
					if (infoAtacante.GetComponent<CartaEfecto>().TieneClave("BRUTAL")) {
						EmblemaEfectos.Activar(
							new EfectoSobreJugador(infoAtacante.gameObject, jugadorAtacado, new SubModificarLP(-500), "CRITICO")
						);
					}
					if (infoAtacante.GetComponent<CartaEfecto>().TieneClave("LIBERAR")) {
						EmblemaEfectos.Activar(
							new EfectoSobreJugador(
								infoAtacante.gameObject,
								infoAtacante.controlador,
								new SubModificarLP(infoAtacada.calcularDefensa()),
								"REVITALIZAR"
							)
						);
					}
					if (infoAtacada.GetComponent<CartaEfecto>().TieneClave("ENVENENAR")) {
						EmblemaEfectos.Activar(
							new EfectoSobreCarta(
								seleccionador.atacado,
								new SubColocarContador("veneno", infoAtacada.GetComponent<CartaEfecto>().GetEfecto("ENVENENAR_N").parametroN),
								seleccionador.atacante
							)
						);
					}

				}
			}
			else {
				ControlDuelo.Instancia.GetComponent<GestorDeSonidos>().ReproducirSonido("FxRebote");
				if (infoAtacante.GetComponent<CartaEfecto>().TieneClave("ENVENENAR_N")) {
					EmblemaEfectos.Activar(
						new EfectoSobreCarta(
							seleccionador.atacante,
							new SubColocarContador("veneno", infoAtacante.GetComponent<CartaEfecto>().GetEfecto("ENVENENAR_N").parametroN),
							seleccionador.atacado
						)
					);
				}
				if (infoAtacada.GetComponent<CartaEfecto>().TieneClave("ENVENENAR")) {
					EmblemaEfectos.Activar(
						new EfectoSobreCarta(
							seleccionador.atacado,
							new SubColocarContador("veneno", infoAtacada.GetComponent<CartaEfecto>().GetEfecto("ENVENENAR_N").parametroN),
							seleccionador.atacante
						)
					);
				}

			}

			PilaEfectos pila = GameObject.FindAnyObjectByType<PilaEfectos>();
			if (pila.EstaVacia()) {
				EmblemaFinalDelCombate.Final(seleccionador.atacante, seleccionador.atacado);
			}
			else {
				FilaFases fila = GameObject.FindAnyObjectByType<FilaFases>();
				fila.Agregar(new SubfaseFinalDelCombate(seleccionador.atacante, seleccionador.atacado));
			}

		}

	}

}