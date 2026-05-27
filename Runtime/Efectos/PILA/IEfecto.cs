using System.Collections.Generic;
using UnityEngine;

namespace Bounds.Duelo.Pila {

	public interface IEfecto {

		GameObject GetFuente();

		List<string> GetEtiquetas();

		void Resolver();

	}

}