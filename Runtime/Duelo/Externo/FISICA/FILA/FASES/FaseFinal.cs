using System.Collections.Generic;
using Bounds.Duelo.Emblemas;
using Bounds.Duelo.Fila.Fases.Subfases;
using Bounds.Duelo.Utiles;
using UnityEngine;

namespace Bounds.Duelo.Fila.Fases {

	public class FaseFinal : IFase {

		private readonly int jugador;

		public FaseFinal(int jugador) {
			this.jugador = jugador;
		}

        void IFase.Resolver() {
			
			FilaFases fila = GameObject.FindAnyObjectByType<FilaFases>();
			fila.Agregar(new SubfaseLimpiezaDePermantentes(JugadorDuelo.Adversario(jugador)));
	
			if (EmblemaDestruccionImperfectos.TraerCartas(JugadorDuelo.Adversario(jugador)).Count > 0)
				fila.Agregar(new SubfaseDestruirImperfectos(JugadorDuelo.Adversario(jugador)));
	
			fila.Agregar(new SubfaseCambioDeTurno(jugador));
        }

    }

}