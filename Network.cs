using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Network : MonoBehaviour
{
    //variables

    //Recibir
    public TMP_Text resultado;

    //Enviar
    public TMP_InputField ifNombre;
    public TMP_InputField ifPuntos;

    //La estructura que se convierte a JSON
    public struct DatosUsuario
    {
        public string nombre;
        public int puntos;
    }

    //Datos que se convierten a JSON para poder ir al servidor
    public DatosUsuario datos;

    //Click del botón Leer JSON
    public void LeerJSON()
    {
        StartCoroutine(DescargarJSON());
    }

    IEnumerator DescargarJSON()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost/unity/enviaJSON.php");
        yield return request.SendWebRequest();
        //Después de cierto tiempo
        if (request.result == UnityWebRequest.Result.Success)
        {
            //Descarga Exitosa
            string textoJson = request.downloadHandler.text;
            resultado.text = textoJson;

            DatosUsuario datos = JsonUtility.FromJson<DatosUsuario>(textoJson);
            resultado.text = resultado.text + "\n\n" + datos.nombre + "-" + datos.puntos;
        }
        else
        {
            resultado.text = "Error en la descarga: " + request.responseCode.ToString();
        }
        request.Dispose();
    }


    //Click del botón Subir JSON
    public void EscribirJSON()
    {
        StartCoroutine(SubirJSON());
    }

    IEnumerator SubirJSON()
    {
        datos.nombre = ifNombre.text;
        datos.puntos = Int32.Parse(ifPuntos.text);

        WWWForm forma = new WWWForm();
        forma.AddField("datosJSON", JsonUtility.ToJson(datos));

        UnityWebRequest request = UnityWebRequest.Post("http://localhost/unity/recibeJSON.php", forma);
        yield return request.SendWebRequest();
        //Después de cierto tiempo
        if (request.result == UnityWebRequest.Result.Success)
        {
            //Descarga Exitosa
            resultado.text = request.downloadHandler.text;
        }
        else
        {
            resultado.text = "Error en la descarga: " + request.responseCode.ToString();
        }
    }

    //Click del botón Enviar Texto
    public void EnviarTextoPlano()
    {
        StartCoroutine(SubirTextoPlano());
    }

    IEnumerator SubirTextoPlano()
    {
        // Subir datos, creamos un WWWform
        WWWForm forma = new WWWForm();
        forma.AddField("nombre", ifNombre.text);
        forma.AddField("puntos", ifPuntos.text);

        UnityWebRequest request = UnityWebRequest.Post("http://localhost/unity/recibeTextoPlano.php",forma);
        yield return request.SendWebRequest();
        //Después de cierto tiempo
        if (request.result == UnityWebRequest.Result.Success)
        {
            //Descarga Exitosa
            resultado.text = request.downloadHandler.text;
        }
        else
        {
            resultado.text = "Error en la descarga: " + request.responseCode.ToString();
        }
    }

    //Button click Descargar texto plano
    public void LeerTextoPlano()
    {
        StartCoroutine(DescargarTextoPlano());
    }

    IEnumerator DescargarTextoPlano()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost/unity/textoPlano.txt");
        yield return request.SendWebRequest(); //Esto se hace en segundo plano
        //Después de cierto tiempo, continua
        if (request.result == UnityWebRequest.Result.Success)
        {
            //Descarga Exitosa
            resultado.text = request.downloadHandler.text;
        }
        else
        {
            resultado.text = "Error en la descarga: " + request.responseCode.ToString();
        }
    }
}
