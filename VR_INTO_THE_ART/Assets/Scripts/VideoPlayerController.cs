using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private bool hasStartedPlaying = false;

    void Start()
    {
        // VideoPlayer ������Ʈ ��������
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnded; // ������ ������ �� ȣ��� �޼��� ���
        }
        else
        {
            Debug.LogError("VideoPlayer ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    public void PlayVideo()
    {
        if (!hasStartedPlaying && videoPlayer != null)
        {
            videoPlayer.Play();
            hasStartedPlaying = true; // �ߺ� ��� ����
        }
    }

    private void OnVideoEnded(VideoPlayer vp)
    {
        // ������ ����Ǿ��� �� ȣ��Ǵ� �޼���
        Debug.Log("Video has ended.");
        // �ʿ信 ���� �߰� ������ ������ �� �ֽ��ϴ�.
    }
}
