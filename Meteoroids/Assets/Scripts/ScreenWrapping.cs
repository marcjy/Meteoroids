using UnityEngine;

public class ScreenWrapping : MonoBehaviour
{
    private Rect _bounds;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _bounds = ScreenBoundsData.GetScreenBounds();
    }

    void FixedUpdate()
    {
        CheckBounds();
    }

    private void CheckBounds()
    {
        float pixelsPerUint = _spriteRenderer.sprite.pixelsPerUnit;

        float offsetX = (_spriteRenderer.sprite.rect.width / pixelsPerUint) / 2;
        float offsetY = (_spriteRenderer.sprite.rect.height / pixelsPerUint) / 2;

        Vector2 currentPosition = transform.position;

        if (currentPosition.x - offsetX > _bounds.xMax)
            currentPosition.x = _bounds.xMin;
        else if (currentPosition.x + offsetX < _bounds.xMin)
            currentPosition.x = _bounds.xMax;

        if (currentPosition.y - offsetY > _bounds.yMax)
            currentPosition.y = _bounds.yMin;
        else if (currentPosition.y + offsetY < _bounds.yMin)
            currentPosition.y = _bounds.yMax;

        transform.position = currentPosition;
    }
}
