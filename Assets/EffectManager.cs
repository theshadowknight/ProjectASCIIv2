using HelperFunctions;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public bool autoStart = false;

    public CanvasGroup consoleCg;
    public CanvasGroup textScreenCg;

    public Image img;
    public bool PcOn = false;
    public RawImage ri;
    public TMP_InputField inputf;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Start()
    {
        // SystemSetup();
        if (autoStart)
        {
            AutoStart();
        }
    }
    public void AutoStart()
    {
        ri.color = new Color(1, 1, 1, 1);
        img.color = new Color(img.color.r, img.color.g, img.color.b,1f/4);
        consoleCg.alpha = 1f;
        StartCoroutine(SetupIE(true));
        PcOn = true;
    }
    public void SystemSetup()
    {
        StartCoroutine(ScreenColorer(true));

    }
    private IEnumerator SetupIE(bool quick)
    {
        Vector2 waitRange = new Vector2(0.15f, 0.35f);
        if (quick)
        {
            waitRange = new Vector2(0, 0);
        }
        yield return new WaitForSeconds(Random.Range(waitRange.x * 5, waitRange.y * 5));
        InputManager.instance.inputField.DeactivateInputField();
        yield return StorageMemoryManager.instance.GetFileFormPath(StorageMemoryManager.instance.systemPath);
        StorageMemoryManager.instance.system = StorageMemoryManager.instance.bufferFileGet;
        yield return StorageMemoryManager.instance.GetFileFormPath(StorageMemoryManager.instance.systemPath + "/logo.ap");
        File logo = StorageMemoryManager.instance.bufferFileGet;
        yield return StorageMemoryManager.instance.GetFileFormPath(StorageMemoryManager.instance.systemPath + "/registry.dat");
        StorageMemoryManager.instance.registry = StorageMemoryManager.instance.bufferFileGet;

        yield return new WaitForSeconds(Random.Range(waitRange.x, waitRange.y));
        yield return CommandLineManager.instance.Write(logo.data + "\n");
        yield return CommandLineManager.instance.Write("CronOS. Timeless Operating System");
        yield return CommandLineManager.instance.Write("Coppyright (C) NASA INC. 1992. All right reserved.\n");
        yield return new WaitForSeconds(Random.Range(waitRange.x * 2, waitRange.y * 2));

        foreach (Drive s in StorageMemoryManager.instance.drives)
        {
            yield return CommandLineManager.instance.Write("Loaded drive " + s.name + "...");
            yield return new WaitForSeconds(Random.Range(waitRange.x, waitRange.y));
            yield return CommandLineManager.instance.Write("[c:10]Success![c:0]");

        }
        yield return CommandLineManager.instance.Write("Loading registry from " + StorageMemoryManager.instance.registry.GetFullName() + "...");
        StorageMemoryManager.instance.registryVariables = Functions.TagVariablesFromFile(StorageMemoryManager.instance.registry);


        yield return CommandLineManager.instance.Write((StorageMemoryManager.instance.registryVariables != null && StorageMemoryManager.instance.registryVariables.Count > 0) ? "[c:10]Success![c:0]" : "[c:12]Failure![c:0]");

        yield return new WaitForSeconds(Random.Range(waitRange.x, waitRange.y));
        yield return CommandLineManager.instance.Write("Found " + Functions.SizeConventer(Processor.instance.RAMmaxSize) + " of RAM.");
        yield return new WaitForSeconds(Random.Range(waitRange.x, waitRange.y));
        yield return CommandLineManager.instance.Write("Processor tact rate is: " + Processor.instance.tactsPS + " tact(s) per second.");
        yield return new WaitForSeconds(Random.Range(waitRange.x, waitRange.y));
        yield return CommandLineManager.instance.Write("System directory:" + (StorageMemoryManager.instance.system != null ? "[c:10]OK[c:0]" : "[c:12]NOT FOUND![c:0]"));
        yield return new WaitForSeconds(Random.Range(waitRange.x, waitRange.y));
        yield return CommandLineManager.instance.Write("Registry file:" + (StorageMemoryManager.instance.registry != null ? "[c:10]OK[c:0]" : "[c:12]NOT FOUND![c:0]"));
        yield return new WaitForSeconds(Random.Range(waitRange.x, waitRange.y));
        yield return CommandLineManager.instance.Write("Thanks for using CronOS!");
        InputManager.instance.inputField.ActivateInputField();

    }




    IEnumerator SetupLight()
    {
        for (int a = 0; a < 256; a++)
        {
            if (a % 16 == 0)
            {
                yield return new WaitForSeconds(0.1f);

            }
            consoleCg.alpha = (1f * a / 255);
            img.color = new Color(img.color.r, img.color.g, img.color.b, (1f * a / (255 * 4)));
            yield return new WaitForSeconds(0.01f);

        }
        yield return new WaitForSeconds(0.1f);
    }
    public void ButtonClicked()
    {
        StopAllCoroutines();
        PcOn = !PcOn;
        if (PcOn)
        {
            SystemSetup();
        }
        else
        {
            StartCoroutine(TurningOff());
        }
    }


    IEnumerator TurningOff()
    {
        yield return CommandLineManager.instance.Write("Saving critical data...");
        yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));
        yield return CommandLineManager.instance.Write("[c:10]Success![c:0]");

        yield return new WaitForSeconds(Random.Range(0.15f, 0.30f));
        StorageMemoryManager.instance.SaveAll();
        foreach (Drive s in StorageMemoryManager.instance.drives)
        {
            yield return CommandLineManager.instance.Write("Saving drive " + s.name + "...");
            yield return new WaitForSeconds(Random.Range(0.15f, 0.30f));
            yield return CommandLineManager.instance.Write("[c:10]Success![c:0]");

        }
        yield return CommandLineManager.instance.Write("Goodbye!");
        int start = Mathf.CeilToInt(consoleCg.alpha * 255);
        for (int a = start; a > -1; a--)
        {
            if (a % 16 == 0)
            {
                yield return new WaitForSeconds(0.1f);

            }
            consoleCg.alpha = (1f * a / 255);
            img.color = new Color(img.color.r, img.color.g, img.color.b, (1f * a / (255 * 4)));

            yield return new WaitForSeconds(0.01f);

        }
        yield return new WaitForSeconds(1f);
        yield return ScreenColorer(false);
    }
    IEnumerator ScreenColorer(bool on)
    {
        if (on)
        {

            for (int a = 0; a < 255; a++)
            {
                if (a % 16 == 0)
                {
                    yield return new WaitForSeconds(0.1f);

                }


                ri.color = new Color(ri.color.r, ri.color.g, ri.color.b, (1f * a / 255));
                yield return new WaitForSeconds(0.01f);

            }
            yield return new WaitForSeconds(0.1f);
            yield return SetupLight();

            yield return SetupIE(false);
        }
        else
        {
            int start = Mathf.CeilToInt(ri.color.a * 255);

            for (int a = start; a > -1; a--)
            {
                if (a % 16 == 0)
                {
                    yield return new WaitForSeconds(0.1f);

                }


                ri.color = new Color(ri.color.r, ri.color.g, ri.color.b, (1f * a / 255));
                yield return new WaitForSeconds(0.01f);

            }
            yield return new WaitForSeconds(0.1f);
            CommandLineManager.instance.consoleText = "";
            CommandLineManager.instance.UpdateText();
        }
    }
    public void TransferToTS()
    {
        InputManager.instance.inputField.DeactivateInputField();
        NoterLogic.instance.enabled = true;

        InputManager.instance.enabled = false;
       consoleCg.alpha = 0;
      textScreenCg.alpha = 1;
        inputf.enabled = false;
    }
    public void TransferToCL()
    {
        InputManager.instance.inputField.ActivateInputField();
        inputf.enabled = true;

        NoterLogic.instance.enabled = false;

        InputManager.instance.enabled = true;
        consoleCg.alpha = 1;
        textScreenCg.alpha = 0;
    }
}
