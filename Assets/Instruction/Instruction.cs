using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instruction : MonoBehaviour
{
    public GameObject yellow;
    public GameObject blue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //»»ÑÕÉ«
        if (Input.GetKeyDown(KeyCode.Y))
        {
            yellow.SetActive(true);
            blue.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            yellow.SetActive(false);
            blue.SetActive(true);
        }

        //»»³¡¾°
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Sokoban");
        }

    }
}
