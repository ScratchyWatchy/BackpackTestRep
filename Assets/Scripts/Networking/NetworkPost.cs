using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// NetworkManger with eventListener can be extender to fit the REST. Adress and auth should be sotred in a separate place, like proprerty settings, never implement it with Unity. TODO
/// </summary>
public class NetworkPost : MonoBehaviour
{
    void Start()
    {
        Backpack.EditEvent.AddListener(Post);
    }

    public void Post(string json)
    {
        StartCoroutine(PostRoutine(json));
    }

    IEnumerator PostRoutine(string json)
    {
        var request = new UnityWebRequest("https://dev3r02.elysium.today/inventory/status", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.SetRequestHeader("auth", "BMeHG5xqJeB4qCjpuJCTQLsqNGaqkfB6");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("" + json);
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
            Debug.Log("All OK");
            Debug.Log("Status Code: " + request.responseCode);
        }

    }
}
