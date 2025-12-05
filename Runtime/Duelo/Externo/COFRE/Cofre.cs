using System.Collections.Generic;
using Ging1991.Persistencia.Direcciones;
using Ging1991.Persistencia.Lectores;
using Ging1991.Persistencia.Lectores.Directos;
using UnityEngine;

namespace Bounds.Cofres {

	public class Cofre {

		private readonly LectorListaCadenas lector;
		private Dictionary<string,LineaReceta> cartas;


		public Cofre() {
			lector = new LectorListaCadenas(
				new DireccionDinamica("COFRE", "COFRE.json").Generar(),
				TipoLector.DINAMICO
			);
			lector.InicializarDesdeRecursos(new DireccionRecursos("MAZOS", "COFRE").Generar());
		}


		public List<LineaReceta> GetCartas() {
			InicializarDatos();
			return new List<LineaReceta>(cartas.Values);
		}


		private void InicializarDatos() {
			if (cartas == null) {
				cartas = new Dictionary<string, LineaReceta>();
				foreach(string codigo in lector.Leer().valor) {
					LineaReceta cartaCofre = new LineaReceta(codigo);
					cartas[cartaCofre.GetCodigoParcial()] = cartaCofre;
				}
			}
		}


		public void AgregarCarta(LineaReceta carta) {
			InicializarDatos();
			string codigo = carta.GetCodigoParcial();
			if (!cartas.ContainsKey(codigo)) {
				cartas[codigo] = carta;
			} else {
				cartas[codigo].cantidad += carta.cantidad;
			}
			if (cartas[codigo].cantidad > 5)
				cartas[codigo].cantidad = 5;
		}


		public void RemoverCarta(LineaReceta carta) {
			InicializarDatos();
			if (!cartas.ContainsKey(carta.GetCodigoParcial())) {
				Debug.LogWarning("Se intentó quitar del cofre una carta que no tenía.");
			} else {
				cartas[carta.GetCodigoParcial()].cantidad -= carta.cantidad;
				if (cartas[carta.GetCodigoParcial()].cantidad <= 0)
					cartas.Remove(carta.GetCodigoParcial());
			}
		}


		public void Guardar() {
			InicializarDatos();
			List<string> codigos = new List<string>();
			foreach(LineaReceta linea in cartas.Values) {
				codigos.Add(linea.GetCodigo());
			}
			codigos.Sort();
			lector.Guardar(codigos);
		}


        public int GetCantidadCartasDiferentes(int cartaID) {
			int cantidad = 0;
			foreach(LineaReceta linea in GetCartas()) {
				if (linea.cartaID == cartaID) {
					cantidad += linea.cantidad;
				}
			}
			return cantidad;
		}
		
        public int GetCantidadCartasDiferentes(List<int> cartas) {
			int cantidad = 0;
			List<int> cartasIDDiferentes = new List<int>();
			foreach(LineaReceta linea in GetCartas()) {
				if (!cartasIDDiferentes.Contains(linea.cartaID)) {
					cartasIDDiferentes.Add(linea.cartaID);
				}
			}
			foreach(int cartaID in cartasIDDiferentes) {
				if (cartas.Contains(cartaID)) {
					cantidad++;
				}
			}
			return cantidad;
		}
		

    }

}