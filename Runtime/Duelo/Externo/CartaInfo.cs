using System.Collections.Generic;
using Bounds.Modulos.Cartas;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using UnityEngine;

namespace Bounds.Fisicas.Carta {

	public class CartaInfo : MonoBehaviour {

		public int cartaID = 0;
		public string rareza = "N";
		public string imagen = "A";
		public int netID = 0;
		public int propietario = 0;
		public int controlador = 0;
		public CartaBD original;
		public Dictionary<GameObject, int> bonosAtaque = new Dictionary<GameObject, int>();
		public Dictionary<GameObject, int> bonosDefensa = new Dictionary<GameObject, int>();
		public Dictionary<string, int> contadores = new Dictionary<string, int>();
		public GameObject criaturaEquipada;


		public void cargar(CartaBD original) {
			GetComponent<CartaEfecto>().Inicializar(original);
			this.original = original;
			restablecer();
		}


		public void restablecer() {
			controlador = propietario;
			bonosAtaque = new Dictionary<GameObject, int>();
			bonosDefensa = new Dictionary<GameObject, int>();
			contadores = new Dictionary<string, int>();

			CartaContador scr = GetComponent<CartaContador>();
			scr.SetContador("veneno", 0, false);
			scr.SetContador("poder", 0, false);
			scr.SetContador("debilidad", 0, false);
			scr.SetContador("supevivencia", 0, false);

			criaturaEquipada = null;

			CartaTipo cartaTipo = GetComponent<CartaTipo>();
			cartaTipo.RemoverTodo();
			if (original.clase == "CRIATURA" && original.datoCriatura.tipos != null)
				foreach (string tipo in original.datoCriatura.tipos)
					cartaTipo.AgregarTipo(tipo);

			// destruyo el efecto de ataque
			CartaFX fx = GetComponent<CartaFX>();
			fx.PotencialAtacante(false);
			fx.EfectoMuro(false);
		}


		public void ColocarContador(string tipo, int cantidad) {
			CartaContador cartaContador = GetComponent<CartaContador>();
			cartaContador.SetContador(tipo, TraerContadores(tipo) + cantidad, false);

			if (!new List<string>(contadores.Keys).Contains(tipo))
				contadores.Add(tipo, 0);

			contadores[tipo] += cantidad;
			RecalcularEstadisticas();

			if (tipo == "poder")
				GetComponent<CartaFX>().GanarPuntaje(500);
		}


		public void RemoverContador(string tipo, int cantidad) {
			CartaContador scr = GetComponent<CartaContador>();
			scr.SetContador(tipo, TraerContadores(tipo) - cantidad, false);

			if (!new List<string>(contadores.Keys).Contains(tipo))
				contadores.Add(tipo, 0);
			contadores[tipo] -= cantidad;
			RecalcularEstadisticas();
		}


		public int TraerContadores(string tipo) {
			if (new List<string>(contadores.Keys).Contains(tipo))
				return contadores[tipo];
			return 0;
		}



		//****************** MANEJO DE BONOS ATAQUE Y DEFENSA *************************************************************



		/// <summary> Coloca un bono de ataque, positivo o negativo, de cantidad x de un origen sobre la carta. </summary>
		public void colocarBonoAtaque(GameObject origen, int cantidad) {
			if (!new List<GameObject>(bonosAtaque.Keys).Contains(origen))
				bonosAtaque.Add(origen, 0);
			bonosAtaque[origen] += cantidad;
			RecalcularEstadisticas();
		}


		/// <summary> Coloca un bono de ataque de cantidad x de un origen sobre la carta. </summary>
		public void removerBonoAtaque(GameObject origen, int cantidad) {
			if (!new List<GameObject>(bonosAtaque.Keys).Contains(origen))
				bonosAtaque.Add(origen, 0);
			bonosAtaque[origen] -= cantidad;
			RecalcularEstadisticas();
		}


		/// <summary> Devuelve el ataque de la criatura. </summary>
		public int calcularAtaque() {
			int ataque = original.datoCriatura.ataque;
			ataque += TraerContadores("poder") * 500;
			ataque -= TraerContadores("debilidad") * 500;
			foreach (GameObject clave in new List<GameObject>(bonosAtaque.Keys))
				ataque += bonosAtaque[clave];
			if (ataque < 0)
				ataque = 0;
			return ataque;
		}


		/// <summary> Coloca un bono de defensa, positivo o negativo, de cantidad x de un origen sobre la carta. </summary>
		public void colocarBonoDefensa(GameObject origen, int cantidad) {
			if (!new List<GameObject>(bonosDefensa.Keys).Contains(origen))
				bonosDefensa.Add(origen, 0);
			bonosDefensa[origen] += cantidad;
			RecalcularEstadisticas();
		}


		/// <summary> Coloca un bono de defensa de cantidad x de un origen sobre la carta. </summary>
		public void removerBonoDefensa(GameObject origen, int cantidad) {
			if (!new List<GameObject>(bonosDefensa.Keys).Contains(origen))
				bonosDefensa.Add(origen, 0);
			bonosDefensa[origen] -= cantidad;
			RecalcularEstadisticas();
		}


		/// <summary> Devuelve el defensa de la criatura. </summary>
		public int calcularDefensa() {
			if (original.clase == "EQUIPO")
				return original.defensa;

			int defensa = original.datoCriatura.defensa;
			defensa += TraerContadores("poder") * 500;
			defensa -= TraerContadores("debilidad") * 500;
			foreach (GameObject clave in new List<GameObject>(bonosDefensa.Keys))
				defensa += bonosDefensa[clave];
			if (defensa < 0)
				defensa = 0;
			return defensa;
		}


		private void RecalcularEstadisticas() {
			CartaFrente cartaFrente = GetComponentInChildren<CartaFrente>();
			cartaFrente.SetEstadisticas();
			if (original.clase == "CRIATURA")
				cartaFrente.SetEstadisticas(calcularAtaque(), calcularDefensa());
			if (original.clase == "EQUIPO")
				cartaFrente.SetEstadisticas(calcularDefensa());
		}

	}


}