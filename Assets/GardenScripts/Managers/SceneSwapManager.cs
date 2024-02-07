using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapManager : MonoBehaviour
{
    public static SceneSwapManager _instance;

    private DoorTriggerInteraction.DoorToSpawnAt _doorToSpawnTo;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }

    //public static void swapSceneFromDoorUse()

    //private IEnumerator FadeOutThenChangeScene(SceneField myScene, DoorTriggerInteraction.DoorToSpawnAt doorToSpawnAt = DoorTriggerInteraction.DoorToSpawnAt.None)
    //{
       // //start fading to black

       // //keep fading out

      //  _doorToSpawnTo = doorToSpawnAt;
       // SceneManager.LoadScene(myScene);
    //}

}
