using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Bounds.Duelo.Carta;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo {

	public class Condicion {

		private int controlador;
		private string tipoCarta;
		private string textoParcial;
		private List<string> tipoCriatura;
		private string perfeccion;
		private bool estaEnderezadoRequerido;


		public Condicion(int controlador = -1, string tipoCarta = "", string perfeccion = "",
				List<string> tipoCriatura = null, string textoParcial = "", bool estaEnderezadoRequerido = false) {
			this.controlador = controlador;
			this.tipoCarta = tipoCarta;
			this.tipoCriatura = tipoCriatura;
			this.perfeccion = perfeccion;
			this.textoParcial = textoParcial;
			this.estaEnderezadoRequerido = estaEnderezadoRequerido;
		}


		public bool Cumple(GameObject carta) {
			bool cumpleCondiciones = true;
			CartaInfo info = carta.GetComponent<CartaInfo>();
			CartaTipo cartaTipo = carta.GetComponent<CartaTipo>();
			CartaMovimiento cartaMovimiento = carta.GetComponent<CartaMovimiento>();

			// Condicion: girado
			cumpleCondiciones = cumpleCondiciones &&
							(estaEnderezadoRequerido == false || cartaMovimiento.estaGirado == false);

			// Condicion: controlador
			cumpleCondiciones = cumpleCondiciones &&
							(controlador == -1 || info.controlador == controlador);

			// Condicion: texto parcial en el nombre
			cumpleCondiciones = cumpleCondiciones &&
							(textoParcial == "" || info.original.nombre.Contains(textoParcial));

			// Condicion: tipo de carta
			cumpleCondiciones = cumpleCondiciones &&
							(tipoCarta == "" || info.original.clase == tipoCarta);

			if (info.original.clase == "CRIATURA") {

				// Condicion: perfeccion
				cumpleCondiciones = cumpleCondiciones &&
								(perfeccion == "" || info.original.datoCriatura.perfeccion == perfeccion);

				// Condicion: tipo criatura
				if (tipoCriatura != null) {
					int coincidencias = tipoCriatura.Intersect(cartaTipo.tipos).ToArray().Count();
					cumpleCondiciones = cumpleCondiciones && coincidencias > 0;
				}
			}

			return cumpleCondiciones;
		}


		public List<GameObject> CumpleLista(List<GameObject> cartas) {
			List<GameObject> cumplen = new List<GameObject>();

			foreach (GameObject carta in cartas)
				if (Cumple(carta))
					cumplen.Add(carta);

			return cumplen;
		}


	}

}