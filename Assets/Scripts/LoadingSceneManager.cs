using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    private static int _LoadingSceneIndex = 1; 



    public static int LoadingSceneIndex
    {
        get
        {
            return _LoadingSceneIndex;
        }

        set
        {
            _LoadingSceneIndex = value;
        }
    }



    [SerializeField]
    private LoadingPanel _loading;



    private void Awake()
    {
        StartCoroutine(LoadingProcess(LoadingSceneIndex));
    }

    private IEnumerator LoadingProcess(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while(!operation.isDone)
        {
            _loading.Setup(operation.progress);

            yield return null;
        }
    }
}
