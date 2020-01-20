using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace digitalsign.application.Sensor.Core
{
    public class RegistrationManager
    {
        private readonly HashSet<RegistrationResult> _registrations;

        public RegistrationManager()
        {
            _registrations = new HashSet<RegistrationResult>();
        }

        public RegistrationResult Validate(string token)
        {
            try
            {
                var result = _registrations.Single(x => string.Equals(token, x.Token, StringComparison.OrdinalIgnoreCase));
                if (!_registrations.TryGetValue(result, out var registrationResult)) return RegistrationResultFactory.Invalid(token);
                registrationResult.State = ValidationState.Validated;
                _registrations.Remove(result);
                return _registrations.Add(registrationResult) ? registrationResult : RegistrationResultFactory.Invalid(token);
            }
            catch (Exception)
            {
                return RegistrationResultFactory.Invalid(token);
            }
        }
        public RegistrationResult? Register()
        {
            return Register(Guid.NewGuid().ToString());
        }
        public RegistrationResult? Register(string token)
        {
            var result = RegistrationResultFactory.New(token, ValidationState.Registered);
            if (_registrations.Add(result))
            {
                return result;
            }
            return null;
        }
    }

    public struct RegistrationResult : IEquatable<RegistrationResult>
    {
        public string Token { get; set; }
        public static DateTime CreationDate => DateTime.Now;

        public ValidationState State { get; set; }


        public static bool operator ==(RegistrationResult left, RegistrationResult right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RegistrationResult left, RegistrationResult right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return obj is RegistrationResult result &&
                   Token == result.Token &&
                   State == result.State;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Token, State);
        }

        public bool Equals([AllowNull] RegistrationResult other)
        {
            return Token == other.Token &&
                   State == other.State;
        }
    }

    internal class RegistrationResultFactory
    {
        public static RegistrationResult Invalid(string token) => new RegistrationResult
            {State = ValidationState.Invalid, Token = token};
        public static RegistrationResult New(string token, ValidationState state) => new RegistrationResult
            { State = state, Token = token };

    }

    public enum ValidationState {
        Registered,
        Validated,
        Invalid
    }
}
