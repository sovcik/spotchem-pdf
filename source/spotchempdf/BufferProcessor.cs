using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spotchempdf
{
    public interface IBufferProcessor
    {
        void processBuffer(byte[] buffer, int length);
    }
}
