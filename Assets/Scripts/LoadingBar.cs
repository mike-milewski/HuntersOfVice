#pragma warning disable 0649
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    [SerializeField]
    private Image LoadingImage;

    [SerializeField]
    private int BuildIndex;

    private Scene scene;

    private void OnEnable()
    {
        StartCoroutine(Async(BuildIndex));
    }

    public IEnumerator Async(int Level)
    {
        yield return new WaitForSeconds(1.0f);
        AsyncOperation async = SceneManager.LoadSceneAsync(Level);

        while (!async.isDone)
        {
            LoadingImage.fillAmount = async.progress;
            if (LoadingImage.fillAmount >= 0.9f)
            {
                LoadingImage.fillAmount = 1;
            }

            yield return null;
        }
    }
}
