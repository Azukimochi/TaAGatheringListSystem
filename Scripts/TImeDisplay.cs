
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace io.github.Azukimochi
{
    public class TImeDisplay : UdonSharpBehaviour
    {
        [SerializeField] Text _text;

        private void Update()
        {
            string time = Util.getJST().ToString("HH:mm:ss");

            time = $"現在時刻 {time} JST";
            _text.text = time;
        }
    }
}