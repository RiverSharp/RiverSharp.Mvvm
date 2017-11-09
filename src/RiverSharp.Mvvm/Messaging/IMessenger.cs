using System;

namespace RiverSharp.Mvvm.Messaging
{
    /// <summary>
    /// Messenger是一个允许对象交换消息的类。
    /// </summary>
    public interface IMessenger
    {
        /// <summary>
        /// 为 <typeparamref name="TMessage"/> 类型的消息注册接收方。当相应的消息被发送时，<paramref name="action"/> 将被执行。
        /// </summary>
        /// <remarks>
        /// 注册接收方不会创建对其的硬引用，因此如果删除此接收方，不会导致内存泄漏。请注意，由于使用WeakActions，暂时不支持闭包。
        /// </remarks>
        /// <typeparam name="TMessage">接收方注册的消息类型。</typeparam>
        /// <param name="recipient">将接收消息的接收方。</param>
        /// <param name="action">在发送 <typeparamref name="TMessage"/> 类型的消息时将执行的操作。</param>
        /// <param name="keepTargetAlive">如果为true，则 <paramref name="action"/> 的目标将被保留为一个硬引用，这可能会导致内存泄漏。如果操作使用闭包，则只应将此参数设置为true。</param>
        void Register<TMessage>(object recipient, Action<TMessage> action, bool keepTargetAlive = false);

        /// <summary>
        /// 为 <typeparamref name="TMessage"/> 类型的消息注册接收方。当相应的消息被发送时，<paramref name="action"/> 参数将被执行。
        /// </summary>
        /// <remarks>
        /// 注册接收方不会创建对其的硬引用，因此如果删除此接收方，不会导致内存泄漏。请注意，由于使用WeakActions，暂时不支持闭包。
        /// <para>如果接收方使用 <paramref name="token"/> 进行注册，并且发送方使用相同的 <paramref name="token"/> 发送消息，则该消息将被传递给接收方。
        /// 其他接收方在注册时没有使用 <paramref name="token"/> （或使用不同 <paramref name="token"/> 的人）将不会收到该消息。
        /// 同样，没有任何 <paramref name="token"/> 或使用不同 <paramref name="token"/> 发送的消息将不会传递给该接收方。</para>
        /// </remarks>
        /// <typeparam name="TMessage">接收方注册的消息类型。</typeparam>
        /// <param name="recipient">将接收消息的接收方。</param>
        /// <param name="token">消息传送通道的令牌。</param>
        /// <param name="action">在发送 <typeparamref name="TMessage"/> 类型的消息时将执行的操作。</param>
        /// <param name="keepTargetAlive">如果为true，则 <paramref name="action"/> 的目标将被保留为一个硬引用，这可能会导致内存泄漏。如果操作使用闭包，则只应将此参数设置为true。</param>
        void Register<TMessage>(object recipient, object token, Action<TMessage> action, bool keepTargetAlive = false);

        /// <summary>
        /// 为 <typeparamref name="TMessage"/> 类型的消息注册接收方。当相应的消息被发送时，<paramref name="action"/> 将被执行。
        /// </summary>
        /// <remarks>
        /// 注册接收方不会创建对其的硬引用，因此如果删除此接收方，不会导致内存泄漏。 请注意，由于使用WeakActions，暂时不支持闭包。
        /// <para>如果将 <paramref name="receiveDerivedMessagesToo"/> 设置为true。
        /// SendOrderMessage和一个ExecuteOrderMessage派生自OrderMessage，注册OrderMessage，则将SendOrderMessage和ExecuteOrderMessage发送给已注册的接收方。
        /// 另外，如果 <typeparamref name="TMessage"/> 是一个接口，那么实现TMessage的消息类型也将被传送给接收者。
        /// 如果SendOrderMessage和ExecuteOrderMessage实现IOrderMessage，注册IOrderMessage，则会将SendOrderMessage和ExecuteOrderMessage发送给已注册的接收方。</para>
        /// </remarks>
        /// <typeparam name="TMessage">接收方注册的消息类型。</typeparam>
        /// <param name="recipient">将接收消息的接收方。</param>
        /// <param name="receiveDerivedMessagesToo">如果为true，则从<typeparamref name="TMessage"/>派生的消息类型也将被发送给接收者。</param>
        /// <param name="action">在发送 <typeparamref name="TMessage"/> 类型的消息时将执行的操作</param>
        /// <param name="keepTargetAlive">如果为true，则 <paramref name="action"/> 的目标将被保留为一个硬引用，这可能会导致内存泄漏。如果操作使用闭包，则只应将此参数设置为true。</param>
        void Register<TMessage>(object recipient, bool receiveDerivedMessagesToo, Action<TMessage> action, bool keepTargetAlive = false);

        /// <summary>
        /// 为 <typeparamref name="TMessage"/> 类型的消息注册接收方。当相应的消息被发送时，<paramref name="action"/> 将被执行。
        /// </summary>
        /// <remarks>
        /// 注册接收方不会创建对其的硬引用，因此如果删除此接收方，不会导致内存泄漏。 请注意，由于使用WeakActions，暂时不支持闭包。
        /// <para>如果接收方使用 <paramref name="token"/> 进行注册，并且发送方使用相同的 <paramref name="token"/> 发送消息，则该消息将被传递给接收方。
        /// 其他接收方在注册时没有使用 <paramref name="token"/> （或使用不同 <paramref name="token"/> 的人）将不会收到该消息。
        /// 同样，没有任何 <paramref name="token"/> 或使用不同 <paramref name="token"/> 发送的消息将不会传递给该接收方。</para>
        /// <para>如果将 <paramref name="receiveDerivedMessagesToo"/> 设置为true。
        /// SendOrderMessage和一个ExecuteOrderMessage派生自OrderMessage，注册OrderMessage，则将SendOrderMessage和ExecuteOrderMessage发送给已注册的接收方。
        /// 另外，如果 <typeparamref name="TMessage"/> 是一个接口，那么实现TMessage的消息类型也将被传送给接收者。
        /// 如果SendOrderMessage和ExecuteOrderMessage实现IOrderMessage，注册IOrderMessage，则会将SendOrderMessage和ExecuteOrderMessage发送给已注册的接收方。</para>
        /// </remarks>
        /// <typeparam name="TMessage">接收方注册的消息类型。</typeparam>
        /// <param name="recipient">将接收消息的接收方。</param>
        /// <param name="token">消息传送通道的令牌。</param>
        /// <param name="receiveDerivedMessagesToo">如果为true，则从<typeparamref name="TMessage"/>派生的消息类型也将被发送给接收者。</param>
        /// <param name="action">在发送 <typeparamref name="TMessage"/> 类型的消息时将执行的操作</param>
        /// <param name="keepTargetAlive">如果为true，则 <paramref name="action"/> 的目标将被保留为一个硬引用，这可能会导致内存泄漏。如果操作使用闭包，则只应将此参数设置为true。</param>
        void Register<TMessage>(object recipient, object token, bool receiveDerivedMessagesToo, Action<TMessage> action, bool keepTargetAlive = false);

        /// <summary>
        /// 发送消息给注册的接收方。
        /// </summary>
        /// <remarks>
        /// 该消息将发送给通过 Register 方法注册此消息类型的所有接收方。
        /// </remarks>
        /// <typeparam name="TMessage">将要发送的消息的类型。</typeparam>
        /// <param name="message">要发送给注册接收方的消息。</param>
        void Send<TMessage>(TMessage message);

        /// <summary>
        /// 发送消息给注册的接收方。
        /// </summary>
        /// <remarks>
        /// 该消息将发送给通过Register方法注册此消息类型，且接收方类型为 <typeparamref name="TTarget"/> 的所有接收方。
        /// </remarks>
        /// <typeparam name="TMessage">将要发送的消息的类型。</typeparam>
        /// <typeparam name="TTarget">将收到消息的接收方的类型。该消息不会被发送到其他类型的接收方。</typeparam>
        /// <param name="message">要发送给注册接收方的消息。</param>
        void Send<TMessage, TTarget>(TMessage message);

        /// <summary>
        /// 发送消息给注册的接收方。
        /// </summary>
        /// <remarks>
        /// 该消息将发送给通过Register方法注册此消息类型，且使用相同 <paramref name="token"/> 的所有接收方。
        /// <para>如果接收方使用 <paramref name="token"/> 进行注册，并且发送方使用相同的 <paramref name="token"/> 发送消息，则该消息将被传递给接收方。
        /// 其他接收方在注册时没有使用 <paramref name="token"/> （或使用不同 <paramref name="token"/> 的人）将不会收到该消息。
        /// 同样，没有任何 <paramref name="token"/> 或使用不同 <paramref name="token"/> 发送的消息将不会传递给该接收方。</para>
        /// </remarks>
        /// <typeparam name="TMessage">将要发送的消息的类型。</typeparam>
        /// <param name="message">要发送给注册接收方的消息。</param>
        /// <param name="token">消息传送通道的令牌。</param>
        void Send<TMessage>(TMessage message, object token);

        /// <summary>
        /// 完全注销消息接收方。
        /// </summary>
        /// <remarks>
        /// 执行此方法后，接收方将不再收到任何消息。
        /// </remarks>
        /// <param name="recipient">需要注销的接收方</param>
        void Unregister(object recipient);

        /// <summary>
        /// 注销指定类型的消息的消息接收方。 
        /// </summary>
        /// <remarks>
        /// 执行此方法后，收件人将不会收到类型为 <typeparamref name="TMessage"/> 的消息了，但仍然会收到其他消息类型（如果他们以前注册过）。
        /// </remarks>
        /// <typeparam name="TMessage">接收方要注销的消息的类型。</typeparam>
        /// <param name="recipient">需要注销的接收方</param>
        void Unregister<TMessage>(object recipient);

        /// <summary>
        /// 注销指定类型和指定 token 的消息接收方。
        /// </summary>
        /// <remarks>
        /// 执行此方法后，收件人将不会收到类型为 <typeparamref name="TMessage"/> 和指定 <paramref name="token"/> 的消息了，但仍然会收到其他消息类型（如果他们以前注册过）。
        /// </remarks>
        /// <param name="recipient">需要注销的接收方</param>
        /// <param name="token">需要注销的接收方的 token </param>
        /// <typeparam name="TMessage">接收方要注销的消息的类型。</typeparam>
        void Unregister<TMessage>(object recipient, object token);

        /// <summary>
        /// Unregisters a message recipient for a given type of messages and for
        /// a given action. Other message types will still be transmitted to the
        /// recipient (if it registered for them previously). Other actions that have
        /// been registered for the message type TMessage and for the given recipient (if
        /// available) will also remain available.
        /// </summary>
        /// <typeparam name="TMessage">接收方要注销的消息的类型。</typeparam>
        /// <param name="recipient">需要注销的接收方</param>
        /// <param name="action">需要注销的</param>
        void Unregister<TMessage>(object recipient, Action<TMessage> action);

        /// <summary>
        /// Unregisters a message recipient for a given type of messages, for
        /// a given action and a given token. Other message types will still be transmitted to the
        /// recipient (if it registered for them previously). Other actions that have
        /// been registered for the message type TMessage, for the given recipient and other tokens (if
        /// available) will also remain available.
        /// </summary>
        /// <typeparam name="TMessage">接收方要注销的消息的类型。</typeparam>
        /// <param name="recipient">需要注销的接收方</param>
        /// <param name="token">需要注销的接收方的 token </param>
        /// <param name="action">需要注销的接收方的TMessage消息类型的action</param>
        void Unregister<TMessage>(object recipient, object token, Action<TMessage> action);
    }
}