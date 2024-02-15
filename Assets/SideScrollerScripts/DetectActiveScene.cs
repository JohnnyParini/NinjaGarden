using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectActiveScene : MonoBehaviour
{

    public string sceneName;
    public string start = "[";
    public string end = "]";
    public string strLvl;
    public int curLvl;

    //SceneManager.activeSceneChanged += GetLevel;

    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += GetLevel;
    }

    public void GetLevel(Scene current, Scene next)
    {
        sceneName = next.name;
        strLvl = getBetween(sceneName, start, end);
        Debug.Log(strLvl);
        if (strLvl != "")
        {
            curLvl = int.Parse(strLvl);
        }
        //return curLvl;
    }

    public static string getBetween(string strSource, string strStart, string strEnd)
    {
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            int Start, End;
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            return strSource.Substring(Start+1, End - Start);
        }

        return "";
    }
}
