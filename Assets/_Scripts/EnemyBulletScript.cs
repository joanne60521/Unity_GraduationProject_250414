using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyBulletScript : MonoBehaviour
{
    public float lifeTime = 5f; // 子彈存活時間
    public GameObject particlePrefab;
    public GameObject explodEffectEmptyOB;
    
    private VisualEffect visualEffect;

    // Start is called before the first frame update
    void Start()
    {
        visualEffect = GetComponent<VisualEffect>();

        Destroy(gameObject, lifeTime); // 一段時間後自動銷毀
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Enemy bullet hit: " + other.gameObject.name);

        // ===== Effect =====
        GameObject particleInstance2 = Instantiate(explodEffectEmptyOB, other.contacts[0].point, Quaternion.identity);
        Destroy(particleInstance2, 0.83f);
        visualEffect.Play();

        if (other.gameObject.tag == "PlayerRobot")
        {
            CameraShakeWhenFire cameraShakeWhenFire = other.gameObject.transform.GetComponentInChildren<CameraShakeWhenFire>();
            if (cameraShakeWhenFire != null)
            {
                cameraShakeWhenFire.TriggerShake(cameraShakeWhenFire.shakeDuration, cameraShakeWhenFire.shakeMagnitude);
            }
        }
        Destroy(gameObject); // 碰到物體後銷毀
    }
}
