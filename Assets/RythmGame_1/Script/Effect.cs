using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float lifetime;

    public float vanishSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().color -= new UnityEngine.Color(0, 0, 0, vanishSpeed);

        Destroy(gameObject, lifetime);
    }
}
