using UnityEngine;
using UnityEngine.Video;

public class VidPlayer : MonoBehaviour
{

    [SerializeField] string videoFileName;
    [SerializeField] GameObject placeHolderImage;

    // Start is called before the first frame update
    void Start()
    {
        PlayVideo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            Debug.Log("videoPath: " + videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
            //placeHolderImage.SetActive(false);
        }
    }
}
