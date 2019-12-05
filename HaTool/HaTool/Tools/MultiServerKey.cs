using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaTool.Tools
{
    public class MultiServerKey : IEquatable<MultiServerKey>
    {
        public string GroupName { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }

        public bool Equals(MultiServerKey other)
        {
            if (GroupName.Equals(other.GroupName)
                && Ip.Equals(other.Ip)
                && Port.Equals(other.Port)
                && Database.Equals(other.Database)
                )
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return GroupName.GetHashCode()
                ^ Ip.GetHashCode()
                ^ Port.GetHashCode()
                ^ Database.GetHashCode()
                ;
        }
    }
}
