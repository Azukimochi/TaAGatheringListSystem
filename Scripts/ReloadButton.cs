﻿using UdonSharp;
using UnityEngine;

namespace io.github.Azukimochi
{
    public class ReloadButton : UdonSharpBehaviour
    {
        [SerializeField] private GatheringListSystem parent;

        public void Pressed()
        {
            parent.InitLoadJson();
        }
    }
}