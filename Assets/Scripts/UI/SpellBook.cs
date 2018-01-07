using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Spells;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    [SerializeField] private BaseSpell[] _spells;
    [SerializeField] private List<BaseSpell> _activeSpells;
    private int MaxSpells = 3;
    [SerializeField] private GameObject BottomPanelContainer;
    [SerializeField] private GameObject AvailablePanelContainer;


    public void RemoveSpell()
    {
    }

    public void ChooseSpell()
    {
    }

    private Button CreateButton(Sprite sprite, Transform transform)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.SetParent(transform);
        gameObject.SetActive(true);
        Button button = gameObject.AddComponent<Button>();
        Image image = gameObject.AddComponent<Image>();
        image.sprite = sprite;
        return button;
    }

    private void Cast(BaseSpell spell)
    {
        BaseSpell spawnedSpell = Instantiate(spell);
        spawnedSpell.gameObject.SetActive(true);
    }

    // Use this for initialization

    void UpdatePanel()
    {
        foreach (Transform child in BottomPanelContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (BaseSpell spell in _activeSpells)
        {
            if (spell == null)
                continue;
            Button button = CreateButton(
                spell.PanelIcon,
                BottomPanelContainer.transform
            );
            button.onClick.AddListener(() => { Cast(spell); });
        }
    }


    void ToggleChosen(BaseSpell spell)
    {
        if (_activeSpells.Contains(spell))
        {
            _activeSpells.Remove(spell);
        }
        else
        {
            _activeSpells.Add(spell);
        }

        UpdatePanel();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        foreach (BaseSpell spell in _spells)
        {
            Button button = CreateButton(
                spell.PanelIcon,
                AvailablePanelContainer.transform
            );
            button.onClick.AddListener(() => { ToggleChosen(spell); });
        }


        UpdatePanel();
    }

    // Update is called once per frame
    void Update()
    {
    }
}