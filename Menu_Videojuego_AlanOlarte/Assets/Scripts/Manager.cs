using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public string nombre_usuarioactual;
    public string correo_usuarioactual;
    public string contraseña_usuarioactual;

    public List<string> usuarios = new List<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        usuarios.Add("Alan");
    }
  
}
