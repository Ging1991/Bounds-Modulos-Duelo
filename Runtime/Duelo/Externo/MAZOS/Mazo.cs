using System.Collections.Generic;
using Bounds.Global.Mazos;
using Bounds.Persistencia.Datos;
using Ging1991.Persistencia.Direcciones;
using Ging1991.Persistencia.Lectores;
using UnityEngine;

namespace Bounds.Global {

	public abstract class Mazo {

		public string codigo;
		public string nombre;
		public string formato;
		public List<CartaMazo> cartas;
		public CartaMazo principalVacio;
		public CartaMazo emblema;
		public Sprite protector;

		protected void Inicializar(MazoBD datos) {
			codigo = datos.codigo;
			nombre = datos.nombre;
			formato = datos.formato;
			principalVacio = ConvertirSeguro(datos.principalVacio);
			emblema = ConvertirSeguro(datos.emblema);
			cartas = ConvertirCartas(datos.cartas);
			if (datos.protector != null && datos.protector != "")
				protector = LectorImagenes.LeerDesdeRecursos(new DireccionRecursos("IMAGENES/PROTECTORES", datos.protector).Generar());
		}

		private CartaMazo ConvertirSeguro(string clave) {
			try {
				return new CartaMazo(clave);
			} catch {

			}
			return null;
		}

		public MazoBD Serializar() {
			MazoBD datos = new MazoBD();

			datos.codigo = codigo;
			datos.nombre = nombre;
			datos.formato = formato;
			if (principalVacio != null)
				datos.principalVacio = principalVacio.GetCodigo();
			if (emblema != null)
				datos.emblema = emblema.GetCodigo();
			datos.cartas = DesconvertirCartas(cartas);
			if (protector != null)
				datos.protector = protector.name;
			
			return datos;
		}


		public static List<CartaMazo> ConvertirCartas(List<string> claves) {
			List<CartaMazo> cartas = new List<CartaMazo>();
			foreach (string clave in claves) {
				cartas.Add(new CartaMazo(clave));
			}
			return cartas;
		}


		public List<string> DesconvertirCartas(List<CartaMazo> claves) {
			List<string> cartas = new List<string>();
			foreach (CartaMazo clave in claves) {
				cartas.Add(clave.GetCodigo());
			}
			return cartas;
		}


		protected class LectorRecursos : LectorGenerico<MazoBD> {

			public LectorRecursos(string direccion) : base(direccion, TipoLector.RECURSOS) {}

		}


		protected class LectorDinamico : LectorGenerico<MazoBD> {

			public LectorDinamico(string direccion) : base(direccion, TipoLector.DINAMICO) {}

		}


	}

}