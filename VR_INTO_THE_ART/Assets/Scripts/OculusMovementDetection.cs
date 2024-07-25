using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic; // List<>�� ����ϱ� ���� �ʿ�
using UnityEngine.UI; // RawImage�� ����ϱ� ���� �ʿ�

public class OculusMovementDetection : MonoBehaviour
{
    public List<Image> instructionImages; // ������� ǥ���� UI �̹��� ����Ʈ
    public float fadeDuration = 1.0f; // ���̵� �ƿ� �� �� ���� �ð�
    public AudioClip footstepClip; // ���ڱ� �Ҹ� ����� Ŭ��
    public float stepInterval = 0.4f; // ���ڱ� �Ҹ� ����

    private AudioSource audioSource;
    private CharacterController characterController;
    private float stepTimer;

    // ���� ���� ����
    public VideoPlayer videoPlayer;
    public RawImage videoDisplay; // ������ ǥ���� RawImage

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController component missing on this GameObject.");
        }

        if (footstepClip == null)
        {
            Debug.LogError("Footstep AudioClip not assigned.");
        }

        stepTimer = stepInterval;

        // ��� �̹����� ��Ȱ��ȭ�մϴ�.
        foreach (Image img in instructionImages)
        {
            img.gameObject.SetActive(false);
        }

        // ���� ���� ����
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.gameObject.SetActive(false); // ������ ����ϴ�.
        }
        if (videoDisplay != null)
        {
            videoDisplay.gameObject.SetActive(false); // ���� ���÷��̸� ����ϴ�.
        }
    }

    void Update()
    {
        if (characterController != null && audioSource != null && footstepClip != null)
        {
            if (characterController.isGrounded && characterController.velocity.magnitude > 0.1f)
            {
                stepTimer -= Time.deltaTime;
                if (stepTimer <= 0f)
                {
                    PlayFootstep();
                    stepTimer = stepInterval;
                }
            }
            else
            {
                stepTimer = stepInterval;
            }
        }
    }


    void OnVideoEnd(VideoPlayer vp)
    {
        // ������ ������ �� ������ ����� ù ��° ���û��� �̹����� ǥ���մϴ�.
        if (videoDisplay != null)
        {
            videoDisplay.gameObject.SetActive(false);
        }
        if (videoPlayer != null)
        {
            videoPlayer.gameObject.SetActive(false);
        }

        if (instructionImages.Count >= 1)
        {
            instructionImages[0].gameObject.SetActive(true);
        }
    }


    void PlayFootstep()
    {
        if (!audioSource.isPlaying) // ���� ������� ��� ���� �ƴ� ���� ���
        {
            audioSource.PlayOneShot(footstepClip);
        }
    }
}
