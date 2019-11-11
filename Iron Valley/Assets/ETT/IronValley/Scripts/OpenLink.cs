using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ett.IronValley.Scripts
{
    public class OpenLink : MonoBehaviour
    {
        public string Url;
        
        public void OpenExternalLink()
        {
            Application.OpenURL(Url);
        }
    }
}