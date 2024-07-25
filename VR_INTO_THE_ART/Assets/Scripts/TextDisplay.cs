using UnityEngine;
using TMPro; // TextMeshProUGUI�� ����ϱ� ���� �ʿ�
using UnityEngine.Video; // VideoPlayer ����ϱ� ���� �ʿ�
using UnityEngine.UI; // RawImage ����ϱ� ���� �ʿ�

public class TextDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public VideoPlayer videoPlayer; // VideoPlayer ������Ʈ
    public RawImage videoDisplay; // ������ ǥ���� RawImage
    public float textDisplayDuration = 5f; // �ؽ�Ʈ ǥ�� �ð�

    void Start()
    {
        // �ؽ�Ʈ�� Ȱ��ȭ ���·� �����մϴ�.
        text.gameObject.SetActive(true);

        // ���� �ð� �� �ؽ�Ʈ�� ��Ȱ��ȭ�ϰ� ���� ����� �����մϴ�.
        Invoke("DisableTextAndPlayVideo", textDisplayDuration);
    }

    void DisableTextAndPlayVideo()
    {
        // �ؽ�Ʈ�� ��Ȱ��ȭ�մϴ�.
        text.gameObject.SetActive(false);

        // ������ ����ϱ� ���� VideoPlayer �� RawImage�� Ȱ��ȭ�մϴ�.
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
