using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VitureCommonLibrary;
using uWindowCapture;

public class VddManager : MonoBehaviour
{
	[SerializeField]
	private Camera mainCamera;

	private MainCamera _mainCamera;

	private GameObject wideScreenObj;

	private UwcWindowTexture wideScreen;

	private GameObject wideScreen_24_9_Obj;

	private UwcWindowTexture wideScreen_24_9;

	private List<GameObject> r6Horizon3Objs = new List<GameObject>();

	private List<GameObject> r6Horizon2Objs = new List<GameObject>();

	private List<UwcWindowTexture> r6Horizon3Textures = new List<UwcWindowTexture>();

	private List<UwcWindowTexture> r6Horizon2Textures = new List<UwcWindowTexture>();

	private List<GameObject> horizonObjs = new List<GameObject>();

	private List<GameObject> verticalObjs = new List<GameObject>();

	private List<UwcWindowTexture> horizonTextures = new List<UwcWindowTexture>();

	private List<UwcWindowTexture> verticalTextures = new List<UwcWindowTexture>();

	private string _glassesModel = "R6";

	private LayoutMode _layoutMode = LayoutMode.HorizonMirror3;

	private bool _turnOffScreen;

	public GameObject R6Left3ScreenObj => r6Horizon3Objs[0];

	public GameObject R6Center3ScreenObj => r6Horizon3Objs[1];

	public GameObject R6Right3ScreenObj => r6Horizon3Objs[2];

	public GameObject R6Left2ScreenObj => r6Horizon2Objs[0];

	public GameObject R6Right2ScreenObj => r6Horizon2Objs[1];

	public GameObject LeftScreenObj => horizonObjs[0];

	public GameObject CenterScreenObj => horizonObjs[1];

	public GameObject RightScreenObj => horizonObjs[2];

	public GameObject TopScreenObj => verticalObjs[0];

	public GameObject BottomScreenObj => verticalObjs[2];

	public GameObject WideScreenObj => wideScreenObj;

	public GameObject WideScreen_24_9_Obj => wideScreen_24_9_Obj;

	public UwcWindowTexture R6Left3Screen => r6Horizon3Textures[0];

	public UwcWindowTexture R6Center3Screen => r6Horizon3Textures[1];

	public UwcWindowTexture R6Right3Screen => r6Horizon3Textures[2];

	public UwcWindowTexture R6Left2Screen => r6Horizon2Textures[0];

	public UwcWindowTexture R6Right2Screen => r6Horizon2Textures[1];

	public UwcWindowTexture LeftScreen => horizonTextures[0];

	public UwcWindowTexture CenterScreen => horizonTextures[1];

	public UwcWindowTexture RightScreen => horizonTextures[2];

	public UwcWindowTexture TopScreen => verticalTextures[0];

	public UwcWindowTexture BottomScreen => verticalTextures[2];

	public UwcWindowTexture WideScreen => wideScreen;

	public UwcWindowTexture WideScreen_24_9 => wideScreen_24_9;

	private void Awake()
	{
		if (mainCamera != null)
		{
			_mainCamera = mainCamera.GetComponent<MainCamera>();
		}
		GetParam();
		FindDisplays();
		AutoLayoutDisplays();
		if (!Application.isEditor)
		{
			StartCoroutine(LogR6LayoutDiag());
		}
	}

	private IEnumerator LogR6LayoutDiag()
	{
		Transform canvasTf = ((r6Horizon3Objs.Count > 0 && r6Horizon3Objs[0] != null) ? r6Horizon3Objs[0].transform.parent : null);
		for (int i = 0; i < 8; i++)
		{
			if (canvasTf != null)
			{
				RectTransform rectTransform = canvasTf as RectTransform;
				Canvas component = canvasTf.GetComponent<Canvas>();
				CanvasScaler component2 = canvasTf.GetComponent<CanvasScaler>();
				string text = $"[R6LayoutDiag #{i}] R6Canvas lossyScale={canvasTf.lossyScale} localScale={canvasTf.localScale}";
				if (rectTransform != null)
				{
					text += $" rect={rectTransform.rect.width:F0}x{rectTransform.rect.height:F0}";
				}
				if (component != null)
				{
					text = text + $" renderMode={component.renderMode} scaleFactor={component.scaleFactor:F3} " + string.Format("worldCam={0} planeDist={1}", (component.worldCamera != null) ? component.worldCamera.name : "null", component.planeDistance);
				}
				if (component2 != null)
				{
					text += $" scaler.mode={component2.uiScaleMode} scaler.factor={component2.scaleFactor} refRes={component2.referenceResolution}";
				}
				VitureCommonLibrary.Logger.Info(text);
			}
			LogQuad(i, "C3-center", r6Horizon3Objs, r6Horizon3Textures, 1);
			LogQuad(i, "L3-left", r6Horizon3Objs, r6Horizon3Textures, 0);
			LogQuad(i, "R3-right", r6Horizon3Objs, r6Horizon3Textures, 2);
			LogQuad(i, "L2-2split", r6Horizon2Objs, r6Horizon2Textures, 0);
			LogQuad(i, "R2-2split", r6Horizon2Objs, r6Horizon2Textures, 1);
			yield return new WaitForSeconds(1f);
		}
	}

	private void LogQuad(int i, string tag, List<GameObject> objs, List<UwcWindowTexture> texs, int idx)
	{
		if (objs == null || idx >= objs.Count || objs[idx] == null)
		{
			return;
		}
		GameObject gameObject = objs[idx];
		Renderer component = gameObject.GetComponent<Renderer>();
		UwcWindowTexture uwcWindowTexture = ((texs != null && idx < texs.Count) ? texs[idx] : null);
		string text = "renderer=none";
		if (component != null)
		{
			Bounds bounds = component.bounds;
			text = $"rendererEnabled={component.enabled} worldX[{bounds.min.x:F2},{bounds.max.x:F2}] worldW={bounds.max.x - bounds.min.x:F2}";
			if (mainCamera != null)
			{
				Vector3 vector = mainCamera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.center.y, bounds.center.z));
				Vector3 vector2 = mainCamera.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.center.y, bounds.center.z));
				text += $" screenX[{vector.x:F0},{vector2.x:F0}] depth={vector.z:F2}";
			}
			Texture texture = ((component.material != null) ? component.material.mainTexture : null);
			text += ((texture != null) ? $" tex={texture.width}x{texture.height}" : " tex=null");
		}
		VitureCommonLibrary.Logger.Info($"[R6LayoutDiag #{i}] {tag} name={gameObject.name} active={gameObject.activeInHierarchy} " + $"localScale=({gameObject.transform.localScale.x:F1},{gameObject.transform.localScale.y:F1}) " + "desktop=" + ((!(uwcWindowTexture != null)) ? "?" : (string.IsNullOrEmpty(uwcWindowTexture.desktopName) ? "<empty>" : uwcWindowTexture.desktopName)) + " scaleType=" + ((uwcWindowTexture != null) ? uwcWindowTexture.scaleControlType.ToString() : "?") + " | " + text);
	}

	private void GetParam()
	{
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			if (commandLineArgs[i] == "-layoutMode" && i + 1 < commandLineArgs.Length)
			{
				string text = commandLineArgs[i + 1];
				VitureCommonLibrary.Logger.Info("Parameter -layoutMode: " + text);
				if (Enum.TryParse<LayoutMode>(text, out var result))
				{
					_layoutMode = result;
				}
			}
			if (commandLineArgs[i] == "-turnOffScreen" && i + 1 < commandLineArgs.Length)
			{
				string text2 = commandLineArgs[i + 1];
				VitureCommonLibrary.Logger.Info("Parameter -turnOffScreen: " + text2);
				if (bool.TryParse(text2, out var result2))
				{
					_turnOffScreen = result2;
				}
			}
			if (commandLineArgs[i] == "-glassesModel" && i + 1 < commandLineArgs.Length)
			{
				string glassesModel = commandLineArgs[i + 1];
				_glassesModel = glassesModel;
				VitureCommonLibrary.Logger.Info("_glassesModel: " + _glassesModel);
			}
		}
	}

	private void FindDisplays()
	{
		VitureCommonLibrary.DisplayInfo[] allDisplays = DisplayManager2.Instance.AllDisplays;
		VitureCommonLibrary.Logger.Info($"All Displays: {allDisplays.ToArray()}");
		wideScreenObj = GameObject.Find("WideScreen_32_9");
		if (wideScreenObj != null)
		{
			UwcWindowTexture component = wideScreenObj.GetComponent<UwcWindowTexture>();
			if (component != null)
			{
				wideScreen = component;
			}
			wideScreenObj.SetActive(value: false);
		}
		wideScreen_24_9_Obj = GameObject.Find("WideScreen_24_9");
		if (wideScreen_24_9_Obj != null)
		{
			UwcWindowTexture component2 = wideScreen_24_9_Obj.GetComponent<UwcWindowTexture>();
			if (component2 != null)
			{
				wideScreen_24_9 = component2;
			}
			wideScreen_24_9_Obj.SetActive(value: false);
		}
		FindScreenToList("R6Left3Screen", r6Horizon3Objs, r6Horizon3Textures);
		FindScreenToList("R6Center3Screen", r6Horizon3Objs, r6Horizon3Textures);
		FindScreenToList("R6Right3Screen", r6Horizon3Objs, r6Horizon3Textures);
		FindScreenToList("R6Left2Screen", r6Horizon2Objs, r6Horizon2Textures);
		FindScreenToList("R6Right2Screen", r6Horizon2Objs, r6Horizon2Textures);
		FindScreenToList("LeftScreen", horizonObjs, horizonTextures);
		FindScreenToList("CenterScreen", horizonObjs, horizonTextures);
		FindScreenToList("RightScreen", horizonObjs, horizonTextures);
		FindScreenToList("TopScreen", verticalObjs, verticalTextures);
		verticalObjs.Add(CenterScreenObj);
		verticalTextures.Add(CenterScreen);
		FindScreenToList("BottomScreen", verticalObjs, verticalTextures);
		if (Application.isEditor)
		{
			DebugDummyVdd();
		}
		else if (wideScreen != null && horizonTextures.Count == 3 && verticalObjs.Count == 3 && r6Horizon3Textures.Count == 3 && r6Horizon3Objs.Count == 3 && r6Horizon2Textures.Count == 2 && r6Horizon2Objs.Count == 2)
		{
			StartCoroutine(SetDisplayNameWhenReady());
		}
	}

	private void DebugDummyVdd()
	{
		if (_layoutMode != 0 && _layoutMode != LayoutMode.VerticalMirror3)
		{
			if (_layoutMode == LayoutMode.HorizonPortraitMirror)
			{
				LeftScreenObj.SetActive(value: true);
				CenterScreenObj.SetActive(value: true);
				RightScreenObj.SetActive(value: true);
				LeftScreen.desktopName = "\\\\.\\DISPLAY1";
				CenterScreen.desktopName = "\\\\.\\DISPLAY2";
				RightScreen.desktopName = "\\\\.\\DISPLAY3";
			}
			else if (_layoutMode == LayoutMode.HorizonPortraitExtend)
			{
				LeftScreenObj.SetActive(value: true);
				CenterScreenObj.SetActive(value: true);
				RightScreenObj.SetActive(value: true);
				LeftScreen.desktopName = "\\\\.\\DISPLAY1";
				CenterScreen.desktopName = "\\\\.\\DISPLAY2";
				RightScreen.desktopName = "\\\\.\\DISPLAY3";
			}
			else
			{
				LeftScreenObj.SetActive(value: true);
				CenterScreenObj.SetActive(value: true);
				RightScreenObj.SetActive(value: true);
				LeftScreen.desktopName = "\\\\.\\DISPLAY1";
				CenterScreen.desktopName = "\\\\.\\DISPLAY2";
				RightScreen.desktopName = "\\\\.\\DISPLAY3";
			}
		}
		else if (_layoutMode != 0)
		{
			TopScreenObj.SetActive(value: true);
			CenterScreenObj.SetActive(value: true);
			BottomScreenObj.SetActive(value: true);
			TopScreen.desktopName = "\\\\.\\DISPLAY1";
			CenterScreen.desktopName = "\\\\.\\DISPLAY2";
			BottomScreen.desktopName = "\\\\.\\DISPLAY3";
		}
		else
		{
			WideScreenObj.SetActive(value: true);
			WideScreen.desktopName = "\\\\.\\DISPLAY1";
		}
	}

	private void FindScreenToList(string name, List<GameObject> objList, List<UwcWindowTexture> windowTextures)
	{
		GameObject gameObject = GameObject.Find(name);
		if (gameObject != null)
		{
			objList.Add(gameObject);
			UwcWindowTexture component = gameObject.GetComponent<UwcWindowTexture>();
			windowTextures.Add(component);
			gameObject.SetActive(value: false);
		}
	}

	private void SetDisplayName()
	{
		DisplayManager2.Instance.LayoutMode = _layoutMode;
		DisplayManager2.Instance.TurnOffBuildInScreen = _turnOffScreen;
		VitureCommonLibrary.DisplayInfo[] allDisplays = DisplayManager2.Instance.AllDisplays;
		VitureCommonLibrary.Logger.Info($"[SetDisplayName] layoutMode={_layoutMode} turnOffScreen={_turnOffScreen} AllDisplays.Count={allDisplays.Length}");
		VitureCommonLibrary.DisplayInfo[] array = allDisplays;
		foreach (VitureCommonLibrary.DisplayInfo displayInfo in array)
		{
			VitureCommonLibrary.Logger.Info($"[SetDisplayName] raw: name='{NormalizeDisplayName(displayInfo.DisplayName)}' connected={displayInfo.IsConnected} active={displayInfo.IsActive} primary={displayInfo.IsGDIPrimary} pos={displayInfo.CurrentSetting.Position} res={displayInfo.CurrentSetting.Resolution}");
		}
		HashSet<string> vitureNames = GetVitureDisplayNames();
		VitureCommonLibrary.Logger.Info("[SetDisplayName] viture(excluded)=[" + string.Join(", ", vitureNames) + "]");
		VitureCommonLibrary.DisplayInfo[] source = (from x in allDisplays
			where x.IsActive
			where !vitureNames.Contains(NormalizeDisplayName(x.DisplayName))
			select x).ToArray();
		source = ((_layoutMode != LayoutMode.VerticalMirror3) ? (from d in source
			orderby d.CurrentSetting.Position.Y + d.CurrentSetting.Resolution.Height / 2, d.CurrentSetting.Position.X + d.CurrentSetting.Resolution.Width / 2
			select d).ToArray() : (from d in source
			orderby d.CurrentSetting.Position.X + d.CurrentSetting.Resolution.Width / 2, d.CurrentSetting.Position.Y + d.CurrentSetting.Resolution.Height / 2
			select d).ToArray());
		array = source;
		foreach (VitureCommonLibrary.DisplayInfo displayInfo2 in array)
		{
			VitureCommonLibrary.Logger.Info($"display:{displayInfo2.DisplayName} primary={displayInfo2.IsGDIPrimary} pos={displayInfo2.CurrentSetting.Position} res={displayInfo2.CurrentSetting.Resolution}");
		}
		switch (_layoutMode)
		{
		case LayoutMode.HorizonExtend1:
			SetDisplayName(source, 1);
			break;
		case LayoutMode.HorizonMirror1:
			SetDisplayName(source, 1);
			break;
		case LayoutMode.HorizonMirror2:
		case LayoutMode.HorizonExtend2:
			SetDisplayName(source, 2);
			break;
		case LayoutMode.HorizonMirror3:
		case LayoutMode.HorizonExtend3:
		case LayoutMode.HorizonPortraitMirror:
		case LayoutMode.HorizonPortraitExtend:
			SetDisplayName(source);
			break;
		case LayoutMode.UltraWide:
			SetWideDisplayName(source);
			break;
		case LayoutMode.VerticalMirror3:
			SetVerticalDisplayName(source);
			break;
		default:
			SetWideDisplayName(source);
			break;
		}
	}

	private static HashSet<string> GetVitureDisplayNames()
	{
		HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		try
		{
			VitureCommonLibrary.DisplayInfo vitureDisplay = DisplayManager2.Instance.VitureDisplay;
			if (vitureDisplay != null && !string.IsNullOrEmpty(vitureDisplay.DisplayName))
			{
				hashSet.Add(NormalizeDisplayName(vitureDisplay.DisplayName));
			}
		}
		catch (Exception ex)
		{
			VitureCommonLibrary.Logger.Error("GetVitureDisplayNames error: " + ex.Message, ex.StackTrace);
		}
		return hashSet;
	}

	private static string NormalizeDisplayName(string name)
	{
		return (name ?? string.Empty).Replace("\0", string.Empty).Trim();
	}

	private IEnumerator SetDisplayNameWhenReady()
	{
		int required = GetRequiredDisplayCount();
		float waited = 0f;
		int num;
		while (true)
		{
			HashSet<string> vitureNames = GetVitureDisplayNames();
			num = DisplayManager2.Instance.AllDisplays.Count((VitureCommonLibrary.DisplayInfo x) => x.IsActive && !vitureNames.Contains(NormalizeDisplayName(x.DisplayName)));
			if (num >= required)
			{
				VitureCommonLibrary.Logger.Info($"[SetDisplayName] ready: available={num} required={required} waited={waited:0.0}s");
				SetDisplayName();
				AutoLayoutDisplays();
				yield break;
			}
			if (waited >= 15f)
			{
				break;
			}
			yield return new WaitForSeconds(0.5f);
			waited += 0.5f;
		}
		VitureCommonLibrary.Logger.Error($"[SetDisplayName] timeout: available={num} required={required} waited={waited:0.0}s; skip layout (no crash)");
	}

	private int GetRequiredDisplayCount()
	{
		switch (_layoutMode)
		{
		case LayoutMode.HorizonMirror1:
		case LayoutMode.HorizonExtend1:
			return 1;
		case LayoutMode.HorizonMirror2:
		case LayoutMode.HorizonExtend2:
			return 2;
		case LayoutMode.HorizonMirror3:
		case LayoutMode.HorizonExtend3:
		case LayoutMode.VerticalMirror3:
		case LayoutMode.HorizonPortraitMirror:
		case LayoutMode.HorizonPortraitExtend:
			return 3;
		default:
			return 1;
		}
	}

	private void SetWideDisplayName(VitureCommonLibrary.DisplayInfo[] displays)
	{
		if (displays.Length < 1)
		{
			VitureCommonLibrary.Logger.Error($"SetWideDisplayName Error, displays.Length: {displays.Length} (skip, no crash)");
			return;
		}
		for (int i = 0; i < horizonObjs.Count; i++)
		{
			horizonObjs[i].SetActive(value: false);
		}
		string displayName = displays.FirstOrDefault().DisplayName;
		if (CheckWideDisplayIs32_9())
		{
			wideScreenObj.SetActive(value: true);
			wideScreen.desktopName = displayName;
		}
		else
		{
			wideScreen_24_9_Obj.SetActive(value: true);
			wideScreen_24_9.desktopName = displayName;
		}
		VitureCommonLibrary.Logger.Info("SetWideDisplayName: " + displayName);
	}

	private bool CheckWideDisplayIs32_9()
	{
		Size resolution = DisplayManager2.Instance.PrimaryDisplay.CurrentSetting.Resolution;
		return resolution.Width * 9 == resolution.Height * 32;
	}

	private void SetVerticalDisplayName(VitureCommonLibrary.DisplayInfo[] displays)
	{
		if (displays.Length < 3)
		{
			VitureCommonLibrary.Logger.Error($"SetVerticalDisplayName Error, displays.Length: {displays.Length} (skip, no crash)");
			return;
		}
		CenterScreen.scaleControlType = WindowTextureScaleControlType.FixedWidth;
		for (int i = 0; i < verticalObjs.Count; i++)
		{
			verticalObjs[i].SetActive(value: true);
			verticalTextures[i].desktopName = displays[i].DisplayName;
			VitureCommonLibrary.Logger.Info("SetVerticalDisplayName: " + displays[i].DisplayName);
		}
	}

	private void SetDisplayName(VitureCommonLibrary.DisplayInfo[] displays, int enableCount = 3)
	{
		if (displays.Length < enableCount)
		{
			VitureCommonLibrary.Logger.Error($"SetDisplayName Error, displays.Length: {displays.Length} enableCount: {enableCount} (skip, no crash)");
			return;
		}
		switch (enableCount)
		{
		case 1:
			LeftScreenObj.SetActive(value: false);
			CenterScreenObj.SetActive(value: true);
			RightScreenObj.SetActive(value: false);
			CenterScreen.desktopName = displays[0].DisplayName;
			VitureCommonLibrary.Logger.Info("setExtendDisplayName: Desktop Center " + displays[0].DisplayName);
			break;
		case 2:
			if (_glassesModel.Contains("Native3Dof"))
			{
				R6Left2ScreenObj.SetActive(value: true);
				R6Right2ScreenObj.SetActive(value: true);
				R6Left2Screen.desktopName = displays[0].DisplayName;
				VitureCommonLibrary.Logger.Info("R6Left2Screen:  " + displays[0].DisplayName);
				R6Right2Screen.desktopName = displays[1].DisplayName;
				VitureCommonLibrary.Logger.Info("R6Right2Screen:  " + displays[1].DisplayName);
			}
			else
			{
				LeftScreenObj.SetActive(value: false);
				CenterScreenObj.SetActive(value: true);
				RightScreenObj.SetActive(value: true);
				CenterScreen.desktopName = displays[0].DisplayName;
				VitureCommonLibrary.Logger.Info("Center Screen: " + displays[0].DisplayName);
				RightScreen.desktopName = displays[1].DisplayName;
				VitureCommonLibrary.Logger.Info("Right Screen: " + displays[1].DisplayName);
			}
			break;
		case 3:
		{
			List<GameObject> list = horizonObjs;
			List<UwcWindowTexture> list2 = horizonTextures;
			if (_glassesModel.Contains("Native3Dof"))
			{
				list = r6Horizon3Objs;
				list2 = r6Horizon3Textures;
			}
			for (int i = 0; i < horizonTextures.Count; i++)
			{
				list[i].SetActive(value: true);
				list2[i].desktopName = displays[i].DisplayName;
				list2[i].scaleControlType = WindowTextureScaleControlType.FixedHeight;
				VitureCommonLibrary.Logger.Info($"SetDisplayName: Desktop{i + 1} {displays[i].DisplayName}");
			}
			break;
		}
		}
	}

	private double Deg2Rad(double deg)
	{
		return deg * Math.PI / 180.0;
	}

	private double Rad2Deg(double deg)
	{
		return deg * 180.0 / Math.PI;
	}

	public void AutoLayoutDisplays()
	{
		if (_layoutMode != 0 && _layoutMode != LayoutMode.VerticalMirror3)
		{
			SetDisplayScale(CenterScreenObj);
			SetDisplayPos(CenterScreenObj);
			SetDisplayScale(LeftScreenObj);
			SetDisplayPos(LeftScreenObj);
			SetDisplayScale(RightScreenObj);
			SetDisplayPos(RightScreenObj);
		}
		else if (_layoutMode != 0)
		{
			SetDisplayScale(CenterScreenObj, vertical: true);
			SetDisplayPos(CenterScreenObj);
			SetDisplayScale(TopScreenObj, vertical: true);
			SetDisplayPos(TopScreenObj);
			SetDisplayScale(BottomScreenObj, vertical: true);
			SetDisplayPos(BottomScreenObj);
		}
		else if (_layoutMode == LayoutMode.UltraWide)
		{
			bool flag = CheckWideDisplayIs32_9();
			SetDisplayScale(flag ? WideScreenObj : WideScreen_24_9_Obj);
		}
	}

	private void ApplySameTransform(GameObject source, GameObject target)
	{
		target.transform.position = source.transform.position;
		target.transform.rotation = mainCamera.transform.rotation;
		VitureCommonLibrary.Logger.Info("ApplySameTransform from " + source.name + " to " + target.name);
	}

	private void SetDisplayScale(GameObject displayObj, bool vertical = false)
	{
		if (_layoutMode == LayoutMode.HorizonPortraitExtend || _layoutMode == LayoutMode.HorizonPortraitMirror)
		{
			if (displayObj == CenterScreenObj)
			{
				FitHeight(displayObj);
			}
			else
			{
				FitPortrait(displayObj);
			}
		}
		else if (vertical)
		{
			FitWidth(displayObj);
		}
		else
		{
			FitHeight(displayObj);
		}
	}

	private void FitHeight(GameObject displayObj)
	{
		double num = _mainCamera.ScreenDistance;
		double num2 = mainCamera.fieldOfView;
		UwcWindowTexture displayTexture = displayObj.GetComponent<UwcWindowTexture>();
		VitureCommonLibrary.DisplayInfo displayInfo = DisplayManager2.Instance.AllDisplays.Where((VitureCommonLibrary.DisplayInfo x) => x.DisplayName == displayTexture.desktopName).FirstOrDefault();
		if (displayInfo != null)
		{
			Size resolution = displayInfo.CurrentSetting.Resolution;
			double num3 = (float)resolution.Width * 1f / (float)resolution.Height;
			double num4 = 2.0 * num * Math.Tan(Deg2Rad(0.5 * num2));
			double num5 = num3 * num4;
			displayObj.transform.localScale = new Vector3((float)num5, (float)num4, 1f);
		}
	}

	private void FitWidth(GameObject displayObj)
	{
		double num = _mainCamera.ScreenDistance;
		double num2 = mainCamera.fieldOfView;
		UwcWindowTexture displayTexture = displayObj.GetComponent<UwcWindowTexture>();
		VitureCommonLibrary.DisplayInfo displayInfo = DisplayManager2.Instance.AllDisplays.Where((VitureCommonLibrary.DisplayInfo x) => x.DisplayName == displayTexture.desktopName).FirstOrDefault();
		if (displayInfo != null)
		{
			Size resolution = displayInfo.CurrentSetting.Resolution;
			double num3 = (float)resolution.Width * 1f / (float)resolution.Height;
			double num4 = (_glassesModel.Contains("P6") ? 1.6 : 1.7777777777777777);
			double num5 = num2 * num4;
			double num6 = 2.0 * num * Math.Tan(Deg2Rad(0.5 * num5));
			double num7 = num6 / num3;
			displayObj.transform.localScale = new Vector3((float)num6, (float)num7, 1f);
		}
	}

	private void FitPortrait(GameObject displayObj)
	{
		double num = _mainCamera.ScreenDistance;
		double num2 = mainCamera.fieldOfView;
		UwcWindowTexture displayTexture = displayObj.GetComponent<UwcWindowTexture>();
		VitureCommonLibrary.DisplayInfo displayInfo = DisplayManager2.Instance.AllDisplays.Where((VitureCommonLibrary.DisplayInfo x) => x.DisplayName == displayTexture.desktopName).FirstOrDefault();
		if (displayInfo != null)
		{
			Size resolution = displayInfo.CurrentSetting.Resolution;
			double num3 = (float)resolution.Width * 1f / (float)resolution.Height;
			double num4 = 2.0 * num * Math.Tan(Deg2Rad(0.5 * num2));
			double num5 = num4 / num3;
			displayObj.transform.localScale = new Vector3((float)num4, (float)num5, 1f);
		}
	}

	private void SetDisplayPos(GameObject displayObj)
	{
		float num = CenterScreenObj.transform.localScale.x * 0.5f;
		float num2 = CenterScreenObj.transform.localScale.y * 0.5f;
		if (displayObj == CenterScreenObj)
		{
			displayObj.transform.localPosition = new Vector3(0f, 0f, 1f);
		}
		float screenDistance = _mainCamera.ScreenDistance;
		if (displayObj == LeftScreenObj || displayObj == RightScreenObj)
		{
			float num3 = num;
			float num4 = screenDistance;
			double val = ((double)(num3 * num4) + (double)screenDistance * Math.Sqrt(num3 * num3 + num4 * num4 - screenDistance * screenDistance)) / (double)(num3 * num3 - screenDistance * screenDistance);
			double val2 = ((double)(num3 * num4) - (double)screenDistance * Math.Sqrt(num3 * num3 + num4 * num4 - screenDistance * screenDistance)) / (double)(num3 * num3 - screenDistance * screenDistance);
			double num5 = Math.Min(val, val2);
			float num6 = displayObj.transform.localScale.x * 0.5f;
			double num7 = (double)num3 + (double)num6 / Math.Sqrt(1.0 + num5 * num5);
			double num8 = (double)num4 + (double)num6 * num5 / Math.Sqrt(1.0 + num5 * num5);
			if (displayObj == LeftScreenObj)
			{
				displayObj.transform.localPosition = new Vector3((float)(0.0 - num7), 0f, (float)num8);
				displayObj.transform.localRotation = Quaternion.Euler(0f, (float)Rad2Deg(Math.Atan(num5)), 0f);
			}
			if (displayObj == RightScreenObj)
			{
				displayObj.transform.localPosition = new Vector3((float)num7, 0f, (float)num8);
				displayObj.transform.localRotation = Quaternion.Euler(0f, (float)(0.0 - Rad2Deg(Math.Atan(num5))), 0f);
			}
		}
		if (displayObj == TopScreenObj || displayObj == BottomScreenObj)
		{
			float num9 = num2;
			float num10 = screenDistance;
			double val3 = ((double)(num9 * num10) + (double)screenDistance * Math.Sqrt(num9 * num9 + num10 * num10 - screenDistance * screenDistance)) / (double)(num9 * num9 - screenDistance * screenDistance);
			double val4 = ((double)(num9 * num10) - (double)screenDistance * Math.Sqrt(num9 * num9 + num10 * num10 - screenDistance * screenDistance)) / (double)(num9 * num9 - screenDistance * screenDistance);
			double num11 = Math.Min(val3, val4);
			float num12 = displayObj.transform.localScale.y * 0.5f;
			double num13 = (double)num9 + (double)num12 / Math.Sqrt(1.0 + num11 * num11);
			double num14 = (double)num10 + (double)num12 * num11 / Math.Sqrt(1.0 + num11 * num11);
			if (displayObj == TopScreenObj)
			{
				displayObj.transform.localPosition = new Vector3(0f, (float)num13, (float)num14);
				displayObj.transform.localRotation = Quaternion.Euler((float)Rad2Deg(Math.Atan(num11)), 0f, 0f);
			}
			if (displayObj == BottomScreenObj)
			{
				displayObj.transform.localPosition = new Vector3(0f, (float)(0.0 - num13), (float)num14);
				displayObj.transform.localRotation = Quaternion.Euler((float)(0.0 - Rad2Deg(Math.Atan(num11))), 0f, 0f);
			}
		}
	}
}
