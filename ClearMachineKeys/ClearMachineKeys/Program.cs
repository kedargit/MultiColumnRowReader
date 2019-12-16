using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ClearMachineKeys
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string userNameToDeleteBy = ConfigurationSettings.AppSettings["userNameToDeleteBy"];
                int fileOlderThan = int.Parse(ConfigurationSettings.AppSettings["fileOlderThan"]);
                Console.WriteLine("Deleting all files older than " + (object)fileOlderThan + " and owned by " + userNameToDeleteBy + ". Do you want to proceed?");
                Console.ReadLine();
                Parallel.ForEach<FileInfo>(new DirectoryInfo("C:\\ProgramData\\Microsoft\\Crypto\\RSA\\MachineKeys").EnumerateFiles(), (Action<FileInfo>)(file =>
                {
                    try
                    {
                        string fullName = file.FullName;
                        DateTime creationTime = file.CreationTime;
                        string str = File.GetAccessControl(fullName).GetOwner(typeof(NTAccount)).ToString();
                        Console.WriteLine("User : " + str + " createdDate : " + (object)creationTime);
                        if (!(creationTime < DateTime.Now.AddDays((double)fileOlderThan)) || !str.Contains(userNameToDeleteBy))
                            return;
                        File.Delete(fullName);
                        Console.WriteLine("Deleted : " + fullName + " User : " + str + " createdDate : " + (object)creationTime);
                    }
                    catch (Exception ex)
                    {
                        Exception exception = ex;
                        Console.WriteLine(exception.Message);
                        while (exception.InnerException != null)
                        {
                            exception = exception.InnerException;
                            Console.WriteLine(exception.Message);
                        }
                    }
                }));
                Console.WriteLine("---- Complete ----");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                Console.WriteLine(exception.Message);
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                    Console.WriteLine(exception.Message);
                }
                Console.ReadLine();
            }
        }
    }
}
