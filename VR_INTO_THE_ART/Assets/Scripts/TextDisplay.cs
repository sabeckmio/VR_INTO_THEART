using UnityEngine;
using TMPro; // TextMeshProUGUI를 사용하기 위해 필요
using UnityEngine.Video; // VideoPlayer 사용하기 위해 필요
using UnityEngine.UI; // RawImage 사용하기 위해 필요

public class TextDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public VideoPlayer videoPlayer; // VideoPlayer 컴포넌트
    public RawImage videoDisplay; // 비디오를 표시할 RawImage
    public float textDisplayDuration = 5f; // 텍스트 표시 시간

    void Start()
    {
        // 텍스트를 활성화 상태로 시작합니다.
        text.gameObject.SetActive(true);

        // 일정 시간 후 텍스트를 비활성화하고 비디오 재생을 시작합니다.
        Invoke("DisableTextAndPlayVideo", textDisplayDuration);
    }

    void DisableTextAndPlayVideo()
    {
        // 텍스트를 비활성화합니다.
        text.gameObject.SetActive(false);

        // 비디오를 재생하기 전에 VideoPlayer 및 RawImage를 활성화합니다.
        if (videoPlayer != null)
        {
            videoPlayer.gameObject.SetActive(true);
            videoPlayer.Play();
        }

        if (videoDisplay != null)
        {
            videoDisplay.gameObject.SetActive(true);
        }
    }
}
