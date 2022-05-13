using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EthosSense : MonoBehaviour
{

    [SerializeField] private KeyCode _key;

    [SerializeField] private Material _material;
    [SerializeField] private Shader _shad1;

    private GameObject _cam;
    [SerializeField] private Shader _shad2;
    private bool high = false;
    [SerializeField] private List<GameObject> _path;
    // Start is called before the first frame update
    void Start()
    {
        _cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_key))
        {
            high = !high;
        }


        if (high)
        {
            foreach (var item in _path)
            {
                item.GetComponent<MeshRenderer>().enabled = true;
            }
            _cam.GetComponent<PostProcessVolume>().enabled = true;
            _material.color = Color.red;
            _material.shader = _shad1;
        }
        else
        {
            foreach (var item in _path)
            {
                item.GetComponent<MeshRenderer>().enabled = false;
            }
            _material.color = Color.magenta;
            _cam.GetComponent<PostProcessVolume>().enabled = false;
            _material.shader = _shad2;
        }
    }


}
