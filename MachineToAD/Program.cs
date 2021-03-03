﻿using System;
using System.DirectoryServices.AccountManagement;

namespace MachineToAD
{
    class Program
    {
        static void Main()
        {
            try
            {
                using (PrincipalContext pcRoot = new PrincipalContext(ContextType.Machine, "192.168.1.84", null, ContextOptions.Negotiate, @"domainNameHere\Administrator", "Pass99"))
                {
                    //Get an access denied error here trying to connect to the Context
                    var group = GroupPrincipal.FindByIdentity(pcRoot, "Administrators");
                    var pc = new PrincipalContext(ContextType.Domain, "FQDNOFDomainHere");
                    var user = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, "vikas");

                    if (group == null)
                    {
                        Console.WriteLine("Group not found.");
                        return;
                    }

                    if (user == null)
                        Console.WriteLine("User not found.");
                    else
                        group.Members.Add(user);

                    group.Save();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            } 

            // Wait for output
            Console.ReadKey();
        }
    }
}
