using Nautilus.Utility;


namespace Ramune.SimpleCoordinates.Monos
{
    public class CoordinateDisplay : MonoBehaviour
    {
        public static BasicText text, targetText;

        public static string targetCoordinatesJson = Path.Combine(Paths.ConfigurationFolder, "TargetCoordinates.json");

        public static bool textHidden, targetTextHidden, stayHidden;

        public static float x, y, z;

        public static Color defaultColor = new(1f, 0.75f, 0.16f);


        public void Start()
        {
            text = new();
            text.ShowMessage("", 1);
            text.SetAlign(TextAlignmentOptions.Center);
            text.SetColor(SimpleCoordinates.config.color);
            text.SetLocation(SimpleCoordinates.config.textX, SimpleCoordinates.config.textY);
            text.SetSize((int)SimpleCoordinates.config.textSize);
            text.SetFontStyle(SimpleCoordinates.config.targetFontStyle);

            targetText = new();
            targetText.ShowMessage("", 1);
            targetText.SetAlign(TextAlignmentOptions.Center);
            targetText.SetColor(SimpleCoordinates.config.targetColor);
            targetText.SetLocation(SimpleCoordinates.config.targetTextX, SimpleCoordinates.config.targetTextY);
            targetText.SetSize((int)SimpleCoordinates.config.targetTextSize);
            targetText.SetFontStyle(SimpleCoordinates.config.targetFontStyle);

            RefreshJson();
        }


        public static void CheckShouldHide(bool configEnabled, ref bool textHidden, BasicText text)
        {
            if(!configEnabled && !textHidden)
            {
                text.Hide();
                textHidden = true;
            }
        }


        public static void ToggleDisplayState(GameInput.Button button, ref bool displayState)
        {
            if(GameInput.GetButtonDown(button))
                displayState = !displayState;

            SimpleCoordinates.config.Save();
        }


        public static void HideAll(bool _stayHidden = false)
        {
            text.Hide();
            targetText.Hide();
            stayHidden = _stayHidden;
        }


        public void Update()
        {
            if(stayHidden)
                return;

            if(!Cursor.visible && GameInput.AnyKeyDown)
            {
                ToggleDisplayState(SimpleCoordinates.ToggleCoordinatesDisplay, ref SimpleCoordinates.config.display);
                ToggleDisplayState(SimpleCoordinates.ToggleTargetCoordinatesDisplay, ref SimpleCoordinates.config.targetDisplay);
            }

            CheckShouldHide(SimpleCoordinates.config.display, ref textHidden, text);
            CheckShouldHide(SimpleCoordinates.config.targetDisplay, ref targetTextHidden, targetText);

            var playerPosition = Player.main.transform.position;

            if(SimpleCoordinates.config.display)
            {
                textHidden = false;
                text.ShowMessage(string.Format("displaytext".LangKeyAbbr(), Mathf.Round(playerPosition.x), Mathf.Round(playerPosition.y), Mathf.Round(playerPosition.z)));
            }

            if(SimpleCoordinates.config.targetDisplay)
            {
                targetTextHidden = false;
                targetText.ShowMessage(string.Format("targetdisplaytext".LangKeyAbbr(), x, y, z));
            }
        }


        public static void AdjustForConfig()
        {
            if(text != null)
            {
                text.SetColor(SimpleCoordinates.config.color);
                text.SetSize((int)SimpleCoordinates.config.textSize);
                text.SetFontStyle(SimpleCoordinates.config.fontStyle);
                text.SetLocation(SimpleCoordinates.config.textX, SimpleCoordinates.config.textY);
            }

            if(targetText != null)
            {
                targetText.SetColor(SimpleCoordinates.config.targetColor);
                targetText.SetSize((int)SimpleCoordinates.config.targetTextSize);
                targetText.SetFontStyle(SimpleCoordinates.config.targetFontStyle);
                targetText.SetLocation(SimpleCoordinates.config.targetTextX, SimpleCoordinates.config.targetTextY);
            }
        }


        public static void RefreshJson()
        {
            var text = File.ReadAllText(targetCoordinatesJson);
            var json = JObject.Parse(text);

            x = (float)json["X"];
            y = (float)json["Y"];
            z = (float)json["Z"];
        }
    }
}