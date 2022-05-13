using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private KeyCode _KeyCode;
    [SerializeField] private float _Radius;
    [SerializeField] private int _Force;
    [SerializeField] Transform _CharacterCenter;
    [SerializeField] Animator _Anim;

    private void Update()
    {
        if (!Input.GetKeyDown(_KeyCode))
            return;

        List<GameObject> enemies = new List<GameObject>();

        Collider[] hitColliders = Physics.OverlapSphere(_CharacterCenter.position, _Radius);
        _Anim.SetTrigger("Knockback");

        foreach (var col in hitColliders)
        {
            if (col.gameObject.tag != "Enemy")
                continue;

            enemies.Add(col.gameObject);
        }

        StartCoroutine(PushEnemies(enemies));
    }

    private IEnumerator PushEnemies(List<GameObject> enemies)
    {
        yield return new WaitForSeconds(.5f);

        foreach (var enemy in enemies)
        {
            var direction = enemy.transform.position - transform.position;            

            enemy.GetComponent<Rigidbody>().AddForce(direction.normalized * _Force, ForceMode.Impulse);
            enemy.GetComponentInChildren<Animator>().SetTrigger("PushedBack");

        }

        StopAllCoroutines();
    }
}
