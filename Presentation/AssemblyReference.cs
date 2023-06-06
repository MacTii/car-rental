using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class AssemblyReference
    {
        public string? ReferenceName { get; set; }
        public string? ReferenceVersion { get; set; }
        public bool IsLoaded { get; private set; }
        public DateTime LoadTime { get; private set; }

        public AssemblyReference() => IsLoaded = false;

        public AssemblyReference(string referenceName, string referenceVersion)
        {
            ReferenceName = referenceName ?? throw new ArgumentNullException(nameof(referenceName));
            ReferenceVersion = referenceVersion ?? throw new ArgumentNullException(nameof(referenceVersion));
            IsLoaded = false;
        }

        public void LoadAssembly()
        {
            if (!IsLoaded)
            {
                Console.WriteLine($"Loading assembly: {ReferenceName} ({ReferenceVersion})");

                IsLoaded = true;
                LoadTime = DateTime.Now;
                Console.WriteLine("Assembly loaded successfully.");
            }
            else
            {
                Console.WriteLine("Assembly is already loaded.");
            }
        }

        public void UseAssembly()
        {
            if (IsLoaded)
            {
                Console.WriteLine($"Using assembly: {ReferenceName} ({ReferenceVersion})");
            }
            else
            {
                Console.WriteLine("Assembly is not loaded yet. Please load it first.");
            }
        }
    }
}
