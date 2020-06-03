﻿using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace CrossCutting
{
    public class PathControl
    {
        public static void Create(string path)
        {
            Thread.BeginCriticalRegion();

            foreach (var folder in path.Split(Path.DirectorySeparatorChar))
            {
                var aux = path.Split(new string[] { folder }, StringSplitOptions.None)[0];

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(aux);
                }
            }
            
            Thread.EndCriticalRegion();
        }

        public static void GrantAccess(string file)
        {
            if (!Directory.Exists(file))
            {
                Directory.CreateDirectory(file);
            }

            DirectoryInfo dInfo = new DirectoryInfo(file);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            dSecurity.AddAccessRule(
                new FileSystemAccessRule(
                    new SecurityIdentifier(WellKnownSidType.WorldSid, null), 
                    FileSystemRights.FullControl, 
                    InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, 
                    PropagationFlags.NoPropagateInherit, AccessControlType.Allow
                ));
            
            dInfo.SetAccessControl(dSecurity);
        }
    }
}
