using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GunFire : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    // public InputActionReference rightSelectReference;
    // private float rightSelectValue;
    [SerializeField] private AudioClip HandAttack;
    // public Image GunCrosshair;
    public LayerMask layerEnemy;
    public float fireRate = 5f;
    public GameObject hitWhere;
    public Transform mainCam;
    public GameObject gunCrosshairImg;
    public GameObject particlePrefab;
    public ScaleUp scaleUp;
    public AudioSourceControl audioSourceControl;

    public GameObject bulletPrefab; // 子彈的 Prefab
    public float bulletSpeed = 20f; // 子彈速度

    private int ignoreLayer;
    private int ignoreLayer1;
    private int ignoreLayer2;
    private int ignoreLayer3;
    private int layerMask;

    private VisualEffect Muzzleflash;

    void Start()
    {
        Muzzleflash = GetComponent<VisualEffect>();

        ignoreLayer = LayerMask.NameToLayer("Bullet");  // 要忽略的圖層
        ignoreLayer1 = LayerMask.NameToLayer("RobotHand");  // 要忽略的圖層
        ignoreLayer2 = LayerMask.NameToLayer("Cockpit");  // 要忽略的圖層
        ignoreLayer3 = LayerMask.NameToLayer("PlayerRobot");  // 要忽略的圖層
        layerMask = ~((1 << ignoreLayer) | (1 << ignoreLayer1) | (1 << ignoreLayer2));  // 反轉，讓 Raycast 無視這個圖層
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, layerMask))
        {
            hitWhere.transform.position = hit.point;
            float distance = Vector3.Distance(hitWhere.transform.position, mainCam.position);
            hitWhere.transform.localScale = Vector3.one * distance * 0.05f;
            Debug.DrawRay(transform.position, transform.forward * distance, Color.yellow);
            if (hit.transform.CompareTag("Enemy"))
            {
                scaleUp.scaleUp = true;
            }else
            {
                scaleUp.scaleUp = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * range/2, Color.yellow);
            hitWhere.transform.position = transform.position + transform.forward * range/2;
            hitWhere.transform.localScale = Vector3.one * range/2 * 0.1f;
            scaleUp.scaleUp = false;
        }
    }

    public void Shoot()
    {
        Debug.Log("shoot");
        
        // ===== Effect =====
        // Quaternion targetRotation = Quaternion.LookRotation(-transform.right);
        // GameObject particleInstance = Instantiate(particlePrefab, transform.position, targetRotation);
        // Destroy(particleInstance, 0.83f); // 秒後銷毀
        Muzzleflash.Play();

        // ===== Audio =====
        // AudioSource.PlayClipAtPoint(HandAttack, transform.position, 0.6f);
        audioSourceControl.PlayShoot();

        // ====== Instantiate bullet ======
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = transform.forward * bulletSpeed;
            }
        }
    }
}
