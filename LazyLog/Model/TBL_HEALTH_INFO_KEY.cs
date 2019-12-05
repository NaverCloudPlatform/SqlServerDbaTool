using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazylog.Model
{
    class TBL_HEALTH_INFO_KEY : IEquatable<TBL_HEALTH_INFO_KEY>
    {
        public string serverName { get; set; }
        public string time { get; set; }
        public bool Equals(TBL_HEALTH_INFO_KEY other)
        {
            if (serverName.Equals(other.serverName) && time.Equals(other.time))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return serverName.GetHashCode() ^ time.GetHashCode();
        }
    }
}
