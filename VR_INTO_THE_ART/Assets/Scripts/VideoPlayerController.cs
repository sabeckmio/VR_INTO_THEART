using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private bool hasStartedPlaying = false;

    void Start()
    {
        // VideoPlayer 컴포넌트 가져오기
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnded; // 비디오가 끝났을 때 호출될 메서드 등록
        }
        else
        {
            Debug.LogError("VideoPlayer 컴포넌트를 찾을 수 없습니다.");
        }
    }

    public void PlayVideo()
    {
        if (!hasStartedPlaying && videoPlayer != null)
        {
            videoPlayer.Play();
            hasStartedPlaying = true; // 중복 재생 방지
        }
    }

    private void OnVideoEnded(VideoPlayer vp)
    {
        // 비디오가 종료되었을 때 호출되는 메서드
        Debug.Log("Video has ended.");
        // 필요에 따라 추가 로직을 구현할 수 있습니다.
    }
}
