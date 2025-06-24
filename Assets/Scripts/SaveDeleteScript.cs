using System.IO;
using UnityEngine;

public class SaveDeleteScript : MonoBehaviour
{
    [SerializeField] private DayNightScript dayNightScript;
    public void DeleteSave()
    {
        dayNightScript.SetDayCycle(DayCycles.Sunrise);
        dayNightScript.SetDayCount(1);
        
        string path = Application.persistentDataPath;

        if (Directory.Exists(path))
        {
            string[] files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                File.Delete(file);
            }
            
            Debug.Log("All save files deleted");
        }
        else
        {
            Debug.Log("No save files found");
        }
    }
}
