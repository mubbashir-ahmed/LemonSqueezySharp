#if !NET5_0_OR_GREATER
namespace System.Runtime.CompilerServices;

/// <summary>
/// Polyfill enabling C# <c>init</c>-only setters and <c>record</c> types on netstandard2.0,
/// where this type is not part of the BCL. Reserved for compiler use only.
/// </summary>
internal static class IsExternalInit
{
}
#endif
