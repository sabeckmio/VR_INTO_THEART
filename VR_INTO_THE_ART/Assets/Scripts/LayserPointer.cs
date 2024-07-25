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
    public float delayDuration = 3f; // �̹����� ��Ÿ���� ���� ��� �ð�
    public float rockDuration = 1f;  // ù ��° ���� ������ �� ��� �ð�
    public GameObject fallOutObject; // ù ��° FallOut ������Ʈ ����
    public GameObject fallOutObject2; // �� ��° FallOut ������Ʈ ����
    public GameObject fallOutObject3;

    public AudioClip fallingSound; // �� �������� �Ҹ� ����� Ŭ��
    private AudioSource audioSource; // ����� �ҽ� ������Ʈ
    public OculusMovementDetection movementDetectionScript;

    void Start()
    {
        laser = gameObject.AddComponent<LineRenderer>();
        Material material = new Material(Shader.Find("Unlit/Color"));
        material.color = new Color(0f, 195f / 255f, 255f / 255f, 0.5f); // �Ķ��� ������
        laser.material = material;
        laser.positionCount = 2;
        laser.startWidth = 0.01f;
        laser.endWidth = 0.01f;
        laser.enabled = true; // ������ �׻� Ȱ��ȭ

        // ����� �ҽ� ������Ʈ �߰� �� ����
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = fallingSound;

        // OVRPlayerController GameObject���� OculusMovementDetection ��ũ��Ʈ ����
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

                // ���� ������ ���� ��ư �Է� Ȯ��
                if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three))
                {
                    collidedObject.collider.gameObject.SetActive(false); // ����ǥ UI �����
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
        // ī�޶� ���ο� ��ġ�� �̵�
        cameraRig.transform.position = newCameraPosition;

        // delayDuration �� ��� �� �̹����� ǥ��
        yield return new WaitForSeconds(delayDuration);

        image_Picture.SetActive(true);

        // imageDuration �� ���� ���
        yield return new WaitForSeconds(imageDuration);

        image_Picture.SetActive(false);

        // ù ��° FallOut ������Ʈ ����߸���
        DropObject(fallOutObject);

        // rockDuration �� ���
        yield return new WaitForSeconds(rockDuration);

        // ������ ������Ʈ�� ���ÿ� ����߸���
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
            PlayFallingSound(); // �Ҹ� ���
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
