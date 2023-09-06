using UnityEngine;

public class AppPaused : MonoBehaviour
{
    [SerializeField] private GameObject PausePanelUI;
    bool isPaused = false;
    bool firstLoad = true;

    public void Unpause()
    {
        PausePanelUI.SetActive(false);
        Time.timeScale = 1;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (!firstLoad) {
            isPaused = pauseStatus;
            PausePanelUI.SetActive(true);
            Time.timeScale = 0;
        }
        firstLoad = false;
    }
}