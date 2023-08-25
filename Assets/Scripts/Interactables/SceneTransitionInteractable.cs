using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionInteractable : Interactable
{
    public string scene = "Workshop";
    private SpriteRenderer onTriggerGraphic;
    // Start is called before the first frame update
    void Start()
    {
       onTriggerGraphic = GetComponentInChildren<SpriteRenderer>(); 
       onTriggerGraphic.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    protected override void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
        onTriggerGraphic.enabled = true;
    }

    protected override void OnTriggerExit(Collider other) {
        base.OnTriggerEnter(other);
        onTriggerGraphic.enabled = false;
    }

    public override void OnInteract() {
        LoadScene(scene);
    }

    void LoadScene(string scene)
    {
        StartCoroutine(LoadYourAsyncScene(scene));
    }

    IEnumerator LoadYourAsyncScene(string scene)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
