using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Pila;
using Bounds.Duelo.Pila.Subefectos;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Efectos {

	public class EfectoEncuentraMaterialesID : EfectoBase {

		private readonly int jugador;
		private readonly GameObject coste;

		public EfectoEncuentraMaterialesID(GameObject fuente, int jugador, GameObject coste) : base(fuente) {
			this.jugador = jugador;
			this.coste = coste;
		}


		public override void Resolver() {
			List<MaterialBD> materiales = coste.GetComponent<CartaInfo>().original.materiales;
			Fisica fisica = Fisica.Instancia;
			List<GameObject> cartasEnMazo = new SubCartasEnMazo(jugador).Generar();
			List<GameObject> encontradas = new();

			foreach (var material in materiales) {
				foreach (GameObject cartaMazo in cartasEnMazo) {
					if (!encontradas.Contains(cartaMazo)) {
						if (cartaMazo.GetComponent<CartaInfo>().cartaID == material.parametroID) {
							encontradas.Add(cartaMazo);
							break;
						}
					}
				}
			}
			foreach (GameObject encontrada in encontradas) {
				fisica.EnviarHaciaMano(encontrada, jugador);
				if (jugador == 1) {
					encontrada.GetComponent<CartaGeneral>().ColocarBocaArriba();
				}
			}
		}


	}

}