using Castle.DynamicProxy;
using System;

namespace PriceEvents.Init
{
    /// <summary>
    /// Helper for adding <see cref="IInterceptor"/>'s to classes.
    /// See <see cref="https://www.nimeshjm.com/cross-cutting-concerns-episerver-using-dynamic-proxies/"/>
    /// for original post.
    /// </summary>
    /// <typeparam name="T">Any type of object that we want to add interceptors on</typeparam>
    public class EnrichmentOf<T> where T : IInterceptor, new()
    {
        private static readonly ProxyGenerator DynamicProxy = new ProxyGenerator();

        /// <summary>
        /// ForInterface
        /// </summary>
        /// <typeparam name="TInterface">object</typeparam>
        /// <param name="concrete"></param>
        /// <returns></returns>
        public static object ForInterface<TInterface>(object concrete)
        {
            return ForInterface(typeof(TInterface), concrete);
        }

        /// <summary>
        /// ForInterface
        /// </summary>
        /// <param name="iinterface"></param>
        /// <param name="concrete"></param>
        /// <returns></returns>
        public static object ForInterface(Type iinterface, object concrete)
        {
            return DynamicProxy.CreateInterfaceProxyWithTarget(iinterface, concrete, new T());
        }

        /// <summary>
        /// ForClass
        /// </summary>
        /// <typeparam name="TClass">object</typeparam>
        /// <param name="concrete"></param>
        /// <returns></returns>
        public static object ForClass<TClass>(object concrete)
        {
            return ForClass(typeof(TClass), concrete);
        }

        /// <summary>
        /// ForClass
        /// </summary>
        /// <param name="cclass"></param>
        /// <param name="concrete"></param>
        /// <returns></returns>
        public static object ForClass(Type cclass, object concrete)
        {
            return DynamicProxy.CreateClassProxyWithTarget(cclass, concrete, new T());
        }
    }
}