using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _showAdButton;
    [SerializeField] Button _showAdButtonP;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms

    TitleManager titleManager;

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        //Disable the button until the ad is ready to show:
        _showAdButton.interactable = false;
        _showAdButtonP.interactable = false;
    }

    void Start()
    {
        LoadAd(); //追加
    }

    // 広告ユニットにコンテンツをロードします：
    public void LoadAd()
    {
        // 重要！コンテンツのロードは初期化した後にのみ行ってください（この例では、初期化は別のスクリプトで処理されています）。
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // 広告が正常にロードされた場合は、ボタンにリスナーを追加してそれを有効にします：
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            // クリックされると ShowAd() メソッドが呼び出されるようボタンを設定します：
            _showAdButton.onClick.AddListener(ShowAd);
            _showAdButtonP.onClick.AddListener(ShowAd);
            // ユーザーがクリックできるようにボタンを有効にします：
            _showAdButton.interactable = true;
            _showAdButtonP.interactable = true;
        }
    }

    // ユーザーがボタンをクリックすると実行されるメソッドを実装します：
    public void ShowAd()
    {
        // ボタンを無効にします：
        _showAdButton.interactable = false;
        // その後、広告を表示します：
        Advertisement.Show(_adUnitId, this);
    }

    // 表示リスナーの OnUnityAdsShowComplete コールバックメソッドを実装して、ユーザーに報酬を受け取る資格があるかどうかを判定します：
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // 報酬を授与します。
            titleManager.RewardedAdd();
            // Load another ad:
            Advertisement.Load(_adUnitId, this);
        }
    }
    // ロードリスナーと表示リスナーのエラーコールバックを実装します：
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // エラーの詳細を使用して、別の広告のロードを試行するかどうかを判断します。
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // エラーの詳細を使用して、別の広告のロードを試行するかどうかを判断します。
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // ボタンリスナーをクリーンアップします：
        _showAdButton.onClick.RemoveAllListeners();
        _showAdButtonP.onClick.RemoveAllListeners();
    }
}