

namespace RamuneLib
{
    internal static class InputUtils
    {
        internal struct BindingState
        {
            public int lastUpdatedFrame;
            public string bindingPath;
            public bool previousPressed;
            public bool currentPressed;
        }


        internal static readonly Dictionary<(GameInput.Device device, GameInput.Button button, GameInput.BindingSet bindingSet), BindingState> _bindingStates = [];


        internal static bool GetButtonHeld(GameInput.Button button, GameInput.BindingSet bindingSet)
        {
            return GetButtonHeld(button, GameInput.PrimaryDevice, bindingSet);
        }


        internal static bool GetButtonHeld(GameInput.Button button, GameInput.Device device, GameInput.BindingSet bindingSet)
        {
            return GetBindingState(device, button, bindingSet).currentPressed;
        }


        internal static bool GetButtonDown(GameInput.Button button, GameInput.BindingSet bindingSet)
        {
            return GetButtonDown(button, GameInput.PrimaryDevice, bindingSet);
        }


        internal static bool GetButtonDown(GameInput.Button button, GameInput.Device device, GameInput.BindingSet bindingSet)
        {
            var state = GetBindingState(device, button, bindingSet);
            return state.currentPressed && !state.previousPressed;
        }

        internal static bool GetButtonUp(GameInput.Button button, GameInput.BindingSet bindingSet)
        {
            return GetButtonUp(button, GameInput.PrimaryDevice, bindingSet);
        }


        internal static bool GetButtonUp(GameInput.Button button, GameInput.Device device, GameInput.BindingSet bindingSet)
        {
            var state = GetBindingState(device, button, bindingSet);
            return !state.currentPressed && state.previousPressed;
        }


        internal static BindingState GetBindingState(GameInput.Device device, GameInput.Button button, GameInput.BindingSet bindingSet)
        {
            if(!GameInput.IsInitialized)
                return default;

            bindingSet = GetEffectiveBindingSet(device, button, bindingSet);

            var bindingPath = GameInput.GetBinding(device, button, bindingSet);

            if(string.IsNullOrWhiteSpace(bindingPath))
                return default;

            var bindingKey = (device, button, bindingSet);

            var frame = Time.frameCount;

            var isPressed = false;

            if(!_bindingStates.TryGetValue(bindingKey, out var state) || state.bindingPath != bindingPath)
            {
                isPressed = IsBindingPressed(bindingPath);

                state = new BindingState
                {
                    lastUpdatedFrame = frame,
                    bindingPath = bindingPath,
                    previousPressed = isPressed,
                    currentPressed = isPressed
                };

                _bindingStates[bindingKey] = state;
                return state;
            }

            if(state.lastUpdatedFrame == frame)
                return state;

            isPressed = IsBindingPressed(bindingPath);

            var updatedLastFrame = state.lastUpdatedFrame == frame - 1;

            state.previousPressed = updatedLastFrame ? state.currentPressed : isPressed;
            state.currentPressed = isPressed;

            state.lastUpdatedFrame = frame;
            _bindingStates[bindingKey] = state;

            return state;
        }


        internal static GameInput.BindingSet GetEffectiveBindingSet(GameInput.Device device, GameInput.Button button, GameInput.BindingSet bindingSet)
        {
            if(bindingSet != GameInput.BindingSet.Secondary)
                return bindingSet;

            var secondaryBindingPath = GameInput.GetBinding(device, button, bindingSet);

            return string.IsNullOrWhiteSpace(secondaryBindingPath) ? GameInput.BindingSet.Primary : GameInput.BindingSet.Secondary;
        }


        internal static bool IsBindingPressed(string bindingPath)
        {
            using var controls = UnityEngine.InputSystem.InputSystem.FindControls(bindingPath);
            var pressPoint = UnityEngine.InputSystem.InputSystem.settings.defaultButtonPressPoint;

            foreach(var control in controls)
            {
                if(control is UnityEngine.InputSystem.Controls.ButtonControl buttonControl)
                {
                    if(buttonControl.isPressed)
                        return true;

                    continue;
                }

                if(control.EvaluateMagnitude() >= pressPoint)
                    return true;
            }

            return false;
        }
    }
}