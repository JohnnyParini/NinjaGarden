using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SlideShow : MonoBehaviour
{
    public Texture[] imageArray;
    public int currentImage;

    float deltaTime = 0.0f;

    public float timer1 = 5.0f;
    public float timer1Remaining = 5.0f;
    public bool timer1IsRunning = true;
    public string timer1Text;

    void OnGUI()
    {
        int w = Screen.width;
        int h = Screen.height;
        Rect imageRect = new Rect(0, 0, Screen.width, Screen.height);

        GUI.DrawTexture(imageRect, imageArray[currentImage]);

        if(currentImage >= imageArray.Length) //resets the slideshow, loads the main menu
        {
            SceneManager.LoadScene(0);
        }
        
    }

    private void Start()
    {
        currentImage = 0;
        bool timer1IsRunning = true;
        timer1Remaining = timer1;

    }

    private void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if (Input.GetKey(KeyCode.Escape))
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }
        }
    }
}
