using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using Common;

namespace Faction
{
    public class WestFaction : Ventriloquism, ICharge
    {
        public WestFaction()
        {
            name = "西派";
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public override void Ignition(int temp)
        {
            if (temp == temperature)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                LogHelper.WriteInfoLog($"{this.Name}摸拟着火现场温度{temperature}：", 300);
                base.OnFire();
            }
        }

        public void Charge(double money)
        {
            LogHelper.WriteInfoLog($"{this.Name}{base.People}收取表演费为：{money}元");
        }

        public override void DogSound()
        {
            LogHelper.WriteInfoLog($"{this.Name}{base.People}模仿狗叫声",300);
        }

        public override void PeopleSound()
        {
            LogHelper.WriteInfoLog($"{this.Name}{base.People}模仿人声",200);
        }

        public override void WindSound()
        {
            LogHelper.WriteInfoLog($"{this.Name}{base.People}模仿风声",400);
        }

        public override void Open()
        {
            LogHelper.WriteInfoLog($"{this.Name}开始表演:");
            base.Open();
        }

        public override void Close()
        {
            LogHelper.WriteInfoLog($"{this.Name}结束表演:");
            base.Close();
        }
    }
}
