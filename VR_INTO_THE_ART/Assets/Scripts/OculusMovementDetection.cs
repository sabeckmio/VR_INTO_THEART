using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic; // List<>를 사용하기 위해 필요
using UnityEngine.UI; // RawImage를 사용하기 위해 필요

public class OculusMovementDetection : MonoBehaviour
{
    public List<Image> instructionImages; // 순서대로 표시할 UI 이미지 리스트
    public float fadeDuration = 1.0f; // 페이드 아웃 및 인 지속 시간
    public AudioClip footstepClip; // 발자국 소리 오디오 클립
    public float stepInterval = 0.4f; // 발자국 소리 간격

    private AudioSource audioSource;
    private CharacterController characterController;
    private float stepTimer;

    // 비디오 관련 변수
    public VideoPlayer videoPlayer;
    public RawImage videoDisplay; // 비디오를 표시할 RawImage

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

        // 모든 이미지를 비활성화합니다.
        foreach (Image img in instructionImages)
        {
            img.gameObject.SetActive(false);
        }

        // 비디오 관련 설정
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.gameObject.SetActive(false); // 비디오를 숨깁니다.
        }
        if (videoDisplay != null)
        {
            videoDisplay.gameObject.SetActive(false); // 비디오 디스플레이를 숨깁니다.
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
        // 비디오가 끝났을 때 비디오를 숨기고 첫 번째 지시사항 이미지를 표시합니다.
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
        if (!audioSource.isPlaying) // 현재 오디오가 재생 중이 아닐 때만 재생
        {
            audioSource.PlayOneShot(footstepClip);
        }
    }
}
