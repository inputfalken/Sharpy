﻿using Sharpy.IProviders;

namespace Sharpy {
    /// <summary>
    ///     Contains methods for generating common datatypes.
    /// </summary>
    public abstract class GeneratorBase : IDoubleProvider, IIntegerProvider, ILongProvider {
        private readonly IDoubleProvider _doubleProvider;
        private readonly IIntegerProvider _integerProvider;
        private readonly ILongProvider _longProvider;

        /// <summary>
        /// </summary>
        /// <param name="doubleProvider"></param>
        /// <param name="integerProvider"></param>
        /// <param name="longProvider"></param>
        protected GeneratorBase(IDoubleProvider doubleProvider, IIntegerProvider integerProvider,
            ILongProvider longProvider) {
            _doubleProvider = doubleProvider;
            _integerProvider = integerProvider;
            _longProvider = longProvider;
        }

        /// <summary>
        ///     <para>Generates a Double.</para>
        /// </summary>
        /// <returns></returns>
        public double Double() => _doubleProvider.Double();

        /// <summary>
        ///     <para>Generates a double within max value.</para>
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public double Double(double max) => _doubleProvider.Double(max);

        /// <summary>
        ///     <para>Generates a within min and max.</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public double Double(double min, double max) => _doubleProvider.Double(min, max);

        /// <summary>
        ///     <para>Generates a Integer.</para>
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Integer(int max) => _integerProvider.Integer(max);

        /// <summary>
        ///     <para>Generates a integer within min and max.</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Integer(int min, int max) => _integerProvider.Integer(min, max);

        /// <summary>
        ///     <para>Generates a integer.</para>
        /// </summary>
        /// <returns></returns>
        public int Integer() => _integerProvider.Integer();

        /// <summary>
        ///     <para>Generates a long within min and max.</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public long Long(long min, long max) => _longProvider.Long(min, max);

        /// <summary>
        ///     <para>Generates a long within max.</para>
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public long Long(long max) => _longProvider.Long(max);

        /// <summary>
        ///     Generates a long.
        /// </summary>
        /// <returns></returns>
        public long Long() => _longProvider.Long();
    }
}