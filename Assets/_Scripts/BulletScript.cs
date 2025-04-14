using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BulletScript : MonoBehaviour
{
    public float damage = 10f; // 子彈傷害
    public float lifeTime = 3f; // 子彈存活時間
    public GameObject particlePrefab;
    public GameObject explodEffectEmptyOB;
    
    private VisualEffect visualEffect;

    void Start()
    {
        visualEffect = GetComponent<VisualEffect>();

        Destroy(gameObject, lifeTime); // 一段時間後自動銷毀
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("bullet hit: " + other.gameObject.name);
        if (other.gameObject.tag != "PlayerRobot")
        {
            // ===== Effect =====
            // GameObject particleInstance2 = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            // Destroy(particleInstance2, 0.83f);
            GameObject particleInstance2 = Instantiate(explodEffectEmptyOB, other.contacts[0].point, Quaternion.identity);
            Destroy(particleInstance2, 0.83f);
            visualEffect.Play();

            if (other.gameObject.tag == "Enemy") // 確保只對敵人造成傷害
            {
                Target target = other.gameObject.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
            Destroy(gameObject); // 碰到物體後銷毀
        }
        
    }
}
