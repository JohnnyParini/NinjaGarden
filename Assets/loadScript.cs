using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadScript : MonoBehaviour
{

    public SaveHandler save;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log("onMouseDown called");
        save.OnLoad();
    }
}
