using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyKeyEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameController gameController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            gameController.GameStart();
            Time.timeScale = 1f;
            this.enabled = false;
        }
    }
}
