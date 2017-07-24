using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface ICharge
    {
        /// <summary>
        /// 收费
        /// </summary>
        /// <param name="money">金额</param>
        void Charge(double money);
    }
}
