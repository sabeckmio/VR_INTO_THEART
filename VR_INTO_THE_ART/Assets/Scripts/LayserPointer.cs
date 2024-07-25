using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LayserPointer : MonoBehaviour
{
    private LineRenderer laser;
    private RaycastHit collidedObject;
    public float raycastDistance = 100f;
    public OVRCameraRig cameraRig;
    public Vector3 newCameraPosition = new Vector3(-2.732942f, 15.56498f, -5.431501f);
    public GameObject image_Picture;
    public float imageDuration = 6f;
    public float delayDuration = 3f; // 이미지가 나타나기 전의 대기 시간
    public float rockDuration = 1f;  // 첫 번째 돌이 떨어진 후 대기 시간
    public GameObject fallOutObject; // 첫 번째 FallOut 오브젝트 참조
    public GameObject fallOutObject2; // 두 번째 FallOut 오브젝트 참조
    public GameObject fallOutObject3;

    public AudioClip fallingSound; // 돌 무너지는 소리 오디오 클립
    private AudioSource audioSource; // 오디오 소스 컴포넌트
    public OculusMovementDetection movementDetectionScript;

    void Start()
    {
        laser = gameObject.AddComponent<LineRenderer>();
        Material material = new Material(Shader.Find("Unlit/Color"));
        material.color = new Color(0f, 195f / 255f, 255f / 255f, 0.5f); // 파란색 레이저
        laser.material = material;
        laser.positionCount = 2;
        laser.startWidth = 0.01f;
        laser.endWidth = 0.01f;
        laser.enabled = true; // 레이저 항상 활성화

        // 오디오 소스 컴포넌트 추가 및 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = fallingSound;

        // OVRPlayerController GameObject에서 OculusMovementDetection 스크립트 참조
        movementDetectionScript = FindObjectOfType<OculusMovementDetection>();
    }

    void Update()
    {
        laser.SetPosition(0, transform.position);

        if (Physics.Raycast(transform.position, transform.forward, out collidedObject, raycastDistance))
        {
            laser.SetPosition(1, collidedObject.point);

            if (collidedObject.collider.gameObject.CompareTag("Question"))
            {
                if (movementDetectionScript != null && movementDetectionScript.instructionImages.Count > 1)
                {
                    Image image = movementDetectionScript.instructionImages[0];
                    if (image != null)
                    {
                        image.enabled = false;
                    }
                }
                else
                {
                    Debug.LogError("instructionImages does not contain enough elements or movementDetectionScript is null.");
                }

                // 물건 선택을 위한 버튼 입력 확인
                if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three))
                {
                    collidedObject.collider.gameObject.SetActive(false); // 물음표 UI 사라짐
                    StartCoroutine(MoveCameraAndShowImages());
                }
            }
        }
        else
        {
            laser.SetPosition(1, transform.position + (transform.forward * raycastDistance));
        }
    }

    private IEnumerator MoveCameraAndShowImages()
    {
        // 카메라를 새로운 위치로 이동
        cameraRig.transform.position = newCameraPosition;

        // delayDuration 초 대기 후 이미지를 표시
        yield return new WaitForSeconds(delayDuration);

        image_Picture.SetActive(true);

        // imageDuration 초 동안 대기
        yield return new WaitForSeconds(imageDuration);

        image_Picture.SetActive(false);

        // 첫 번째 FallOut 오브젝트 떨어뜨리기
        DropObject(fallOutObject);

        // rockDuration 초 대기
        yield return new WaitForSeconds(rockDuration);

        // 나머지 오브젝트들 동시에 떨어뜨리기
        DropObject(fallOutObject2);

        DropObject(fallOutObject3);

    }

    private void DropObject(GameObject obj)
    {
        if (obj != null)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = obj.AddComponent<Rigidbody>();
                Debug.Log($"Rigidbody added to {obj.name}.");
            }
            rb.isKinematic = false;
            PlayFallingSound(); // 소리 재생
        }
    }

    private void PlayFallingSound()
    {
        if (audioSource && fallingSound)
        {
            audioSource.PlayOneShot(fallingSound);
        }
    }
}
