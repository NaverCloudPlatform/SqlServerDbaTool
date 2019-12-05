using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaTool.Model
{

    class TBL_CLUSTER_SERVER_KEY : IEquatable<TBL_CLUSTER_SERVER_KEY>
    {
        public string clusterName { get; set; }
        public string serverName { get; set; }

        public bool Equals(TBL_CLUSTER_SERVER_KEY other)
        {
            if (clusterName.Equals(other.clusterName) && serverName.Equals(other.serverName))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return clusterName.GetHashCode() ^ serverName.GetHashCode();
        }
    }
}
