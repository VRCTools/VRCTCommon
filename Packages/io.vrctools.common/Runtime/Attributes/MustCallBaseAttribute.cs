using System;

namespace VRCTools.Common.Attributes
{
  /// <summary>
  /// Indicates that the annotated method should always be called when overridden in a subclass.
  /// </summary>
  public sealed class MustCallBaseAttribute : Attribute
  {
  }
}
