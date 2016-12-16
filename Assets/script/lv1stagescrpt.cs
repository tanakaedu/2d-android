using UnityEngine;
using System.Collections;

public class lv1stagescrpt : MonoBehaviour {
    private AsyncOperation async;

    IEnumerator Start()
    {
        AsyncOperation async = Application.LoadLevelAsync("lv1");
        async.allowSceneActivation = false;

        while(async.progress < 0.9f)
        {
            Debug.Log(async.progress);

            yield return new WaitForEndOfFrame();

        }

        Debug.Log("Scene Loaded");


        yield return new WaitForSeconds(3);

        async.allowSceneActivation = true;    // シーン遷移許可


    }
}
