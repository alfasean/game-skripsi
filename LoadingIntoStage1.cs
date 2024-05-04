using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingIntoStage1 : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image image3;
    public Image charImage1;
    public Image charImage2;
    public Image charImage3;
    public float switchInterval = 0.5f;
    public float loadingTime = 10.0f; 

    private void Start()
    {
        StartCoroutine(SwitchImages());
        StartCoroutine(LoadStage1());
    }

    IEnumerator SwitchImages()
    {
        while (true)
        {
            image1.gameObject.SetActive(true);
            image2.gameObject.SetActive(false);
            image3.gameObject.SetActive(false);

            charImage1.gameObject.SetActive(true);
            charImage2.gameObject.SetActive(false);
            charImage3.gameObject.SetActive(false);

            yield return new WaitForSeconds(switchInterval);

            image1.gameObject.SetActive(false);
            image2.gameObject.SetActive(true);
            image3.gameObject.SetActive(false);

            charImage1.gameObject.SetActive(false);
            charImage2.gameObject.SetActive(true);
            charImage3.gameObject.SetActive(false);

            yield return new WaitForSeconds(switchInterval);

            image1.gameObject.SetActive(false);
            image2.gameObject.SetActive(false);
            image3.gameObject.SetActive(true);

            charImage1.gameObject.SetActive(false);
            charImage2.gameObject.SetActive(false);
            charImage3.gameObject.SetActive(true);

            yield return new WaitForSeconds(switchInterval);
        }
    }

    IEnumerator LoadStage1()
    {
        yield return new WaitForSeconds(loadingTime);
        SceneManager.LoadScene("AnimationFuture");
    }
}
