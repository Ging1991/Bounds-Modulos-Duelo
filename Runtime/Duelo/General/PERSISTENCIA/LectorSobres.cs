using System.Collections.Generic;
using Ging1991.Persistencia.Direcciones;
using Ging1991.Persistencia.Lectores;

namespace Bounds.Persistencia {

	public class LectorSobres {

		private static LectorSobres instancia;
		private readonly LectorInterno lectorInterno;


		public static LectorSobres GetInstancia() {
			if (instancia == null)
				instancia = new LectorSobres();
			return instancia;
		}


		private LectorSobres() {
			Direccion direccion = new DireccionDinamica("TIENDA", "SOBRES.json");
			lectorInterno = new LectorInterno(direccion.Generar(), TipoLector.DINAMICO);
			if (!lectorInterno.ExistenDatos()) {
				lectorInterno.Guardar(new DatoLista());
			}
		}


		public int GetCantidad(string coleccion) {
			Dato dato = lectorInterno.GetDato(coleccion);
			if (dato != null) {
				return dato.cantidad;
			}
			return 0;
		}


		public void ModificarCantidad (string coleccion, int cantidad) {
			CrearSobreSiNoExiste(coleccion);
			Dato dato = lectorInterno.GetDato(coleccion);
			dato.cantidad += cantidad;
			lectorInterno.Guardar(lectorInterno.Leer());
			EliminarSobreSiEstaVacio(coleccion);
		}


		private void CrearSobreSiNoExiste(string coleccion) {
			Dato dato = lectorInterno.GetDato(coleccion);
			DatoLista datoLista = lectorInterno.Leer();
			if (dato == null) {
				dato = new Dato();
				dato.nombre = coleccion;
				dato.cantidad = 0;
				datoLista.lista.Add(dato);
				lectorInterno.Guardar(datoLista);
			}
		}


		private void EliminarSobreSiEstaVacio(string coleccion) {
			Dato dato = lectorInterno.GetDato(coleccion);
			DatoLista datoLista = lectorInterno.Leer();
			if (dato != null && dato.cantidad == 0) {
				datoLista.lista.Remove(dato);
				lectorInterno.Guardar(datoLista);
			}
		}


        private class LectorInterno : LectorGenerico<DatoLista> {

            public LectorInterno(string direccion, TipoLector tipo) : base(direccion, tipo) {}

			public Dato GetDato(string coleccion) {
				Dato ret = null;
				foreach (var dato in Leer().lista) {
					if (dato.nombre == coleccion)
						ret = dato;
				}
				return ret;
			}

        }


        [System.Serializable]
		public class DatoLista {

			public List<Dato> lista;

		}


		[System.Serializable]
		public class Dato {

			public string nombre;
			public int cantidad;

		}


	}

}