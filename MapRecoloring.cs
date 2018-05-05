using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadline
{
    public class MapRecoloring
    {
        public int[] recolor(int H, int[] regions, int[] oldColors)
        {
            // number of regions = max element in regions + 1
            int reg = regions[0];
            for (int i = 1; i < regions.Length; ++i)
                if (regions[i] > reg)
                    reg = regions[i];
            reg++;
            int[] ret = new int[reg];
            for (int i = 0; i < reg; ++i)
                ret[i] = i;
            return ret;
        }
    }
}
