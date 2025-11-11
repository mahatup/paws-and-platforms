using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesViewPresenter : MonoBehaviour
{
    [SerializeField] private CatService _catService;
    [SerializeField] private ViewPrefabConfig _config;
    [SerializeField] private ViewFactory _viewFactory;
    [SerializeField] private float _distance = 100f;
    [SerializeField] private float _margin = 30f;

    private LivesView _livesView;

    private void Awake()
    {
        _viewFactory = new ViewFactory(_config, transform);

        _livesView = _config.LivesViewPrefab.GetComponent<LivesView>();
    }

    private void OnEnable()
    {
        _catService.HeartSpawned += OnHeartSpawned;
        _catService.HeartDropped += OnHeartDropped;
    }

    private void OnDisable()
    {
        _catService.HeartSpawned -= OnHeartSpawned;
        _catService.HeartDropped -= OnHeartDropped;
    }

    private void OnHeartSpawned(int lives)
    {
        RectTransform canvasRect = GetComponent<Canvas>().GetComponent<RectTransform>();
        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        for (int i = 0; i < lives; i++)
        {
            var heartObj = _viewFactory.CreateLivesView();

            RectTransform rect = heartObj.GetComponent<RectTransform>();

            float xPos = -_margin - (i * _distance);
            float yPos = _margin;

            rect.anchoredPosition = new Vector2(xPos, yPos);

            GameObject whole = heartObj.transform.Find("WholeHeart").gameObject;
            GameObject broken = heartObj.transform.Find("BrokenHeart").gameObject;

            _livesView.AddHeart(whole, broken);
        }
    }

    private void OnHeartDropped()
    {
        _livesView?.BreakLastHeart();
    }
}

