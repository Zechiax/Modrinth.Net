﻿using System.Reflection;

namespace Modrinth;

internal static class PackageVersion
{
    public static string GetVersion()
    {
        var assembly = Assembly.GetAssembly(typeof(PackageVersion));

        if (assembly == null)
            return "";
        
        var assemblyName = AssemblyName.GetAssemblyName(assembly.Location);
        
        if (assemblyName.Version == null)
            return "";
        
        return assemblyName.Version.ToString();
    }
}