using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManage : MonoBehaviour
{
    [SerializeField] GameObject UICanvas;
    [SerializeField] GameObject cutSceneCanvas;

    // Start is called before the first frame update
    void Start()
    {
        //enableCutscene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void enableUI()
    {
        cutSceneCanvas.SetActive(false);
        UICanvas.SetActive(true);
    }

    void enableCutscene()
    {
        UICanvas.SetActive(false);
        cutSceneCanvas.SetActive(true);
    }

}
