using Ging1991.Persistencia.Direcciones;
using Ging1991.Persistencia.Lectores;

namespace Bounds.Persistencia.Lectores {

	public class LectorConfiguracion {

		private readonly LectorInterno lector;
		private readonly int ORO_INICIAL = 20000;

		public LectorConfiguracion() {
			lector = new LectorInterno(new DireccionDinamica("CONFIGURACION", "CONFIGURACION.json").Generar(), TipoLector.DINAMICO);
			if (!lector.ExistenDatos()) {
				DatoBD dato = new DatoBD();
				dato.capituloHistoria = 1;
				dato.capituloLeccion = 1;
				dato.inicioCarta = 7;
				dato.oro = ORO_INICIAL;
				lector.Guardar(dato);
			}
		}


		public DatoBD Leer() {
			return lector.Leer();
		}


		public void Guardar(DatoBD dato) {
			lector.Guardar(dato);
		}


		[System.Serializable]
		public class DatoBD {
			public string nombre;
			public int capituloHistoria;
			public int capituloLeccion;
			public int oro;
			public int inicioCarta;
			public int inicioPersonaje;
		}


		private class LectorInterno : LectorGenerico<DatoBD> {

			public LectorInterno(string direccion, TipoLector tipo) : base(direccion, tipo) { }

		}


	}

}