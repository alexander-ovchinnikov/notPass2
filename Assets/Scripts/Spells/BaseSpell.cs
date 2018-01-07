using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

namespace Spells
{
    public class BaseSpell : MonoBehaviour
    {
        [SerializeField] private Sprite _panelIcon;

        public Sprite PanelIcon
        {
            get { return _panelIcon; }
            set { _panelIcon = value; }
        }
    }
}