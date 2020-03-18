using EXILED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFE
{
    public class EFEs
    {
        public Dictionary<RoleType, int> drops = new Dictionary<RoleType, int>();

        public void AddToList(string item, int amount)
        {
            try
            {
                drops.Add((RoleType)Enum.Parse(typeof(RoleType), item), amount);
            }
            catch
            {
                //Plugin.Error("Failed adding item, " + item + " with the amount of " + amount + ". Does it exist?");
            }
        }

    }
}
