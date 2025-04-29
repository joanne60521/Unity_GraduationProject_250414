using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    // public VRRig_test VRRig_test;
    // public bool reachedEnemy = false;
    // public Animator enemyAnimator;
    // [SerializeField] private AudioClip AttackHand;
    [SerializeField] private GameObject explode;
    private Target target;


    // private void OnCollisionEnter(Collision col)
    // {
    //     if (VRRig_test.leftHand.attacking | VRRig_test.rightHand.attacking)
    //     {
    //         reachedEnemy = true;
    //         AudioSource.PlayClipAtPoint(AttackHand, new(transform.position.x, -6, transform.position.z), 1f);
    //         explode.Play();
    //         target.TakeDamage(10);
    //     }
    // }

    void Start()
    {
        target = transform.parent.GetComponent<Target>();
    }

    public void PlayEffects()
    {
        GameObject particleInstance = Instantiate(explode, transform.position + transform.forward * 0.1f, Quaternion.identity);
        Destroy(particleInstance, 4f);
        target.TakeDamage(10);
    }
}
