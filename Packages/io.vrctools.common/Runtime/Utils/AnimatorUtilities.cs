// Copyright 2026 .start <https://dotstart.tv>
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

using UnityEngine;

namespace VRCTools.Common.Utils {
  /// <summary>
  /// Provides functions which simplify the interaction with animators.
  /// </summary>
  public static class AnimatorUtilities {
    /// <summary>
    /// Resolves an array of animator parameters.
    /// </summary>
    /// <param name="parameterNames">an array of parameter names</param>
    /// <returns>an array of parameter ids</returns>
    public static int[] ResolveParameterIds(string[] parameterNames) {
      var parameterIds = new int[parameterNames.Length];

      for (var i = 0; i < parameterNames.Length; i++) {
        var parameterName = parameterNames[i];
        if (string.IsNullOrEmpty(parameterName)) {
          parameterIds[i] = -1;
          continue;
        }

        parameterIds[i] = Animator.StringToHash(parameterName);
      }

      return parameterIds;
    }
  }
}
