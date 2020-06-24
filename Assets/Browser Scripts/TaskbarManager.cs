using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This whole class is... really dumb
public class TaskbarManager : MonoBehaviour
{
    [SerializeField]
    private GameObject taskbarIcon;

    private GameObject windows;
    private int maxOpen = 3;
    private List<GameObject> windowsOpen;
    private List<GameObject> taskbarSlots;
    private GameObject regionTwo;

    private void Start()
    {
        windows = new GameObject();
        windows = Instantiate(windows, Vector3.zero, Quaternion.identity);
        windows.name = "Windows";
        windows.transform.SetParent(this.transform.parent.parent);
        windows.transform.SetSiblingIndex(windows.transform.parent.childCount - 2);

        windowsOpen = new List<GameObject>();
        taskbarSlots = new List<GameObject>();
        regionTwo = this.gameObject;
    }

    public void UpdateButtons(int index)
    {
        for (int i = index; i < windowsOpen.Count; i++)
        {
            int tmp = i;
            GameObject window = windowsOpen[i];
            GameObject taskbarIcon = taskbarSlots[i];

            /* Set up the close button */

            Button close = window.transform.GetChild(2).GetComponent<Button>();
            close.onClick.RemoveAllListeners();
            close.GetComponent<Button>().onClick.AddListener(() =>
            {
                DestroyWindow(tmp);
            });

            /* Set up the min button */

            Button min = window.transform.GetChild(3).GetComponent<Button>();
            min.onClick.RemoveAllListeners();
            min.GetComponent<Button>().onClick.AddListener(() =>
            {
                MinimizeWindow(tmp);
            });

            /* Set up the taskbar button */

            Button slot = taskbarIcon.GetComponent<Button>();
            slot.onClick.RemoveAllListeners();
            slot.GetComponent<Button>().onClick.AddListener(() =>
            {
                FocusWindow(tmp);
            });
        }
    }

    //TODO: Add animation?
    //Currently not very interesting...
    public void MinimizeWindow(int index)
    {
        FocusWindow(index);
    }

    public void DestroyWindow(int index)
    {
        GameObject remove = windowsOpen[index];
        GameObject icon = taskbarSlots[index];
        remove.GetComponent<WindowManager>().InformOfDeath();
        SceneInterpreter interpreter = remove.GetComponent<SceneInterpreter>();
        if(interpreter != null)
            interpreter.CloseMyGame();
        windowsOpen.RemoveAt(index);
        taskbarSlots.RemoveAt(index);
        Destroy(remove);
        Destroy(icon);
        UpdateButtons(0);
    }

    public void FocusWindow(int index)
    {
        //Change color of taskbar icon
        windowsOpen[index].transform.SetAsLastSibling();
        windowsOpen[index].SetActive(!windowsOpen[index].activeSelf);
    }

    public void CreateWindow(GameObject window, Vector3 pos, string name, GameObject icon, Image image)
    {
        GameObject canvas = this.transform.parent.gameObject;
        GameObject newWindow = Instantiate(window, pos, Quaternion.identity);
        newWindow.transform.SetParent(windows.transform);
        WindowManager newWinManager = newWindow.GetComponent<WindowManager>();
        newWinManager.SetName(name);
        newWinManager.SetIcon(icon);
        newWinManager.SetImage(image);
        windowsOpen.Add(newWindow);

        GameObject newIcon = Instantiate(taskbarIcon);
        newIcon.GetComponent<IconData>().SetupIcon(image, name);
        newIcon.transform.SetParent(regionTwo.transform); //TODO: Change the icon message
        taskbarSlots.Add(newIcon);
        UpdateButtons(windowsOpen.Count - 1);
    }

    //TODO: Write code to toggle child force expand for dynamic sizing

    //But...what if no?
}
