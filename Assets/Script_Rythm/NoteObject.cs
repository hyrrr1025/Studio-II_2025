using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class NoteObject : MonoBehaviour
{
    public bool canBePress;

    public KeyCode keyTopress;

    public float perfectAdjust;

    public GameObject hitEffect, perfectEffect;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        //if (UdpClient.instance.IsPlay)
        if (UnityEngine.Input.GetKeyDown(keyTopress) || Manager.instance.gameData.aY<=-3000)
        {
            if (canBePress)
            {
                gameObject.SetActive(false);
                //UdpClient.instance.IsPlay = false;

                // Hit判定
                if (transform.position.x <= -6.23f + perfectAdjust && transform.position.x >= -6.23f - perfectAdjust)
                {
                    Debug.Log("Perfect Hit");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Normal Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePress = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            canBePress = false;

            GameManager.instance.NoteMiss();
        }
    }
   

}
