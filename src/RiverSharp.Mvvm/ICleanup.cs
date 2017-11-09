using System;
using System.Collections.Generic;
using System.Text;

namespace RiverSharp.Mvvm
{
    /// <summary>
    /// 为需要清理的类定义一个通用接口，但不包含IDisposable预设的含义。 
    /// 执行ICleanup的实例可以被清理而不被丢弃和垃圾收集。
    /// </summary>
    public interface ICleanup
    {
        /// <summary>
        /// 清理实例，例如保存状态，删除资源等。
        /// </summary>
        void Cleanup();
    }
}
