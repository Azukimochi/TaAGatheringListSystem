
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace io.github.Azukimochi
{
    public class Settings : UdonSharpBehaviour
    {
        [Header("JSONへのURL")]
        [SerializeField] public VRCUrl _URL;
        [Header("日付確認の更新頻度(分)")]
        [SerializeField] public int _UpdateIntervalMinutes = 5;

        [Header("週表示ボタンのカラー設定")]
        [SerializeField] public Color _defaultColor = Color.black;
        [SerializeField] public Color _selectedColor = Color.gray;
        [SerializeField] public Color _todayColor = new Color(0.3f, 0.3f, 0.3f);
        
        [Header("デバッグ用")]
        [SerializeField] public string _DebugTime;
    }
}
