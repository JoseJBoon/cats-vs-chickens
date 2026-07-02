using System.Collections;
using UnityEngine;

/// Test component for the health component
public class HurtMe : MonoBehaviour
{
    [SerializeField] private float damage = 10.0f;

    private Health _health;
    private Coroutine _displayHealth;

    private void Awake()
    {
        _health = GetComponentInChildren<Health>();
        _health.OnHealthDepleted += LogDeath;
    }

    private void Start()
    {
        StartCoroutine(BobAndWeave());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && _displayHealth == null)
        {
            _displayHealth = StartCoroutine(DisplayHealth());
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _health.TakeDamage(damage);
        }
    }

    private void LogDeath()
    {
        Debug.Log("DED! (╥﹏╥)");
    }

    private IEnumerator DisplayHealth()
    {
        _health.DisplayHealthBar();
        yield return new WaitForSeconds(3.0f);
        _health.HideHealthBar();

        _displayHealth = null;
    }

    private IEnumerator BobAndWeave()
    {
        const float duration = 2.0f;
        var anchor = transform.position;

        while (true)
        {
            var timer = 0.0f;
            var point = anchor + Random.insideUnitSphere * 3.0f;
            while (timer < duration)
            {
                yield return null;
                timer += Time.deltaTime;
                transform.position = Vector3.Lerp(anchor, point, timer / duration);
            }
            while (timer > 0.0f)
            {
                yield return null;
                timer -= Time.deltaTime;
                transform.position = Vector3.Lerp(anchor, point, timer / duration);
            }
            transform.position = anchor;
        }
    }
}
