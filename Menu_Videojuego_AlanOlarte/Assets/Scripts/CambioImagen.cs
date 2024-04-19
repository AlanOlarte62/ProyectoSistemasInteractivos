using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.Networking;

public class ImageSelector : MonoBehaviour
{
    public RawImage imageDisplay;

    public void SelectImage()
    {
#if UNITY_EDITOR
        Debug.LogWarning("Selecting images from the gallery is not supported in the Unity Editor.");
#else
        StartCoroutine(LoadImageFromGallery());
#endif
    }

    private IEnumerator LoadImageFromGallery()
    {
        // Esperar un frame para evitar un error de renderizado de la UI
        yield return new WaitForEndOfFrame();

#if UNITY_ANDROID || UNITY_IOS
        // Permitir que el usuario seleccione una imagen de la galería
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Cargar y mostrar la imagen seleccionada
                StartCoroutine(LoadImage(path));
            }
        }, "Select an image");

        // Esperar a que el usuario conceda permiso
        while (permission == NativeGallery.Permission.Pending)
            yield return null;

        // Mostrar un mensaje de error si se deniega el permiso
        if (permission == NativeGallery.Permission.Denied)
        {
            Debug.LogWarning("Permission to access gallery denied");
        }
        else if (permission == NativeGallery.Permission.Granted)
        {
            Debug.Log("Permission to access gallery granted");
        }
#else
        Debug.LogWarning("Accessing gallery is only supported on Android and iOS platforms.");
#endif
    }

    private IEnumerator LoadImage(string path)
    {
        // Cargar la imagen seleccionada desde la galería y mostrarla en un objeto RawImage
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("file://" + path);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            imageDisplay.texture = texture;
        }
        else
        {
            Debug.LogWarning("Failed to load image: " + www.error);
        }
    }
}

