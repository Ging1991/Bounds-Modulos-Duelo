using Bounds.Duelo.Carta;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionPerfeccion : CondicionCarta {
		
		private readonly bool soloImperfectos;
		private readonly bool soloPerfectos;
		private readonly bool soloPuedeInvocar;

		public CondicionPerfeccion(bool soloImperfectos = false, bool soloPuedeInvocar = false, bool soloPerfectos = false) {
			this.soloImperfectos= soloImperfectos;
			this.soloPerfectos= soloPerfectos;
			this.soloPuedeInvocar= soloPuedeInvocar;
		}


		public override bool Cumple(GameObject carta) {
			CartaPerfeccion info = carta.GetComponent<CartaPerfeccion>();
			Acumulador acumulador = new Acumulador();
			acumulador.AgregarVerificacion(soloPerfectos, info.EsPerfecta());
			acumulador.AgregarVerificacion(soloImperfectos, !info.EsPerfecta());
			acumulador.AgregarVerificacion(soloPuedeInvocar, info.PuedeInvocar());
			return acumulador.valor;
		}


	}

}