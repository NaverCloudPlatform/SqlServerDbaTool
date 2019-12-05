using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaTool.Model
{

    class TBL_CLUSTER_KEY : IEquatable<TBL_CLUSTER_KEY>
    {
        public string clusterName { get; set; }

        public bool Equals(TBL_CLUSTER_KEY other)
        {
            if (clusterName.Equals(other.clusterName))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return clusterName.GetHashCode();
        }
    }
}
