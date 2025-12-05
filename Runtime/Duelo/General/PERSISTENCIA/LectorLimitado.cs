using System.Collections.Generic;
using Bounds.Persistencia.Datos;
using Ging1991.Persistencia.Direcciones;
using Ging1991.Persistencia.Lectores;
using Ging1991.Persistencia.Lectores.Directos;

namespace Bounds.Persistencia {

	public class LectorLimitado {

		private readonly LectorCadena lectorColeccion;
		private readonly LectorCadena lectorEstado;
		private readonly LectorInternoStream lectorMazo;
		private readonly LectorCadena lectorEnemigoResultado1;
		private readonly LectorCadena lectorEnemigoResultado2;
		private readonly LectorCadena lectorEnemigoResultado3;

		public LectorLimitado() {
			DireccionDinamica direccion = new DireccionDinamica("LIMITADO");
			lectorColeccion = GenerarLector(direccion.Generar("COLECCION.json"));
			lectorEstado = GenerarLector(direccion.Generar("ESTADO.json"));
			lectorEnemigoResultado1 = GenerarLector(direccion.Generar("PESADILLA.json"));
			lectorEnemigoResultado2 = GenerarLector(direccion.Generar("PIRATA.json"));
			lectorEnemigoResultado3 = GenerarLector(direccion.Generar("SIRENA.json"));
			lectorMazo = new LectorInternoStream(direccion.Generar("MAZO_JUGADOR.json"));
		}


		private LectorCadena GenerarLector(string direccion) {
			LectorCadena lector = new LectorCadena(direccion, TipoLector.DINAMICO);
			if (!lector.ExistenDatos())
				lector.Guardar("");
			return lector;
		}


		public string LeerColeccion() {
			return lectorColeccion.Leer().valor;
		}


		public void GuardarColeccion(string coleccion) {
			lectorColeccion.Guardar(coleccion);
		}


		public string LeerEstado() {
			return lectorEstado.Leer().valor;
		}


		public void GuardarEstado(string estado) {
			lectorEstado.Guardar(estado);
		}


		public MazoBD LeerMazo() {
			return lectorMazo.Leer();
		}


		public void GuardarMazo(MazoBD valor) {
			lectorMazo.Guardar(valor);
		}


		public string LeerResultado(string enemigo) {
			if (enemigo == "PESADILLA")
				return lectorEnemigoResultado1.Leer().valor;
			if (enemigo == "PIRATA")
				return lectorEnemigoResultado2.Leer().valor;
			if (enemigo == "SIRENA")
				return lectorEnemigoResultado3.Leer().valor;
			return "";
		}


		public void GuardarResultado(string enemigo, string resultado) {
			if (enemigo == "PESADILLA") {
				lectorEnemigoResultado1.Guardar(resultado);
			}
			if (enemigo == "PIRATA") {
				lectorEnemigoResultado2.Guardar(resultado);
			}
			if (enemigo == "SIRENA") {
				lectorEnemigoResultado3.Guardar(resultado);
			}
		}


		private class LectorInternoStream : LectorGenerico<MazoBD> {

			public LectorInternoStream(string direccion) : base(direccion, TipoLector.DINAMICO) { }

		}


	}

}