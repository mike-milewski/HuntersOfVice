using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    [SerializeField]
    private Image LoadingImage;

    private void OnEnable()
    {
        StartCoroutine(Async("Level1"));
    }

    public IEnumerator Async(string Level)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(Level);

        while(!async.isDone)
        {
            LoadingImage.fillAmount = async.progress;

            yield return null;
        }
    }
}
