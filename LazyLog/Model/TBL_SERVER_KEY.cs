using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazylog.Model
{

    class TBL_SERVER_KEY : IEquatable<TBL_SERVER_KEY>
    {
        public string serverName { get; set; }

        public bool Equals(TBL_SERVER_KEY other)
        {
            if (serverName.Equals(other.serverName))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return serverName.GetHashCode();
        }
    }
}
