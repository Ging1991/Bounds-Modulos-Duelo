using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.CPU.Condiciones;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Duelo.Fisicas;
using UnityEngine;

namespace Bounds.Duelo.CPU.Acciones {

	public class AccionAtacar : AccionBasica {

		public AccionAtacar(int prioridad, int jugador) : base(prioridad, jugador) {

			CondicionMultiple condicionObjetivos = new CondicionMultiple(CondicionMultiple.Tipo.O);
			condicionObjetivos.AgregarCondicion(new CondicionClase("CRIATURA"));
			condicionObjetivos.AgregarCondicion(new CondicionClase("EQUIPO"));

			CondicionMultiple condicionAtacantes = new CondicionMultiple(CondicionMultiple.Tipo.Y);
			condicionAtacantes.AgregarCondicion(new CondicionClase("CRIATURA"));
			condicionAtacantes.AgregarCondicion(new CondicionEstaGirado(false));

			condiciones = new List<ICondicionDeJuego>();
			condiciones.Add(new EsFaseDeTurno(EmblemaTurnos.Fase.FASE_DE_BATALLA));
			condiciones.Add(new TieneCartasEnZona(jugador, condicionAtacantes, Zonas.CAMPO, 1));
			condiciones.Add(new TieneCartasEnZona(1, condicionObjetivos, Zonas.CAMPO, 1));
		}

		public override void Ejecutar() {
			BuscadorCartas buscador = BuscadorCartas.GetInstancia();
			GameObject atacante = buscador.BuscarMejorAtacante(2);
			if (atacante != null) {
				Entrada entrada = Entrada.GetInstancia();
				entrada.PresionarCarta(2, atacante);
				GameObject objetivo = buscador.BuscarMejorAtacado(1, atacante.GetComponent<CartaInfo>().calcularAtaque());
				if (objetivo != null) {
					entrada.PresionarCarta(2, objetivo);
				}
				else {
					posponer = true;
				}
			}
			else {
				posponer = true;
			}
		}

	}

}