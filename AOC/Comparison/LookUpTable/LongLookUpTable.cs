using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Comparison.LookUpTable
{
    

    public class LongLookUpTable
    {
        internal LongLookUpTable(List<MappedPairsRange> coupleRanges)
        {
            couples = coupleRanges;
        }

        List<MappedPairsRange> couples;

        internal long GetTarget(long sourceStart)
        {
            foreach (var c in couples)
            {
                if (c.TryGetTarget(sourceStart, out long target))
                {
                    return target;
                }
            }

            return sourceStart;
        }

        internal long GetSource(long targetStart)
        {
            foreach (var c in couples)
            {
                if (c.TryGetSource(targetStart, out long source))
                {
                    return source;
                }
            }

            return targetStart;
        }
    }

    internal class LongLookUpTableMultiple
    {
        private List<LongLookUpTable> _tables;

        internal LongLookUpTableMultiple(List<LongLookUpTable> tables)
        {
            _tables = tables;
        }


        public long GetTarget(long source)
        {
            return GetOutValue(0, _tables.Count() - 1, source);
        }

        public long GetSource(long target)
        {
            return GetOutValue(_tables.Count() - 1, 0, target);
        }

        public long GetOutValue(int tableIndexIn, int tableIndexOut, long inValue)
        {
            bool getTarget = tableIndexIn < tableIndexOut;

            var ret = inValue;

            if (getTarget)
            {
                for (int i = tableIndexIn; i <= tableIndexOut; i++)
                {
                    ret = _tables[i].GetTarget(ret);
                }
            }
            else
            {
                for (int i = tableIndexIn; i >= tableIndexOut; i--)
                {
                    ret = _tables[i].GetSource(ret);
                }
            }

            return ret;
        }
    }
}
