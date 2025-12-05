using Bounds.Persistencia.Lectores;
using static Bounds.Persistencia.Lectores.LectorConfiguracion;

namespace Bounds.Global {

	public class Configuracion {

		private readonly LectorConfiguracion lector;

		public Configuracion() {
			lector = new LectorConfiguracion();
		}


		public bool GastarOro(int cantidad) {
			int cantidadActual = lector.Leer().oro;
			if (cantidadActual < cantidad)
				return false;
			GuardarOro(lector.Leer().oro -= cantidad);
			return true;
		}


		public void GanarOro(int cantidad) {
			GuardarOro(lector.Leer().oro += cantidad);
		}

		public string GetIdioma() {
			return "ESPAÑOL";
//			return "INGLES";
		}


		private void GuardarOro(int cantidad) {
			DatoBD dato = lector.Leer();
			dato.oro = cantidad;
			lector.Guardar(dato);
		}


		public void GuardarCapituloLeccion(int capitulo) {
			if (capitulo > 9 || capitulo < 1) {
				capitulo = 1;
			}
			DatoBD dato = lector.Leer();
			dato.capituloLeccion = capitulo;
			lector.Guardar(dato);
		}
		
		
		public void GuardarCapituloHistoria(int capitulo) {
			if (capitulo > 6 || capitulo < 1) {
				capitulo = 1;
			}
			DatoBD dato = lector.Leer();
			dato.capituloHistoria = capitulo;
			lector.Guardar(dato);
		}


		public void GuardarInicioCarta(int cartaID) {
			if (cartaID > 497 || cartaID < 1)
				cartaID = 1;
			DatoBD dato = lector.Leer();
			dato.inicioCarta = cartaID;
			lector.Guardar(dato);
		}


		public void GuardarInicioPersonaje(int personaje) {
			if (personaje > 500 || personaje < 1)
				personaje = 1;
			DatoBD dato = lector.Leer();
			dato.inicioPersonaje = personaje;
			lector.Guardar(dato);
		}


		public int LeerCapituloLeccion() {
			return lector.Leer().capituloLeccion;
		}


		public int LeerCapituloHistoria() {
			return lector.Leer().capituloHistoria;
		}


		public int LeerInicioCarta() {
			return lector.Leer().inicioCarta;
		}


		public int LeerInicioPersonaje() {
			return lector.Leer().inicioPersonaje;
		}


		public int LeerOro() {
			return lector.Leer().oro;
		}

	}
	
}