using Bounds.Persistencia.Parametros;
using Ging1991.Persistencia.Direcciones;

namespace Bounds.Modulos.Duelo {

	public class ParametrosControlDuelo : ParametrosControl {

		public override void SetParametros() {
			parametros.direcciones["COLORES"] = new DireccionRecursos("Configuracion", "COLORES").Generar();
			parametros.direcciones["SISTEMA"] = new DireccionRecursos("Configuracion", "IDIOMA").Generar();

			parametros.direcciones["CARTA_NOMBRES"] = new DireccionRecursos("Cartas", "Nombres").Generar();
			parametros.direcciones["CARTA_NOMBRES"] = new DireccionRecursos("Cartas", "Nombres").Generar();
			parametros.direcciones["CARTA_EFECTOS"] = new DireccionRecursos("Cartas", "Efectos").Generar();
			parametros.direcciones["CARTA_AMBIENTACION"] = new DireccionRecursos("Cartas", "Ambientacion").Generar();
			parametros.direcciones["CARTA_CLASES"] = new DireccionRecursos("Cartas", "Clases").Generar();
			parametros.direcciones["CARTA_TIPOS"] = new DireccionRecursos("Cartas", "Tipos").Generar();
			parametros.direcciones["CARTA_INVOCACIONES"] = new DireccionRecursos("Cartas", "Invocaciones").Generar();
			parametros.direcciones["MUSICA_TIENDA"] = new DireccionRecursos("Musica", "TIENDA").Generar();
			parametros.direcciones["MUSICA_DERROTA"] = new DireccionRecursos("Musica", "DERROTA").Generar();
			parametros.direcciones["MUSICA_VICTORIA"] = new DireccionRecursos("Musica", "VICTORIA").Generar();
			parametros.direcciones["SONIDOS"] = "Sonidos";
			parametros.direcciones["CARTAS_DATOS"] = "Cartas/Datos";
			parametros.direcciones["CARTAS_HABILIDADES"] = new DireccionRecursos("HABILIDADES", "HABILIDADES").Generar();

			parametros.direcciones["CONFIGURACION"] = new DireccionDinamica("CONFIGURACION", "CONFIGURACION.json").Generar();
			parametros.direcciones["BILLETERA"] = new DireccionDinamica("CONFIGURACION", "BILLETERA.json").Generar();
			parametros.direcciones["CARTAS_RECURSO"] = "Cartas/Imagenes";
			parametros.direcciones["CARTAS_DINAMICA"] = "IMAGENES/Cartas/Imagenes";
			parametros.direcciones["COFRE_RECURSOS"] = new DireccionRecursos("MAZOS", "COFRE").Generar();
			parametros.direcciones["COFRE"] = new DireccionDinamica("CONFIGURACION", "COFRE.json").Generar();


		}

	}

}