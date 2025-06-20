using UnityEngine;

public static class ScreenBoundsData
{
    private static Rect _bounds;
    private static bool _isInitialized = false;


    private static void Initialize()
    {
        CalculateLimits();
        _isInitialized = true;
    }

    private static void CalculateLimits()
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize * 2.0f;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector2 mainCameraCenter = mainCamera.transform.position;

        _bounds = new Rect(
            mainCameraCenter.x - cameraWidth / 2.0f,
            mainCameraCenter.y - cameraHeight / 2.0f,
            cameraWidth,
            cameraHeight);
    }

    public static Rect GetScreenBounds()
    {
        if (!_isInitialized)
            Initialize();

        return _bounds;
    }
}

