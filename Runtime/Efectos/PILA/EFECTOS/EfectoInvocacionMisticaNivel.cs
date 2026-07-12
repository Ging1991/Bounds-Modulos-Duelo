using UnityEngine;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Pila.Subefectos;
using System.Collections.Generic;
using Ging1991.Musica;
using Bounds.Duelo.Carta;

namespace Bounds.Duelo.Efectos {

	public class EfectoInvocacionMisticaNivel : EfectoBase {

		private readonly int jugador;
		private readonly int nivel;

		public EfectoInvocacionMisticaNivel(GameObject fuente, int jugador, int nivel) : base(fuente) {
			this.jugador = jugador;
			this.nivel = nivel;
		}

		public override void Resolver() {
			CondicionMultiple condicion = new CondicionMultiple(CondicionMultiple.Tipo.Y);
			condicion.AgregarCondicion(new CondicionNivel(nivel));
			condicion.AgregarCondicion(new CondicionCriaturaPerfeccion("MISTICO"));

			List<GameObject> criaturas = new SubCartasEnMazo(jugador, condicion).Generar();
			criaturas.AddRange(new SubCartasEnMano(jugador, condicion).Generar());
			criaturas.AddRange(new SubCartasEnCementerio(jugador, condicion).Generar());
			GameObject campo = BuscadorCampo.getInstancia().buscarCampoLibre(jugador);

			if (campo != null && criaturas.Count > 0) {
				criaturas[0].GetComponent<CartaPerfeccion>().esPefecta = true;
				EmblemaInvocacionPerfecta.Invocar(jugador, criaturas[0], new List<GameObject>());
			}
			else {
				ControlDuelo.Instancia.gestorDeSonidos.ReproducirSonido("FxRebote");
			}
		}

	}

}