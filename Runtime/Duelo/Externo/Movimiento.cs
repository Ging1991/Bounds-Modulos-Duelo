using Ging1991.Core.Movimiento;
using UnityEngine;

namespace infraestructura
{

	public class Movimiento : MonoBehaviour
	{

		public float velocidad = 0.1f;
		private bool debeDesplazar = false;
		public ILlegar accion;
		public Vector3 direccion;


		public void Posicionar(Vector3 direccion, ILlegar accion = null)
		{
			this.direccion = direccion;
			debeDesplazar = true;
			this.accion = accion;
		}


		void FixedUpdate()
		{
			desplazar();
		}


		private void desplazar()
		{
			if (debeDesplazar)
			{
				transform.position = Vector3.MoveTowards(transform.position, direccion, velocidad);
				if (Vector3.Distance(transform.position, direccion) < 0.001f)
					debeDesplazar = false;
				if (accion != null)
					accion.Llegar();
			}
		}


	}

}