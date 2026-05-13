

namespace Ramune.MouseCursorToggle.Monos
{
    public class MouseCursorController : MonoBehaviour
    {
        public static bool Active => main != null && main.cursorMode;

        public static MouseCursorController main;

        public Utility.BasicText exitHint;

        public CursorLockMode cachedLockState;

        public bool cursorMode, pauseActive, reticleHidden, cachedLockCursor, cachedCursorVisible;


        public void Awake() => main = this;


        public void Start()
        {
            exitHint = new();
            exitHint.ShowMessage("", 1);
            exitHint.SetAlign(TextAlignmentOptions.Center);
            exitHint?.Hide();
        }


        public void OnDestroy()
        {
            DisableCursorMode();

            if(main == this)
                main = null;
        }


        public void Update()
        {
            if(!GameInput.IsInitialized)
                return;

            if(cursorMode && ShouldForceExit())
            { 
                DisableCursorMode(); 
                return; 
            }

            if(cursorMode)
            {
                if(GameInput.GetButtonDown(MouseCursorToggle.EnterCursorMode) || GameInput.GetButtonDown(MouseCursorToggle.ExitCursorMode))
                    DisableCursorMode();
            }
            else if(GameInput.GetButtonDown(MouseCursorToggle.EnterCursorMode) && CanEnterCursorMode())
            {
                cursorMode = true;
                cachedLockCursor = UWE.Utils.lockCursor;
                cachedCursorVisible = Cursor.visible;
                cachedLockState = Cursor.lockState;
                GameInput.ClearInput(1);
            }

            if(cursorMode)
            {
                UWE.Utils.lockCursor = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                if(MouseCursorToggle.config.PauseGame && !pauseActive)
                {
                    FreezeTime.Begin(FreezeTime.Id.IngameMenu);
                    pauseActive = true;
                }
                else if(!MouseCursorToggle.config.PauseGame && pauseActive)
                {
                    FreezeTime.End(FreezeTime.Id.IngameMenu);
                    pauseActive = false;
                }

                if(exitHint == null || !MouseCursorToggle.config.ShowExitHint)
                {
                    exitHint?.Hide();
                }
                else
                {
                    exitHint.SetColor(MouseCursorToggle.config.ExitHintColor);
                    exitHint.SetFontStyle(MouseCursorToggle.config.ExitHintStyle);
                    exitHint.SetSize(MouseCursorToggle.config.ExitHintSize);
                    exitHint.SetLocation(MouseCursorToggle.config.ExitHintX, MouseCursorToggle.config.ExitHintY);
                    exitHint.ShowMessage(string.Format("exittext".LangKeyAbbr(), GameInput.FormatButton(MouseCursorToggle.EnterCursorMode), GameInput.FormatButton(MouseCursorToggle.ExitCursorMode)));
                }
            }
            else exitHint?.Hide();
        }


        public static bool TryHandleEscapeMenu()
        {
            if(!Active)
                return false;

            if(GameInput.GetButtonDown(MouseCursorToggle.ExitCursorMode))
            {
                main.DisableCursorMode();
                return true;
            }

            if(GameInput.GetButtonDown(GameInput.Button.UIMenu))
                main.DisableCursorMode();

            return false;
        }


        public void DisableCursorMode()
        {
            if(!cursorMode)
                return;

            cursorMode = false;

            if(pauseActive)
            {
                FreezeTime.End(FreezeTime.Id.IngameMenu);
                pauseActive = false;
            }

            if(reticleHidden && HandReticle.main != null)
            {
                HandReticle.main.UnrequestCrosshairHide();
                reticleHidden = false;
            }

            UWE.Utils.lockCursor = cachedLockCursor;
            Cursor.visible = cachedCursorVisible;
            Cursor.lockState = cachedLockState;

            exitHint?.Hide();
            uGUI_Tooltip.Clear();
            GameInput.ClearInput(1);
        }


        public static bool CanEnterCursorMode()
        {
            if(!uGUI.isMainLevel || Player.main == null || GameInput.IsRebinding || uGUI.isIntro || LaunchRocket.isLaunching)
                return false;

            if(Player.main.cinematicModeActive)
                return false;

            var pda = Player.main.GetPDA();

            if(pda != null && pda.isInUse)
                return false;

            if(IngameMenu.main != null && IngameMenu.main.selected)
                return false;

            return true;
        }


        public static bool ShouldForceExit()
        {
            if(!uGUI.isMainLevel || Player.main == null || GameInput.IsRebinding || LaunchRocket.isLaunching)
                return true;

            if(Player.main.cinematicModeActive)
                return true;

            var pda = Player.main.GetPDA();

            if(pda != null && pda.isInUse)
                return true;

            if(IngameMenu.main != null && IngameMenu.main.selected)
                return true;

            return false;
        }
    }
}