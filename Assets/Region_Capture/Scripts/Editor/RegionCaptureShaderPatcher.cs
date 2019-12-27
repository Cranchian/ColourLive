using UnityEditor.Callbacks;
using System.Linq;
using System.IO;
using UnityEditor;

public class RegionCaptureShaderPatcher
{
    private static string path = Path.GetFullPath("Packages/");
    private static string[] data;

    [PostProcessScene()]
    private static void OnPPScene()
    {
        PatchShader();
    }

    [PostProcessBuild()]
    private static void OnPPBuild(BuildTarget target, string str)
    {
        PatchShader();
    }

    public static void PatchShader()
    {
        if (Directory.Exists(path))
        {
            if (File.Exists(path + "manifest.json"))
            {
                data = File.ReadAllLines(path + "manifest.json");
            }
        }
        if (data.Length > 2)
        {
            foreach (string line in data)
            {
                if (line.Contains("com.ptc.vuforia.engine"))
                {
                    string version = line.Substring(31, line.Length - 1 - 31);
                    while (version.Contains("\""))
                    { version = version.Remove(version.Length - 1); }
                    path = Path.GetFullPath("Library/PackageCache/com.ptc.vuforia.engine@" + version + "/Vuforia/Shaders/");
                    break;
                }
            }
        }

        data = new string[0];
        string[] dataEdit = data;

        if (Directory.Exists(path))
        {
            if (File.Exists(path + "VideoBackground.shader"))
            {
                data = File.ReadAllLines(path + "VideoBackground.shader");
            }
        }

        for (int i = 0; i < data.Length; i++)
        {
            dataEdit = dataEdit.Append(data[i]).ToArray();

            if (data[i].Contains("Pass {") && (i < data.Length-1 && !data[i+1].Contains("Name")))
            {
                dataEdit = dataEdit.Append("            Name \"VUFORIA\"").ToArray();
            }
            else if (i < data.Length - 1 && data[i + 1].Contains("Name"))
            {
                dataEdit = new string[0];
                break;
            }
        }

        if (dataEdit.Length > data.Length)
        File.WriteAllLines(path + "VideoBackground.shader", dataEdit);
        AssetDatabase.Refresh();
    }
}
