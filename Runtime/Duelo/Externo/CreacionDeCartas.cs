using Bounds.Duelo.Carta;
using Bounds.Modulos.Cartas;
using Bounds.Modulos.Cartas.Ilustradores;
using Bounds.Modulos.Cartas.Persistencia;
using Bounds.Modulos.Cartas.Persistencia.Datos;
using Bounds.Modulos.Cartas.Tinteros;
using UnityEngine;

public class CreacionDeCartas : MonoBehaviour {

	public GameObject objCarta;
	private static Vector3 ESCALA_CARTA = new Vector3(0.95f, 0.95f, 1f);
	public IlustradorDeCartas ilustradorDeCartas;
	public DatosDeCartas datosDeCartas;


	public GameObject CrearCarta(int jugador, int cartaID, string nombre, Vector3 posicion, GameObject padre,
				string rareza, string imagen, Sprite protector = null) {

		GameObject carta = Instantiate(objCarta);
		carta.transform.SetParent(padre.transform);
		carta.transform.localPosition = posicion;
		carta.transform.localScale = ESCALA_CARTA;

		CartaGeneral general = carta.GetComponent<CartaGeneral>();
		general.Iniciar(cartaID, imagen, rareza, datosDeCartas, ilustradorDeCartas, new TinteroBounds());

		CartaBD datoCarta = DatosDeCartas.Instancia.lector.LeerDatos(cartaID);
		CartaInfo info = carta.GetComponent<CartaInfo>();

		info.cargar(datoCarta);
		info.propietario = jugador;
		info.controlador = jugador;
		info.cartaID = cartaID;
		info.rareza = rareza;
		info.imagen = imagen;
		carta.name = nombre;
		carta.GetComponent<CartaPerfeccion>().CalcularPerfeccion();
		if (protector != null) {
			carta.GetComponentInChildren<CartaFisica>().SetReverso(protector);
		}
		//carta.GetComponentInChildren<CartaFisica>().ColocarBocaAbajo(true);
		return carta;
	}


}