using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _target;        // the health component to monitor
    private Image _image;

    private void Awake()
    {
        // get the image component
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        // target is missing or dead
        if(_target == null)
        {
            // empty health bar
            _image.fillAmount = 0f;
        }
        else
        {
            // set fill to current health HP percentage
            _image.fillAmount = _target.HealthPercentage;
        }
    }
}
