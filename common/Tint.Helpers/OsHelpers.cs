namespace Tint.Helpers;

public static class OsHelpers
{
    public static char PathDelimiter()
    {
        return Environment.OSVersion.Platform == PlatformID.Win32NT ? '\\' : '/';
    }
}