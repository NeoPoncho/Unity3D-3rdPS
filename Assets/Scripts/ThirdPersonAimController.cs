using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ThirdPersonAimController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera followVirtualCamera;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private Transform pfBullet;
    [SerializeField] private Transform spawnBullet;

    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;
    private Animator anim;
    public GameObject crosshair;
    public GameObject gun;
    public AudioSource shot;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new(Screen.width / 2f, Screen.height / 2f);
        
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }

        if (starterAssetsInputs.aim)
        {
            thirdPersonController.SetSensitivity(aimSensitivity);
            aimVirtualCamera.gameObject.SetActive(true);
            followVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetRotateOnMove(false);
            gun.SetActive(true);
            crosshair.SetActive(true);
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            thirdPersonController.SetSensitivity(normalSensitivity);
            followVirtualCamera.gameObject.SetActive(true);
            aimVirtualCamera.gameObject.SetActive(false);          
            thirdPersonController.SetRotateOnMove(true);
            crosshair.SetActive(false);
            gun.SetActive(false);
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }

        if (starterAssetsInputs.shoot)
        {
            shot.PlayOneShot(shot.clip);

            Vector3 aimDirection = (mouseWorldPosition - spawnBullet.position).normalized;
            Instantiate(pfBullet, spawnBullet.position, Quaternion.LookRotation(aimDirection, Vector3.up));

            starterAssetsInputs.shoot = false;
        }
    }
}