using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ScreenWrapping))]
public abstract class BaseLaser : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] protected float _lifeTime = 3.0f;
    [SerializeField] protected float _speed = 8.0f;

    private Vector2 _direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up, Space.Self);

#if UNITY_EDITOR
        Debug.DrawRay(transform.position, transform.up * 0.5f, Color.magenta);
#endif
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.ASTEROID))
        {
            AsteroidManager asteroidManager = collision.gameObject.GetComponent<AsteroidManager>();
            OnAsteroidCollision(asteroidManager);
        }
    }
    protected abstract void OnAsteroidCollision(AsteroidManager asteroid);

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(_lifeTime);

        float elapsedTime = 0.0f;
        float fadeOutDuration = 0.5f;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color baseColor = spriteRenderer.color;
        float alphaTarget = 0.0f;

        while (elapsedTime < fadeOutDuration)
        {
            spriteRenderer.color = Color.Lerp(baseColor, new Color(baseColor.r, baseColor.g, baseColor.b, alphaTarget), elapsedTime / fadeOutDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = new Color(baseColor.r, baseColor.g, baseColor.b, alphaTarget);

        Destroy(gameObject);
    }
}
