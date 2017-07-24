using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Interface
{
    /// <summary>
    /// 抽象父类:口技表演
    /// </summary>
    [Serializable]
    public abstract class Ventriloquism
    {
        #region 五个属性：一人、一桌、一椅、一扇、一抚尺
        /// <summary>
        /// 人
        /// </summary>
        public string People { get; set; } = "一人";
        /// <summary>
        /// 桌
        /// </summary>
        public string Desk { get; set; } = "一桌";
        /// <summary>
        /// 椅
        /// </summary>
        public string Chair { get; set; } = "一椅";
        /// <summary>
        /// 扇
        /// </summary>
        public string Fan { get; set; } = "一扇";
        /// <summary>
        /// 抚尺
        /// </summary>
        public string Ruler { get; set; } = "一抚尺";
        #endregion

        /// <summary>
        /// 有一个普通方法打印表演开始了有一个普通方法打印表演开始了
        /// </summary>
        public void Start()
        {
            LogHelper.WriteInfoLog("表演开始");
        }

        #region 有三个抽象方法：模仿狗叫声、模仿人声、模仿风声；
        public abstract void DogSound();

        public abstract void PeopleSound();

        public abstract void WindSound();
        #endregion

        #region 有两个虚方法，分别用于开场白和结束语，并提供默认实现；
        public virtual void Open()
        {
            LogHelper.WriteInfoLog("京中有善口技者。会宾客大宴，于厅事之东北角，施八尺屏障，口技人坐屏障中，一桌、一椅、一扇、一抚尺而已。众宾团坐。少顷，但闻屏障中抚尺一下,满坐寂然，无敢哗者。");
        }

        public virtual  void Close()
        {
            LogHelper.WriteInfoLog("结束：忽然抚尺一下，群响毕绝。撤屏视之，一人、一桌、一椅、一扇、一抚尺而已。 ");
        }

        #endregion

        #region 2 口技表演抽象类定义一个事件，用于模拟"火起"后的场景
        protected int temperature=400;

        public event Action<Ventriloquism> Fire;

        protected  void OnFire()
        {
            if (Fire != null)
            {
                Fire.Invoke(this);
            }
        }

        /// <summary>
        /// 模拟着火
        /// </summary>
        /// <param name="temp"></param>
        public abstract void Ignition(int temp);
        #endregion


    }
}
