using UnityEngine;
using System.Collections.Generic;

public class PlayerAttach : MonoBehaviour
{
    private List<GameObject> attachedEnemies = new List<GameObject>();
    public float enemySpacing = 0.5f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                AttachEnemy(other.gameObject);
            }
        }
        else if(other.CompareTag("BadEnemy"))
        {
            GameManager.Instance.AddScore(-1);
            Destroy(other.gameObject);
        }
    }
    void AttachEnemy(GameObject enemy)
    {
        foreach (var attached in attachedEnemies)
        {
            if (attached == null) continue;
            float dist = Vector3.Distance(attached.transform.position, enemy.transform.position);
            if (dist < enemySpacing)
            {
                return;
            }
        }

        var enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.enabled = false;
        }

        float playerRadius = 0.5f;
        float enemyRadius = 0.5f;

        Collider2D playerCollider = GetComponent<Collider2D>();
        if (playerCollider != null)
        {
            float width = playerCollider.bounds.size.x;
            float height = playerCollider.bounds.size.y;
            playerRadius = Mathf.Max(width, height) / 2f;
        }

        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
        if (enemyCollider != null)
        {
            float enemyWidth = enemyCollider.bounds.size.x;
            float enemyHeight = enemyCollider.bounds.size.y;
            enemyRadius = Mathf.Max(enemyWidth, enemyHeight) / 2f;
        }

        float attachDistance = playerRadius + enemyRadius;

        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        Vector3 attachPosition = transform.position + direction * attachDistance;

        enemy.transform.position = attachPosition;
        enemy.transform.SetParent(transform);
        attachedEnemies.Add(enemy);
    }
    public void DetachAllEnemies()
    {
        foreach (var enemy in attachedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        attachedEnemies.Clear();
    }

    public int DetachAndCountMatchingEnemies(string shapeType)
    {
        int count = 0;

        for (int i = attachedEnemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = attachedEnemies[i];
            if (enemy == null) continue;

            EnemyController ec = enemy.GetComponent<EnemyController>();
            if (ec != null && ec.enemyShape == shapeType)
            {
                count++; 
            }

            Destroy(enemy); 
            attachedEnemies.RemoveAt(i);
        }

        return count;
    }
    public int GetAttachedEnemyCount()
    {
        return attachedEnemies.Count;
    }
}
