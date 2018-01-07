using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private MapNodeCanvas map;
    [SerializeField] private GameObject SlidesContainer;
    [SerializeField] private GameObject SlidePrefab;
    private IEnumerable<LevelNode> levels;
    private List<GameObject> slides = new List<GameObject>();

    private int X = 600;
    private int slideWidth = 300;
    private int slidePadding = 100;


    void SetLevel(LevelNode level)
    {
        GameControll.Instance.CurrentLevel = level;
        GameControll.Instance.StartLevel();
    }

    // Use this for initialization
    void Start()
    {
        SliderMenu sliderMenu = FindObjectOfType<SliderMenu>();
        levels = map.GetAllLevels();
        foreach (LevelNode level in levels)
        {
            GameObject go = Instantiate(SlidePrefab, SlidesContainer.transform);
            go.GetComponentInChildren<Text>().text = level.Caption;
            go.GetComponentInChildren<Button>().onClick.AddListener(() => { SetLevel(level); });
            RectTransform r = go.GetComponent<RectTransform>();
            Vector3 rpos = r.position;
            rpos.x = X;
            X += slideWidth + slidePadding;
            r.position = rpos;
            slides.Add(go);
        }

        sliderMenu.Init(slides);
    }


    // Update is called once per frame
    void Update()
    {
    }
}