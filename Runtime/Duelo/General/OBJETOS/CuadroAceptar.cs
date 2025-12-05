using UnityEngine;
using UnityEngine.UI;

public class CuadroAceptar : MonoBehaviour {

    void Start() {
        gameObject.name = "cuadro";
    }

    public void iniciar(string texto) {
        Text ui = transform.GetChild(2).GetComponentInChildren<Text>();
        ui.text = texto;
    }

    public void aceptar() {
        Destroy(gameObject);
    }

    public static bool existenCuadros() {
        return GameObject.Find("cuadro") != null;
    }

}