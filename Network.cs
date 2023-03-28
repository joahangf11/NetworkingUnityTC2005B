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
