using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _loadingSlider;
    private float smooth = 5.0f;

    public void LoadScene(int sceneId){
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        _loadingScreen.SetActive(true);

        while(!operation.isDone){
            _loadingSlider.value += 0.1f + Time.deltaTime * smooth;
            yield return null;
        }
    }
}
