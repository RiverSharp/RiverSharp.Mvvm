using RiverSharp.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiverSharp.Mvvm
{
    /// <summary>
    /// MVVM模式中的ViewModel类的基类。
    /// </summary>
    public abstract class ViewModelBase : ObservableObject, ICleanup
    {
        /// <summary>
        /// 
        /// </summary>
        //private IMessenger _messengerInstance;
        public void Cleanup()
        {
            //MessengerInstance.Unregister(this);
        }
    }
}
