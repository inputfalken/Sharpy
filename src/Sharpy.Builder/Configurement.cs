using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder {
    /// <summary>
    ///     <para>
    ///         Pass an instance of this class to a <see cref="Builder" /> constructor if you want to change the default
    ///         behavior of the
    ///         <see cref="Builder" />.
    ///     </para>
    /// </summary>
    public class Configurement : IServiceCollection {
        private readonly IServiceCollection _services;
        private IMovieDbProvider _movieDbProvider;
        private IArgumentProvider _argumentProvider;
        private ISecurityNumberProvider _securityNumberProvider;
        private IPhoneNumberProvider _phoneNumberProvider;
        private IUserNameProvider _userNameProvider;
        private IPostalCodeProvider _postalCodeProvider;
        private IDateProvider _dateProvider;
        private IEmailProvider _mailProvider;
        private IElementProvider _listElementPicker;
        private IBoolProvider _boolProvider;
        private INameProvider _nameProvider;
        private IDoubleProvider _doubleProvider;
        private IIntegerProvider _integerProvider;
        private ILongProvider _longProvider;
        private Random _random;

        internal static IServiceCollection ServiceFactory(int? seed = null) => new ServiceCollection()
            .AddSingleton(_ => seed != null ? new Random(seed.Value) : new Random())
            .AddSingleton<IDoubleProvider, DoubleRandomizer>(x => new DoubleRandomizer(x.GetService<Random>()))
            .AddSingleton<IBoolProvider, BoolRandomizer>(x => new BoolRandomizer(x.GetService<Random>()))
            .AddSingleton<IEmailProvider, UniqueEmailBuilder>(x => new UniqueEmailBuilder(
                new[] {"gmail.com", "live.com", "outlook.com", "hotmail.com", "yahoo.com"},
                x.GetService<Random>()
            ))
            .AddSingleton<ILongProvider, LongRandomizer>(x => new LongRandomizer(x.GetService<Random>()))
            .AddSingleton<IIntegerProvider, IntegerRandomizer>(x => new IntegerRandomizer(x.GetService<Random>()))
            .AddSingleton<IUserNameProvider, UserNameRandomizer>(x =>
                new UserNameRandomizer(Data.GetUserNames, x.GetService<Random>())
            )
            .AddSingleton<ISecurityNumberProvider, UniqueFormattedSecurityBuilder>(x =>
                new UniqueFormattedSecurityBuilder(x.GetService<Random>())
            )
            .AddSingleton<IPhoneNumberProvider, UniquePhoneNumberRandomizer>(x =>
                new UniquePhoneNumberRandomizer(x.GetService<Random>())
            )
            .AddSingleton<IArgumentProvider, ArgumentRandomizer>(x => new ArgumentRandomizer(x.GetService<Random>()))
            .AddSingleton<INameProvider, NameByOrigin>(x => new NameByOrigin(x.GetService<Random>()))
            .AddSingleton<IPostalCodeProvider, SwePostalCodeRandomizer>(x =>
                new SwePostalCodeRandomizer(x.GetService<Random>()))
            .AddSingleton<IDateProvider, DateRandomizer>(x => new DateRandomizer(x.GetService<Random>()))
            .AddSingleton<IElementProvider, ListRandomizer>(x => new ListRandomizer(x.GetService<Random>()))
            .AddSingleton<IMovieDbProvider, MovieDbRandomizer>(x =>
                new MovieDbRandomizer(string.Empty, x.GetService<Random>()));

        public Configurement(IServiceCollection services) => _services = services ?? throw new ArgumentNullException(nameof(services));

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="Configurement" /> with default implementations.
        ///     </para>
        ///     <para>
        ///         The seed supplied will be used to instantiate a <see cref="Random" /> for the default implementations.
        ///     </para>
        /// </summary>
        /// <param name="seed"></param>
        public Configurement(int seed) : this(ServiceFactory(seed)) { }

        public Configurement() : this(ServiceFactory()) { }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IMovieDbProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="MovieDbRandomizer" />.
        ///     </para>
        ///     <remarks>
        ///         You must provide a valid API key in order for the methods to work.
        ///     </remarks>
        /// </summary>
        public IMovieDbProvider MovieDbProvider {
            get => _movieDbProvider;
            set {
                _services.AddSingleton(_ => value);
                _movieDbProvider = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IArgumentProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="ArgumentRandomizer" />.
        ///     </para>
        /// </summary>
        public IArgumentProvider ArgumentProvider {
            get => _argumentProvider;
            set {
                _services.AddSingleton(_ => value);
                _argumentProvider = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="ISecurityNumberProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="UniqueSecurityNumberBuilder" />.
        ///     </para>
        /// </summary>
        public ISecurityNumberProvider SecurityNumberProvider {
            get => _securityNumberProvider;
            set {
                _securityNumberProvider = value;
                _services.AddSingleton(_ => value);
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IPhoneNumberProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="UniquePhoneNumberRandomizer" />.
        ///     </para>
        /// </summary>
        public IPhoneNumberProvider PhoneNumberProvider {
            get => _phoneNumberProvider;
            set {
                _phoneNumberProvider = value;
                _services.AddSingleton(_ => value);
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IUserNameProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="UserNameRandomizer" />.
        ///     </para>
        /// </summary>
        public IUserNameProvider UserNameProvider {
            get => _userNameProvider;
            set {
                _userNameProvider = value;
                _services.AddSingleton(_ => value);
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IPostalCodeProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="SwePostalCodeRandomizer" />.
        ///     </para>
        /// </summary>
        public IPostalCodeProvider PostalCodeProvider {
            get => _postalCodeProvider;
            set {
                _postalCodeProvider = value;
                _services.AddSingleton(_ => value);
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IDateProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="DateRandomizer" />.
        ///     </para>
        /// </summary>
        public IDateProvider DateProvider {
            get => _dateProvider;
            set {
                _dateProvider = value;
                _services.AddSingleton(_ => value);
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IEmailProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="UniqueEmailBuilder" />.
        ///     </para>
        /// </summary>
        public IEmailProvider MailProvider {
            get => _mailProvider;
            set {
                _mailProvider = value;
                _services.AddSingleton(_ => value);
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IElementProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="ListRandomizer" />.
        ///     </para>
        /// </summary>
        public IElementProvider ListElementPicker {
            get => _listElementPicker;
            set {
                _listElementPicker = value;
                _services.AddSingleton(_ => value);
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IBoolProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="BoolRandomizer" />.
        ///     </para>
        /// </summary>
        public IBoolProvider BoolProvider {
            get => _boolProvider;
            set {
                _boolProvider = value;
                _services.AddSingleton(_ => value);
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="INameProvider" />.
        ///     </para>
        ///     <para>
        ///         By Default it is <see cref="NameByOrigin" />.
        ///     </para>
        /// </summary>
        public INameProvider NameProvider {
            get => _nameProvider;
            set {
                _nameProvider = value;
                _services.AddSingleton(_ => value);
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IDoubleProvider" />
        ///     </para>
        ///     <para>
        ///         By Default it is <see cref="DoubleRandomizer" />.
        ///     </para>
        /// </summary>
        public IDoubleProvider DoubleProvider {
            get => _doubleProvider;
            set {
                _doubleProvider = value;
                _services.AddSingleton(_ => value);
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="IIntegerProvider" />.
        ///     </para>
        ///     <para>
        ///         By Default it is <see cref="IntegerRandomizer" />.
        ///     </para>
        /// </summary>
        public IIntegerProvider IntegerProvider {
            get => _integerProvider;
            set {
                _integerProvider = value;
                _services.AddSingleton(_ => value);
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and sets the implementation for <see cref="ILongProvider" />.
        ///     </para>
        ///     <para>
        ///         By default it is <see cref="LongRandomizer" />.
        ///     </para>
        /// </summary>
        public ILongProvider LongProvider {
            get => _longProvider;
            set {
                _longProvider = value;
                _services.AddSingleton(_ => value);
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets and sets the <see cref="System.Random" /> for the default implementations of <see cref="Builder" />.
        ///     </para>
        /// </summary>
        public Random Random {
            get => _random;
            set {
                _random = value;
                _services.AddSingleton(_ => value);
            }
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator() => _services.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _services).GetEnumerator();

        public void Add(ServiceDescriptor item) {
            _services.Add(item);
        }

        public void Clear() {
            _services.Clear();
        }

        public bool Contains(ServiceDescriptor item) => _services.Contains(item);

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex) {
            _services.CopyTo(array, arrayIndex);
        }

        public bool Remove(ServiceDescriptor item) => _services.Remove(item);

        public int Count => _services.Count;

        public bool IsReadOnly => _services.IsReadOnly;

        public int IndexOf(ServiceDescriptor item) => _services.IndexOf(item);

        public void Insert(int index, ServiceDescriptor item) {
            _services.Insert(index, item);
        }

        public void RemoveAt(int index) {
            _services.RemoveAt(index);
        }

        public ServiceDescriptor this[int index] {
            get => _services[index];
            set => _services[index] = value;
        }
    }
}