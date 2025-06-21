using System;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    public event EventHandler OnAsteroidCollision;

    void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.ASTEROID))
            OnAsteroidCollision?.Invoke(this, EventArgs.Empty);
    }
}
