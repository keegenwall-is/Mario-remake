using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 引入UI命名空间

public class PauseGame : MonoBehaviour
{
    public Text pauseText; // 在Inspector中设置对Pause Text的引用
    private bool isPaused = false; // 跟踪游戏是否暂停

    void Update()
    {
        // 监听ESC键的按下
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // 监听P键的按下，返回开始菜单
        if (Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            // 确保游戏的时间流逝速度被重置
            Time.timeScale = 1;
            // 加载开始菜单场景
            SceneManager.LoadScene("Begaining Menu");
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // 暂停游戏
            Time.timeScale = 0;
            // 显示Pause Text
            pauseText.enabled = true;
        }
        else
        {
            // 继续游戏
            Time.timeScale = 1;
            // 隐藏Pause Text
            pauseText.enabled = false;
        }
    }
}
