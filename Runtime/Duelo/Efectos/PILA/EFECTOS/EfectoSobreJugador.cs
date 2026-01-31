using Bounds.Duelo.Pila.Subefectos;
using Bounds.Visuales;
using UnityEngine;

namespace Bounds.Duelo.Pila.Efectos {

	public class EfectoSobreJugador : EfectoBase {

		private readonly ISubSobreJugador subefecto;
		private readonly int jugador;

		public EfectoSobreJugador(GameObject fuente, int jugador, ISubSobreJugador subefecto, string etiqueta = "") : base(fuente) {
			this.jugador = jugador;
			this.subefecto = subefecto;
			if (etiqueta != "")
				AgregarEtiqueta(etiqueta);
		}


		public override void Resolver() {
			subefecto.AplicarEfecto(jugador, etiquetas);
			if (etiquetas.Contains("EXPLOSION"))
				fuente.GetComponentInChildren<GestorVisual>().Animar("EXPLOSION", "FxExplosion");
			if (etiquetas.Contains("CRITICO"))
				fuente.GetComponentInChildren<GestorVisual>().Animar("CRITICO", "FxExplosion");
			if (etiquetas.Contains("ROBAR"))
				fuente.GetComponentInChildren<GestorVisual>().Animar("NUBE", "FxAdquisicion");
			if (etiquetas.Contains("REVITALIZAR"))
				fuente.GetComponentInChildren<GestorVisual>().Animar("REVITALIZAR", "FxAdquisicion");
			if (etiquetas.Contains("VENENO"))
				fuente.GetComponentInChildren<GestorVisual>().Animar("VENENO", "FxSerpiente");
		}


	}

}