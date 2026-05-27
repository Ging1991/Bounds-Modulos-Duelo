using UnityEngine;
using System.Collections.Generic;
using Bounds.Duelo.Emblema;
using Bounds.Duelo.Carta;
using Bounds.Duelo.Condiciones;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Utiles;
using Bounds.Modulos.Duelo.Fisicas;
using Bounds.Fisicas.Carta;

namespace Bounds.Duelo.Efectos {

	public class EfectoAlienEncuentraFusion : EfectoAlien {

		public EfectoAlienEncuentraFusion(GameObject fuente) : base(fuente) { }


		public override void Resolver() {

			GameObject fusion = EnviarFusion();
			if (fusion != null) {
				int jugador = fuente.GetComponent<CartaInfo>().controlador;
				Fisica fisica = Fisica.Instancia;

				CondicionMultiple condicion = new CondicionMultiple(CondicionMultiple.Tipo.Y);
				condicion.AgregarCondicion(new CondicionCriaturaPerfeccion("FUSION"));
				condicion.AgregarCondicion(new CondicionPerfeccion(soloPerfectos: true));
				List<GameObject> cartas = condicion.CumpleLista(fisica.TraerCartasEnMazo(jugador));
				int contador = 0;

				foreach (GameObject carta in cartas) {
					if (fisica.TraerCartasEnMano(jugador).Count == 5)
						break;

					fisica.EnviarHaciaMano(carta, jugador);
					contador++;
					if (jugador == 1) {
						CartaGeneral componente = carta.GetComponent<CartaGeneral>();
						componente.ColocarBocaArriba();
					}
				}
				EmblemaVida.DisminuirVida(JugadorDuelo.Adversario(jugador), contador * 500, "VENENO");
			}
		}


	}

}