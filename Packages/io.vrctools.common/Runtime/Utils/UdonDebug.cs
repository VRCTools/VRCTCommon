// Copyright 2025-2026 .start <https://dotstart.tv>
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UdonSharp;
using UnityEngine;

namespace VRCTools.Common.Utils
{
    /// <summary>
    /// Provides logging primitives for Udon components.
    /// </summary>
    public static class UdonDebug
    {
        
        /// <summary>
        /// Logs an informative message within the context of an Udon behaviour.
        /// </summary>
        /// <param name="behaviour">the originating behaviour</param>
        /// <param name="message">a message to display</param>
        public static void Info(UdonSharpBehaviour behaviour, string message)
        {
            Debug.Log(FormatMessage(behaviour, message), behaviour);
        }
        
        /// <summary>
        /// Logs an informative message within the context of an Udon behaviour.
        /// </summary>
        /// <param name="behaviour">the originating behaviour</param>
        /// <param name="message">a message to pass to the formatter</param>
        /// <param name="args">a set of arguments to pass to the formatter</param>
        public static void Info(UdonSharpBehaviour behaviour, string message, params object[] args)
        {
            Debug.LogFormat(behaviour, FormatMessage(behaviour, message, args));
        }

        /// <summary>
        /// Logs a warning message within the context of an Udon behaviour.
        /// </summary>
        /// <param name="behaviour">the originating behaviour</param>
        /// <param name="message">a message to display</param>
        public static void Warn(UdonSharpBehaviour behaviour, string message)
        {
            Debug.LogWarning(FormatMessage(behaviour, message), behaviour);
        }
        
        /// <summary>
        /// Logs a warning message within the context of an Udon behaviour.
        /// </summary>
        /// <param name="behaviour">the originating behaviour</param>
        /// <param name="message">a message to pass to the formatter</param>
        /// <param name="args">a set of arguments to pass to the formatter</param>
        public static void Warn(UdonSharpBehaviour behaviour, string message, params object[] args)
        {
            Debug.LogWarningFormat(behaviour, FormatMessage(behaviour, message, args));
        }

        /// <summary>
        /// Logs an error message within the context of an Udon behaviour.
        /// </summary>
        /// <param name="behaviour">the originating behaviour</param>
        /// <param name="message">a message to display</param>
        public static void Error(UdonSharpBehaviour behaviour, string message)
        {
            Debug.LogError(FormatMessage(behaviour, message), behaviour);
        }

        /// <summary>
        /// Logs an error message within the context of an Udon behaviour.
        /// </summary>
        /// <param name="behaviour">the originating behaviour</param>
        /// <param name="message">a message to pass to the formatter</param>
        /// <param name="args">a set of arguments to pass to the formatter</param>
        public static void Error(UdonSharpBehaviour behaviour, string message, params object[] args)
        {
            Debug.LogErrorFormat(behaviour, FormatMessage(behaviour, message, args));
        }

        /// <summary>
        /// Prefixes the message with Udon behaviour related information to ensure its source is clear.
        /// </summary>
        /// <param name="behaviour">the originating behaviour</param>
        /// <param name="message">a message to decorate</param>
        /// <param name="args">array of parameters to pass into the format</param>
        /// <returns>a decorated message</returns>
        private static string FormatMessage(UdonSharpBehaviour behaviour, string message, params object[] args)
        {
            var objectName = behaviour.gameObject.name;
            var typeName = behaviour.GetUdonTypeName();
            
            var separator = typeName.LastIndexOf('.');
            if (separator != -1)
            {
                // for display within the log, we use a short version of the component rather than writing the fully
                // qualified value every time
                // TODO: Consider only doing this in editor
                typeName = typeName.Substring(separator + 1);
            }
            
            #if UNITY_EDITOR
            // inside of Unity Editor builds we color the type names randomly based on their fully qualified name within
            // the project
            var color = ChooseColor(behaviour);
            typeName = $"<color=#{color}>{typeName}</color>";
            #endif

            return string.Format($"[{typeName}#{objectName}]] {message}", args);
        }

        #if UNITY_EDITOR
        
        /// <summary>
        /// Defines a number of colors for display within the editor.
        /// </summary>
        private const string TypeColorPalette =
            "4FA3F7" + // blue
            "F0954A" + // orange
            "4CC27A" + // green
            "EE6FB8" + // pink
            "A98BF0" + // violet
            "2EC4B6" + // teal
            "7EA6FF" + // periwinkle
            "8BCB4A"; // lime

        /// <summary>
        /// Defines the number of colors present within <see cref="TypeColorPalette"/>.
        ///
        /// Always update this value if you add or remove a color. To keep things fast and efficient, we sadly have to
        /// deal with this little mess for now until we get a better version of Udon#.
        /// </summary>
        private const int TypeColorCount = 8;
        
        /// <summary>
        /// Picks a color from a pre-defined palette to make recognition of messages within the editor easier.
        /// </summary>
        /// <param name="behaviour">the originating behaviour</param>
        /// <returns>a color preset</returns>
        private static string ChooseColor(UdonSharpBehaviour behaviour)
        {
            var index = (Animator.StringToHash(behaviour.GetUdonTypeName()) & 0x7FFFFFFF) % TypeColorCount;
            return TypeColorPalette.Substring(index * 6, 6);
        }
        #endif
    }
}