using UnityEngine;
using UnityEngine.UI;

public class PauseBtn : MonoBehaviour
{
    [SerializeField] private Button pauseBtn;
    void Awake()
    {
        pauseBtn = this.GetComponent<Button>();
    }

    void Update()
    {
        
    }

    private void ShowDialogPauseMenu()
    {
        
    }
}
