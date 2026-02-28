using UnityEngine;
using UnityEngine.UI;
using infraestructura;
using Ging1991.Core.Movimiento;
using Ging1991.Core.Interfaces;

namespace Bounds.Duelo.Utiles {

	public class AniTexto : MonoBehaviour, IEjecutable {


		public void Iniciar(int cantidad) {

			// El texto se mueve levemente
			Movimiento movimiento = gameObject.GetComponent<Movimiento>();
			Vector3 direccion = transform.position + new Vector3(5, 0, 0);
			movimiento.Posicionar(direccion, this);
			movimiento.velocidad = 0.05f;

			// Seteo el texto, si es positivo le agrego el simbolo de mas
			Text texto = gameObject.GetComponent<Text>();
			texto.text = "" + cantidad;
			if (cantidad > 0)
				texto.text = "+" + cantidad;
		}


		public void Ejecutar() {
			Destroy(gameObject, 2);
		}


	}

}