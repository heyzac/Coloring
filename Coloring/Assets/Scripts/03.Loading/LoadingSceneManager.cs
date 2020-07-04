using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync("ScenePuzzleIn");

        operation.allowSceneActivation = false;

        float timer = 0.0f;
        while (!operation.isDone)
        {

            yield return true;
            timer += Time.deltaTime;

            if(timer > 2.00)
                operation.allowSceneActivation = true;

            //Debug.Log(timer);

        }
    }
}
