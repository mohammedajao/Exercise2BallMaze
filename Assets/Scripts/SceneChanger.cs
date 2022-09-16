using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public MazeRenderer rendererScript;
    private bool debounce = false;

    void Start()
    {
        rendererScript = GameObject.FindObjectOfType(typeof(MazeRenderer)) as MazeRenderer;
    }

    // void OnCollisionEnter(Collision col)
    // {
    //     Debug.Log("HIT");
    //     if(!debounce && (col.gameObject.name == "Player" || col.gameObject.name == "PlayerBody")) {
    //         debounce = true;
    //         Debug.Log("Player hit me");
    //         // rendererScript.Redraw(rendererScript.width + 10, rendererScript.height + 10);
    //     }
    // }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("HIT TRIGGER");
        if(debounce == false && (col.gameObject.name == "Player" || col.gameObject.name == "PlayerBody")) {
            debounce = true;
            Debug.Log("Player hit me");
            rendererScript.Redraw(rendererScript.width + 10, rendererScript.height + 10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
