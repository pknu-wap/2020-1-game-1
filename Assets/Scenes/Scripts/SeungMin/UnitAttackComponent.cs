using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackComponent : MonoBehaviour
{
    private GameObject player;
    private UnitAttackComponent playerStatus;
    [SerializeField]
    public float healthPoint = 100f;
    [SerializeField]
    private float attackSpeed = 1f;
    [SerializeField]
    private float attackDamage = 10f;
    [SerializeField]
    public float attackRange = 2f;

    public static WaitForSeconds statusCheckPeriod = new WaitForSeconds(0.2f);
    public static WaitForSeconds waitAttackSpeed;
    private IEnumerator AttackPlayer()
    {
        while (true)
        {
            CheckDead();
            if ((transform.position - player.transform.position).magnitude <= attackRange)
            {
                playerStatus.healthPoint = -attackDamage;
                yield return waitAttackSpeed;
            }
            else
                yield return statusCheckPeriod;
        }
    }
    private IEnumerator CheckDead()
    {
        while (true) {
            if (healthPoint <= 0)
            {
                Destroy(gameObject);
                StopAllCoroutines();
            }

            yield return statusCheckPeriod;
            
        }
    }

    private void OnEnable()
    {
        player = GameObject.Find("Player");
        playerStatus = player.GetComponent<UnitAttackComponent>();
        waitAttackSpeed = new WaitForSeconds(attackSpeed);
        StartCoroutine(AttackPlayer());
        StartCoroutine(CheckDead());
    }

}
