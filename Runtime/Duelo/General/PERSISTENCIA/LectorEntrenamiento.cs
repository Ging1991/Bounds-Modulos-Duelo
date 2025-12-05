using System.Collections.Generic;
using Ging1991.Persistencia.Direcciones;
using Ging1991.Persistencia.Lectores;

namespace Bounds.Persistencia {

	public class LectorEntrenamiento {

		private static LectorEntrenamiento instancia;
		private readonly LectorInterno lectorInterno;
		
		public static LectorEntrenamiento GetInstancia() {
			if (instancia == null)
				instancia = new LectorEntrenamiento();
			return instancia;
		}


		private LectorEntrenamiento () {
			DireccionDinamica direccion = new DireccionDinamica("ENTRENAMIENTO", "ENTRENAMIENTO.json");
			lectorInterno = new LectorInterno(direccion.Generar(), TipoLector.DINAMICO);
			
			if (!lectorInterno.ExistenDatos())
				lectorInterno.Guardar(new ListaDato());
		}


		public void Habilitar(string nombre) {
			lectorInterno.CrearSiNoExiste(nombre);
			Dato dato = lectorInterno.GetDato(nombre);
			dato.habilitado = true;
			lectorInterno.Guardar();
		}

		
		public void IncrementarVictorias(string nombre) {
			lectorInterno.CrearSiNoExiste(nombre);
			Dato dato = lectorInterno.GetDato(nombre);
			dato.victorias++;
			lectorInterno.Guardar();
		}


		public void IncrementarDerrotas(string nombre) {
			lectorInterno.CrearSiNoExiste(nombre);
			Dato dato = lectorInterno.GetDato(nombre);
			dato.derrotas++;
			lectorInterno.Guardar();
		}

		
		public int LeerVictorias(string nombre) {
			lectorInterno.CrearSiNoExiste(nombre);
			Dato dato = lectorInterno.GetDato(nombre);
			return dato.victorias;
		}


		public int LeerDerrotas(string nombre) {
			lectorInterno.CrearSiNoExiste(nombre);
			Dato dato = lectorInterno.GetDato(nombre);
			return dato.derrotas;
		}


		public bool EstaHabilitado(string nombre) {
			lectorInterno.CrearSiNoExiste(nombre);
			Dato dato = lectorInterno.GetDato(nombre);
			return dato.habilitado;
		}


		[System.Serializable]
		public class Dato {

			public string nombre;
			public int victorias;
			public int derrotas;
			public bool habilitado;

		}


		[System.Serializable]
		public class ListaDato {

			public List<Dato> lista;

		}


		private class LectorInterno : LectorGenerico<ListaDato> {

			public LectorInterno(string direccion, TipoLector tipo) : base(direccion, tipo) {}


			public Dato GetDato(string nombre) {
				Dato ret = null;
				foreach (var dato in Leer().lista) {
					if (dato.nombre == nombre) {
						ret = dato;
					}
				}
				return ret;
			}


			public void CrearSiNoExiste(string nombre) {
				Dato dato = GetDato(nombre);
				if (dato == null) {
					ListaDato listaDato = Leer();
					dato = new Dato();
					dato.nombre = nombre;
					dato.habilitado = false;
					dato.derrotas = 0;
					dato.victorias = 0;
					listaDato.lista.Add(dato);
					Guardar();
				}
			}


		}


	}

}