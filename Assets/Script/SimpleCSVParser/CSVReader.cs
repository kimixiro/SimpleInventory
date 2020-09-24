using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    private const string SpitRe = @",(?=(?:[^""]^""[^""]^"")*(?![^""]^""))";
    private const string LineSplitRe = @"\r\n|\n\r|\n|\r";
    private static readonly char[] TrimChars = {'\"'};

    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();

        string rawText = System.IO.File.ReadAllText(file);

        var lines = Regex.Split(rawText, LineSplitRe);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SpitRe);

        for (int i = 0; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SpitRe);
            if(values.Length == 0 || values[0]=="") continue;
            
            var entry = new Dictionary<string,object>();

            for (int j = 0; j < header.Length&& j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TrimChars).TrimEnd(TrimChars).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }

                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}
