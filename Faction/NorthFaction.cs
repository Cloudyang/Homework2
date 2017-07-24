using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using Common;

namespace Faction
{
    public class NorthFaction : Ventriloquism, ICharge
    {
        public NorthFaction()
        {
            this.name = "北派";
            base.temperature = 1000;
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public void ignition(int temp)
        {
            if (temp >= temperature)
            {
                base.OnFire();
            }
        }

        public void Charge(double money)
        {
            LogHelper.WriteInfoLog($"{this.Name}{base.People}收取表演费为：{money}元");
        }

        public override void DogSound()
        {
            LogHelper.WriteInfoLog($"{this.Name}{base.People}模仿狗叫声",500);
        }

        public override void PeopleSound()
        {
            LogHelper.WriteInfoLog($"{this.Name}{base.People}模仿人声",300);
        }

        public override void WindSound()
        {
            LogHelper.WriteInfoLog($"{this.Name}{base.People}模仿风声",200);
        }

        public override void Close()
        {
            LogHelper.WriteInfoLog($"{this.Name}结束表演:");
            base.Close();
        }

        public override void Ignition(int temp)
        {
            if (temp == temperature)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                LogHelper.WriteInfoLog($"{this.Name}摸拟着火现场温度{temperature}：", 300);
                base.OnFire();
            }
        }
    }
}
