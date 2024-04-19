using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animaciones : MonoBehaviour
{
    [SerializeField] public GameObject logo;

  
    public void ActivarAnimacion()
    {
        LeanTween.moveY(logo.GetComponent<RectTransform>(), -152, 1.5f).setDelay(.50f)
        .setEase(LeanTweenType.easeOutElastic);

        
    }

}
