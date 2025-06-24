using UnityEngine;
using UnityEngine.UI;

public class UILivesRenderer : MonoBehaviour
{
    [SerializeField] private Color _fadedHeartColor;

    private Image[] _heartImages;
    private int _currentIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _heartImages = GetComponentsInChildren<Image>();
        _currentIndex = _heartImages.Length - 1;

        SubscribeToGameManagerEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromGameManagerEvents();
    }


    #region Event Subscription
    private void SubscribeToGameManagerEvents()
    {
        GameManager.Instance.OnGameStart += HandleGameStart;
        GameManager.Instance.OnRoundEnd += HandleRoundEnd;
    }


    private void UnsubscribeFromGameManagerEvents()
    {
        GameManager.Instance.OnGameStart -= HandleGameStart;
        GameManager.Instance.OnRoundEnd -= HandleRoundEnd;
    }
    #endregion

    #region Event Handling
    private void HandleGameStart(object sender, System.EventArgs e) => ResetHearts();
    private void HandleRoundEnd(object sender, System.EventArgs e) => FadeHeart();
    #endregion

    private void FadeHeart()
    {
        if (_currentIndex >= 0)
        {
            _heartImages[_currentIndex].color = _fadedHeartColor;
            _currentIndex--;
        }
        else
            Debug.LogWarning($"'{nameof(FadeHeart)}' called but no hearts remain to be faded. Current index had value '{_currentIndex}'.");
    }
    private void ResetHearts()
    {
        foreach (Image image in _heartImages)
            image.color = Color.white;
        _currentIndex = _heartImages.Length - 1;
    }
}
