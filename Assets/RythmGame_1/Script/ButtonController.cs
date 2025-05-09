using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer SR;

    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode keytoPress;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (UdpClient.instance.IsPlay==false) {SR.sprite=pressedImage;}

        if (UdpClient.instance.IsPlay == false) {SR.sprite=defaultImage;}
    }
}
