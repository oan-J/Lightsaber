using UnityEngine;

public class BladeBase : MonoBehaviour
{
    [SerializeField] private float extendSpeed = 0.1f;
    private bool weaponActive;
    private const float SCALE_MIN = 0f;
    private float scaleMax;
    private float extendDelta;
    private float scaleCurrent;
    private float localScaleX;
    private float localScaleZ;
    public GameObject blade;
    public AudioClip swordSound;
    private AudioSource audioSource;

    private void Start()
    {
        localScaleX = transform.localScale.x;
        localScaleZ = transform.localScale.z;

        scaleMax = transform.localScale.y;
        scaleCurrent = scaleMax;

        extendDelta = scaleMax / extendSpeed;

        weaponActive = true;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = swordSound;
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            extendDelta = weaponActive ? -Mathf.Abs(extendDelta) : Mathf.Abs(extendDelta);
            audioSource.Play();
        }

        scaleCurrent += extendDelta * Time.deltaTime;
        scaleCurrent = Mathf.Clamp(scaleCurrent, SCALE_MIN, scaleMax);
        transform.localScale = new Vector3(localScaleX, scaleCurrent, localScaleZ);
        weaponActive = scaleCurrent > 0;

        if (weaponActive && !blade.activeSelf)
        {
            blade.SetActive(true);
        }
        else if (!weaponActive && blade.activeSelf)
        {
            blade.SetActive(false);
        }
    }
}