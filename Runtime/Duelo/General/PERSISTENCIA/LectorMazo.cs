using System.Collections.Generic;
using Bounds.Persistencia.Datos;
using Ging1991.Persistencia.Direcciones;
using Ging1991.Persistencia.Lectores;
using Ging1991.Persistencia.Lectores.Directos;

namespace Bounds.Persistencia {
		
	public class LectorMazo {

		private readonly Direccion direccionJugador;
		private readonly Direccion direccionOponentes;
		private readonly Direccion direccionInicial;

		public LectorMazo() {
			direccionJugador = new DireccionDinamica("MAZOS");
			direccionOponentes = new DireccionRecursos("MAZOS\\OPONENTES");
			direccionInicial = new DireccionRecursos("MAZOS\\INICIAL");
		}


		public MazoBD LeerMazoOponente(string nombre) {
			return new LectorInternoRecursos(direccionOponentes.Generar(nombre)).Leer();
		}


		public MazoBD LeerMazoInicial(string nombre) {
			return new LectorInternoRecursos(direccionInicial.Generar(nombre)).Leer();
		}


		public MazoBD LeerMazoJugador(string nombre) {
			LectorInternoStream lector = new LectorInternoStream(direccionJugador.Generar($"{nombre}.json"));
			if (!lector.ExistenDatos()) {
				lector.InicializarDesdeRecursos(new DireccionRecursos("MAZOS", nombre).Generar());
			}
			return lector.Leer();
		}
		

		public void EliminarMazo(string nombre) {
			LectorInternoStream lector = new LectorInternoStream(direccionJugador.Generar($"{nombre}.json"));
			lector.EliminarDatos();
		}
		

		public MazoBD LeerMazoJugador() {
			return LeerMazoJugador(GetMazoPredeterminado());
		}


		public void Guardar(List<string> cartas, string nombre, int principalVacio, int principalCarta) {
			/*Dato dato = LeerMazoJugador();
			dato.cartas = cartas;
			dato.nombre = nombre;
			dato.principalVacio = principalVacio;
			dato.principalCarta = principalCarta;
			LectorInternoStream lectorInterno = new LectorInternoStream(direccionJugador.Generar($"{GetMazoPredeterminado()}.json"));
			lectorInterno.Guardar(dato);*/
		}


		public string GetMazoPredeterminado() {
			Direccion direccionCadena = new DireccionDinamica("MAZOS", "PREDETERMINADO.json");
			Direccion direccionInicial = new DireccionRecursos("MAZOS", "PREDETERMINADO.json");
			LectorCadena lectorCadena = new LectorCadena(direccionCadena.Generar(), TipoLector.DINAMICO);
			lectorCadena.InicializarDesdeRecursos(direccionInicial.Generar());
			return lectorCadena.Leer().valor;
		}
		

        private class LectorInternoStream : LectorGenerico<MazoBD> {

            public LectorInternoStream(string direccion) : base(direccion, TipoLector.DINAMICO) {}

        }


        private class LectorInternoRecursos : LectorGenerico<MazoBD> {

            public LectorInternoRecursos(string direccion) : base(direccion, TipoLector.RECURSOS) {}

        }




	}

}