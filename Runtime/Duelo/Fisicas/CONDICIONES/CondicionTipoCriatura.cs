using System.Collections.Generic;
using System.Linq;
using Bounds.Duelo.Carta;
using UnityEngine;

namespace Bounds.Duelo.Condiciones {

	public class CondicionTipoCriatura : CondicionCadena {
		

		public CondicionTipoCriatura(string tipo = null, List<string> tipos = null) :
				base(valor:tipo, valores:tipos) {

			precondiciones.Add(new CondicionClase(clase:"CRIATURA"));
		}


		public override bool Cumple(GameObject carta) {
			if (!CumplePrecondiciones(carta))
				return false;

			CartaInfo info = carta.GetComponent<CartaInfo>();
			CartaTipo cartaTipo = carta.GetComponent<CartaTipo>();
			Acumulador acumulador = new Acumulador();
			acumulador.AgregarVerificacion(valor != null, cartaTipo.ContieneTipo(valor));
			acumulador.AgregarVerificacion(valores.Count > 0, valores.Intersect(info.original.datoCriatura.tipos).ToArray().Count() > 0);
			return acumulador.valor;
		}


		public override string GetValor(GameObject carta) {
			return null;
		}


	}

}