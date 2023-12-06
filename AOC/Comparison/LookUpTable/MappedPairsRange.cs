using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Comparison.LookUpTable
{
    internal class MappedPairsRange
    {
        internal long sourceMin;
        internal long targetMin;

        internal long sourceMax;
        internal long targetMax;

        internal static MappedPairsRange GetRange(long sourceStart, long targetStart, long count)
        {
            return new MappedPairsRange(sourceStart, targetStart, sourceStart + count - 1, targetStart + count - 1);
        }

        internal MappedPairsRange(long sourceMin, long targetMin, long sourceMax, long targetMax)
        {
            this.sourceMin = sourceMin;
            this.targetMin = targetMin;
            this.sourceMax = sourceMax;
            this.targetMax = targetMax;
        }

        internal bool TryGetTarget(long source, out long target)
        {
            target = source + targetMin - sourceMin;

            return source >= sourceMin && source <= sourceMax;
        }

        internal bool TryGetSource(long target, out long source)
        {
            source = target + sourceMin - targetMin;

            return source >= sourceMin && source <= sourceMax;
        }
    }
}
