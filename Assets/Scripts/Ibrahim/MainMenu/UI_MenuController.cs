using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Menu
{

    public class UI_MenuController : MonoBehaviour
    {
        private UIDocument _document;

        public UIDocument Document { get => _document;}

        void Awake()
        {
            _document = GetComponent<UIDocument>();
        }
    }
}
