using UnityEngine;
using System.Collections;

public class ColorAction : MonoBehaviour
{
    [SerializeField]
    StandardManager timeManager;

    public void OnTriggerEnter(Collider other)
    {

        var renderer = GetComponent<MeshRenderer>();

        // remember value before changing
        var startColor = renderer.material.color;
        // change
        renderer.material.color = Color.red;

        // remember as reversible action
        timeManager.TimeManager.RememberAction(()=>
        {
            renderer.material.color = startColor;
        });
        
    }

    GameObject explosionPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
