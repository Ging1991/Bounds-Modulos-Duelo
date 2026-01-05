using Bounds.Infraestructura;
using Ging1991.Interfaces.Temas;
using UnityEngine;

public class TemaBounds : MonoBehaviour {

	void Start() {
		Tema tema = new Tema();
		tema.AgregarColor("TEMA_RELLENO", Colores.RELLENO_GLOBAL);
		tema.AgregarColor("TEMA_TEXTO", Colores.LETRA);
		tema.AgregarColor("TEMA_BORDE", Colores.BORDE);
		tema.AgregarColor("RELLENO_BOTON", Colores.RELLENO_BOTON);
		tema.AgregarColor("RELLENO_CARTEL", Colores.RELLENO_CARTEL);
		GameObject.FindAnyObjectByType<TemaControl>().EstablecerTemaPrincipal(tema);
	}

}