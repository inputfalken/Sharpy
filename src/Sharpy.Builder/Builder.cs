using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sharpy.Builder.Enums;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder {
    /// <summary>
    ///     <para>
    ///         Contains various methods for providing data.
    ///     </para>
    /// </summary>
    public class Builder : IDoubleProvider, IIntegerProvider, ILongProvider, INameProvider, IElementProvider,
        IBoolProvider, IDateProvider, IEmailProvider, IPostalCodeProvider, ISecurityNumberProvider,
        IPhoneNumberProvider, IUserNameProvider, IArgumentProvider, IMovieDbProvider {
        private readonly IArgumentProvider _argumentProvider;
        private readonly IBoolProvider _boolProvider;
        private readonly IDateProvider _dateprovider;
        private readonly IDoubleProvider _doubleProvider;
        private readonly IElementProvider _elementProvider;
        private readonly IEmailProvider _emailProvider;
        private readonly IIntegerProvider _integerProvider;
        private readonly ILongProvider _longProvider;
        private readonly INameProvider _nameProvider;
        private readonly IPhoneNumberProvider _phoneNumberProvider;
        private readonly IPostalCodeProvider _postalCodeProvider;
        private readonly ISecurityNumberProvider _securityNumberProvider;
        private readonly IUserNameProvider _userNameProvider;
        private readonly Lazy<IMovieDbProvider> _movieDbProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public Builder(IServiceProvider provider) {
            if (provider is null) throw new ArgumentNullException(nameof(provider));
            _argumentProvider = provider.GetService<IArgumentProvider>() ??
                                throw new ArgumentNullException(nameof(_argumentProvider));
            _boolProvider = provider.GetService<IBoolProvider>() ??
                            throw new ArgumentNullException(nameof(_boolProvider));
            _dateprovider = provider.GetService<IDateProvider>() ??
                            throw new ArgumentNullException(nameof(_dateprovider));
            _doubleProvider = provider.GetService<IDoubleProvider>() ??
                              throw new ArgumentNullException(nameof(_doubleProvider));
            _elementProvider = provider.GetService<IElementProvider>() ??
                               throw new ArgumentNullException(nameof(_elementProvider));
            _emailProvider = provider.GetService<IEmailProvider>() ??
                             throw new ArgumentNullException(nameof(_emailProvider));
            _integerProvider = provider.GetService<IIntegerProvider>() ??
                               throw new ArgumentNullException(nameof(_integerProvider));
            _longProvider = provider.GetService<ILongProvider>() ??
                            throw new ArgumentNullException(nameof(_longProvider));
            _nameProvider = provider.GetService<INameProvider>() ??
                            throw new ArgumentNullException(nameof(_nameProvider));
            _phoneNumberProvider = provider.GetService<IPhoneNumberProvider>() ??
                                   throw new ArgumentNullException(nameof(_phoneNumberProvider));
            _postalCodeProvider = provider.GetService<IPostalCodeProvider>() ??
                                  throw new ArgumentNullException(nameof(_postalCodeProvider));
            _securityNumberProvider =
                provider.GetService<ISecurityNumberProvider>() ??
                throw new ArgumentNullException(nameof(_securityNumberProvider));
            _userNameProvider = provider.GetService<IUserNameProvider>() ??
                                throw new ArgumentNullException(nameof(_userNameProvider));

            _movieDbProvider = new Lazy<IMovieDbProvider>(() => provider.GetService<IMovieDbProvider>() ??
                                                                throw new ArgumentNullException(nameof(_movieDbProvider)
                                                                )
            );
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="Builder" /> with <paramref name="configurement" />.
        ///     </para>
        /// </summary>
        /// <param name="configurement">
        ///     The configuration for the <see cref="Builder" />.
        /// </param>
        public Builder(IServiceCollection configurement) : this(configurement.BuildServiceProvider()) { }

        public Builder() : this(new Configuration()) { }

        public Builder(int seed) : this(new Configuration(seed)) { }

        /// <inheritdoc />
        public T Argument<T>(T first, T second, params T[] additional) =>
            _argumentProvider.Argument(first, second, additional);

        /// <inheritdoc />
        public bool Bool() => _boolProvider.Bool();

        /// <inheritdoc />
        public DateTime DateByAge(int age) => _dateprovider.DateByAge(age);

        /// <inheritdoc />
        public DateTime DateByYear(int year) => _dateprovider.DateByYear(year);

        /// <inheritdoc />
        public DateTime Date() => _dateprovider.Date();

        /// <inheritdoc />
        public double Double() => _doubleProvider.Double();

        /// <inheritdoc />
        public double Double(double max) => _doubleProvider.Double(max);

        /// <inheritdoc />
        public double Double(double min, double max) => _doubleProvider.Double(min, max);

        /// <inheritdoc />
        public T Element<T>(IReadOnlyList<T> list) => _elementProvider.Element(list);

        /// <inheritdoc />
        public string Mail(params string[] names) => _emailProvider.Mail(names);

        ///<inheritdoc />
        public string Mail() => _emailProvider.Mail();

        /// <inheritdoc />
        public int Integer(int max) => _integerProvider.Integer(max);

        /// <inheritdoc />
        public int Integer(int min, int max) => _integerProvider.Integer(min, max);

        /// <inheritdoc />
        public int Integer() => _integerProvider.Integer();

        /// <inheritdoc />
        public long Long(long min, long max) => _longProvider.Long(min, max);

        /// <inheritdoc />
        public long Long(long max) => _longProvider.Long(max);

        /// <inheritdoc />
        public long Long() => _longProvider.Long();

        /// <inheritdoc />
        public string FirstName() => _nameProvider.FirstName();

        /// <inheritdoc />
        public string FirstName(Gender gender) => _nameProvider.FirstName(gender);

        /// <inheritdoc />
        public string LastName() => _nameProvider.LastName();

        ///<inheritdoc />
        public string PhoneNumber(int length) => _phoneNumberProvider.PhoneNumber(length);

        ///<inheritdoc />
        public string PhoneNumber() => _phoneNumberProvider.PhoneNumber();

        /// <inheritdoc />
        public string PostalCode() => _postalCodeProvider.PostalCode();

        /// <inheritdoc />
        public string PostalCode(string county) => _postalCodeProvider.PostalCode(county);

        /// <inheritdoc />
        public string SecurityNumber(DateTime date) =>
            _securityNumberProvider.SecurityNumber(date);

        /// <inheritdoc />
        public string SecurityNumber() => _securityNumberProvider.SecurityNumber();

        /// <inheritdoc />
        public string UserName() => _userNameProvider.UserName();

        public Task<IReadOnlyList<Movie>> RandomMovies() => _movieDbProvider.Value.RandomMovies();
    }
}