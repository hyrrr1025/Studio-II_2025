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
        //if (UdpClient.instance.IsPlay==true) { SR.sprite = pressedImage; }
        //if (UdpClient.instance.IsPlay == false) { SR.sprite = defaultImage; }

        if (Input.GetKeyDown(keytoPress) || Manager.instance.gameData.aY <= -3000) { SR.sprite = pressedImage; }
        if (Input.GetKeyUp(keytoPress) || Manager.instance.gameData.aY <= -3000) { SR.sprite = defaultImage; }
    }
}
