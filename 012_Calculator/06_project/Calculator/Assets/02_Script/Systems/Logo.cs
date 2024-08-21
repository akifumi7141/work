using UnityEngine;

public class Logo : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Invoke("StartScenes", 1.0f);
    }

    void StartScenes()
    {
        SceneNavigator.Instance.Change("02_Title", 0.5f);
    }
}
