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
                try
                {
                    File.Delete(file);
                    Debug.Log("Deleted file: " + file);
                }
                catch (IOException ex)
                {
                    Debug.LogError($"Failed to delete {file}: {ex.Message}");
                }
            }

            
            //Debug.Log("All save files deleted");
        }
        else
        {
            Debug.Log("No save files found");
        }
    }
}
