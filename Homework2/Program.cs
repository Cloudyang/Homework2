using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Interface;
using Faction;

namespace Homework2
{
    class Program
    {
        static Program()
        {
            //程序异常统一处理
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler((o, e) =>
                {
                    LogHelper.WriteErrorLog($"程序发生错误:{e.ExceptionObject.ToString()}");
                    Console.ReadLine();
                });
        }

        static void Main(string[] args)
        {
            #region  展示各派口技
            var east = new EastFaction();
            Show(east, 1000);
            Console.WriteLine("=============================");
            var south = new SouthFaction();
            Show(south, 1200);
            Console.WriteLine("=============================");
            var north = new NorthFaction();
            Show(north, 1500);
            Console.WriteLine("=============================");
            var west = new WestFaction();
            Show(west, 800);
            Console.WriteLine("=============================");
            #endregion

            #region 用于注册模拟"火起"后的场景
            RegisterEvent(east);
            RegisterEvent(south);
            RegisterEvent(north);
            RegisterEvent(west);
            #endregion

            #region 模拟温度上升
            for (int i = 0; i <= 1000; i++)
            {
                Console.WriteLine($"现场温度：{i}");
                east.Ignition(i);
                south.Ignition(i);
                north.Ignition(i);
                west.Ignition(i);
            }
            #endregion

            #region 练习用XML/JSON文件配置出来的
            //JsonHelper.WriteJsonFile(EnvironmentArgument.JsonPath, east);
            //east = JsonHelper.ReadJsonFile<EastFaction>(EnvironmentArgument.JsonPath);
            //Show(east, 2000);

            //XmlHelper.ToXml(EnvironmentArgument.XmlPath, east);

            //var eastXml = XmlHelper.FromXml<EastFaction>(EnvironmentArgument.XmlPath);
            //Show(eastXml, 500);

            //XmlHelper.ToXml(EnvironmentArgument.XmlPath, south);
            var Factions = new List<Ventriloquism>();
            Factions.Add(east);
            Factions.Add(west);
            Factions.Add(south);
            Factions.Add(north);
            JsonHelper.WriteJsonFile(EnvironmentArgument.JsonPath, Factions);

            //无法实现List<Ventriloquism> 主要原因是对象信息丢失
            var jsonFactions = JsonHelper.ReadJsonFile<List<WestFaction>>(EnvironmentArgument.JsonPath);
            jsonFactions.ForEach(faction =>
            {
                Show(faction, 800);
            });


            var southXml = XmlHelper.FromXml<SouthFaction>(EnvironmentArgument.XmlPath);
            Show(southXml, 500);
            #endregion

            Console.ReadLine();
        }

        static void RegisterEvent<T>(T v) where T : Ventriloquism
        {
            if ((v as NorthFaction) != null)
            {
                v.Fire += (f) =>
                {
                    LogHelper.WriteInfoLog($"{f.People}夫起大呼", 1000);
                };
            }
            if ((v as NorthFaction) != null)
            {
                v.Fire += (f) =>
                {
                    SpeechPlay.SpeakContent("妇亦起大呼");
                    LogHelper.WriteInfoLog("妇亦起大呼", 1000);
                };
            }
            v.Fire += (f) =>
            {
                LogHelper.WriteInfoLog("两儿齐哭", 1000);
                SpeechPlay.SpeakContent("两儿齐哭");
            };
            v.Fire += (f) =>
            {
                LogHelper.WriteInfoLog("俄而百千人大呼", 1000);
                SpeechPlay.SpeakContent("俄而百千人大呼");
            };
            v.Fire += (f) =>
            {
                LogHelper.WriteInfoLog("百千儿哭", 1000);
                SpeechPlay.SpeakContent("百千儿哭");
            };
            v.Fire += (f) =>
            {
                LogHelper.WriteInfoLog("百千犬吠");
            };
        }

        //提供一个泛型方法(用接口和基类约束)
        static void Show<T>(T obj, double money) where T : Ventriloquism, ICharge
        {
            Type type = typeof(T);
            foreach (var prop in type.GetProperties())
            {
                LogHelper.WriteInfoLog($"属性名:{prop.Name},属性值:{prop.GetValue(obj)}");
            }
            obj.Start();
            obj.Open();
            obj.DogSound();
            obj.PeopleSound();
            obj.WindSound();
            obj.Close();
            obj.Charge(money);
        }




    }
}
