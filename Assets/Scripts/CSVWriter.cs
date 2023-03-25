using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class CSVWriter : MonoBehaviour
{
    public string fileName;
    string filePath = "";

    private bool isExecuting = false;
    private float executionTime = 5f;
    private float timer = 0f;

    public Material grassShader;

    private FPSCounter FPSCounter;
    private GrassTrampleMultiple CharactersCounter;

    public class Data
    {
        public float Tasselation;
        public float BladeWidth;
        public float BladeHeigth;
        public float WindStrength;
    }

    // Start is called before the first frame update
    void Start()
    {
        FPSCounter = GetComponent<FPSCounter>();
        CharactersCounter = GetComponent<GrassTrampleMultiple>();

        filePath = Application.dataPath + "/"+ fileName + ".csv";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && !isExecuting)
        {
            StartCoroutine(WriteCSV());
        }
    }

    public IEnumerator WriteCSV()
    {
        Debug.Log("Writing Started");

        isExecuting = true;

        TextWriter tw = new StreamWriter(filePath, false);
        tw.WriteLine("FPS, Characters, Tasselation, Blade Width, Blade Heigth, Wind Strength");
        tw.Close();

        while (timer < executionTime)
        {
            tw = new StreamWriter(filePath, true);
            Data currentData = GetShaderValues();
            tw.WriteLine(FPSCounter.frameRate + "," + 
                CharactersCounter.cachedObjectsList.Count + "," + 
                currentData.Tasselation.ToString(CultureInfo.InvariantCulture) + "," + 
                currentData.BladeWidth.ToString(CultureInfo.InvariantCulture) + "," + 
                currentData.BladeHeigth.ToString(CultureInfo.InvariantCulture) + "," + 
                currentData.WindStrength.ToString(CultureInfo.InvariantCulture));
            tw.Close();

            timer += Time.deltaTime;
            yield return null;
        }

        isExecuting = false;
        timer = 0f;

        Debug.Log("Writing Finished");
    }

    public Data GetShaderValues()
    {
        Data newData = new Data();

        newData.Tasselation = grassShader.GetFloat("_TessellationUniform");
        newData.BladeWidth = grassShader.GetFloat("_BladeWidth");
        newData.BladeHeigth = grassShader.GetFloat("_BladeHeight");
        newData.WindStrength = grassShader.GetFloat("_WindStrength");

        return newData;
    }
}