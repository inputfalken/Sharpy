using Sharpy.Enums;
using Sharpy.IProviders;

namespace Sharpy {
    public abstract class GeneratorBase : IDoubleProvider, IIntegerProvider, IStringProvider, INameProvider, ILongProvider {
        private readonly IDoubleProvider _doubleProvider;
        private readonly IIntegerProvider _integerProvider;
        private readonly IStringProvider _stringProvider;
        private readonly INameProvider _nameProvider;
        private readonly ILongProvider _longProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doubleProvider"></param>
        /// <param name="integerProvider"></param>
        /// <param name="stringProvider"></param>
        /// <param name="nameProvider"></param>
        /// <param name="longProvider"></param>
        public GeneratorBase(IDoubleProvider doubleProvider, IIntegerProvider integerProvider,
            IStringProvider stringProvider, INameProvider nameProvider, ILongProvider longProvider) {
            _doubleProvider = doubleProvider;
            _integerProvider = integerProvider;
            _stringProvider = stringProvider;
            _nameProvider = nameProvider;
            _longProvider = longProvider;
        }

        public double Double() => _doubleProvider.Double();

        public double Double(double max) => _doubleProvider.Double(max);

        public double Double(double min, double max) => _doubleProvider.Double(min, max);

        public int Integer(int max) => _integerProvider.Integer(max);

        public int Integer(int min, int max) => _integerProvider.Integer(min, max);

        public int Integer() => _integerProvider.Integer();

        public string String() => _stringProvider.String();

        public string Name(NameType arg) => _nameProvider.Name(arg);

        public long Long(long min, long max) => _longProvider.Long(min, max);

        public long Long(long max) => _longProvider.Long(max);

        public long Long() => _longProvider.Long();
    }
}