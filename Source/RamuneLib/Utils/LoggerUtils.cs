

namespace RamuneLib.Utils
{
    public static class LoggerUtils
    {
        public static class Logfile
        {
            /// <summary>
            /// Logs a message to the logfile with the severity of 'Info'.
            /// </summary>
            /// <param name="message">The message to log.</param>
            /// <example>
            /// <code>
            /// LoggerUtils.Logfile.LogInfo("This is an example message to print in the logfile.");
            /// </code>
            /// </example>
            public static void Info(string message) => Variables.logger?.LogInfo(message);


            /// <summary>
            /// Logs a message to the logfile with the severity of 'Error'.
            /// </summary>
            /// <param name="message">The message to log.</param>
            /// <example>
            /// <code>
            /// LoggerUtils.Logfile.Info("This is an example message to print in the logfile.");
            /// </code>
            /// </example>
            public static void Error(string message) => Variables.logger?.LogError(message);


            /// <summary>
            /// Logs a message to the logfile with the severity of 'Debug'.
            /// </summary>
            /// <param name="message">The message to log.</param>
            /// <example>
            /// <code>
            /// LoggerUtils.Logfile.Debug("This is an example message to print in the logfile.");
            /// </code>
            /// </example>
            public static void Debug(string message) => Variables.logger?.LogDebug(message);


            /// <summary>
            /// Logs a message to the logfile with the severity of 'Warning'
            /// </summary>
            /// <param name="message">The message to log</param>
            /// <example>
            /// <code>
            /// LoggerUtils.Logfile.Warning("This is an example message to print in the logfile.");
            /// </code>
            /// </example>
            public static void Warning(string message) => Variables.logger?.LogWarning(message);


            /// <summary>
            /// Logs a message to the logfile with the severity of 'Fatal'.
            /// </summary>
            /// <param name="message">The message to log.</param>
            /// <example>
            /// <code>
            /// LoggerUtils.Logfile.Fatal("This is an example message to print in the logfile.");
            /// </code>
            /// </example>
            public static void Fatal(string message) => Variables.logger?.LogFatal(message);


            /// <summary>
            /// Logs a divider to the logfile
            /// </summary>
            public static void Divider() => Variables.logger?.LogWarning("----------------------------------------------");



            /// <summary>
            /// Logs a message to the logfile with the specified severity.
            /// </summary>
            /// <param name="level"></param>
            /// <param name="message">The message to log.</param>
            /// <example>
            /// <code>
            /// LoggerUtils.Logfile.FromLevel(LogLevel.Info, "This is an example message to print in the logfile.");
            /// </code>
            /// </example>
            public static void WithLevel(LogLevel level, string message)
            {
                switch(level)
                {
                    case LogLevel.Info:
                        Info(message);
                        break;
                    case LogLevel.Error:
                        Error(message);
                        break;
                    case LogLevel.Debug:
                        Debug(message);
                        break;
                    case LogLevel.Warning:
                        Warning(message);
                        break;
                    case LogLevel.Fatal:
                        Fatal(message);
                        break;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static class Screen
        {
            /// <summary>
            /// 
            /// </summary>
            public static List<string> LogLevel = new()
            {
                "<b><color=#54c8f2>[Info]</color></b> ",
                "<b><color=#b30000>[Error]</color></b> ",
                "<b><color=#b1b1b1>[Debug]</color></b> ",
                "<b><color=#ffac00>[Warning]</color></b> ",
            };


            /// <summary>
            /// Logs a message to the screen using ErrorMessage.AddError
            /// </summary>
            /// <param name="message">The message to log</param>
            /// <example>
            /// <code>
            /// LoggerUtils.Screen.LogMessage("This is an example message to display on the screen.");
            /// </code>
            /// </example>
            public static void Message(string message) => ErrorMessage.AddError(message);


            /// <summary>
            /// Logs a message to the screen using ErrorMessage.AddError, prefixed with 'Info:' in a blue color
            /// </summary>
            /// <param name="message">The message to log</param>
            /// <example>
            /// <code>
            /// LoggerUtils.Screen.LogInfo("This is an example message to display on the screen.");
            /// </code>
            /// </example>
            public static void Info(string message) => ErrorMessage.AddError(LogLevel[0] + message);


            /// <summary>
            /// Logs a message to the screen using ErrorMessage.AddError, prefixed with 'Error:' in a red color
            /// </summary>
            /// <param name="message">The message to log</param>
            /// <example>
            /// <code>
            /// LoggerUtils.Screen.LogError("This is an example message to display on the screen.");
            /// </code>
            /// </example>
            public static void Error(string message) => ErrorMessage.AddError(LogLevel[1] + message);


            /// <summary>
            /// Logs a message to the screen using ErrorMessage.AddError, prefixed with 'Debug:' in a grey color
            /// </summary>
            /// <param name="message">The message to log</param>
            /// <example>
            /// <code>
            /// LoggerUtils.Screen.LogDebug("This is an example message to display on the screen.");
            /// </code>
            /// </example>
            public static void Debug(string message) => ErrorMessage.AddError(LogLevel[2] + message);


            /// <summary>
            /// Logs a message to the screen using ErrorMessage.AddError, prefixed with 'Debug:' in a red color
            /// </summary>
            /// <param name="message">The message to log</param>
            /// <example>
            /// <code>
            /// LoggerUtils.Screen.LogWarning("This is an example message to display on the screen.");
            /// </code>
            /// </example>
            public static void Warning(string message) => ErrorMessage.AddError(LogLevel[3] + message);
        }


        /// <summary>
        /// Logs a subtitle message with optional duration and delay.
        /// </summary>
        /// <param name="message">The subtitle message to log.</param>
        /// <param name="duration">The duration to display the subtitle (default is 5 seconds).</param>
        /// <param name="delay">The delay before displaying the subtitle (default is -1 seconds).</param>
        /// <example>
        /// <code>
        /// LoggerUtils.LogSubtitle("This is an example subtitle message.", duration: 10f, delay: 2f);
        /// </code>
        /// </example>
        public static void Subtitle(string langKey, string message, float duration = 5f, float delay = 0f)
        {
            LanguageHandler.SetLanguageLine(langKey, $"{message} <delay={delay}>");
            Language.main.Exists()?.ParseMetaData();

            StringBuilder builder = new StringBuilder().Append(message);
            Subtitles.AddRawLong(1, builder, 0f, duration);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="duration"></param>
        /// <param name="delay"></param>
        public static void Subtitle(string message, float duration = 5f)
        {
            StringBuilder builder = new StringBuilder().Append(message);
            Subtitles.AddRawLong(1, builder, 0f, duration);
        }
    }
}