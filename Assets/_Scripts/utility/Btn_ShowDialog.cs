using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Btn_ShowDialog : MonoBehaviour
{
    [SerializeField] private RectTransform prefabs;
    [SerializeField] private Button backBtn;
    [Header("Scale when apear")]
    [SerializeField] private float hideScale;
    [SerializeField] private float showScale;

    private float elapsedTime;
    [SerializeField] private float TimeToShowFullDialog = .5f;

    private bool isResetScale = false;

    public Button btn;
    void Awake()
    {
        btn = this.GetComponent<Button>();
    }
    void Start()
    {
        if(prefabs != null)
            prefabs.gameObject.SetActive(false);

        prefabs.localScale = new Vector3(1,1,1) * hideScale;
    }

    void OnEnable()
    {
        if(btn != null)
            btn.onClick.AddListener(OnClickShowDialog);
        
        if(backBtn != null)
            backBtn.onClick.AddListener(OnCloseDialog);

        isResetScale = false;
    }

    private void OnClickShowDialog()
    {
        prefabs.gameObject.SetActive(true);
        this.StartCoroutine(showCoroutine());
    }
    private void OnCloseDialog()
    {
        prefabs.gameObject.SetActive(false);
    }

    private IEnumerator showCoroutine()
    {
        while(true)
        {
            OnReset();
            elapsedTime += Time.deltaTime;

            if(elapsedTime > TimeToShowFullDialog)  break;

            float t = elapsedTime / TimeToShowFullDialog;

            Vector3 currentScale = Vector3.Lerp(new Vector3(1,1,1) * hideScale,
                                                new Vector3(1,1,1) * showScale,
                                                t);
            prefabs.localScale = currentScale;

            yield return null;
        }

        isResetScale = false;
    }

    private void OnReset()
    {
        if(!isResetScale)
        {
            prefabs.localScale = new Vector3(1,1,1) * hideScale;
            elapsedTime = 0;
            isResetScale = true;
        }
    }
}
