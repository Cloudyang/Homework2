using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace Common
{
    /// <summary>
    /// 日志文件操作类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// log 配置文件的路径
        /// </summary>
        private static string logPath = EnvironmentArgument.LogPath;
        /// <summary>
        /// 加一个锁
        /// </summary>
        private static readonly object objLock = new object();

        static LogHelper()
        {
            //如果日志保存路径为空，没办法只好报错。
            //不建议程序直接设定保存路径或者保存在程序根目录。（出于Web程序考虑，因为不能确定Web程序目录一定有写入的权限）
            if (string.IsNullOrWhiteSpace(logPath))
                throw new ArgumentNullException("日志保存文件夹配置错误，请添加[LogPath]节点并指定日志保存路径");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
        }

        /// <summary>
        /// 输出异常到控制台并写入异常日志
        /// </summary>
        /// <param name="ex">异常信息</param>
        public static void WriteErrorLog(Exception ex)
        {
            //挂起，让信息间隔输出
            Thread.Sleep(700);
            //输出到控制台
            Console.WriteLine($"异常：{ex.Message}");
            //写入日志文件
            _WriteLog(LogEnum.ERROR, ex.ToString());
        }

        public static void WriteErrorLog(string msg)
        {
            //挂起，让信息间隔输出
            Thread.Sleep(700);
            //输出到控制台
            Console.WriteLine($"异常：{msg}");
            //写入日志文件
            _WriteLog(LogEnum.ERROR, msg);
        }

        /// <summary>
        /// 输出信息到控制台并写入提示日志
        /// </summary>
        /// <param name="ex">异常信息</param>
        public static void WriteInfoLog(string message,int timeSecond=1000)
        {
            //输出到控制台
            Console.Write("提示：");
            var msgs = message.ToCharArray();
            var delay = timeSecond / (msgs.Length == 0 ? 1 : msgs.Length);
            foreach (var msg in msgs)
            {
                Console.Write(msg);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
            //写入日志文件
            _WriteLog(LogEnum.INFO, message);
        }

        #region 内部方法
        /// <summary>
        /// 将日志信息写入文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="message">日志内容</param>
        private static void _WriteLog(LogEnum e, string message)
        {
            string fileName = string.Empty;
            switch (e)
            {
                case LogEnum.INFO:
                    fileName = $"Info_{DateTime.Now.ToString("yyyyMMdd")}.log";
                        //string.Format("Info_{0}.log", DateTime.Now.ToString("yyyyMMdd"));
                    break;
                default:
                    fileName = $"Error_{DateTime.Now.ToString("yyyyMMdd")}.log";
                        //string.Format("Error_{0}.log", DateTime.Now.ToString("yyyyMMdd"));
                    break;
            }
            lock (objLock)
            {
                using (FileStream fs = File.Open(Path.Combine(logPath, fileName), FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(string.Format("{0} --- 线程：{1} --- {2} ", DateTime.Now.ToString().PadRight(20), Thread.CurrentThread.ManagedThreadId.ToString().PadRight(2), message));
                        sw.Flush();
                    }
                }
            }
        }

        /// <summary>
        /// 日志类型
        /// </summary>
        private enum LogEnum
        {
            /// <summary>
            /// 异常日志
            /// </summary>
            ERROR,
            /// <summary>
            /// 信息日志
            /// </summary>
            INFO
        }
        #endregion

    }
}
